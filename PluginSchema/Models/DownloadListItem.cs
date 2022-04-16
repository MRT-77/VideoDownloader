namespace VideoDownloader.PluginSchema.Models
{
    public class DownloadListItem
    {
        public DownloadListItem(string id, DownloadListItemType type, string? format,
            string? resolution, string? note)
        {
            Id = id;
            Type = type;
            Format = format;
            Resolution = resolution;
            Note = note;
        }

        public string Id { get; set; }
        public DownloadListItemType Type { get; set; }
        public string? Format { get; set; }
        public string? Resolution { get; set; }
        public string? Note { get; set; }
    }
}
