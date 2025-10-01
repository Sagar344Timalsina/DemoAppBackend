namespace DemoAppBE.Domain
{
    public class FileShares:EntityBase
    {
        public int FileId { get; set; }
        public FileItem File { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }
        public bool IsActive { get; set; }

    }
}
