using DemoAppBE.Data;
using DemoAppBE.Domain;
using DemoAppBE.Features.Email.Services.Interface;
using DemoAppBE.Features.Files.DTOs;
using DemoAppBE.Features.Files.Services.Interface;
using DemoAppBE.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace DemoAppBE.Features.Files.Services.Implementation
{
    public class FileService(ApplicationDBContext _context, IUserContext _userContext, IConfiguration _configuration, ILogger<FolderService> _logger, IFileStorageService _fileStorageService, IMailLogService _mailLogService) : IFileService
    {
        public async Task<string> getFullPath(int? FolderId)
        {
            try
            {
                var list = await _context.Set<WrapperData>()
                            .FromSqlInterpolated($@"
                                EXEC spFiles 
                                @Flag={"GP"},  
                                @FolderId={FolderId},
                                @UserId={_userContext.Id}")
                            .ToListAsync();

                var result = list.FirstOrDefault();
                return result.Data;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> getOriginalFilePath(int Id)
        {
            try
            {
                var list = await _context.Set<WrapperData>()
                            .FromSqlInterpolated($@"
                                EXEC spFiles 
                                @Flag={"GOP"},  
                                @Id={Id},
                                @UserId={_userContext.Id}")
                            .ToListAsync();

                var result = list.FirstOrDefault();
                return result.Data;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<Result> getSharedDataAsync()
        {
            try
            {
                var list = await _context.Set<FileShareResponseModel>()
                            .FromSqlInterpolated($@"
                                EXEC spFiles 
                                @Flag={"GS"},  
                                @UserId={_userContext.Id}")
                            .ToListAsync();

                return Result.Success(list);
            }
            catch (Exception ex)
            {

                return Result.Failure(Error.Failure("500", ex.Message));
            }
        }

        public async Task<Result> saveFilePath(SaveFileRequestDTO saveFileRequestDTO)
        {
            try
            {
                var list = await _context.Set<Wrapper>()
                            .FromSqlInterpolated($@"
                                EXEC spFiles 
                                @Flag={"I"},  
                                @FolderId={saveFileRequestDTO.FolderId},
                                @FileName={saveFileRequestDTO.FileName},
                                @OriginalName={saveFileRequestDTO.OriginalName},
                                @OriginalPath={saveFileRequestDTO.OriginalPath},
                                @FileSize={saveFileRequestDTO.FileSize},
                                @CreatedDate={saveFileRequestDTO.CreatedDate},
                                @ModifiedDate={saveFileRequestDTO.ModifiedDate},
                                @UserId={_userContext.Id}")
                            .ToListAsync();

                var result = list.FirstOrDefault();
                if (result.Status)
                {
                    return Result.Success(result);
                }
                else
                {
                    return Result.Failure(Error.Failure("", result.Messages));
                }
            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Failure("500", ex.Message));
            }
        }

        public async Task<Result> saveToken(int FileId, string Email)
        {
            try
            {
                var token = Guid.NewGuid().ToString();
                var expiresAt = DateTime.Now.AddMinutes(10);


                var fileshare = new FileShares
                {
                    FileId = FileId,
                    Email = Email,
                    Token = token,
                    ExpiresAt = expiresAt,
                    IsActive=true,
                    IsUsed=false
                };
                var res = await _context.FileShares.AddAsync(fileshare);
                var re = await _context.SaveChangesAsync();
                var frontendBaseUrl = _configuration["FrontendUrl"];
                var url = $"{frontendBaseUrl}/FileShare/access?token={token}";
                await _mailLogService.MailSend(Email, url);


                return Result.Success(new { messages = $"File shared successfully to {Email}", status = true });

            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Failure("500", ex.Message));
            }
        }

        public async Task<Result> updateshareDetails(int Id,bool IsActive)
        {
            try
            {
                var share = await _context.FileShares.FindAsync(Id);

                share.IsActive = IsActive;
                await _context.SaveChangesAsync();
                return Result.Success(new { messages = "Updated Successfully!", status = true });
            }
            catch (Exception ex)
            {

                return Result.Failure(Error.Failure("500", ex.Message));
            }
        }

        public async Task<Result> uploadFile(IFormFile file, string? folder)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return Result.Failure(Error.Failure("400", "File is empty"));

                // Allow only image or pdf
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                    return Result.Failure(Error.Failure("400", "File type is not compatible. Please check the file!"));

                // Generate a GUID file name (with extension)
                var newFileName = $"{Guid.NewGuid()}{extension}";

                var path = await getFullPath(Convert.ToInt32(folder));

                // Full storage path — combine folder + newFileName
                string storageName = string.Empty;
                if (path is not null)
                {
                    storageName = Path.Combine(path, newFileName);
                }
                else
                {
                    storageName = newFileName;
                }

                // Save file to storage
                await using var stream = file.OpenReadStream();
                var saveResult = await _fileStorageService.saveAsync(storageName, stream);

                if (saveResult.IsFailure)
                    return saveResult;

                // Build DTO to store in DB
                SaveFileRequestDTO saveFileRequestDTO = new()
                {
                    FileSize = file.Length,
                    FileName = newFileName,
                    OriginalName = file.FileName,
                    OriginalPath = saveResult.Value,
                    UserId = _userContext.Id,
                    FolderId = folder is null ? null : Convert.ToInt32(folder),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                // Save file metadata in DB
                var response = await saveFilePath(saveFileRequestDTO);
                if (response.IsSuccess)
                    return Result.Success(response);

                return Result.Failure(Error.Failure("500", "File uploaded but failed to save metadata"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while uploading file");
                return Result.Failure(Error.Failure("400", ex.Message));
            }
        }

        public async Task<Result<FileResponseDTO>> validateToken(string token)
        {
            try
            {
                var share = await _context.FileShares.FirstOrDefaultAsync(x => x.Token == token && !x.IsUsed );

                if (share is null || share.ExpiresAt < DateTime.Now || !share.IsActive)
                {
                    return Result.Failure<FileResponseDTO>(Error.NotFound("400", "Invalid link or the link has been expired!"));
                }

                share.IsUsed = true;
                await _context.SaveChangesAsync();
                var file = await _context.Files.FindAsync(share.FileId);
                FileResponseDTO fileResponseDTO = new()
                {
                    FileName = file.FileName,
                    OriginalName=file.OriginalName,
                    OriginalPath=file.OriginalPath,
                    FileSize=file.FileSize,
                    UserId=file.UserId,
                    FolderId=file.FolderId
                };
                return Result.Success<FileResponseDTO>(fileResponseDTO);

            }
            catch (Exception ex)
            {
                return Result.Failure<FileResponseDTO>(Error.Failure("400", ex.Message));

            }
        }
    }
}
