using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoDownloader.Helpers;
using VideoDownloader.Models;
using VideoDownloader.PluginSchema.Models;

namespace VideoDownloader.Forms
{
    internal partial class FormList : Form
    {
        private const string DownloadFolderName = "Video Downloader";
        private readonly string _downloadFolderPath;

        private readonly ILog? _logger;
        private readonly IPlugin[] _plugins;

        public FormList(IPlugin[] plugins, ILog? logger)
        {
            InitializeComponent();

            _plugins = plugins;
            foreach (var plugin in _plugins)
            {
                plugin.DownloadStateChanged += _ =>
                {
                    Invoke((Action)UpdateList);
                    Invoke((Action)CheckDownloadQueue);
                };
            }

            _logger = logger;

            // Download Directory
            _downloadFolderPath = Path.Combine(KnownFolders.GetPath(KnownFolder.Downloads),
                DownloadFolderName);
            var dir = Directory.CreateDirectory(_downloadFolderPath);
            if (!dir.Exists)
                dir.Create();
        }

        private void CheckDownloadQueue()
        {
            var allInfo = list.Items.Cast<ListViewItem>()
                .Select(item => item.Tag).Cast<DownloadInfo>().ToArray();

            if (allInfo.Any(a => a.State == DownloadState.Downloading ||
                             a.State == DownloadState.Pending))
                return;

            var toDownload = allInfo.FirstOrDefault(info => info.Queued);
            if (toDownload == null)
                return;

            toDownload.Queued = false;
            GetPlugin(toDownload.PluginName).StartDownloadAsync(toDownload, _downloadFolderPath);
        }

        private IPlugin GetPlugin(string pluginName)
        {
            return _plugins.First(plugin => plugin.PluginName == pluginName);
        }

        private void FormWait(bool waitMode)
        {
            toolStrip.Enabled = list.Enabled = !waitMode;
            Cursor = waitMode ? Cursors.WaitCursor : Cursors.Default;
        }

        private void UpdateList()
        {
            var updated = false;

            foreach (ListViewItem item in list.Items)
            {
                if (item.Tag is DownloadInfo info)
                {
                    var state = GetListIconName(info);
                    string progress = info.State switch
                    {
                        DownloadState.Completed => "Completed",
                        DownloadState.Downloading => info.Progress.HasValue
                            ? (info.Progress.Value * 100).ToString("F0") + "%"
                            : "",
                        _ => string.Empty
                    };

                    if (item.ImageKey != state)
                    {
                        item.ImageKey = state;
                        updated = true;
                    }

                    if (item.SubItems[0].Text != progress)
                    {
                        item.SubItems[0].Text = progress;
                        updated = true;
                    }

                    if (item.SubItems[1].Text != info.CurrentFileName)
                    {
                        item.SubItems[1].Text = info.CurrentFileName;
                        updated = true;
                    }
                }
            }

            if (updated)
                list.Refresh();
        }

