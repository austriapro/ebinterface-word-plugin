namespace ebIViewModels.Views
{
    partial class FrmSkontoList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.skontoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alleLöschenToolStripMenuItem = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.bindSrcSkonto = new System.Windows.Forms.BindingSource(this.components);
            this.speichernUndSchliessenToolStripMenuItem = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.schliessenToolStripMenuItem = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.bearbeitenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.einfügenToolStripMenuItem = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.editToolStripCommandMenuItem = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.löschenToolStripMenuItem = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.einfügenToolStripMenuItem1 = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.ändernToolStripMenuItem = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.löschenToolStripMenuItem1 = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxSkontoTage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.invoiceDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.skontoFaelligDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skontoTageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skontoProzentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skontoBetragDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skontoListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.validationProvider1 = new Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindSrcSkonto)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.skontoListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.skontoToolStripMenuItem,
            this.bearbeitenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(537, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // skontoToolStripMenuItem
            // 
            this.skontoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alleLöschenToolStripMenuItem,
            this.speichernUndSchliessenToolStripMenuItem,
            this.schliessenToolStripMenuItem});
            this.skontoToolStripMenuItem.Name = "skontoToolStripMenuItem";
            this.skontoToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.skontoToolStripMenuItem.Text = "Skonto";
            // 
            // alleLöschenToolStripMenuItem
            // 
            this.alleLöschenToolStripMenuItem.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.bindSrcSkonto, "ClearCommand", true));
            this.alleLöschenToolStripMenuItem.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindSrcSkonto, "HasSkontoElements", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.alleLöschenToolStripMenuItem.Name = "alleLöschenToolStripMenuItem";
            this.alleLöschenToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.alleLöschenToolStripMenuItem.Text = "Alle löschen";
            // 
            // bindSrcSkonto
            // 
            this.bindSrcSkonto.DataSource = typeof(ebIViewModels.ViewModels.SkontoViewModels);
            // 
            // speichernUndSchliessenToolStripMenuItem
            // 
            this.speichernUndSchliessenToolStripMenuItem.Name = "speichernUndSchliessenToolStripMenuItem";
            this.speichernUndSchliessenToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.speichernUndSchliessenToolStripMenuItem.Text = "Speichern und Schliessen";
            this.speichernUndSchliessenToolStripMenuItem.Click += new System.EventHandler(this.speichernUndSchliessenToolStripMenuItem_Click);
            // 
            // schliessenToolStripMenuItem
            // 
            this.schliessenToolStripMenuItem.Name = "schliessenToolStripMenuItem";
            this.schliessenToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.schliessenToolStripMenuItem.Text = "Schliessen";
            this.schliessenToolStripMenuItem.Click += new System.EventHandler(this.schliessenToolStripMenuItem_Click);
            // 
            // bearbeitenToolStripMenuItem
            // 
            this.bearbeitenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.einfügenToolStripMenuItem,
            this.editToolStripCommandMenuItem,
            this.löschenToolStripMenuItem});
            this.bearbeitenToolStripMenuItem.Name = "bearbeitenToolStripMenuItem";
            this.bearbeitenToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.bearbeitenToolStripMenuItem.Text = "Bearbeiten";
            // 
            // einfügenToolStripMenuItem
            // 
            this.einfügenToolStripMenuItem.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.bindSrcSkonto, "InsertCommand", true));
            this.einfügenToolStripMenuItem.Name = "einfügenToolStripMenuItem";
            this.einfügenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.einfügenToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.einfügenToolStripMenuItem.Text = "Einfügen";
            // 
            // editToolStripCommandMenuItem
            // 
            this.editToolStripCommandMenuItem.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindSrcSkonto, "HasSkontoElements", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.editToolStripCommandMenuItem.Name = "editToolStripCommandMenuItem";
            this.editToolStripCommandMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.editToolStripCommandMenuItem.Size = new System.Drawing.Size(155, 22);
            this.editToolStripCommandMenuItem.Text = "Ändern";
            this.editToolStripCommandMenuItem.Click += new System.EventHandler(this.editToolStripCommandMenuItem_Click);
            // 
            // löschenToolStripMenuItem
            // 
            this.löschenToolStripMenuItem.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindSrcSkonto, "HasSkontoElements", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.löschenToolStripMenuItem.Name = "löschenToolStripMenuItem";
            this.löschenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.löschenToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.löschenToolStripMenuItem.Text = "Löschen";
            this.löschenToolStripMenuItem.Click += new System.EventHandler(this.löschenToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.einfügenToolStripMenuItem1,
            this.ändernToolStripMenuItem,
            this.löschenToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(156, 70);
            // 
            // einfügenToolStripMenuItem1
            // 
            this.einfügenToolStripMenuItem1.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.bindSrcSkonto, "InsertCommand", true));
            this.einfügenToolStripMenuItem1.Name = "einfügenToolStripMenuItem1";
            this.einfügenToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.einfügenToolStripMenuItem1.Size = new System.Drawing.Size(155, 22);
            this.einfügenToolStripMenuItem1.Text = "Einfügen";
            // 
            // ändernToolStripMenuItem
            // 
            this.ändernToolStripMenuItem.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindSrcSkonto, "HasSkontoElements", true));
            this.ändernToolStripMenuItem.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindSrcSkonto, "HasSkontoElements", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ändernToolStripMenuItem.Name = "ändernToolStripMenuItem";
            this.ändernToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.ändernToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.ändernToolStripMenuItem.Text = "Ändern";
            this.ändernToolStripMenuItem.Click += new System.EventHandler(this.editToolStripCommandMenuItem_Click);
            // 
            // löschenToolStripMenuItem1
            // 
            this.löschenToolStripMenuItem1.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindSrcSkonto, "HasSkontoElements", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.löschenToolStripMenuItem1.Name = "löschenToolStripMenuItem1";
            this.löschenToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.löschenToolStripMenuItem1.Size = new System.Drawing.Size(155, 22);
            this.löschenToolStripMenuItem1.Text = "Löschen";
            this.löschenToolStripMenuItem1.Click += new System.EventHandler(this.löschenToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBox2);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.tbxSkontoTage);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.dateTimePicker2);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.invoiceDateTimePicker);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(537, 237);
            this.splitContainer1.SplitterDistance = 129;
            this.splitContainer1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindSrcSkonto, "BaseAmount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "N2"));
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(15, 191);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 7;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Basisbetrag";
            // 
            // tbxSkontoTage
            // 
            this.tbxSkontoTage.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindSrcSkonto, "InvoiceDueDays", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "N0"));
            this.tbxSkontoTage.Location = new System.Drawing.Point(12, 137);
            this.tbxSkontoTage.Name = "tbxSkontoTage";
            this.validationProvider1.SetPerformValidation(this.tbxSkontoTage, true);
            this.tbxSkontoTage.Size = new System.Drawing.Size(47, 20);
            this.validationProvider1.SetSourcePropertyName(this.tbxSkontoTage, "InvoiceDueDays");
            this.tbxSkontoTage.TabIndex = 5;
            this.tbxSkontoTage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tage";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindSrcSkonto, "InvoiceDueDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "d"));
            this.dateTimePicker2.DataBindings.Add(new System.Windows.Forms.Binding("MinDate", this.bindSrcSkonto, "InvoiceDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(12, 85);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.validationProvider1.SetPerformValidation(this.dateTimePicker2, true);
            this.dateTimePicker2.Size = new System.Drawing.Size(103, 20);
            this.validationProvider1.SetSourcePropertyName(this.dateTimePicker2, "InvoiceDueDate");
            this.dateTimePicker2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fälligkeitsdatum";
            // 
            // invoiceDateTimePicker
            // 
            this.invoiceDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.invoiceDateTimePicker.Location = new System.Drawing.Point(12, 30);
            this.invoiceDateTimePicker.Name = "invoiceDateTimePicker";
            this.validationProvider1.SetPerformValidation(this.invoiceDateTimePicker, true);
            this.invoiceDateTimePicker.Size = new System.Drawing.Size(101, 20);
            this.validationProvider1.SetSourcePropertyName(this.invoiceDateTimePicker, "InvoiceDate");
            this.invoiceDateTimePicker.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rechnungsdatum";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.skontoFaelligDateDataGridViewTextBoxColumn,
            this.skontoTageDataGridViewTextBoxColumn,
            this.skontoProzentDataGridViewTextBoxColumn,
            this.skontoBetragDataGridViewTextBoxColumn});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.DataSource = this.skontoListBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(404, 237);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.editToolStripCommandMenuItem_Click);
            // 
            // skontoFaelligDateDataGridViewTextBoxColumn
            // 
            this.skontoFaelligDateDataGridViewTextBoxColumn.DataPropertyName = "SkontoFaelligDate";
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.skontoFaelligDateDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.skontoFaelligDateDataGridViewTextBoxColumn.HeaderText = "Fällig";
            this.skontoFaelligDateDataGridViewTextBoxColumn.Name = "skontoFaelligDateDataGridViewTextBoxColumn";
            this.skontoFaelligDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // skontoTageDataGridViewTextBoxColumn
            // 
            this.skontoTageDataGridViewTextBoxColumn.DataPropertyName = "SkontoTage";
            dataGridViewCellStyle2.Format = "N0";
            this.skontoTageDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.skontoTageDataGridViewTextBoxColumn.HeaderText = "Tage";
            this.skontoTageDataGridViewTextBoxColumn.Name = "skontoTageDataGridViewTextBoxColumn";
            this.skontoTageDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // skontoProzentDataGridViewTextBoxColumn
            // 
            this.skontoProzentDataGridViewTextBoxColumn.DataPropertyName = "SkontoProzent";
            dataGridViewCellStyle3.Format = "N2";
            this.skontoProzentDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.skontoProzentDataGridViewTextBoxColumn.HeaderText = "Prozent";
            this.skontoProzentDataGridViewTextBoxColumn.Name = "skontoProzentDataGridViewTextBoxColumn";
            this.skontoProzentDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // skontoBetragDataGridViewTextBoxColumn
            // 
            this.skontoBetragDataGridViewTextBoxColumn.DataPropertyName = "SkontoBetrag";
            dataGridViewCellStyle4.Format = "N2";
            this.skontoBetragDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.skontoBetragDataGridViewTextBoxColumn.HeaderText = "Betrag";
            this.skontoBetragDataGridViewTextBoxColumn.Name = "skontoBetragDataGridViewTextBoxColumn";
            this.skontoBetragDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // skontoListBindingSource
            // 
            this.skontoListBindingSource.DataMember = "SkontoList";
            this.skontoListBindingSource.DataSource = this.bindSrcSkonto;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.DataSource = this.bindSrcSkonto;
            // 
            // validationProvider1
            // 
            this.validationProvider1.ErrorProvider = this.errorProvider1;
            this.validationProvider1.RulesetName = "";
            this.validationProvider1.SourceTypeName = "ebIViewModels.ViewModels.SkontoViewModels, ebIViewModels";
            // 
            // FrmSkontoList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 261);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmSkontoList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Skonto Zahlungsbedingungen";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindSrcSkonto)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.skontoListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem skontoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bearbeitenToolStripMenuItem;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem einfügenToolStripMenuItem;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem alleLöschenToolStripMenuItem;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem speichernUndSchliessenToolStripMenuItem;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem schliessenToolStripMenuItem;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem editToolStripCommandMenuItem;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem löschenToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.BindingSource bindSrcSkonto;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker invoiceDateTimePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxSkontoTage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource skontoListBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn skontoFaelligDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn skontoTageDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn skontoProzentDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn skontoBetragDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem ändernToolStripMenuItem;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem löschenToolStripMenuItem1;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem einfügenToolStripMenuItem1;
        private Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider validationProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}