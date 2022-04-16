namespace VideoDownloader.Forms
{
    partial class FormAddToList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddToList));
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblVideoQuality = new System.Windows.Forms.Label();
            this.btnGetList = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cbMuxItems = new System.Windows.Forms.CheckBox();
            this.lItems = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpRow5 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpRow3 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpRow2 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpRow1 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpMain.SuspendLayout();
            this.tlpRow5.SuspendLayout();
            this.tlpRow3.SuspendLayout();
            this.tlpRow2.SuspendLayout();
            this.tlpRow1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbAddress
            // 
            this.tbAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.tbAddress.Location = new System.Drawing.Point(70, 3);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(511, 20);
            this.tbAddress.TabIndex = 3;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblAddress.Location = new System.Drawing.Point(3, 0);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(61, 27);
            this.lblAddress.TabIndex = 2;
            this.lblAddress.Text = "Address:";
            this.lblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblFileName.Location = new System.Drawing.Point(3, 0);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(61, 27);
            this.lblFileName.TabIndex = 5;
            this.lblFileName.Text = "File Name:";
            this.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbFileName
            // 
            this.tbFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.tbFileName.Location = new System.Drawing.Point(70, 3);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(511, 20);
            this.tbFileName.TabIndex = 6;
            this.tbFileName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbFileName_KeyPress);
            // 
            // btnStop
            // 
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnStop.Location = new System.Drawing.Point(439, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(68, 21);
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnCancelClear_Click);
            // 
            // lblVideoQuality
            // 
            this.lblVideoQuality.AutoSize = true;
            this.lblVideoQuality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVideoQuality.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblVideoQuality.Location = new System.Drawing.Point(3, 0);
            this.lblVideoQuality.Name = "lblVideoQuality";
            this.lblVideoQuality.Size = new System.Drawing.Size(61, 27);
            this.lblVideoQuality.TabIndex = 8;
            this.lblVideoQuality.Text = "Items:";
            this.lblVideoQuality.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGetList
            // 
            this.btnGetList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnGetList.Location = new System.Drawing.Point(513, 3);
            this.btnGetList.Name = "btnGetList";
            this.btnGetList.Size = new System.Drawing.Size(68, 21);
            this.btnGetList.TabIndex = 9;
            this.btnGetList.Text = "Get List";
            this.btnGetList.UseVisualStyleBackColor = true;
            this.btnGetList.Click += new System.EventHandler(this.BtnGetList_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(456, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(125, 23);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "Add To List";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // cbMuxItems
            // 
            this.cbMuxItems.AutoSize = true;
            this.cbMuxItems.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbMuxItems.Enabled = false;
            this.cbMuxItems.Location = new System.Drawing.Point(334, 3);
            this.cbMuxItems.Name = "cbMuxItems";
            this.cbMuxItems.Size = new System.Drawing.Size(116, 23);
            this.cbMuxItems.TabIndex = 13;
            this.cbMuxItems.Tag = "1";
            this.cbMuxItems.Text = "Mux selected items";
            this.cbMuxItems.UseVisualStyleBackColor = true;
            this.cbMuxItems.CheckedChanged += new System.EventHandler(this.cbMuxItems_CheckedChanged);
            // 
            // lItems
            // 
            this.lItems.CheckBoxes = true;
            this.lItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lItems.FullRowSelect = true;
            this.lItems.GridLines = true;
            this.lItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lItems.HideSelection = false;
            this.lItems.Location = new System.Drawing.Point(3, 84);
            this.lItems.Name = "lItems";
            this.lItems.Size = new System.Drawing.Size(578, 245);
            this.lItems.TabIndex = 11;
            this.lItems.UseCompatibleStateImageBehavior = false;
            this.lItems.View = System.Windows.Forms.View.Details;
            this.lItems.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.LItems_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Type";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Format";
            this.columnHeader2.Width = 45;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Resulation";
            this.columnHeader3.Width = 65;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Note";
            this.columnHeader4.Width = 365;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tlpRow5, 0, 4);
            this.tlpMain.Controls.Add(this.tlpRow3, 0, 2);
            this.tlpMain.Controls.Add(this.lItems, 0, 3);
            this.tlpMain.Controls.Add(this.tlpRow2, 0, 1);
            this.tlpMain.Controls.Add(this.tlpRow1, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 5;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tlpMain.Size = new System.Drawing.Size(584, 361);
            this.tlpMain.TabIndex = 0;
            // 
            // tlpRow5
            // 
            this.tlpRow5.ColumnCount = 2;
            this.tlpRow5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRow5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 131F));
            this.tlpRow5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpRow5.Controls.Add(this.btnAdd, 1, 0);
            this.tlpRow5.Controls.Add(this.cbMuxItems, 0, 0);
            this.tlpRow5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRow5.Location = new System.Drawing.Point(0, 332);
            this.tlpRow5.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRow5.Name = "tlpRow5";
            this.tlpRow5.RowCount = 1;
            this.tlpRow5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRow5.Size = new System.Drawing.Size(584, 29);
            this.tlpRow5.TabIndex = 12;
            // 
            // tlpRow3
            // 
            this.tlpRow3.ColumnCount = 3;
            this.tlpRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tlpRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tlpRow3.Controls.Add(this.lblVideoQuality, 0, 0);
            this.tlpRow3.Controls.Add(this.btnStop, 1, 0);
            this.tlpRow3.Controls.Add(this.btnGetList, 2, 0);
            this.tlpRow3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRow3.Location = new System.Drawing.Point(0, 54);
            this.tlpRow3.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRow3.Name = "tlpRow3";
            this.tlpRow3.RowCount = 1;
            this.tlpRow3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRow3.Size = new System.Drawing.Size(584, 27);
            this.tlpRow3.TabIndex = 7;
            // 
            // tlpRow2
            // 
            this.tlpRow2.ColumnCount = 2;
            this.tlpRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tlpRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRow2.Controls.Add(this.lblFileName, 0, 0);
            this.tlpRow2.Controls.Add(this.tbFileName, 1, 0);
            this.tlpRow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRow2.Location = new System.Drawing.Point(0, 27);
            this.tlpRow2.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRow2.Name = "tlpRow2";
            this.tlpRow2.RowCount = 1;
            this.tlpRow2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRow2.Size = new System.Drawing.Size(584, 27);
            this.tlpRow2.TabIndex = 4;
            // 
            // tlpRow1
            // 
            this.tlpRow1.ColumnCount = 2;
            this.tlpRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tlpRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRow1.Controls.Add(this.lblAddress, 0, 0);
            this.tlpRow1.Controls.Add(this.tbAddress, 1, 0);
            this.tlpRow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRow1.Location = new System.Drawing.Point(0, 0);
            this.tlpRow1.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRow1.Name = "tlpRow1";
            this.tlpRow1.RowCount = 1;
            this.tlpRow1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRow1.Size = new System.Drawing.Size(584, 27);
            this.tlpRow1.TabIndex = 1;
            // 
            // FormAddToList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(350, 310);
            this.Name = "FormAddToList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New Item";
            this.Load += new System.EventHandler(this.FormAddToList_Load);
            this.tlpMain.ResumeLayout(false);
            this.tlpRow5.ResumeLayout(false);
            this.tlpRow5.PerformLayout();
            this.tlpRow3.ResumeLayout(false);
            this.tlpRow3.PerformLayout();
            this.tlpRow2.ResumeLayout(false);
            this.tlpRow2.PerformLayout();
            this.tlpRow1.ResumeLayout(false);
            this.tlpRow1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox tbAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblVideoQuality;
        private System.Windows.Forms.Button btnGetList;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox cbMuxItems;
        private System.Windows.Forms.ListView lItems;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpRow3;
        private System.Windows.Forms.TableLayoutPanel tlpRow2;
        private System.Windows.Forms.TableLayoutPanel tlpRow1;
        private System.Windows.Forms.TableLayoutPanel tlpRow5;
    }
}

