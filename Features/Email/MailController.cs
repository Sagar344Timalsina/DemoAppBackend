using DemoAppBE.Features.Email.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAppBE.Features.Email
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController(IMailLogService _mailLogService) : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> sendMail(string mail,string link)
        {
             await _mailLogService.MailSend(mail, link);
            return Ok();
        }
    }
}
