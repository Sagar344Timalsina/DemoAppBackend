namespace DemoAppBE.Features.Files.DTOs
{
    public class FileShareRequestDTO
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
