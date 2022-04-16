using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;
using VideoDownloader.Forms;
using VideoDownloader.Helpers;
using VideoDownloader.PluginSchema.Helpers;
using VideoDownloader.PluginSchema.Models;

namespace VideoDownloader
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)(192 | 768 | 3072 | 12288);

            using var logger = new Logger();
            logger.Log(LogType.Info, "Program Started!");

            var loader = new PluginLoader
            {
                Logger = logger
            };

            var json = File.ReadAllText(Path.GetFullPath(Path.Combine(".", "pugings.json")));
            loader.Load(JsonConvert.DeserializeObject<string[]>(json)!, logger);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormList(loader.Plugins, logger));
        }
    }
}
