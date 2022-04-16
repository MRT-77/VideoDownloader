using System.Diagnostics;
using System.IO;
using System.Threading;
using VideoDownloader.PluginSchema.Models;

namespace VideoDownloader.PluginSchema.Helpers
{
    public class CliAppRunner
    {
        private readonly Process _process;

        public delegate void TextDelegate(string text);
        public delegate void VoidDelegate();

        public event TextDelegate? OnMessage;
        public event TextDelegate? OnError;
        public event VoidDelegate? Exited;

        public ILog? Logger { get; set; }

        public CliAppRunner(string appName)
        {
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = Path.GetFullPath(Path.Combine(".", appName)),
                WorkingDirectory = Path.GetFullPath(@"."),
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true
            };

            _process = new Process
            {
                EnableRaisingEvents = true,
                StartInfo = startInfo
            };

            _process.ErrorDataReceived += (_, arg) =>
            {
                Logger?.Log(LogType.InStream, arg.Data);
                OnError?.Invoke(arg.Data);
            };

            _process.OutputDataReceived += (_, arg) =>
            {
                Logger?.Log(LogType.InStream, arg.Data);
                OnMessage?.Invoke(arg.Data);
            };

            _process.Exited += (o, e) =>
            {
                Logger?.Log(LogType.Info, $"CLI APP: {appName} exited!");
                Exited?.Invoke();
            };
        }

        public void Kill()
        {
            if (Send_Ctrl_C_ToConsole(_process))
                _process.WaitForExit(3000);

            if (!_process.HasExited)
                _process.Kill();
        }

        public void WaitForExit()
        {
            _process.WaitForExit();
        }

        public void WaitForExit(CancellationToken token)
        {
            while (!_process.HasExited && !token.IsCancellationRequested)
            {
            }
        }

        public bool Start(string args)
        {
            Logger?.Log(LogType.OutStream, _process.StartInfo.FileName + " " + args);

            try
            {
                _process.StartInfo.Arguments = args;

                _process.Start();
                _process.BeginErrorReadLine();
                _process.BeginOutputReadLine();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool Send_Ctrl_C_ToConsole(Process p)
        {
            try
            {
                if (!WinApi.AttachConsole((uint)p.Id))
                    return false;

                WinApi.SetConsoleCtrlHandler(null, true);
                try
                {
                    if (!WinApi.GenerateConsoleCtrlEvent(WinApi.ConsoleCtrlEvent.CTRL_C, 0))
                        return false;
                }
                finally
                {
                    WinApi.FreeConsole();
                    WinApi.SetConsoleCtrlHandler(null, false);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
