using System.ComponentModel.DataAnnotations.Schema;

namespace DemoAppBE.Domain
{
    public class FileItem : EntityBase
    {
        public string FileName { get; set; }
        public string OriginalName { get; set; }
        public string OriginalPath { get; set; }
        public decimal FileSize { get; set; }
        public int UserId {  get; set; }
        public int? FolderId {  get; set; }
        public Folder? Folder { get; set; }
        public User User { get; set; }

    }
}
