using System;
using System.IO;
using System.Text;
using VideoDownloader.PluginSchema.Models;

namespace VideoDownloader.Helpers
{
    internal class Logger : IDisposable, ILog
    {
        private FileStream? _file;

        // todo: remove logs older than 30 days

        public bool Open()
        {
            if (_file != null)
                return true;

            try
            {
                _file = new FileStream(Path.GetFullPath(
                        Path.Combine(".", $"Log {DateTime.Now:yyyy-MM-dd}.txt")),
                    FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Log(LogType logType, params string[] lines)
        {
            if (!Open())
                return;

            var title = DateTime.Now.ToString("HH:mm:ss ") + logType switch
            {
                LogType.Danger => " ERR! ",
                LogType.Warning => "WARN! ",
                LogType.Info => "INFO: ",
                LogType.InStream => " IN<- ",
                LogType.OutStream => "OUT-> ",
                _ => throw new ArgumentOutOfRangeException(nameof(logType), logType, null)
            };

            var buffer = Encoding.UTF8.GetBytes(title +
                                                string.Join(Environment.NewLine, lines) +
                                                Environment.NewLine);

            _file!.Write(buffer, 0, buffer.Length);
            _file.Flush();
        }

        public void Dispose()
        {
            _file?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
