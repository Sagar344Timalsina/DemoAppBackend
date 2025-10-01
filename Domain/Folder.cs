using System.ComponentModel.DataAnnotations.Schema;

namespace DemoAppBE.Domain
{
    public class Folder:EntityBase
    {
        public string Name { get; set; }
        public int UserId {  get; set; }
        public int? ParentFolderId { get; set; }
        public Folder? ParentFolder { get; set; }
        public User User { get; set; }
        public ICollection<Folder> SubFolders { get; set; }=new List<Folder>();
        public ICollection<FileItem> Files { get; set; } = new List<FileItem>();

    }
}
