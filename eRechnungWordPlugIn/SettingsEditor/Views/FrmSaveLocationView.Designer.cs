namespace SettingsEditor.Views
{
    partial class FrmSaveLocationView
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
            this.tbxSignedFile = new System.Windows.Forms.TextBox();
            this.cbxSaveLocal = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbxTemplatePath = new System.Windows.Forms.TextBox();
            this.bndSrcPathSettings = new System.Windows.Forms.BindingSource(this.components);
            this.label18 = new System.Windows.Forms.Label();
            this.tbxLocalFilePath = new System.Windows.Forms.TextBox();
            this.cmdBtnClose = new WinFormsMvvm.Controls.CommandButton();
            this.commandButton2 = new WinFormsMvvm.Controls.CommandButton();
            this.cmdBtnSignedFolderDlg = new WinFormsMvvm.Controls.CommandButton();
            this.cmdBtnTemplatePathDlg = new WinFormsMvvm.Controls.CommandButton();
            this.cmdBtnStandardFolderDlg = new WinFormsMvvm.Controls.CommandButton();
            ((System.ComponentModel.ISupportInitialize)(this.bndSrcPathSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // tbxSignedFile
            // 
            this.tbxSignedFile.Location = new System.Drawing.Point(14, 126);
            this.tbxSignedFile.Name = "tbxSignedFile";
            this.tbxSignedFile.Size = new System.Drawing.Size(515, 20);
            this.tbxSignedFile.TabIndex = 74;
            this.tbxSignedFile.Visible = false;
            // 
            // cbxSaveLocal
            // 
            this.cbxSaveLocal.AutoSize = true;
            this.cbxSaveLocal.Checked = true;
            this.cbxSaveLocal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxSaveLocal.Location = new System.Drawing.Point(14, 103);
            this.cbxSaveLocal.Name = "cbxSaveLocal";
            this.cbxSaveLocal.Size = new System.Drawing.Size(243, 17);
            this.cbxSaveLocal.TabIndex = 75;
            this.cbxSaveLocal.Text = "Signierte Rechnung zusätzlich lokal speichern";
            this.cbxSaveLocal.UseVisualStyleBackColor = true;
            this.cbxSaveLocal.Visible = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(12, 54);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(133, 13);
            this.label19.TabIndex = 73;
            this.label19.Text = "... für ebInterface Vorlagen";
            // 
            // tbxTemplatePath
            // 
            this.tbxTemplatePath.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bndSrcPathSettings, "TemplatePath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbxTemplatePath.Location = new System.Drawing.Point(15, 70);
            this.tbxTemplatePath.Name = "tbxTemplatePath";
            this.tbxTemplatePath.Size = new System.Drawing.Size(514, 20);
            this.tbxTemplatePath.TabIndex = 71;
            // 
            // bndSrcPathSettings
            // 
            this.bndSrcPathSettings.DataSource = typeof(SettingsEditor.ViewModels.SaveLocationSettingsViewModel);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(12, 9);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(222, 13);
            this.label18.TabIndex = 70;
            this.label18.Text = "... für ebInterface Rechnungen ohne Signatur";
            // 
            // tbxLocalFilePath
            // 
            this.tbxLocalFilePath.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bndSrcPathSettings, "UnsignedPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbxLocalFilePath.Location = new System.Drawing.Point(14, 26);
            this.tbxLocalFilePath.Name = "tbxLocalFilePath";
            this.tbxLocalFilePath.Size = new System.Drawing.Size(515, 20);
            this.tbxLocalFilePath.TabIndex = 68;
            // 
            // cmdBtnClose
            // 
            this.cmdBtnClose.CausesValidation = false;
            this.cmdBtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdBtnClose.Location = new System.Drawing.Point(492, 163);
            this.cmdBtnClose.Name = "cmdBtnClose";
            this.cmdBtnClose.Size = new System.Drawing.Size(75, 23);
            this.cmdBtnClose.TabIndex = 77;
            this.cmdBtnClose.Text = "Schliessen";
            this.cmdBtnClose.UseVisualStyleBackColor = true;
            this.cmdBtnClose.Click += new System.EventHandler(this.cmdBtnClose_Click);
            // 
            // commandButton2
            // 
            this.commandButton2.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.bndSrcPathSettings, "SaveCommand", true));
            this.commandButton2.Location = new System.Drawing.Point(411, 163);
            this.commandButton2.Name = "commandButton2";
            this.commandButton2.Size = new System.Drawing.Size(75, 23);
            this.commandButton2.TabIndex = 78;
            this.commandButton2.Text = "Speichern";
            this.commandButton2.UseVisualStyleBackColor = true;
            this.commandButton2.Click += new System.EventHandler(this.commandButton2_Click);
            // 
            // cmdBtnSignedFolderDlg
            // 
            this.cmdBtnSignedFolderDlg.Location = new System.Drawing.Point(535, 124);
            this.cmdBtnSignedFolderDlg.Name = "cmdBtnSignedFolderDlg";
            this.cmdBtnSignedFolderDlg.Size = new System.Drawing.Size(32, 23);
            this.cmdBtnSignedFolderDlg.TabIndex = 87;
            this.cmdBtnSignedFolderDlg.Text = "...";
            this.cmdBtnSignedFolderDlg.UseVisualStyleBackColor = true;
            this.cmdBtnSignedFolderDlg.Visible = false;
            // 
            // cmdBtnTemplatePathDlg
            // 
            this.cmdBtnTemplatePathDlg.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.bndSrcPathSettings, "TemplatePathCommand", true));
            this.cmdBtnTemplatePathDlg.Location = new System.Drawing.Point(535, 68);
            this.cmdBtnTemplatePathDlg.Name = "cmdBtnTemplatePathDlg";
            this.cmdBtnTemplatePathDlg.Size = new System.Drawing.Size(32, 23);
            this.cmdBtnTemplatePathDlg.TabIndex = 88;
            this.cmdBtnTemplatePathDlg.Text = "...";
            this.cmdBtnTemplatePathDlg.UseVisualStyleBackColor = true;
            // 
            // cmdBtnStandardFolderDlg
            // 
            this.cmdBtnStandardFolderDlg.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.bndSrcPathSettings, "UnsignedPathCommand", true));
            this.cmdBtnStandardFolderDlg.Location = new System.Drawing.Point(535, 24);
            this.cmdBtnStandardFolderDlg.Name = "cmdBtnStandardFolderDlg";
            this.cmdBtnStandardFolderDlg.Size = new System.Drawing.Size(32, 23);
            this.cmdBtnStandardFolderDlg.TabIndex = 89;
            this.cmdBtnStandardFolderDlg.Text = "...";
            this.cmdBtnStandardFolderDlg.UseVisualStyleBackColor = true;
            // 
            // FrmSaveLocationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdBtnClose;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(579, 195);
            this.Controls.Add(this.cmdBtnStandardFolderDlg);
            this.Controls.Add(this.cmdBtnTemplatePathDlg);
            this.Controls.Add(this.cmdBtnSignedFolderDlg);
            this.Controls.Add(this.commandButton2);
            this.Controls.Add(this.cmdBtnClose);
            this.Controls.Add(this.tbxSignedFile);
            this.Controls.Add(this.cbxSaveLocal);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.tbxTemplatePath);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.tbxLocalFilePath);
            this.Name = "FrmSaveLocationView";
            this.Text = "Speicherorte";
            ((System.ComponentModel.ISupportInitialize)(this.bndSrcPathSettings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxSignedFile;
        private System.Windows.Forms.CheckBox cbxSaveLocal;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbxTemplatePath;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbxLocalFilePath;
        private WinFormsMvvm.Controls.CommandButton cmdBtnClose;
        private WinFormsMvvm.Controls.CommandButton commandButton2;
        private WinFormsMvvm.Controls.CommandButton cmdBtnSignedFolderDlg;
        private WinFormsMvvm.Controls.CommandButton cmdBtnTemplatePathDlg;
        private WinFormsMvvm.Controls.CommandButton cmdBtnStandardFolderDlg;
        private System.Windows.Forms.BindingSource bndSrcPathSettings;
    }
}