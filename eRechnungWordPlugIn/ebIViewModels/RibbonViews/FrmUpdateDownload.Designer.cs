namespace ebIViewModels.RibbonViews
{
    partial class FrmUpdateDownload
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
            this.components = new System.ComponentModel.Container();
            this.cmdBtnClose = new WinFormsMvvm.Controls.CommandButton();
            this.lblVersion = new System.Windows.Forms.Label();
            this.downloadViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.downloadViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdBtnClose
            // 
            this.cmdBtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdBtnClose.Location = new System.Drawing.Point(487, 125);
            this.cmdBtnClose.Name = "cmdBtnClose";
            this.cmdBtnClose.Size = new System.Drawing.Size(75, 23);
            this.cmdBtnClose.TabIndex = 0;
            this.cmdBtnClose.Text = "Schliessen";
            this.cmdBtnClose.UseVisualStyleBackColor = true;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.downloadViewModelBindingSource, "NewVersion", true));
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(13, 13);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(195, 13);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "New Version Ready for download";
            // 
            // downloadViewModelBindingSource
            // 
            this.downloadViewModelBindingSource.DataSource = typeof(ebIViewModels.RibbonViewModels.DownloadViewModel);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Hinweis:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 95);
            this.label2.MaximumSize = new System.Drawing.Size(500, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(466, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "Zur Installation folgen Sie der Anleitung in Ausfuellhilfe.pdf.  Das Dokument kan" +
    "n unter dem oben angegebenen Link  heruntergeladen werden.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Diese Version herunterladen:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Liste aller Versionen anzeigen:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.downloadViewModelBindingSource, "AllVersionsUrl", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.linkLabel1.Location = new System.Drawing.Point(173, 59);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(55, 13);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.downloadViewModelBindingSource, "LatestVersionUrl", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.linkLabel2.Location = new System.Drawing.Point(173, 37);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(361, 13);
            this.linkLabel2.TabIndex = 7;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "https://github.com/austriapro/ebinterface-word-plugin/releases/tag/V4.2.4";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // FrmUpdateDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdBtnClose;
            this.ClientSize = new System.Drawing.Size(574, 155);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.cmdBtnClose);
            this.Name = "FrmUpdateDownload";
            this.Text = "Download bestätigen";
            ((System.ComponentModel.ISupportInitialize)(this.downloadViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WinFormsMvvm.Controls.CommandButton cmdBtnClose;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.BindingSource downloadViewModelBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
    }
}