using DemoAppBE.Shared;

namespace DemoAppBE.Features.Email.Services.Interface
{
    public interface IMailLogService:IService
    {
        Task MailSend(string email,string ShareableLink);
    }
}