        private void FormList_Load(object sender, EventArgs e)
        {
            if (_plugins.Length == 0)
            {
                MessageBox.Show(@"No plugin found!", @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                Close();
                return;
            }

            if (Settings.Get.CheckForUpdatePlugins)
            {
                FormWait(true);

                Task.WhenAll(_plugins.Select(s =>
                        s.GetVersionInfo(CancellationToken.None)).ToArray())
                    .ContinueWith(async results =>
                    {
                        var pluginsInfo = (await results)
                            .Where(info => info.CanUpdate || !info.IsExists)
                            .OrderByDescending(o => o.IsExists).ToArray();

                        Invoke((Action)(() =>
                        {
                            foreach (var versionInfo in pluginsInfo)
                            {
                                if (string.IsNullOrEmpty(versionInfo.Url))
                                    continue;

                                var name = $@"{versionInfo.FileName} v{versionInfo.Version}";

                                if (versionInfo.IsExists &&
                                    MessageBox.Show(name + @" available" + '\n' +
                                                        @"Do you want to update?", @"Update Available",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                                    continue;

                                var progress = new FormProgress(name, versionInfo.FileName,
                                    versionInfo.Url, !versionInfo.IsExists);
                                progress.ShowDialog();
                            }

                            FormWait(false);
                        }));
                    });
            }

            btnAbout.Click += (se, ea) =>
                new FormAbout().ShowDialog();
            btnSettings.Click += (se, ea) =>
                new FormSettings().ShowDialog();
            btnShowsDownloads.Click += (se, ea) =>
                Process.Start("explorer.exe", _downloadFolderPath);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var formAddToList = new FormAddToList(_plugins, _logger);
            if (formAddToList.ShowDialog() != DialogResult.OK)
                return;

            var newItemsIndex = new List<int>();
            foreach (var info in formAddToList.DownloadInfo)
            {
                var newListItem = list.Items.Add(GetListIconName(info));
                newListItem.SubItems.Add(info.FileName);
                newListItem.SubItems.Add(info.Address);
                newListItem.SubItems.Add(info.RegisterDate.ToString("yyyy/MM/dd HH:mm:ss",
                    CultureInfo.InstalledUICulture));
                newListItem.Tag = info;

                newItemsIndex.Add(newListItem.Index);
            }

            _logger?.Log(LogType.Info, newItemsIndex.Count + " new item added to download list.");

            list.SelectedIndices.Clear();
            foreach (int index in newItemsIndex)
                list.SelectedIndices.Add(index);
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show(@"Are you sure you want to remove?", @"Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.No)
                return;

            var selectedItems = list.SelectedItems.Cast<ListViewItem>().ToArray();
            foreach (var item in selectedItems)
            {
                list.Items.Remove(item);

                if (list.Tag is DownloadInfo info &&
                    (info.State == DownloadState.Downloading ||
                     info.State == DownloadState.Pending))
                    GetPlugin(info.PluginName).StopDownloadAsync(info);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            var items = list.Items.Cast<ListViewItem>().ToArray();

            foreach (var item in items)
            {
                if (item.Tag is DownloadInfo info && info.State == DownloadState.Completed)
                    list.Items.Remove(item);
            }
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            var selectedIndices = list.SelectedIndices.Cast<int>()
                .OrderBy(o => o).ToArray();
            if (selectedIndices.Length == 0 || selectedIndices[0] == 0)
                return;

            foreach (var index in selectedIndices)
            {
                var item = list.Items[index];
                list.Items.RemoveAt(index);
                list.Items.Insert(index - 1, item);
            }
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            var selectedIndices = list.SelectedIndices.Cast<int>()
                .OrderByDescending(o => o).ToArray();
            if (selectedIndices.Length == 0 || selectedIndices[0] == list.Items.Count - 1)
                return;

            foreach (var index in selectedIndices)
            {
                var item = list.Items[index];
                list.Items.RemoveAt(index);
                list.Items.Insert(index + 1, item);
            }
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in list.SelectedItems)
                if (item.Tag is DownloadInfo info)
                    info.Queued = true;

            UpdateList();
            CheckDownloadQueue();
        }

        private void BtnDownloadAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in list.Items)
                if (item.Tag is DownloadInfo info && info.State == DownloadState.None)
                    info.Queued = true;

            UpdateList();
            CheckDownloadQueue();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in list.SelectedItems)
            {
                if (!(item.Tag is DownloadInfo info))
                    continue;

                info.Queued = false;

                if (info.State == DownloadState.Downloading ||
                    info.State == DownloadState.Pending)
                    GetPlugin(info.PluginName).StopDownloadAsync(info);
            }

            UpdateList();
        }

        private void BtnStopAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in list.Items)
            {
                if (!(item.Tag is DownloadInfo info))
                    continue;

                info.Queued = false;

                if (info.State == DownloadState.Downloading ||
                    info.State == DownloadState.Pending)
                    GetPlugin(info.PluginName).StopDownloadAsync(info);
            }

            UpdateList();
        }

        private static string GetListIconName(DownloadInfo info)
        {
            const string iconQueued = "queued";
            const string iconDownloading = "downloading";
            const string iconCompleted = "completed";

            if (info.Queued)
                return iconQueued;

            return info.State switch
            {
                DownloadState.None => string.Empty,
                DownloadState.Completed => iconCompleted,
                DownloadState.Downloading => iconDownloading,
                DownloadState.Pending => iconQueued,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
