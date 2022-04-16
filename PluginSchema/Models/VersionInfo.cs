namespace VideoDownloader.PluginSchema.Models
{
    public class VersionInfo
    {
        public VersionInfo(bool isExists, bool canUpdate, string fileName, string version, string url)
        {
            IsExists = isExists;
            CanUpdate = canUpdate;
            Version = version;
            FileName = fileName;
            Url = url;
        }

        public bool IsExists { get; }
        public bool CanUpdate { get; }
        public string FileName { get; }
        public string Version { get; }
        public string Url { get; }

        public override string ToString()
        {
            return $"File \"{FileName}\"" +
                   $"{(string.IsNullOrEmpty(Version) ? "" : " version " + Version)}, " +
                   $"{nameof(IsExists)}: {(IsExists ? "Yes" : "No")}, " +
                   $"{nameof(CanUpdate)}: {(CanUpdate ? "Yes" : "No")}";
        }
    }
}
