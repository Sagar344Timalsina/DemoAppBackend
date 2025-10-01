namespace DemoAppBE.Features.Files.DTOs
{
    public class FolderDTO
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public int? ParentFolderId { get; set; }
    }
}
