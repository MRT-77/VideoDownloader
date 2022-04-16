namespace VideoDownloader.PluginSchema.Models
{
    public interface ILog
    {
        void Log(LogType logType, params string[] lines);
    }
}
