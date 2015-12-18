namespace ZustellDienstWindowSample
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBiller = new System.Windows.Forms.TextBox();
            this.txtReceipient = new System.Windows.Forms.TextBox();
            this.txtReNr = new System.Windows.Forms.TextBox();
            this.txtFn = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(343, 124);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Schliessen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Dateiname:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "RechnungsNr:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Rechnungssteller eMail:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Empänger eMail:";
            // 
            // txtBiller
            // 
            this.txtBiller.Location = new System.Drawing.Point(139, 67);
            this.txtBiller.Name = "txtBiller";
            this.txtBiller.Size = new System.Drawing.Size(279, 20);
            this.txtBiller.TabIndex = 5;
            // 
            // txtReceipient
            // 
            this.txtReceipient.Location = new System.Drawing.Point(139, 98);
            this.txtReceipient.Name = "txtReceipient";
            this.txtReceipient.Size = new System.Drawing.Size(279, 20);
            this.txtReceipient.TabIndex = 6;
            // 
            // txtReNr
            // 
            this.txtReNr.Location = new System.Drawing.Point(139, 38);
            this.txtReNr.Name = "txtReNr";
            this.txtReNr.Size = new System.Drawing.Size(279, 20);
            this.txtReNr.TabIndex = 7;
            // 
            // txtFn
            // 
            this.txtFn.Location = new System.Drawing.Point(139, 13);
            this.txtFn.Name = "txtFn";
            this.txtFn.Size = new System.Drawing.Size(279, 20);
            this.txtFn.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "label5";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 161);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtFn);
            this.Controls.Add(this.txtReNr);
            this.Controls.Add(this.txtReceipient);
            this.Controls.Add(this.txtBiller);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBiller;
        private System.Windows.Forms.TextBox txtReceipient;
        private System.Windows.Forms.TextBox txtReNr;
        private System.Windows.Forms.TextBox txtFn;
        private System.Windows.Forms.Label label5;
    }
}

