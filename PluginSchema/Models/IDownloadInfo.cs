namespace VideoDownloader.PluginSchema.Models
{
    public interface IDownloadInfo
    {
        string Address { get; }
        string? FileName { get; }
        DownloadListItem[] Items { get; }
        DownloadState State { get; set; }
        string? CurrentFileName { get; set; }
        double? Progress { get; set; }
    }
}
