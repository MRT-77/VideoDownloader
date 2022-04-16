using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace VideoDownloader.Forms
{
    internal partial class FormProgress : Form
    {
        private readonly string _url;
        private readonly WebClient _web;

        public FormProgress(string title, string fileName, string url, bool forceDownload)
        {
            InitializeComponent();

            Text = title;
            _url = url;
            BtnCancel.Enabled = !forceDownload;

            _web = new WebClient();
            _web.DownloadProgressChanged += (_, args) =>
            {
                Invoke((Action)(() =>
                {
                    progressBar.Value = args.ProgressPercentage;
                    progressLbl.Text = $@"{args.ProgressPercentage:D}%  -  " +
                                       $@"{GetSize(args.BytesReceived)} from {GetSize(args.TotalBytesToReceive)}";
                }));
            };
            _web.DownloadDataCompleted += (_, args) =>
            {
                var path = Path.GetFullPath(Path.Combine(".", fileName));

                using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
                fileStream.Write(args.Result, 0, args.Result.Length);
                fileStream.Flush();
                fileStream.Close();

                Invoke((Action)Close);
            };
        }

        private static string GetSize(long size)
        {
            if (size < 1000)
                return size + " Bytes";

            if ((size /= 1024) < 1000)
                return size + " KB";

            if ((size /= 1024) < 1000)
                return size + " MB";

            return size / 1024 + " GB";
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var myCp = base.CreateParams;

                // CP_NO_CLOSE_BUTTON
                myCp.ClassStyle |= 0x200;

                return myCp;
            }
        }

        private void FormProgress_Load(object sender, EventArgs e)
        {
            _web.DownloadDataAsync(new Uri(_url));
        }
    }
}
