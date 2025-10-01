using DemoAppBE.Data;
using DemoAppBE.Domain;
using DemoAppBE.Features.Files.DTOs;
using DemoAppBE.Features.Files.Services.Interface;
using DemoAppBE.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;

namespace DemoAppBE.Features.Files.Services.Implementation
{
    public class FolderService(ApplicationDBContext _context, IUserContext _userContext, ILogger<FolderService> _logger) : IFolderService
    {
        public async Task<Result> addFolderAsync(FolderDTO folder)
        {
            try
            {
                _logger.LogInformation("AddFolderAsync called with Name={FolderName}, ParentId={ParentFolderId}, UserId={UserId}", folder.Name, folder.ParentFolderId, _userContext.Id);
                var list = await _context.Set<Wrapper>()
                .FromSqlInterpolated($@"
                    EXEC spFolders 
                    @Flag={"I"}, 
                    @FolderId={folder.ParentFolderId}, 
                    @FolderName={folder.Name},
                    @CreatedBy={_userContext.Id},
                    @UserId={folder.UserId},
                    @CreatedDate={DateTime.Now}")
                .ToListAsync();

                var result = list.FirstOrDefault();

                return result!.Status==true
                    ? Result.Success(result)
                    : Result.Failure(Error.Problem("Something went wrong!", result.Messages));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while adding folder: {FolderName}", folder.Name);
                return Result.Failure(Error.Problem("Something went wrong!", ex.Message));
            }
        }

        public async Task<Result> deleteFolderAsync(int Id)
        {
            try
            {
                _logger.LogInformation("DeleteFolderAsync called with Id={FolderId}, UserId={UserId}", Id, _userContext.Id);

                var list = await _context.Set<Wrapper>()
               .FromSqlInterpolated($@"
                    EXEC spFolders 
                    @Flag={"D"}, 
                    @Id={Id}, 
                    @CreatedBy={_userContext.Id},
                    @UserId={_userContext.Id},
                    @CreatedDate={DateTime.Now}")
               .ToListAsync();

                var result = list.FirstOrDefault();

                return result!.Status == true
                    ? Result.Success(result)
                    : Result.Failure(Error.Problem("Something went wrong!", result.Messages));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while deleting folder: Id={FolderId}", Id);
                return Result.Failure(Error.Problem("Something went wrong!", ex.Message));
            }
        }

        public async Task<Result> getAllFolderAsync()
        {
            try
            {
                var list = await _context.Set<FolderResponseDTO>()
                           .FromSqlInterpolated($@"
                                EXEC spFolders 
                                @Flag={"G"}, 
                                @UserId={_userContext.Id}")
                           .ToListAsync();

                return Result.Success(list);

            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Problem("Failed To Fetch Data", $"{ex.Message}"));
            }
        }

        public async Task<Result> getAllFolderByParentIdAsync(int Id)
        {
            try
            {
                var list = await _context.Set<FolderResponseDTO>()
                            .FromSqlInterpolated($@"
                                EXEC spFolders 
                                @Flag={"GP"}, 
                                @Id={Id}, 
                                @UserId={_userContext.Id}")
                            .ToListAsync();
                return Result.Success(list);

            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Problem("Failed To Fetch Data", $"{ex.Message}"));
            }
        }
        public async Task<Result> getAllParentFolderByAsync()
        {
            try
            {
                var list = await _context.Set<FolderResponseDTO>()
                            .FromSqlInterpolated($@"
                                EXEC spFolders 
                                @Flag={"GPI"}, 
                                @UserId={_userContext.Id}")
                            .ToListAsync();
                return Result.Success(list);

            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Problem("Failed To Fetch Data", $"{ex.Message}"));
            }
        }

        public async Task<Result> getFolderByIdAsync(int Id)
        {
            try
            {
                var list = await _context.Set<FolderResponseDTO>()
                            .FromSqlInterpolated($@"
                                EXEC spFolders 
                                @Flag={"GI"}, 
                                @Id={Id}, 
                                @UserId={_userContext.Id}")
                            .ToListAsync();

                var result = list.FirstOrDefault();
                return Result.Success(result);

            }
            catch (Exception ex)
            {
                return Result.Failure(Error.Problem("Failed To Fetch Data", $"{ex.Message}"));
            }
        }

        public async Task<Result> updateFolderAsync(FolderDTO folder, int Id)
        {
            try
            {
                _logger.LogInformation("UpdateFolderAsync called with Id={FolderId}, Name={FolderName}, UserId={UserId}", Id, folder.Name, _userContext.Id);

                var list = await _context.Set<Wrapper>()
                .FromSqlInterpolated($@"
                     EXEC spFolders 
                     @Flag={"U"}, 
                     @FolderId={folder.ParentFolderId}, 
                     @FolderName={folder.Name},
                     @Id={Id},
                     @ModifiedBy={_userContext.Id},
                     @UserId={_userContext.Id},
                     @ModifiedDate={DateTime.Now}")
                .ToListAsync();

                var result = list.FirstOrDefault();

                return result!.Status == true
                    ? Result.Success(result)
                    : Result.Failure(Error.Problem("Something went wrong!", result.Messages));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while Updating folder: Id={FolderId}", Id);
                return Result.Failure(Error.Problem("Something went wrong!", ex.Message));
            }
        }
    }
}
