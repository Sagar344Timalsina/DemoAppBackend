using DemoAppBE.Features.Files.Services.Implementation;
using DemoAppBE.Features.Files.Services.Interface;
using DemoAppBE.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace DemoAppBE.Features.Files.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileController : ControllerBase
    {
        private IFileStorageService _StorageService { get; set; }
        private IFileService _fileService { get; set; }
        public FileController(IFileStorageService StorageService, IFileService fileService)
        {
            _StorageService = StorageService;
            _fileService = fileService;
        }
        [HttpGet("{FolderId}")]
        public async Task<IActionResult> getAllList(int? FolderId)
        {
            return Ok();
        }
        [HttpPost("upload")]
        [RequestSizeLimit(200_000_000)]//200MB
        public async Task<IActionResult> upload([FromForm] IFormFile file, [FromQuery] string? Folder)
        {
            if (string.Equals(Folder, "null", StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(Folder))
            {
                Folder = null;
            }
            var response = await _fileService.uploadFile(file, Folder);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        [HttpGet("download")]
        public async Task<IActionResult> Download([FromQuery] string fileName, [FromServices] FileStorageService fileService)
        {
            var result = await _StorageService.openReadAsync(fileName);

            if (result.IsFailure)
                return NotFound(result);

            var stream = result.Value;
            var contentType = "application/octet-stream";
            return File(stream, contentType, fileName);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteFile(int id)
        {
            return NoContent();
        }
        [HttpGet("Fullpath")]
        public async Task<IActionResult> getFullPath([FromQuery] int? FolderId)
        {
            var res = await _fileService.getFullPath(FolderId);
            return Ok(res);
        }
        [HttpGet("View/{id}")]
        public async Task<IActionResult> ViewData([FromRoute] int id, [FromQuery] bool isFile)
        {
            try
            {
                string filePath = await _fileService.getOriginalFilePath(id);

                if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
                    return NotFound("File not found");
                var extension = Path.GetExtension(filePath).ToLowerInvariant();

                // Pick content type
                string contentType = extension switch
                {
                    ".jpg" or ".jpeg" => "image/jpeg",
                    ".png" => "image/png",
                    ".gif" => "image/gif",
                    ".webp" => "image/webp",
                    ".pdf" => "application/pdf",
                    _ => "application/octet-stream"
                };

                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

                return File(fileBytes, contentType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
       


    }
}
