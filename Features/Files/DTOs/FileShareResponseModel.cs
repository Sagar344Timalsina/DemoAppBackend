namespace DemoAppBE.Features.Files.DTOs
{
    public class FileShareResponseModel
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public DateTime expiresAt { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public bool IsUsed { get; set; }
        public bool IsActive { get; set; }
        public string FileName { get; set; }
        public string Email{ get; set; }
        public string UserName { get; set; }
        public string OriginalName{ get; set; }
        public decimal FileSize { get; set; }
    }
}
