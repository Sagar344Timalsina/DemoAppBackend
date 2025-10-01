using DemoAppBE.Shared;

namespace DemoAppBE.Features.Files.Services.Interface
{
    public interface IFileStorageService:IService
    {
        Task<Result<string>> saveAsync(string storageName, Stream data, CancellationToken cancellationToken = default);
        Task<Result<Stream>> openReadAsync(string storageName,CancellationToken cancellationToken = default);
        Task<Result> deleteAsync(string storageName, CancellationToken cancellationToken = default);
        Task<Result> ExistsAsync(string storageName, CancellationToken cancellationToken = default);
    }
}
