using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoDownloader.Models;
using VideoDownloader.PluginSchema.Models;

namespace VideoDownloader.Forms
{
    internal partial class FormAddToList : Form
    {
        private readonly ILog? _logger;
        private readonly IPlugin[] _plugins;
        private IPlugin _currentPlugin = null!;

        private bool _ffmpegMissingWarningShown;

        public List<DownloadInfo> DownloadInfo { get; }

        public FormAddToList(IPlugin[] plugins, ILog? logger)
        {
            InitializeComponent();

            DownloadInfo = new List<DownloadInfo>();

            _plugins = plugins;
            _logger = logger;
        }

        private void FormAddToList_Load(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;

            btnAdd.Click += (se, ea) => { SetDataAndClose(); };

            tbAddress.TextChanged += (se, ea) =>
            {
                lItems.Items.Clear();
                LItems_ItemChecked(null, null);
            };

            if (Clipboard.ContainsText())
            {
                string clipboard = Clipboard.GetText();
                if (Uri.TryCreate(clipboard, UriKind.Absolute, out _))
                    tbAddress.Text = clipboard;
            }
        }

        private CancellationTokenSource? _cancelToken;

        private void BtnGetList_Click(object sender, EventArgs e)
        {
            var address = tbAddress.Text;

            if (string.IsNullOrWhiteSpace(tbAddress.Text)) return;
            btnGetList.Enabled = false;
            btnAdd.Enabled = false;
            lItems.Items.Clear();
            tbAddress.ReadOnly = true;

            btnStop.Enabled = true;

            _cancelToken = new CancellationTokenSource();
            Task.Run(async () =>
            {
                var listItems = Array.Empty<DownloadListItem>();

                foreach (var plugin in _plugins)
                {
                    try
                    {
                        listItems = await plugin.GetList(address, _cancelToken.Token);
                    }
                    catch
                    {
                        continue;
                    }

                    if (listItems.Length == 0)
                        continue;

                    _currentPlugin = plugin;
                    break;
                }

                Invoke((Action)(() =>
               {
                   if (listItems.Length != 0)
                   {
                       foreach (var item in listItems)
                       {
                           var listViewItem = lItems.Items.Add(item.Type.ToString("G"));
                           listViewItem.SubItems.Add(item.Format);
                           listViewItem.SubItems.Add(item.Resolution);
                           listViewItem.SubItems.Add(item.Note);
                           listViewItem.Tag = item;
                       }

                       _logger?.Log(LogType.Info, "\"Items\" updated!");
                   }

                   btnStop.Enabled = false;
                   tbAddress.ReadOnly = false;
                   btnGetList.Enabled = true;
               }));

            }, _cancelToken.Token);
        }

        private void BtnCancelClear_Click(object sender, EventArgs e)
        {
            if (_cancelToken == null || _cancelToken.IsCancellationRequested)
                return;

            _cancelToken.Cancel();
            _logger?.Log(LogType.Warning, "Get \"Items\" stopped by user!");
        }

        private void TbFileName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
                return;

            if (Path.GetInvalidFileNameChars().Any(c => e.KeyChar == c))
                e.Handled = true;
        }

        private void SetDataAndClose()
        {
            DownloadInfo.Clear();

            var checkedItems = lItems.Items.OfType<ListViewItem>()
                .Where(w => w.Checked)
                .Select(item => item.Tag)
                .Cast<DownloadListItem>()
                .ToArray();

            if (cbMuxItems.Enabled && cbMuxItems.Checked)
            {
                DownloadInfo.Add(new DownloadInfo(_currentPlugin.PluginName,
                    tbAddress.Text, tbFileName.Text, checkedItems));
            }
            else
            {
                foreach (var item in checkedItems)
                    DownloadInfo.Add(new DownloadInfo(_currentPlugin.PluginName, tbAddress.Text,
                        tbFileName.Text, item));
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void LItems_ItemChecked(object? sender, ItemCheckedEventArgs? e)
        {
            var checkedItems = lItems.Items.Cast<ListViewItem>()
                .Where(a => a.Checked)
                .Select(listItem => listItem.Tag)
                .Cast<DownloadListItem>()
                .ToArray();

            if (checkedItems.Length == 0)
            {
                btnAdd.Enabled = cbMuxItems.Enabled = false;
                return;
            }

            var normal = checkedItems.Count(item => item.Type == DownloadListItemType.Normal);
            var audio = checkedItems.Count(item => item.Type == DownloadListItemType.Audio);
            var videoOnly = checkedItems.Count(item => item.Type == DownloadListItemType.VideoOnly);

            var canMux = normal == 0 && audio == 1 && videoOnly == 1;
            cbMuxItems.Enabled = cbMuxItems.Checked = canMux;
            btnAdd.Enabled = true;
        }

        private void cbMuxItems_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbMuxItems.Checked || _ffmpegMissingWarningShown)
                return;

            var path = Path.GetFullPath(Path.Combine(".", "ffmpeg.exe"));
            if (File.Exists(path))
                return;

            MessageBox.Show(@"The ffmpeg.exe tool does not exist, " +
                            @"so the Mux operation will not work!", @"Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);

            _ffmpegMissingWarningShown = true;
        }
    }
}
