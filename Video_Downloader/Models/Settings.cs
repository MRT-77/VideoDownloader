using System;
using System.IO;
using Newtonsoft.Json;

namespace VideoDownloader.Models
{
    [Serializable]
    internal class Settings
    {
        private static string GetFilePath() =>
            Path.GetFullPath(Path.Combine(".", "settings.json"));

        private static Settings? _settings;
        public static Settings Get
        {
            get
            {
                if (_settings == null)
                    Load();

                return _settings ??= new Settings();
            }
        }

        public static void Load()
        {
            if (!File.Exists(GetFilePath()))
                return;

            try
            {
                _settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(GetFilePath()));
            }
            catch { /* ignore */ }
        }

        public static void Save()
        {
            try
            {
                using var fs = new FileStream(GetFilePath(), FileMode.OpenOrCreate, FileAccess.ReadWrite);

                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(JsonConvert.SerializeObject(_settings, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Include,
                        Formatting = Formatting.Indented
                    }));
                    sw.Flush();
                }

                fs.Close();
            }
            catch { /* ignore */ }
        }

        public bool AnonymousUserAgent { get; set; }
        public bool UseCookiesText { get; set; } = true;
        public bool RemovePageDump { get; set; } = true;
        public bool ForceEnableMuxMedia { get; set; }
        public bool CheckForUpdatePlugins { get; set; } = true;
    }
}
