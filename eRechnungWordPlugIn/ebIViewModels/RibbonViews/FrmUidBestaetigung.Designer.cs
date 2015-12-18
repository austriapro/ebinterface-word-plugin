namespace ebIViewModels.RibbonViews
{
    partial class FrmUidBestaetigung
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txUIDReceiver = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txUIDBiller = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txPIN = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txBenutzer = new System.Windows.Forms.TextBox();
            this.txTeilnehmer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new WinFormsMvvm.Controls.CommandButton();
            this.btnExecute = new WinFormsMvvm.Controls.CommandButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.validationProvider1 = new Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider();
            this.bSrcUidQuery = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bSrcUidQuery)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txUIDReceiver);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txUIDBiller);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(411, 85);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "UID Daten";
            // 
            // txUIDReceiver
            // 
            this.txUIDReceiver.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bSrcUidQuery, "ReceiverUid", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txUIDReceiver.Location = new System.Drawing.Point(179, 50);
            this.txUIDReceiver.Name = "txUIDReceiver";
            this.validationProvider1.SetPerformValidation(this.txUIDReceiver, true);
            this.txUIDReceiver.Size = new System.Drawing.Size(208, 20);
            this.validationProvider1.SetSourcePropertyName(this.txUIDReceiver, "ReceiverUid");
            this.txUIDReceiver.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Ihre UID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "UID des Rechnungsempfängers:";
            // 
            // txUIDBiller
            // 
            this.txUIDBiller.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bSrcUidQuery, "BillerUid", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txUIDBiller.Location = new System.Drawing.Point(179, 24);
            this.txUIDBiller.Name = "txUIDBiller";
            this.validationProvider1.SetPerformValidation(this.txUIDBiller, true);
            this.txUIDBiller.Size = new System.Drawing.Size(208, 20);
            this.validationProvider1.SetSourcePropertyName(this.txUIDBiller, "BillerUid");
            this.txUIDBiller.TabIndex = 16;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txPIN);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txBenutzer);
            this.groupBox2.Controls.Add(this.txTeilnehmer);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(411, 117);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Login Daten";
            // 
            // txPIN
            // 
            this.txPIN.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bSrcUidQuery, "Pin", true));
            this.txPIN.Location = new System.Drawing.Point(179, 80);
            this.txPIN.Name = "txPIN";
            this.txPIN.PasswordChar = '*';
            this.validationProvider1.SetPerformValidation(this.txPIN, true);
            this.txPIN.Size = new System.Drawing.Size(208, 20);
            this.validationProvider1.SetSourcePropertyName(this.txPIN, "Pin");
            this.txPIN.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "PIN:";
            // 
            // txBenutzer
            // 
            this.txBenutzer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bSrcUidQuery, "BenutzerId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txBenutzer.Location = new System.Drawing.Point(179, 54);
            this.txBenutzer.Name = "txBenutzer";
            this.validationProvider1.SetPerformValidation(this.txBenutzer, true);
            this.txBenutzer.Size = new System.Drawing.Size(208, 20);
            this.validationProvider1.SetSourcePropertyName(this.txBenutzer, "BenutzerId");
            this.txBenutzer.TabIndex = 19;
            // 
            // txTeilnehmer
            // 
            this.txTeilnehmer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bSrcUidQuery, "TeilNehmerId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txTeilnehmer.Location = new System.Drawing.Point(179, 28);
            this.txTeilnehmer.Name = "txTeilnehmer";
            this.validationProvider1.SetPerformValidation(this.txTeilnehmer, true);
            this.txTeilnehmer.Size = new System.Drawing.Size(208, 20);
            this.validationProvider1.SetSourcePropertyName(this.txTeilnehmer, "TeilNehmerId");
            this.txTeilnehmer.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "FinOnlBenutzer-Identifikation:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "FinOnlTeilnehmer-Identifikation:";
            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(333, 227);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Schliessen";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnExecute
            // 
            this.btnExecute.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.bSrcUidQuery, "BestaetigenCommand", true));
            this.btnExecute.Location = new System.Drawing.Point(207, 226);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(120, 23);
            this.btnExecute.TabIndex = 20;
            this.btnExecute.Text = "Bestätigung abrufen";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.bSrcUidQuery;
            // 
            // validationProvider1
            // 
            this.validationProvider1.ErrorProvider = this.errorProvider1;
            this.validationProvider1.RulesetName = "";
            this.validationProvider1.SourceTypeName = "ebIViewModels.RibbonViewModels.UidBestaetigungViewModel, ebIViewModels";
            // 
            // bSrcUidQuery
            // 
            this.bSrcUidQuery.DataSource = typeof(ebIViewModels.RibbonViewModels.UidBestaetigungViewModel);
            // 
            // FrmUidBestaetigung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(435, 263);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmUidBestaetigung";
            this.Text = "UID Bestätigung";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bSrcUidQuery)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox txUIDReceiver;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txUIDBiller;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txPIN;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txBenutzer;
        public System.Windows.Forms.TextBox txTeilnehmer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private WinFormsMvvm.Controls.CommandButton btnCancel;
        private WinFormsMvvm.Controls.CommandButton btnExecute;
        private System.Windows.Forms.BindingSource bSrcUidQuery;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider validationProvider1;
    }
}