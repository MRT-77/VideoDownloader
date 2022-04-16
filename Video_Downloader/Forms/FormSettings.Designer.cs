namespace VideoDownloader.Forms
{
    partial class FormSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbAnonymousUserAgent = new System.Windows.Forms.CheckBox();
            this.chbUseCookiesText = new System.Windows.Forms.CheckBox();
            this.chbDeleteDumpPage = new System.Windows.Forms.CheckBox();
            this.chbForceEnableMuxMedia = new System.Windows.Forms.CheckBox();
            this.chbCheckForUpdatePlugins = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnOk, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(197, 155);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(119, 129);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOk.Location = new System.Drawing.Point(32, 129);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.cbAnonymousUserAgent);
            this.flowLayoutPanel1.Controls.Add(this.chbUseCookiesText);
            this.flowLayoutPanel1.Controls.Add(this.chbDeleteDumpPage);
            this.flowLayoutPanel1.Controls.Add(this.chbForceEnableMuxMedia);
            this.flowLayoutPanel1.Controls.Add(this.chbCheckForUpdatePlugins);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(191, 120);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // cbAnonymousUserAgent
            // 
            this.cbAnonymousUserAgent.AutoSize = true;
            this.cbAnonymousUserAgent.Enabled = false;
            this.flowLayoutPanel1.SetFlowBreak(this.cbAnonymousUserAgent, true);
            this.cbAnonymousUserAgent.Location = new System.Drawing.Point(3, 3);
            this.cbAnonymousUserAgent.Name = "cbAnonymousUserAgent";
            this.cbAnonymousUserAgent.Size = new System.Drawing.Size(134, 17);
            this.cbAnonymousUserAgent.TabIndex = 0;
            this.cbAnonymousUserAgent.Text = "Anonymous user agent";
            this.cbAnonymousUserAgent.UseVisualStyleBackColor = true;
            // 
            // chbUseCookiesText
            // 
            this.chbUseCookiesText.AutoSize = true;
            this.chbUseCookiesText.Enabled = false;
            this.flowLayoutPanel1.SetFlowBreak(this.chbUseCookiesText, true);
            this.chbUseCookiesText.Location = new System.Drawing.Point(3, 26);
            this.chbUseCookiesText.Name = "chbUseCookiesText";
            this.chbUseCookiesText.Size = new System.Drawing.Size(99, 17);
            this.chbUseCookiesText.TabIndex = 1;
            this.chbUseCookiesText.Text = "Use cookies.txt";
            this.chbUseCookiesText.UseVisualStyleBackColor = true;
            // 
            // chbDeleteDumpPage
            // 
            this.chbDeleteDumpPage.AutoSize = true;
            this.chbDeleteDumpPage.Enabled = false;
            this.flowLayoutPanel1.SetFlowBreak(this.chbDeleteDumpPage, true);
            this.chbDeleteDumpPage.Location = new System.Drawing.Point(3, 49);
            this.chbDeleteDumpPage.Name = "chbDeleteDumpPage";
            this.chbDeleteDumpPage.Size = new System.Drawing.Size(122, 17);
            this.chbDeleteDumpPage.TabIndex = 2;
            this.chbDeleteDumpPage.Text = "Remove page dump";
            this.chbDeleteDumpPage.UseVisualStyleBackColor = true;
            // 
            // chbForceEnableMuxMedia
            // 
            this.chbForceEnableMuxMedia.AutoSize = true;
            this.chbForceEnableMuxMedia.Location = new System.Drawing.Point(3, 72);
            this.chbForceEnableMuxMedia.Name = "chbForceEnableMuxMedia";
            this.chbForceEnableMuxMedia.Size = new System.Drawing.Size(141, 17);
            this.chbForceEnableMuxMedia.TabIndex = 3;
            this.chbForceEnableMuxMedia.Text = "Force enable mux media";
            this.chbForceEnableMuxMedia.UseVisualStyleBackColor = true;
            // 
            // chbCheckForUpdatePlugins
            // 
            this.chbCheckForUpdatePlugins.AutoSize = true;
            this.chbCheckForUpdatePlugins.Location = new System.Drawing.Point(3, 95);
            this.chbCheckForUpdatePlugins.Name = "chbCheckForUpdatePlugins";
            this.chbCheckForUpdatePlugins.Size = new System.Drawing.Size(143, 17);
            this.chbCheckForUpdatePlugins.TabIndex = 4;
            this.chbCheckForUpdatePlugins.Text = "Check for update at start";
            this.chbCheckForUpdatePlugins.UseVisualStyleBackColor = true;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(197, 155);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox cbAnonymousUserAgent;
        private System.Windows.Forms.CheckBox chbUseCookiesText;
        private System.Windows.Forms.CheckBox chbDeleteDumpPage;
        private System.Windows.Forms.CheckBox chbForceEnableMuxMedia;
        private System.Windows.Forms.CheckBox chbCheckForUpdatePlugins;
    }
}