namespace ebIViewModels.SettingsViews
{
    partial class FrmBillerSettingsView
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
            this.bindingSourceReStellerSetting = new System.Windows.Forms.BindingSource(this.components);
            this.countryCodesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vatDefaultListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnSave = new WinformsMvvm.Controls.CommandButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.validationProviderBillerSettings = new Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider();
            this.tbxMyGLN = new System.Windows.Forms.TextBox();
            this.txtBxVStText = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtBxStrasse = new System.Windows.Forms.TextBox();
            this.txtBxPLZ = new System.Windows.Forms.MaskedTextBox();
            this.txtBxOrt = new System.Windows.Forms.TextBox();
            this.txtBxVATID = new System.Windows.Forms.TextBox();
            this.txtBxEmail = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.currencyListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.ckBxVstBerechtigt = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBxBillerContact = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cBxMwSt = new System.Windows.Forms.ComboBox();
            this.cBxBillerCountry = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtBxPhone = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txBxBIC = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtIBAN = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtBxKtoWortlaut = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtBxBank = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.commandButton1 = new WinformsMvvm.Controls.CommandButton();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceReStellerSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryCodesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vatDefaultListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currencyListBindingSource)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bindingSourceReStellerSetting
            // 
            this.bindingSourceReStellerSetting.DataSource = typeof(ebIViewModels.SettingsViewModels.BillerSettingsViewModel);
            // 
            // countryCodesBindingSource
            // 
            this.countryCodesBindingSource.DataMember = "CountryCodes";
            this.countryCodesBindingSource.DataSource = this.bindingSourceReStellerSetting;
            // 
            // vatDefaultListBindingSource
            // 
            this.vatDefaultListBindingSource.DataMember = "VatDefaultList";
            this.vatDefaultListBindingSource.DataSource = this.bindingSourceReStellerSetting;
            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(511, 495);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceReStellerSetting, "Save2Form", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox1.Location = new System.Drawing.Point(12, 499);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(279, 17);
            this.checkBox1.TabIndex = 30;
            this.checkBox1.Text = "Alle Daten bei Speichern in das Formular übernehmen";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.bindingSourceReStellerSetting, "SaveCommand", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnSave.Location = new System.Drawing.Point(430, 495);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 31;
            this.btnSave.Text = "Speichern";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.bindingSourceReStellerSetting;
            // 
            // validationProviderBillerSettings
            // 
            this.validationProviderBillerSettings.ErrorProvider = this.errorProvider1;
            this.validationProviderBillerSettings.RulesetName = "";
            this.validationProviderBillerSettings.SourceTypeName = "ebIViewModels.SettingsViewModels.BillerSettingsViewModel, ebIViewModels";
            // 
            // tbxMyGLN
            // 
            this.tbxMyGLN.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceReStellerSetting, "Gln", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tbxMyGLN.Location = new System.Drawing.Point(344, 229);
            this.tbxMyGLN.Name = "tbxMyGLN";
            this.validationProviderBillerSettings.SetPerformValidation(this.tbxMyGLN, true);
            this.tbxMyGLN.Size = new System.Drawing.Size(136, 20);
            this.validationProviderBillerSettings.SetSourcePropertyName(this.tbxMyGLN, "Gln");
            this.tbxMyGLN.TabIndex = 53;
            // 
            // txtBxVStText
            // 
            this.txtBxVStText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceReStellerSetting, "VatText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBxVStText.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindingSourceReStellerSetting, "IsNotVatBerechtigt", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBxVStText.Location = new System.Drawing.Point(9, 301);
            this.txtBxVStText.Name = "txtBxVStText";
            this.validationProviderBillerSettings.SetPerformValidation(this.txtBxVStText, true);
            this.txtBxVStText.Size = new System.Drawing.Size(549, 20);
            this.validationProviderBillerSettings.SetSourcePropertyName(this.txtBxVStText, "VatText");
            this.txtBxVStText.TabIndex = 51;
            // 
            // txtName
            // 
            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceReStellerSetting, "Name", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtName.Location = new System.Drawing.Point(94, 16);
            this.txtName.Name = "txtName";
            this.validationProviderBillerSettings.SetPerformValidation(this.txtName, true);
            this.txtName.Size = new System.Drawing.Size(468, 20);
            this.validationProviderBillerSettings.SetSourcePropertyName(this.txtName, "Name");
            this.txtName.TabIndex = 30;
            // 
            // txtBxStrasse
            // 
            this.txtBxStrasse.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceReStellerSetting, "Strasse", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBxStrasse.Location = new System.Drawing.Point(94, 43);
            this.txtBxStrasse.Name = "txtBxStrasse";
            this.validationProviderBillerSettings.SetPerformValidation(this.txtBxStrasse, true);
            this.txtBxStrasse.Size = new System.Drawing.Size(468, 20);
            this.validationProviderBillerSettings.SetSourcePropertyName(this.txtBxStrasse, "Strasse");
            this.txtBxStrasse.TabIndex = 31;
            // 
            // txtBxPLZ
            // 
            this.txtBxPLZ.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBxPLZ.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceReStellerSetting, "Plz", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBxPLZ.Location = new System.Drawing.Point(95, 71);
            this.txtBxPLZ.Mask = "00000";
            this.txtBxPLZ.Name = "txtBxPLZ";
            this.validationProviderBillerSettings.SetPerformValidation(this.txtBxPLZ, true);
            this.txtBxPLZ.Size = new System.Drawing.Size(38, 20);
            this.validationProviderBillerSettings.SetSourcePropertyName(this.txtBxPLZ, "Plz");
            this.txtBxPLZ.TabIndex = 33;
            // 
            // txtBxOrt
            // 
            this.txtBxOrt.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceReStellerSetting, "Ort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBxOrt.Location = new System.Drawing.Point(173, 71);
            this.txtBxOrt.Name = "txtBxOrt";
            this.validationProviderBillerSettings.SetPerformValidation(this.txtBxOrt, true);
            this.txtBxOrt.Size = new System.Drawing.Size(388, 20);
            this.validationProviderBillerSettings.SetSourcePropertyName(this.txtBxOrt, "Ort");
            this.txtBxOrt.TabIndex = 34;
            // 
            // txtBxVATID
            // 
            this.txtBxVATID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceReStellerSetting, "VatId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBxVATID.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindingSourceReStellerSetting, "IsVatBerechtigt", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBxVATID.Location = new System.Drawing.Point(95, 229);
            this.txtBxVATID.Name = "txtBxVATID";
            this.validationProviderBillerSettings.SetPerformValidation(this.txtBxVATID, true);
            this.txtBxVATID.Size = new System.Drawing.Size(136, 20);
            this.validationProviderBillerSettings.SetSourcePropertyName(this.txtBxVATID, "VatId");
            this.txtBxVATID.TabIndex = 42;
            // 
            // txtBxEmail
            // 
            this.txtBxEmail.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceReStellerSetting, "Email", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBxEmail.Location = new System.Drawing.Point(93, 176);
            this.txtBxEmail.Name = "txtBxEmail";
            this.validationProviderBillerSettings.SetPerformValidation(this.txtBxEmail, true);
            this.txtBxEmail.Size = new System.Drawing.Size(468, 20);
            this.validationProviderBillerSettings.SetSourcePropertyName(this.txtBxEmail, "Email");
            this.txtBxEmail.TabIndex = 40;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tbxMyGLN);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtBxVStText);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.ckBxVstBerechtigt);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtBxBillerContact);
            this.groupBox1.Controls.Add(this.txtBxStrasse);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cBxMwSt);
            this.groupBox1.Controls.Add(this.txtBxPLZ);
            this.groupBox1.Controls.Add(this.cBxBillerCountry);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtBxOrt);
            this.groupBox1.Controls.Add(this.txtBxVATID);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtBxPhone);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtBxEmail);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(574, 339);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rechnungssteller";
            // 
            // comboBox1
            // 
            this.comboBox1.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.bindingSourceReStellerSetting, "CurrSelected", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.comboBox1.DataSource = this.currencyListBindingSource;
            this.comboBox1.DisplayMember = "DisplayText";
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(344, 202);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(66, 21);
            this.comboBox1.TabIndex = 55;
            // 
            // currencyListBindingSource
            // 
            this.currencyListBindingSource.DataMember = "CurrencyList";
            this.currencyListBindingSource.DataSource = this.bindingSourceReStellerSetting;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(259, 205);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 54;
            this.label9.Text = "Währung";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(259, 232);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 52;
            this.label8.Text = "Meine GLN";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Name";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(9, 285);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(268, 13);
            this.label22.TabIndex = 50;
            this.label22.Text = "Text wenn keine Berechtigung zum VSt. Abzug besteht";
            // 
            // ckBxVstBerechtigt
            // 
            this.ckBxVstBerechtigt.AutoSize = true;
            this.ckBxVstBerechtigt.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckBxVstBerechtigt.Checked = true;
            this.ckBxVstBerechtigt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckBxVstBerechtigt.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSourceReStellerSetting, "IsVatBerechtigt", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ckBxVstBerechtigt.Location = new System.Drawing.Point(9, 255);
            this.ckBxVstBerechtigt.Name = "ckBxVstBerechtigt";
            this.ckBxVstBerechtigt.Size = new System.Drawing.Size(159, 17);
            this.ckBxVstBerechtigt.TabIndex = 49;
            this.ckBxVstBerechtigt.Text = "Berechtigung z. VSt Abzug  ";
            this.ckBxVstBerechtigt.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Straße";
            // 
            // txtBxBillerContact
            // 
            this.txtBxBillerContact.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceReStellerSetting, "Kontakt", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBxBillerContact.Location = new System.Drawing.Point(94, 150);
            this.txtBxBillerContact.Name = "txtBxBillerContact";
            this.txtBxBillerContact.Size = new System.Drawing.Size(468, 20);
            this.txtBxBillerContact.TabIndex = 38;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 153);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(44, 13);
            this.label16.TabIndex = 48;
            this.label16.Text = "Kontakt";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "PLZ";
            // 
            // cBxMwSt
            // 
            this.cBxMwSt.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.bindingSourceReStellerSetting, "VatSelected", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cBxMwSt.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindingSourceReStellerSetting, "IsVatBerechtigt", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cBxMwSt.DataSource = this.vatDefaultListBindingSource;
            this.cBxMwSt.DisplayMember = "MwStSatz";
            this.cBxMwSt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBxMwSt.FormattingEnabled = true;
            this.cBxMwSt.Location = new System.Drawing.Point(94, 202);
            this.cBxMwSt.Name = "cBxMwSt";
            this.cBxMwSt.Size = new System.Drawing.Size(90, 21);
            this.cBxMwSt.TabIndex = 43;
            // 
            // cBxBillerCountry
            // 
            this.cBxBillerCountry.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.bindingSourceReStellerSetting, "CountryCodeSelected", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cBxBillerCountry.DataSource = this.countryCodesBindingSource;
            this.cBxBillerCountry.DisplayMember = "Country";
            this.cBxBillerCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBxBillerCountry.FormattingEnabled = true;
            this.cBxBillerCountry.Location = new System.Drawing.Point(95, 97);
            this.cBxBillerCountry.Name = "cBxBillerCountry";
            this.cBxBillerCountry.Size = new System.Drawing.Size(228, 21);
            this.cBxBillerCountry.TabIndex = 36;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(146, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "Ort";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 205);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(34, 13);
            this.label15.TabIndex = 47;
            this.label15.Text = "MwSt";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 41;
            this.label5.Text = "Telefon";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 232);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 13);
            this.label13.TabIndex = 46;
            this.label13.Text = "USt-ID";
            // 
            // txtBxPhone
            // 
            this.txtBxPhone.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceReStellerSetting, "Phone", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBxPhone.Location = new System.Drawing.Point(94, 124);
            this.txtBxPhone.Name = "txtBxPhone";
            this.txtBxPhone.Size = new System.Drawing.Size(136, 20);
            this.txtBxPhone.TabIndex = 37;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 97);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 45;
            this.label11.Text = "Land";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 179);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 44;
            this.label6.Text = "Email";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txBxBIC);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.txtIBAN);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.txtBxKtoWortlaut);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtBxBank);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(12, 366);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(574, 111);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kontoinformation";
            // 
            // txBxBIC
            // 
            this.txBxBIC.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceReStellerSetting, "Bic", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txBxBIC.Location = new System.Drawing.Point(357, 80);
            this.txBxBIC.Name = "txBxBIC";
            this.txBxBIC.Size = new System.Drawing.Size(180, 20);
            this.txBxBIC.TabIndex = 13;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(326, 83);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(24, 13);
            this.label20.TabIndex = 12;
            this.label20.Text = "BIC";
            // 
            // txtIBAN
            // 
            this.txtIBAN.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceReStellerSetting, "Iban", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtIBAN.Location = new System.Drawing.Point(98, 78);
            this.txtIBAN.Name = "txtIBAN";
            this.txtIBAN.Size = new System.Drawing.Size(224, 20);
            this.txtIBAN.TabIndex = 6;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 83);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "IBAN";
            // 
            // txtBxKtoWortlaut
            // 
            this.txtBxKtoWortlaut.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceReStellerSetting, "Inhaber", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBxKtoWortlaut.Location = new System.Drawing.Point(98, 52);
            this.txtBxKtoWortlaut.Name = "txtBxKtoWortlaut";
            this.txtBxKtoWortlaut.Size = new System.Drawing.Size(440, 20);
            this.txtBxKtoWortlaut.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Kontoinhaber";
            // 
            // txtBxBank
            // 
            this.txtBxBank.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceReStellerSetting, "Bank", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtBxBank.Location = new System.Drawing.Point(98, 25);
            this.txtBxBank.Name = "txtBxBank";
            this.txtBxBank.Size = new System.Drawing.Size(440, 20);
            this.txtBxBank.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Bank";
            // 
            // commandButton1
            // 
            this.commandButton1.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.bindingSourceReStellerSetting, "ClearCommand", true));
            this.commandButton1.Location = new System.Drawing.Point(341, 495);
            this.commandButton1.Name = "commandButton1";
            this.commandButton1.Size = new System.Drawing.Size(83, 23);
            this.commandButton1.TabIndex = 34;
            this.commandButton1.Text = "Zurücksetzen";
            this.commandButton1.UseVisualStyleBackColor = true;
            // 
            // FrmBillerSettingsView
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(603, 529);
            this.Controls.Add(this.commandButton1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnCancel);
            this.Name = "FrmBillerSettingsView";
            this.Text = "Einstellungen - Rechnungssteller";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceReStellerSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryCodesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vatDefaultListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currencyListBindingSource)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox checkBox1;
        private WinformsMvvm.Controls.CommandButton btnSave;
        private System.Windows.Forms.BindingSource bindingSourceReStellerSetting;
        private System.Windows.Forms.BindingSource countryCodesBindingSource;
        private System.Windows.Forms.BindingSource vatDefaultListBindingSource;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider validationProviderBillerSettings;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txBxBIC;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtIBAN;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtBxKtoWortlaut;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtBxBank;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbxMyGLN;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBxVStText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckBox ckBxVstBerechtigt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBxBillerContact;
        private System.Windows.Forms.TextBox txtBxStrasse;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cBxMwSt;
        private System.Windows.Forms.MaskedTextBox txtBxPLZ;
        private System.Windows.Forms.ComboBox cBxBillerCountry;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtBxOrt;
        private System.Windows.Forms.TextBox txtBxVATID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtBxPhone;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBxEmail;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.BindingSource currencyListBindingSource;
        private System.Windows.Forms.ComboBox comboBox1;
        private WinformsMvvm.Controls.CommandButton commandButton1;

    }
}