namespace SettingsEditor.Views
{
    partial class FrmZustellSettingsView
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
            this.cBtnSave = new WinFormsMvvm.Controls.CommandButton();
            this.cmdBtnClose = new WinFormsMvvm.Controls.CommandButton();
            this.label19 = new System.Windows.Forms.Label();
            this.tbxTemplatePath = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tbxLocalFilePath = new System.Windows.Forms.TextBox();
            this.cmdBtnGetFileDlg = new WinFormsMvvm.Controls.CommandButton();
            this.label1 = new System.Windows.Forms.Label();
            this.commandButton1 = new WinFormsMvvm.Controls.CommandButton();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cBtnTest = new WinFormsMvvm.Controls.CommandButton();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // cBtnSave
            // 
            this.cBtnSave.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.bindingSource1, "SaveCommand", true));
            this.cBtnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cBtnSave.Location = new System.Drawing.Point(406, 225);
            this.cBtnSave.Name = "cBtnSave";
            this.cBtnSave.Size = new System.Drawing.Size(75, 23);
            this.cBtnSave.TabIndex = 85;
            this.cBtnSave.Text = "Speichern";
            this.cBtnSave.UseVisualStyleBackColor = true;
            // 
            // cmdBtnClose
            // 
            this.cmdBtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdBtnClose.Location = new System.Drawing.Point(490, 225);
            this.cmdBtnClose.Name = "cmdBtnClose";
            this.cmdBtnClose.Size = new System.Drawing.Size(75, 23);
            this.cmdBtnClose.TabIndex = 84;
            this.cmdBtnClose.Text = "Schliessen";
            this.cmdBtnClose.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(12, 12);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(352, 13);
            this.label19.TabIndex = 83;
            this.label19.Text = "Pfad und Dateiname des aufzurufenden Programmes oder der Batchdatei";
            // 
            // tbxTemplatePath
            // 
            this.tbxTemplatePath.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ExeFileName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbxTemplatePath.Location = new System.Drawing.Point(12, 37);
            this.tbxTemplatePath.Name = "tbxTemplatePath";
            this.tbxTemplatePath.Size = new System.Drawing.Size(514, 20);
            this.tbxTemplatePath.TabIndex = 81;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(12, 126);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(125, 13);
            this.label18.TabIndex = 80;
            this.label18.Text = "Parameter für den Aufruf ";
            // 
            // tbxLocalFilePath
            // 
            this.tbxLocalFilePath.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Arguments", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbxLocalFilePath.Location = new System.Drawing.Point(12, 151);
            this.tbxLocalFilePath.Name = "tbxLocalFilePath";
            this.tbxLocalFilePath.Size = new System.Drawing.Size(553, 20);
            this.tbxLocalFilePath.TabIndex = 79;
            // 
            // cmdBtnGetFileDlg
            // 
            this.cmdBtnGetFileDlg.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.bindingSource1, "GetExeFileCommand", true));
            this.cmdBtnGetFileDlg.Location = new System.Drawing.Point(532, 35);
            this.cmdBtnGetFileDlg.Name = "cmdBtnGetFileDlg";
            this.cmdBtnGetFileDlg.Size = new System.Drawing.Size(32, 23);
            this.cmdBtnGetFileDlg.TabIndex = 86;
            this.cmdBtnGetFileDlg.Text = "...";
            this.cmdBtnGetFileDlg.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 65);
            this.label1.TabIndex = 87;
            this.label1.Text = "Ersetzungsmöglichkeiten im Parameter:\r\n\r\n{0} = Dateiname der ebInterface Rechnung" +
    "\r\n{1} = e-Mail Adresse des Rechnungsstellers\r\n{2} = e-Mail Adresse des Rechnungs" +
    "empfängers";
            // 
            // commandButton1
            // 
            this.commandButton1.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.bindingSource1, "WorkingDirCommand", true));
            this.commandButton1.Location = new System.Drawing.Point(533, 92);
            this.commandButton1.Name = "commandButton1";
            this.commandButton1.Size = new System.Drawing.Size(32, 23);
            this.commandButton1.TabIndex = 90;
            this.commandButton1.Text = "...";
            this.commandButton1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 13);
            this.label2.TabIndex = 89;
            this.label2.Text = "Vollständiger Pfad zum Arbeitsverzeichnis";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "WorkingDirectory", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox1.Location = new System.Drawing.Point(12, 94);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(514, 20);
            this.textBox1.TabIndex = 88;
            // 
            // cBtnTest
            // 
            this.cBtnTest.Location = new System.Drawing.Point(325, 225);
            this.cBtnTest.Name = "cBtnTest";
            this.cBtnTest.Size = new System.Drawing.Size(75, 23);
            this.cBtnTest.TabIndex = 91;
            this.cBtnTest.Text = "Testen";
            this.cBtnTest.UseVisualStyleBackColor = true;
            this.cBtnTest.Visible = false;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(SettingsEditor.ViewModels.ZustellSettingsViewModel);
            // 
            // FrmZustellSettingsView
            // 
            this.AcceptButton = this.cBtnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdBtnClose;
            this.ClientSize = new System.Drawing.Size(579, 270);
            this.Controls.Add(this.cBtnTest);
            this.Controls.Add(this.commandButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdBtnGetFileDlg);
            this.Controls.Add(this.cBtnSave);
            this.Controls.Add(this.cmdBtnClose);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.tbxTemplatePath);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.tbxLocalFilePath);
            this.Name = "FrmZustellSettingsView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Einstellungen: Zustelldienst";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WinFormsMvvm.Controls.CommandButton cBtnSave;
        private WinFormsMvvm.Controls.CommandButton cmdBtnClose;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbxTemplatePath;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbxLocalFilePath;
        private WinFormsMvvm.Controls.CommandButton cmdBtnGetFileDlg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private WinFormsMvvm.Controls.CommandButton commandButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private WinFormsMvvm.Controls.CommandButton cBtnTest;
    }
}