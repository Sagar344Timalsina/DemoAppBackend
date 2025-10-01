namespace DemoAppBE.Features.Files.DTOs
{
    public class SaveFileRequestDTO
    {
        public string FileName { get; set; }
        public string OriginalName { get; set; }
        public string OriginalPath { get; set; }
        public decimal FileSize { get; set; }
        public int UserId{ get; set; }
        public int? FolderId{ get; set; }
        public DateTimeOffset CreatedDate {  get; set; }
        public DateTimeOffset ModifiedDate {  get; set; }
    }
}
