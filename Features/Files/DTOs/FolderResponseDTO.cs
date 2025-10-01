namespace DemoAppBE.Features.Files.DTOs
{
    public class FolderResponseDTO
    {
        public int Id { get; set; }
        public string FolderName { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int? ParentFolderId { get; set; }
        public bool IsFile {  get; set; }
        public bool IsFolder {  get; set; }
        public decimal FileSize {  get; set; }
        public string FilePath {  get; set; }
        public DateTimeOffset CreatedDate{ get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}
