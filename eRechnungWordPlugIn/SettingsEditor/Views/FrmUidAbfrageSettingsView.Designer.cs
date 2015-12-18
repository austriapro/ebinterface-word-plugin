namespace SettingsEditor.Views
{
    partial class FrmUidAbfrageSettingsView
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
            this.label32 = new System.Windows.Forms.Label();
            this.txBenutzer = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.txTeilnehmer = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.cBtnSave = new WinFormsMvvm.Controls.CommandButton();
            this.cmdBtnClose = new WinFormsMvvm.Controls.CommandButton();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(12, 9);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(374, 13);
            this.label32.TabIndex = 16;
            this.label32.Text = "Geben Sie hier die Daten Ihres Benutzers für WebService im Finanzonline ein:";
            // 
            // txBenutzer
            // 
            this.txBenutzer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Benutzer", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txBenutzer.Location = new System.Drawing.Point(155, 64);
            this.txBenutzer.Name = "txBenutzer";
            this.txBenutzer.Size = new System.Drawing.Size(231, 20);
            this.txBenutzer.TabIndex = 15;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(SettingsEditor.ViewModels.UidAbfrageSettingsViewModel);
            // 
            // txTeilnehmer
            // 
            this.txTeilnehmer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Teilnehmer", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txTeilnehmer.Location = new System.Drawing.Point(155, 37);
            this.txTeilnehmer.Name = "txTeilnehmer";
            this.txTeilnehmer.Size = new System.Drawing.Size(231, 20);
            this.txTeilnehmer.TabIndex = 14;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(12, 67);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(115, 13);
            this.label23.TabIndex = 13;
            this.label23.Text = "Benutzer-Identifikation:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(12, 40);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(125, 13);
            this.label24.TabIndex = 12;
            this.label24.Text = "Teilnehmer-Identifikation:";
            // 
            // cBtnSave
            // 
            this.cBtnSave.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.bindingSource1, "SaveCommand", true));
            this.cBtnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cBtnSave.Location = new System.Drawing.Point(230, 90);
            this.cBtnSave.Name = "cBtnSave";
            this.cBtnSave.Size = new System.Drawing.Size(75, 23);
            this.cBtnSave.TabIndex = 42;
            this.cBtnSave.Text = "Speichern";
            this.cBtnSave.UseVisualStyleBackColor = true;
            // 
            // cmdBtnClose
            // 
            this.cmdBtnClose.CausesValidation = false;
            this.cmdBtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdBtnClose.Location = new System.Drawing.Point(311, 90);
            this.cmdBtnClose.Name = "cmdBtnClose";
            this.cmdBtnClose.Size = new System.Drawing.Size(75, 23);
            this.cmdBtnClose.TabIndex = 41;
            this.cmdBtnClose.Text = "Schliessen";
            this.cmdBtnClose.UseVisualStyleBackColor = true;
            // 
            // FrmUidAbfrageSettingsView
            // 
            this.AcceptButton = this.cBtnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdBtnClose;
            this.ClientSize = new System.Drawing.Size(401, 126);
            this.Controls.Add(this.cBtnSave);
            this.Controls.Add(this.cmdBtnClose);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.txBenutzer);
            this.Controls.Add(this.txTeilnehmer);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label24);
            this.Name = "FrmUidAbfrageSettingsView";
            this.Text = "UID Abfrage - Einstellungen";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txBenutzer;
        private System.Windows.Forms.TextBox txTeilnehmer;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private WinFormsMvvm.Controls.CommandButton cBtnSave;
        private WinFormsMvvm.Controls.CommandButton cmdBtnClose;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}