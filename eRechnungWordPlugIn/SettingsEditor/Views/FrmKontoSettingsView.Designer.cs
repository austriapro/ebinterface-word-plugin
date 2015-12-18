namespace SettingsEditor.Views
{
    partial class FrmKontoSettingsView
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
            this.txBxBIC = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtIBAN = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtBxKtoWortlaut = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtBxBank = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmdBtnClose = new WinFormsMvvm.Controls.CommandButton();
            this.commandButton2 = new WinFormsMvvm.Controls.CommandButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txBxBIC
            // 
            this.txBxBIC.Location = new System.Drawing.Point(359, 67);
            this.txBxBIC.Name = "txBxBIC";
            this.txBxBIC.Size = new System.Drawing.Size(180, 20);
            this.txBxBIC.TabIndex = 13;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(328, 70);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(24, 13);
            this.label20.TabIndex = 12;
            this.label20.Text = "BIC";
            // 
            // txtIBAN
            // 
            this.txtIBAN.Location = new System.Drawing.Point(100, 65);
            this.txtIBAN.Name = "txtIBAN";
            this.txtIBAN.Size = new System.Drawing.Size(224, 20);
            this.txtIBAN.TabIndex = 6;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 70);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "IBAN";
            // 
            // txtBxKtoWortlaut
            // 
            this.txtBxKtoWortlaut.Location = new System.Drawing.Point(100, 39);
            this.txtBxKtoWortlaut.Name = "txtBxKtoWortlaut";
            this.txtBxKtoWortlaut.Size = new System.Drawing.Size(440, 20);
            this.txtBxKtoWortlaut.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 43);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Kontoinhaber";
            // 
            // txtBxBank
            // 
            this.txtBxBank.Location = new System.Drawing.Point(100, 12);
            this.txtBxBank.Name = "txtBxBank";
            this.txtBxBank.Size = new System.Drawing.Size(440, 20);
            this.txtBxBank.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Bank";
            // 
            // cmdBtnClose
            // 
            this.cmdBtnClose.CausesValidation = false;
            this.cmdBtnClose.Location = new System.Drawing.Point(464, 93);
            this.cmdBtnClose.Name = "cmdBtnClose";
            this.cmdBtnClose.Size = new System.Drawing.Size(75, 23);
            this.cmdBtnClose.TabIndex = 35;
            this.cmdBtnClose.Text = "Schliessen";
            this.cmdBtnClose.UseVisualStyleBackColor = true;
            // 
            // commandButton2
            // 
            this.commandButton2.Location = new System.Drawing.Point(383, 93);
            this.commandButton2.Name = "commandButton2";
            this.commandButton2.Size = new System.Drawing.Size(75, 23);
            this.commandButton2.TabIndex = 36;
            this.commandButton2.Text = "Speichern";
            this.commandButton2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Location = new System.Drawing.Point(13, 97);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(279, 17);
            this.checkBox1.TabIndex = 37;
            this.checkBox1.Text = "Alle Daten bei Speichern in das Formular übernehmen";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // FrmKontoSettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdBtnClose;
            this.ClientSize = new System.Drawing.Size(554, 131);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.txBxBIC);
            this.Controls.Add(this.commandButton2);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.cmdBtnClose);
            this.Controls.Add(this.txtIBAN);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtBxKtoWortlaut);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtBxBank);
            this.Name = "FrmKontoSettingsView";
            this.Text = "Konto";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txBxBIC;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtIBAN;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtBxKtoWortlaut;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtBxBank;
        private System.Windows.Forms.Label label7;
        private WinFormsMvvm.Controls.CommandButton cmdBtnClose;
        private WinFormsMvvm.Controls.CommandButton commandButton2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}