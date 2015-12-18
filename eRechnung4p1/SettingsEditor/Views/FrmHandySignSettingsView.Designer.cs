namespace SettingsEditor.Views
{
    partial class FrmHandySignSettingsView
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
            this.label9 = new System.Windows.Forms.Label();
            this.cbxSignType = new System.Windows.Forms.ComboBox();
            this.tbxHandyNr = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.commandButton2 = new WinFormsMvvm.Controls.CommandButton();
            this.cmdBtnClose = new WinFormsMvvm.Controls.CommandButton();
            this.handySignSettingsViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.handySignSettingsViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(136, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Möglichkeiten  der Signatur";
            // 
            // cbxSignType
            // 
            this.cbxSignType.FormattingEnabled = true;
            this.cbxSignType.Location = new System.Drawing.Point(192, 6);
            this.cbxSignType.Name = "cbxSignType";
            this.cbxSignType.Size = new System.Drawing.Size(270, 21);
            this.cbxSignType.TabIndex = 10;
            // 
            // tbxHandyNr
            // 
            this.tbxHandyNr.Location = new System.Drawing.Point(13, 54);
            this.tbxHandyNr.Name = "tbxHandyNr";
            this.tbxHandyNr.Size = new System.Drawing.Size(449, 20);
            this.tbxHandyNr.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(138, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Handynummer inkl. Vorwahl";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Location = new System.Drawing.Point(12, 92);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(279, 17);
            this.checkBox1.TabIndex = 40;
            this.checkBox1.Text = "Alle Daten bei Speichern in das Formular übernehmen";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // commandButton2
            // 
            this.commandButton2.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.handySignSettingsViewModelBindingSource, "SaveCommand", true));
            this.commandButton2.Location = new System.Drawing.Point(306, 88);
            this.commandButton2.Name = "commandButton2";
            this.commandButton2.Size = new System.Drawing.Size(75, 23);
            this.commandButton2.TabIndex = 39;
            this.commandButton2.Text = "Speichern";
            this.commandButton2.UseVisualStyleBackColor = true;
            // 
            // cmdBtnClose
            // 
            this.cmdBtnClose.CausesValidation = false;
            this.cmdBtnClose.Location = new System.Drawing.Point(387, 88);
            this.cmdBtnClose.Name = "cmdBtnClose";
            this.cmdBtnClose.Size = new System.Drawing.Size(75, 23);
            this.cmdBtnClose.TabIndex = 38;
            this.cmdBtnClose.Text = "Schliessen";
            this.cmdBtnClose.UseVisualStyleBackColor = true;
            // 
            // handySignSettingsViewModelBindingSource
            // 
            this.handySignSettingsViewModelBindingSource.DataSource = typeof(SettingsEditor.ViewModels.HandySignSettingsViewModel);
            // 
            // FrmHandySignSettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdBtnClose;
            this.ClientSize = new System.Drawing.Size(488, 128);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.commandButton2);
            this.Controls.Add(this.cmdBtnClose);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbxSignType);
            this.Controls.Add(this.tbxHandyNr);
            this.Controls.Add(this.label10);
            this.Name = "FrmHandySignSettingsView";
            this.Text = "Handy Signatur";
            ((System.ComponentModel.ISupportInitialize)(this.handySignSettingsViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbxSignType;
        private System.Windows.Forms.TextBox tbxHandyNr;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBox1;
        private WinFormsMvvm.Controls.CommandButton commandButton2;
        private WinFormsMvvm.Controls.CommandButton cmdBtnClose;
        private System.Windows.Forms.BindingSource handySignSettingsViewModelBindingSource;
    }
}