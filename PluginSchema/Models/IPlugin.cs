using System.Threading;
using System.Threading.Tasks;

namespace VideoDownloader.PluginSchema.Models
{
    public interface IPlugin
    {
        string PluginName { get; }

        ILog? Logger { get; set; }

        Task<VersionInfo> GetVersionInfo(CancellationToken cancellationToken);

        Task<DownloadListItem[]> GetList(string url, CancellationToken cancellationToken);

        void StartDownloadAsync(IDownloadInfo info, string outputDirectory);

        void StopDownloadAsync(IDownloadInfo downloadInfo);

        delegate void DownloadEvent(IDownloadInfo downloadInfo);
        event DownloadEvent DownloadStateChanged;
    }
}
