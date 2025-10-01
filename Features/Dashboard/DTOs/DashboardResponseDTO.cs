namespace DemoAppBE.Features.Dashboard.DTOs
{
    public class DashboardResponseDTO
    {
        public StorageSummary StorageSummary { get; set; }
        public List<FolderDetail> FolderDetails { get; set; }
        public List<latestFolder> LatestFolders { get; set; }
    }
    public class StorageSummary
    {
        public double TotalUsedGB { get; set; }
        public double maxStorageGB { get; set; }

    }
    public class FolderDetail
    {
        public int? FolderId { get; set; }
        public string FolderName { get; set; }
        public double TotalSize {  get; set; }
        public DateTime LastModified {  get; set; }
    }
    public class latestFolder
    {
        public int Id {  get; set; }
        public string Name {  get; set; }
        public int ParentFolderId {  get; set; }
    }

}
