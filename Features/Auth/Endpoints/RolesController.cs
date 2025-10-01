using DemoAppBE.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAppBE.Features.Auth.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        public RolesController()
        {
            
        }
        [HttpGet]
        public async Task<IActionResult> getAllRoles()
        {
            return Ok("Done");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getRoleById(int Id)
        {
            return Ok("Done");
        }
        [HttpPost]
        public async Task<IActionResult> saveRole(Role role)
        {
            return Ok("Done");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> updateRole([FromBody]Role role ,[FromQuery]int Id)
        {
            return Ok("Done");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteRole([FromQuery] int Id)
        {
            return Ok("Done");
        }
    }
}
