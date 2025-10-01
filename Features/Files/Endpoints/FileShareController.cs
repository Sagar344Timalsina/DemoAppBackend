using DemoAppBE.Features.Files.DTOs;
using DemoAppBE.Features.Files.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAppBE.Features.Files.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileShareController(IFileService _fileService) : ControllerBase
    {

        //create shareable Link
        [HttpPost("generate")]
        public async Task<IActionResult> generateShareableLink([FromQuery] int fileId, [FromQuery] string email)
        {
            var response = await _fileService.saveToken(fileId, email);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        [HttpGet("")]
        public async Task<IActionResult> getSharedData()
        {
            var response = await _fileService.getSharedDataAsync();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        //Access the File 
        [HttpGet("access")]
        [AllowAnonymous]
        public async Task<IActionResult> accessFile([FromQuery] string token)
        {
            var file = await _fileService.validateToken(token);
            if (file.IsSuccess)
            {
                //return PhysicalFile(res.Value.OriginalPath, "application/octet-stream", res.Value.FileName);
                var contentType = file.Value.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase)
               ? "application/pdf"
               : file.Value.FileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                 file.Value.FileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                 file.Value.FileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)
                   ? $"image/{Path.GetExtension(file.Value.FileName).TrimStart('.')}"
           : "application/octet-stream"; // fallback

                // Important: don't force download, so remove the "fileDownloadName"
                return PhysicalFile(file.Value.OriginalPath, contentType);
            }
            else
            {
                var html = @"
<!doctype html>
<html lang='en'><head><meta charset='utf-8'><meta name='viewport' content='width=device-width,initial-scale=1'><title>Link not found</title>
<style>body{font-family:Arial,Helvetica,sans-serif;background:#f6f7fb;margin:0;padding:40px;text-align:center}h1{color:#111}p{color:#555}</style>
</head><body><h1>Link not found or expired</h1><p>The file link is invalid or has expired. Please request a new link.</p></body></html>";

                return new ContentResult
                {
                    Content = html,
                    ContentType = "text/html",
                    StatusCode = 404
                };
            }
        }

        //Revoke Shareable Link
        [HttpPut("revoke")]
        public async Task<IActionResult> revokelink([FromQuery]int Id, [FromQuery]bool IsActive)
        {
            var response = await _fileService.updateshareDetails(Id,IsActive);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
