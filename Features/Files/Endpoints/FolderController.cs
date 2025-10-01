using DemoAppBE.Domain;
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
    public class FolderController(IFolderService _folderService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> getAllFolder()
        {
            var result = await _folderService.getAllFolderAsync();
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> getFolderById(int Id)
        {
            var result = await _folderService.getFolderByIdAsync(Id);
            if (result.IsSuccess)
                return Ok(result);
            return NotFound(result);
        }
        [HttpGet("parent/{ParentId}")]
        public async Task<IActionResult> getFolderByParentId(int ParentId)
        {
            var result = await _folderService.getAllFolderByParentIdAsync(ParentId);
            if (result.IsSuccess)
                return Ok(result);
            return NotFound(result);
        }
        [HttpGet("parentFolder")]
        public async Task<IActionResult> getAllParentFolderByAsync()
        {
            var result = await _folderService.getAllParentFolderByAsync();
            if (result.IsSuccess)
                return Ok(result);
            return NotFound(result);
        }
        [HttpPost]
        public async Task<IActionResult> create([FromBody] FolderDTO folder)
        {
            var result = await _folderService.addFolderAsync(folder);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> update(int id,[FromBody] FolderDTO folder)
        {
            var result = await _folderService.updateFolderAsync(folder,id);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int Id)
        {
            var result = await _folderService.deleteFolderAsync(Id);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
