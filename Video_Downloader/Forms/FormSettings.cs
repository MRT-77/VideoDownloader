using System.Windows.Forms;
using VideoDownloader.Models;

namespace VideoDownloader.Forms
{
    internal partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
            Load += (se, ea) => { LoadSettings(); };
            btnCancel.Click += (se, ea) => { Close(); };
            btnOk.Click += (se, ea) =>
            {
                SaveSettings();
                Close();
            };
        }

        private void LoadSettings()
        {
            var settings = Settings.Get;
            cbAnonymousUserAgent.Checked = settings.AnonymousUserAgent;
            chbUseCookiesText.Checked = settings.UseCookiesText;
            chbDeleteDumpPage.Checked = settings.RemovePageDump;
            chbForceEnableMuxMedia.Checked = settings.ForceEnableMuxMedia;
            chbCheckForUpdatePlugins.Checked = settings.CheckForUpdatePlugins;
        }

        private void SaveSettings()
        {
            var settings = Settings.Get;
            settings.AnonymousUserAgent = cbAnonymousUserAgent.Checked;
            settings.UseCookiesText = chbUseCookiesText.Checked;
            settings.RemovePageDump = chbDeleteDumpPage.Checked;
            settings.ForceEnableMuxMedia = chbForceEnableMuxMedia.Checked;
            settings.CheckForUpdatePlugins = chbCheckForUpdatePlugins.Checked;
            Settings.Save();
        }
    }
}
