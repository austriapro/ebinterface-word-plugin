namespace SettingsEditor.Views
{
    partial class FrmMailSettingsView
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
            this.tbxBetreff = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.rTbxMailBody = new System.Windows.Forms.RichTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.cBtnSave = new WinFormsMvvm.Controls.CommandButton();
            this.cmdBtnClose = new WinFormsMvvm.Controls.CommandButton();
            this.cBtnTest = new WinFormsMvvm.Controls.CommandButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.validationProvider1 = new Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbxBetreff
            // 
            this.tbxBetreff.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Subject", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbxBetreff.Location = new System.Drawing.Point(56, 17);
            this.tbxBetreff.Name = "tbxBetreff";
            this.validationProvider1.SetPerformValidation(this.tbxBetreff, true);
            this.tbxBetreff.Size = new System.Drawing.Size(586, 20);
            this.validationProvider1.SetSourcePropertyName(this.tbxBetreff, "Subject");
            this.tbxBetreff.TabIndex = 10;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(SettingsEditor.ViewModels.MailSettingsViewModel);
            // 
            // rTbxMailBody
            // 
            this.rTbxMailBody.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Body", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.rTbxMailBody.Location = new System.Drawing.Point(15, 58);
            this.rTbxMailBody.Name = "rTbxMailBody";
            this.rTbxMailBody.Size = new System.Drawing.Size(627, 229);
            this.rTbxMailBody.TabIndex = 12;
            this.rTbxMailBody.Text = "";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 42);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(50, 13);
            this.label17.TabIndex = 14;
            this.label17.Text = "Mail-Text";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 290);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(499, 39);
            this.label20.TabIndex = 13;
            this.label20.Text = "In Betreff und Mail-Text können folgende Marker zum Einfügen verschiedener Daten " +
    "verwendet werden:\r\n[RECHNUNGNR]  [RECHNUNGSNR]  [RECHNUNGSDATUM]\r\n[RECHNUNGSSTEL" +
    "LER]  [KONTAKT]  [TELEFON]  [EMAIL]";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(12, 20);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(38, 13);
            this.label21.TabIndex = 11;
            this.label21.Text = "Betreff";
            // 
            // cBtnSave
            // 
            this.cBtnSave.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.bindingSource1, "SaveCommand", true));
            this.cBtnSave.Location = new System.Drawing.Point(486, 314);
            this.cBtnSave.Name = "cBtnSave";
            this.cBtnSave.Size = new System.Drawing.Size(75, 23);
            this.cBtnSave.TabIndex = 39;
            this.cBtnSave.Text = "Speichern";
            this.cBtnSave.UseVisualStyleBackColor = true;
            this.cBtnSave.Click += new System.EventHandler(this.cBtnSave_Click);
            // 
            // cmdBtnClose
            // 
            this.cmdBtnClose.CausesValidation = false;
            this.cmdBtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdBtnClose.Location = new System.Drawing.Point(567, 314);
            this.cmdBtnClose.Name = "cmdBtnClose";
            this.cmdBtnClose.Size = new System.Drawing.Size(75, 23);
            this.cmdBtnClose.TabIndex = 38;
            this.cmdBtnClose.Text = "Schliessen";
            this.cmdBtnClose.UseVisualStyleBackColor = true;
            this.cmdBtnClose.Click += new System.EventHandler(this.cmdBtnClose_Click);
            // 
            // cBtnTest
            // 
            this.cBtnTest.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.bindingSource1, "TestenCommand", true));
            this.cBtnTest.Location = new System.Drawing.Point(405, 314);
            this.cBtnTest.Name = "cBtnTest";
            this.cBtnTest.Size = new System.Drawing.Size(75, 23);
            this.cBtnTest.TabIndex = 40;
            this.cBtnTest.Text = "Testen";
            this.cBtnTest.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.bindingSource1;
            // 
            // validationProvider1
            // 
            this.validationProvider1.ErrorProvider = this.errorProvider1;
            this.validationProvider1.RulesetName = "";
            this.validationProvider1.SourceTypeName = "SettingsEditor.ViewModels.MailSettingsViewModel, SettingsEditor";
            // 
            // FrmMailSettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdBtnClose;
            this.ClientSize = new System.Drawing.Size(665, 353);
            this.Controls.Add(this.cBtnTest);
            this.Controls.Add(this.cBtnSave);
            this.Controls.Add(this.cmdBtnClose);
            this.Controls.Add(this.tbxBetreff);
            this.Controls.Add(this.rTbxMailBody);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label21);
            this.Name = "FrmMailSettingsView";
            this.Text = "e-Mail Einstellungen";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxBetreff;
        private System.Windows.Forms.RichTextBox rTbxMailBody;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private WinFormsMvvm.Controls.CommandButton cBtnSave;
        private WinFormsMvvm.Controls.CommandButton cmdBtnClose;
        private WinFormsMvvm.Controls.CommandButton cBtnTest;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider validationProvider1;
    }
}