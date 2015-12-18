namespace ebIViewModels.Views
{
    partial class FrmDetailsEdit
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
            this.cmdBtnCancel = new WinFormsMvvm.Controls.CommandButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.vatDefaultValuesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmBxMwSt = new System.Windows.Forms.ComboBox();
            this.multiColumnComboBox1 = new WinFormsMvvm.MultiColumnComboBox();
            this.uoMListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.detailsViewValidationProvider = new Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider();
            this.cmdBtnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vatDefaultValuesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uoMListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdBtnCancel
            // 
            this.cmdBtnCancel.CausesValidation = false;
            this.cmdBtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdBtnCancel.Location = new System.Drawing.Point(290, 268);
            this.cmdBtnCancel.Name = "cmdBtnCancel";
            this.cmdBtnCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdBtnCancel.TabIndex = 9;
            this.cmdBtnCancel.Text = "Schliessen";
            this.cmdBtnCancel.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Bezeichnung", true));
            this.textBox1.Location = new System.Drawing.Point(84, 64);
            this.textBox1.Name = "textBox1";
            this.detailsViewValidationProvider.SetPerformValidation(this.textBox1, true);
            this.textBox1.Size = new System.Drawing.Size(277, 20);
            this.detailsViewValidationProvider.SetSourcePropertyName(this.textBox1, "Bezeichnung");
            this.textBox1.TabIndex = 2;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ebIViewModels.ViewModels.DetailsViewModel);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Bezeichnung";
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ArtikelNr", true));
            this.textBox2.Location = new System.Drawing.Point(84, 38);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(277, 20);
            this.textBox2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Artikel Nr.";
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Menge", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "#,##0.0000"));
            this.textBox3.Location = new System.Drawing.Point(84, 90);
            this.textBox3.Name = "textBox3";
            this.detailsViewValidationProvider.SetPerformValidation(this.textBox3, true);
            this.textBox3.Size = new System.Drawing.Size(101, 20);
            this.detailsViewValidationProvider.SetSourcePropertyName(this.textBox3, "Menge");
            this.textBox3.TabIndex = 3;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Menge";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(195, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Einheit";
            // 
            // textBox5
            // 
            this.textBox5.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "EinzelPreis", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N4"));
            this.textBox5.Location = new System.Drawing.Point(84, 116);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(101, 20);
            this.textBox5.TabIndex = 4;
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Einzelpreis";
            // 
            // textBox6
            // 
            this.textBox6.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Rabatt", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.textBox6.Location = new System.Drawing.Point(84, 142);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(101, 20);
            this.textBox6.TabIndex = 5;
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Rabatt%";
            // 
            // vatDefaultValuesBindingSource
            // 
            this.vatDefaultValuesBindingSource.DataMember = "VatList";
            this.vatDefaultValuesBindingSource.DataSource = this.bindingSource1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "MwSt %";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(196, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Netto";
            // 
            // textBox4
            // 
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "NettoBetragZeile", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.textBox4.Location = new System.Drawing.Point(240, 116);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(121, 20);
            this.textBox4.TabIndex = 19;
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox7
            // 
            this.textBox7.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "RabattBetragZeile", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.textBox7.Location = new System.Drawing.Point(240, 142);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(121, 20);
            this.textBox7.TabIndex = 21;
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(196, 145);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Rabatt";
            // 
            // textBox8
            // 
            this.textBox8.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "BruttoBetragZeile", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.textBox8.Location = new System.Drawing.Point(240, 196);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(121, 20);
            this.textBox8.TabIndex = 23;
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(195, 200);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Brutto";
            // 
            // textBox9
            // 
            this.textBox9.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "MwStBetragZeile", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.textBox9.Location = new System.Drawing.Point(240, 168);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(121, 20);
            this.textBox9.TabIndex = 25;
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(195, 171);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "MwSt";
            // 
            // textBox10
            // 
            this.textBox10.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "BestellBezug", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox10.Location = new System.Drawing.Point(84, 12);
            this.textBox10.Name = "textBox10";
            this.detailsViewValidationProvider.SetPerformValidation(this.textBox10, true);
            this.textBox10.Size = new System.Drawing.Size(101, 20);
            this.detailsViewValidationProvider.SetSourcePropertyName(this.textBox10, "BestellBezug");
            this.textBox10.TabIndex = 0;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Bestell-Pos.";
            // 
            // cmBxMwSt
            // 
            this.cmBxMwSt.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.bindingSource1, "VatItem", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmBxMwSt.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindingSource1, "IsVatBerechtigt", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cmBxMwSt.DataSource = this.vatDefaultValuesBindingSource;
            this.cmBxMwSt.DisplayMember = "MwStSatz";
            this.cmBxMwSt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmBxMwSt.FormattingEnabled = true;
            this.cmBxMwSt.Location = new System.Drawing.Point(84, 171);
            this.cmBxMwSt.Name = "cmBxMwSt";
            this.cmBxMwSt.Size = new System.Drawing.Size(101, 21);
            this.cmBxMwSt.TabIndex = 29;
            this.cmBxMwSt.ValueMember = "MwStSatz";
            // 
            // multiColumnComboBox1
            // 
            this.multiColumnComboBox1.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.bindingSource1, "UomSelected", true));
            this.multiColumnComboBox1.DataSource = this.uoMListBindingSource;
            this.multiColumnComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.multiColumnComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.multiColumnComboBox1.FormattingEnabled = true;
            this.multiColumnComboBox1.Location = new System.Drawing.Point(241, 91);
            this.multiColumnComboBox1.Name = "multiColumnComboBox1";
            this.multiColumnComboBox1.Size = new System.Drawing.Size(121, 21);
            this.multiColumnComboBox1.TabIndex = 30;
            // 
            // uoMListBindingSource
            // 
            this.uoMListBindingSource.DataMember = "UoMList";
            this.uoMListBindingSource.DataSource = this.bindingSource1;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.bindingSource1;
            // 
            // detailsViewValidationProvider
            // 
            this.detailsViewValidationProvider.ErrorProvider = this.errorProvider1;
            this.detailsViewValidationProvider.RulesetName = "";
            this.detailsViewValidationProvider.SourceTypeName = "ebIViewModels.ViewModels.DetailsViewModel, ebIViewModels";
            // 
            // cmdBtnSave
            // 
            this.cmdBtnSave.Location = new System.Drawing.Point(199, 268);
            this.cmdBtnSave.Name = "cmdBtnSave";
            this.cmdBtnSave.Size = new System.Drawing.Size(85, 23);
            this.cmdBtnSave.TabIndex = 31;
            this.cmdBtnSave.Text = "Übernehmen";
            this.cmdBtnSave.UseVisualStyleBackColor = true;
            this.cmdBtnSave.Click += new System.EventHandler(this.cmdBtnSave_Click);
            // 
            // FrmDetailsEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdBtnCancel;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(377, 303);
            this.Controls.Add(this.cmdBtnSave);
            this.Controls.Add(this.multiColumnComboBox1);
            this.Controls.Add(this.cmBxMwSt);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdBtnCancel);
            this.Name = "FrmDetailsEdit";
            this.ShowIcon = false;
            this.Text = "Detailposition bearbeiten";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vatDefaultValuesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uoMListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WinFormsMvvm.Controls.CommandButton cmdBtnCancel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingSource vatDefaultValuesBindingSource;
        private System.Windows.Forms.ComboBox cmBxMwSt;
        private WinFormsMvvm.MultiColumnComboBox multiColumnComboBox1;
        private System.Windows.Forms.BindingSource uoMListBindingSource;
        private Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider detailsViewValidationProvider;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button cmdBtnSave;
    }
}