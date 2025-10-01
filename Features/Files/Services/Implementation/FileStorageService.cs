using DemoAppBE.Features.Files.Services.Interface;
using DemoAppBE.Shared;
using System.IO;
using System.Runtime.Intrinsics.Arm;

namespace DemoAppBE.Features.Files.Services.Implementation
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _basePath = "C:\\Sagar\\Uploads";
        ILogger<FolderService> _logger;
        public FileStorageService(ILogger<FolderService> logger)
        {
            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }

            _logger = logger;
        }
        public async Task<Result> deleteAsync(string storageName, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Checking if file exists and Deleting at storageName={storageName}", storageName);
                var filePath = Path.Combine(_basePath, storageName);

                if (!File.Exists(filePath))
                {
                    return Result.Failure<Stream>(Error.NotFound("404", $"File '{storageName}' not found."));
                }

                await Task.Run(() => File.Delete(filePath), cancellationToken);

                var returnData = new
                {
                    messages =  "Successfull",
                    status = true
                };
                return Result.Success(returnData);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while Deleting file storageName={storageName}",storageName);
                return Result.Failure<bool>(Error.Problem("Something went wrong while checking file existence.", ex.Message));
            }
        }

        public async Task<Result> ExistsAsync(string storageName, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Checking if file exists at storageName={storageName}", storageName);

                var filePath = Path.Combine(_basePath, storageName);

                var exists = await Task.Run(() => File.Exists(filePath), cancellationToken);
                var returnData = new
                {
                    messages = exists ? "Successfull" : "Something went wrong",
                    status = exists
                };
                return Result.Success(returnData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while checking file existence");
                return Result.Failure<bool>(Error.Problem("Something went wrong while checking file existence.", ex.Message));
            }
        }

        public async Task<Result<Stream>> openReadAsync(string storageName, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Opening file for reading at storageName={storageName}", storageName);
                var filePath = Path.Combine(_basePath, storageName);

                if (!File.Exists(filePath))
                {
                    return Result.Failure<Stream>(Error.NotFound("404", $"File '{storageName}' not found."));
                }
                var fileStream = await Task.Run(() =>
                (Stream)new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read),
                cancellationToken);

                return Result.Success(fileStream);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while opening file for reading");
                return Result.Failure<Stream>(Error.Problem("Something went wrong while opening the file.", ex.Message));
            }
        }

        public async Task<Result<string>> saveAsync(string storageName, Stream data, CancellationToken cancellationToken = default)
        {
            try
            {

                _logger.LogInformation("Uploading File at storageName={storageName}", storageName);
                var filePath = Path.Combine(_basePath, storageName);

                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory!);

                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await data.CopyToAsync(fileStream, cancellationToken);
                }

                return Result.Success($"{filePath}");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while Uploading File");
                return Result.Failure<string>(Error.Problem("Something went wrong!", ex.Message));
            }

        }
    }
}
