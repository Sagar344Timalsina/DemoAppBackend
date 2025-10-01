using DemoAppBE.Features.Files.DTOs;
using DemoAppBE.Shared;

namespace DemoAppBE.Features.Files.Services.Interface
{
    public interface IFileService:IService
    {
        Task<string> getFullPath(int? FolderId);
        Task<string> getOriginalFilePath(int Id);
        Task<Result> saveFilePath(SaveFileRequestDTO saveFileRequestDTO);
        Task<Result> uploadFile(IFormFile file,string? Folder);




        ///File share
        Task<Result> saveToken(int FileId, string Email);
        Task<Result<FileResponseDTO>> validateToken(string token);
        Task<Result> updateshareDetails(int Id,bool IsActive);
        Task<Result> getSharedDataAsync();
    }
}
