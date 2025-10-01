using DemoAppBE.Features.Email.DTOs;
using DemoAppBE.Features.Email.Services.Interface;
using DemoAppBE.Shared;
using System.Net;
using System.Net.Mail;
namespace DemoAppBE.Features.Email.Services.Implementation
{
    public class MailLogService: IMailLogService
    {
        private readonly EmailSetupModelDTO EmailSetupModelDTO = new();
        private readonly IConfiguration _configuration;
        private ILogger<MailLogService> _logger {  get; set; }

        public MailLogService(ILogger<MailLogService> logger, IConfiguration configuration) 
        {
            // Load settings once in constructor
            _configuration = configuration;
            _logger = logger;
            _configuration.GetSection("EmailSettings").Bind(EmailSetupModelDTO);
        }
        public Task MailSend(string email, string ShareableLink)
        {
            try
            {
                var subject = "Shareable Link";
                var contentType = "";
                var body = EmailTemplate.EmailFormat(ShareableLink);


                MailMessage mail= new MailMessage();
              //  SmtpClient smtpClient = new SmtpClient(EmailSetupModelDTO.SMTP);
                mail.From = new MailAddress(EmailSetupModelDTO.SenderEmail);
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                //smtpClient.Port=EmailSetupModelDTO.Port;
                using var smtpClient = new SmtpClient(EmailSetupModelDTO.SMTP, EmailSetupModelDTO.Port)
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,
                    UseDefaultCredentials = false, 
                    Credentials = new NetworkCredential(
                        EmailSetupModelDTO.SenderEmail,
                        EmailSetupModelDTO.Password)
                };
                smtpClient.Send(mail);
                return Task.CompletedTask;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while sending email");
                throw;
            }
        }
    }
}
