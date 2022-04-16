using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using VideoDownloader.PluginSchema.Helpers;
using VideoDownloader.PluginSchema.Models;

namespace VideoDownloader.YoutubeDlPlugin
{
    [Serializable]
    public class YoutubeDlPlugin : IPlugin
    {
        private readonly Dictionary<IDownloadInfo, CancellationTokenSource> _downloadList =
            new Dictionary<IDownloadInfo, CancellationTokenSource>();

        public const string AppFileName = "youtube-dl.exe";
        public const string CookieFileName = "cookies.txt";

        public event IPlugin.DownloadEvent? DownloadStateChanged;

        public string PluginName => "youtube-dl";

        public ILog? Logger { get; set; }

        public async Task<VersionInfo> GetVersionInfo(CancellationToken cancellationToken)
        {
            const string fileName = "youtube-dl.exe";
            string version = string.Empty;
            bool exists = false, needUpdate = false;

            // get current version
            try
            {
                var cli = new CliAppRunner(fileName)
                {
                    Logger = Logger
                };

                string output = "";
                cli.OnMessage += data =>
                    output += data;

                if (cli.Start("--version"))
                {
                    await Task.Run(cli.WaitForExit, cancellationToken);

                    version = output.Trim('\r', '\n', ' ');
                    exists = true;
                }
            }
            catch
            {
                exists = false;
            }

            // find latest version on web
            {
                const string mainPageUrl = "https://github.com/ytdl-org/youtube-dl/releases/latest";
                var response = await new HttpClient().GetAsync(mainPageUrl,
                    HttpCompletionOption.ResponseHeadersRead, cancellationToken);

                var uri = response.RequestMessage.RequestUri.ToString();
                var webVersion = uri.Substring(uri.LastIndexOf('/') + 1);

                if (!exists)
                    version = webVersion;
                else if (version != webVersion)
                {
                    needUpdate = true;
                    version = webVersion;
                }
            }

            var info = new VersionInfo(exists, needUpdate, fileName, version,
                $"https://github.com/ytdl-org/youtube-dl/releases/download/{version}/{fileName}");
            Logger?.Log(LogType.Info, info.ToString());
            return info;
        }

        public async Task<DownloadListItem[]> GetList(string url, CancellationToken token)
        {
            var cli = new CliAppRunner(AppFileName)
            {
                Logger = Logger
            };

            var stdOut = string.Empty;
            cli.OnMessage += str => stdOut += str + "\n";

            //_ = proc.Start((Settings.Get.AnonymousUserAgent ? "--user-agent \"\" " : "") +
            //                (Settings.Get.UseCookiesText && File.Exists(@".\cookies.txt") ?
            //                    "--cookies \"..\\cookies.txt\" " : "") +
            //                "--no-playlist -F \"" + address + "\" " + (writePage ? "--write-pages" : ""));

            if (!cli.Start((CookiesExists() ? "--cookies \"" + CookieFileName + "\" " : "") +
                            "--no-playlist -F \"" + url + "\" "))
                return Array.Empty<DownloadListItem>();

            await Task.Run(cli.WaitForExit, token);

            var list = CliTextToModel.GetItems(stdOut);
            return list;
        }

        public void StopDownloadAsync(IDownloadInfo info)
        {
            Task.Run(() =>
            {
                lock (_downloadList)
                {
                    if (_downloadList.TryGetValue(info, out var token))
                        token.Cancel();
                }
            });
        }

        public void StartDownloadAsync(IDownloadInfo info, string outputDirectory)
        {
            string path = "%(title)s-%(resolution)s.%(ext)s";
            if (info.FileName != null && !string.IsNullOrWhiteSpace(info.FileName))
                path = path.Replace("%(title)s", info.FileName.Trim());
            path = Path.Combine(outputDirectory, path);

            var formats = string.Join("+", info.Items
                .OrderBy(item => item.Type)
                .Where(item => item.Format != null)
                .Select(item => item.Id));

            var tokenSource = new CancellationTokenSource();
            var cli = new CliAppRunner(AppFileName)
            {
                Logger = Logger
            };

            TimeSpan? ffmpegDuration = null;

            cli.OnError += text =>
            {
                //  youtube-dl + FFMPEG
                if (YtDlFfmpegProgressChecker(text))
                {
                    info.State = DownloadState.Downloading;
                    DownloadStateChanged?.Invoke(info);
                }
            };

            cli.OnMessage += text =>
            {
                if (string.IsNullOrWhiteSpace(text))
                    return;

                // youtube-dl
                var ytDlProgress = text.GetProgressInfo();
                if (ytDlProgress.HasValue)
                {
                    info.Progress = ytDlProgress / 100D;

                    goto SET_STATE;
                }

                //  youtube-dl + FFMPEG
                if (YtDlFfmpegProgressChecker(text))
                    goto SET_STATE;

                string fileName = text.GetFileName();
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    info.CurrentFileName = fileName;
                    goto SET_STATE;
                }

                return;

            SET_STATE:
                info.State = DownloadState.Downloading;
                DownloadStateChanged?.Invoke(info);
            };

            bool YtDlFfmpegProgressChecker(string text)
            {
                //  youtube-dl + FFMPEG
                ffmpegDuration = text.GetDuration() ?? ffmpegDuration;
                var progressTime = text.GetProgressTime();
                if (!ffmpegDuration.HasValue || !progressTime.HasValue)
                    return false;

                info.Progress = progressTime.Value.TotalSeconds / ffmpegDuration.Value.TotalSeconds;
                DownloadStateChanged?.Invoke(info);

                return true;
            }

            //cli.Start("-o \"" +
            //          path + "\" -i --newline --no-playlist -f \"" +
            //          ci.Value.Formats + "\" -c -w \"" +
            //          url + "\"" +
            //          (Settings.Get.AnonymousUserAgent ? " --user-agent \"\" " : "") +
            //          (Settings.Get.UseCookiesText && File.Exists(@".\cookies.txt")
            //              ? " --cookies \"..\\cookies.txt\""
            //              : ""));

            cli.Start($"-o \"{path}\" " +
                      "-i --newline --no-playlist -f " +
                      $"\"{formats}\" -c -w " +
                      $"\"{info.Address}\"" +
                      (CookiesExists() ? $" --cookies \"{CookieFileName}\"" : ""));

            info.State = DownloadState.Pending;
            DownloadStateChanged?.Invoke(info);

            Task.Run(() =>
                {
                    cli.WaitForExit(tokenSource.Token);
                    cli.Kill();

                }, CancellationToken.None)
                .ContinueWith(_ =>
                {
                    var filePath = Path.Combine(outputDirectory, info.CurrentFileName ?? "");
                    if (!tokenSource.IsCancellationRequested && File.Exists(filePath))
                        info.State = DownloadState.Completed;
                    else
                        info.State = DownloadState.None;

                    DownloadStateChanged?.Invoke(info);

                    lock (_downloadList)
                    {
                        _downloadList.Remove(info);
                    }

                }, CancellationToken.None);

            lock (_downloadList)
            {
                _downloadList.Add(info, tokenSource);
            }
        }

        private static bool CookiesExists()
        {
            var path = Path.GetFullPath(Path.Combine(".", CookieFileName));
            return File.Exists(path);
        }
    }
}
