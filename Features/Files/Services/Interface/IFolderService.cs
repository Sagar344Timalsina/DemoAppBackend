using DemoAppBE.Domain;
using DemoAppBE.Features.Files.DTOs;
using DemoAppBE.Shared;

namespace DemoAppBE.Features.Files.Services.Interface
{
    public interface IFolderService:IService
    {
        Task<Result> getAllFolderAsync();
        Task<Result> getAllFolderByParentIdAsync(int Id);
        Task<Result> getFolderByIdAsync(int Id);
        Task<Result> getAllParentFolderByAsync();
        Task<Result> updateFolderAsync(FolderDTO folder,int Id);
        Task<Result> addFolderAsync(FolderDTO folder);
        Task<Result> deleteFolderAsync(int Id);
    }
}
