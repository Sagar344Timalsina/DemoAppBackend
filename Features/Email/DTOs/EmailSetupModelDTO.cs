namespace DemoAppBE.Features.Email.DTOs
{
    public class EmailSetupModelDTO
    {
        public int Id { get; set; }
        public string SenderEmail { get; set; }=string.Empty;
        public string Password { get; set; }=string.Empty;
        public int Port { get; set; }
        public string SMTP {  get; set; }=string.Empty;
        public int MailType {  get; set; }
        public string MailTypeName { get; set; } = string.Empty;
        public string MailFormat {  get; set; } = string.Empty;
    }
}
