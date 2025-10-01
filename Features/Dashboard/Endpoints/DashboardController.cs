using DemoAppBE.Features.Dashboard.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAppBE.Features.Dashboard.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController (IDashboardService _dashboardService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> getDashboardData()
        {
            var res = await _dashboardService.getAllDashboardDataAsync();
            return Ok(res);
        }
    }
}
