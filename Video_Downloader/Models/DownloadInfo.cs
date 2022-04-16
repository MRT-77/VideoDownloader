using System;
using Newtonsoft.Json;
using VideoDownloader.PluginSchema.Models;

namespace VideoDownloader.Models
{
    [Serializable]
    internal class DownloadInfo : IDownloadInfo
    {
        public DownloadInfo(string pluginName, string address, string? fileName, params DownloadListItem[] items)
        {
            PluginName = pluginName;
            Address = address;
            FileName = fileName;
            Items = items;
            RegisterDate = DateTime.Now;
            State = DownloadState.None;
        }

        [JsonConstructor]
        public DownloadInfo(string pluginName, string address, string? fileName, DateTime registerDate,
            DownloadState state, params DownloadListItem[] items)
        {
            PluginName = pluginName;
            Address = address;
            FileName = fileName;
            Items = items;
            RegisterDate = registerDate;
            State = state;
        }

        public string PluginName { get; }
        public string Address { get; }
        public string? FileName { get; }
        public DownloadListItem[] Items { get; }
        public DateTime RegisterDate { get; }
        public DownloadState State { get; set; }

        [JsonIgnore]
        public string? CurrentFileName { get; set; }
        
        [JsonIgnore]
        public double? Progress { get; set; }

        [JsonIgnore]
        public bool Queued { get; set; }
    }
}
