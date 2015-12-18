namespace ebIViewModels.Views
{
    partial class FrmDetailsList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.detailpositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearToolStripCommandButton = new WinFormsMvvm.Controls.ToolStripCommandButton();
            this.SaveTtoolStripCommandMenuItem = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.schliessenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bearbeitenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hinzufügenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ändernToolStripMenuItem = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.löschenToolStripMenuItem = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.dGVDetails = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.einfügenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ändernToolStripMenuItem1 = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.löschenToolStripMenuItem1 = new WinFormsMvvm.Controls.ToolStripCommandMenuItem();
            this.detailsViewListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindSrcDetailsList = new System.Windows.Forms.BindingSource(this.components);
            this.bestellBezugDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.artikelNrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bezeichnungDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mengeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.einheitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.einzelPreisDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabattDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vatSatzDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nettoBetragZeileDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVDetails)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detailsViewListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindSrcDetailsList)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detailpositionToolStripMenuItem,
            this.bearbeitenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(934, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // detailpositionToolStripMenuItem
            // 
            this.detailpositionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearToolStripCommandButton,
            this.SaveTtoolStripCommandMenuItem,
            this.schliessenToolStripMenuItem});
            this.detailpositionToolStripMenuItem.Name = "detailpositionToolStripMenuItem";
            this.detailpositionToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.detailpositionToolStripMenuItem.Text = "Detailposition";
            // 
            // ClearToolStripCommandButton
            // 
            this.ClearToolStripCommandButton.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.bindSrcDetailsList, "ClearCommand", true));
            this.ClearToolStripCommandButton.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindSrcDetailsList, "IsEditable", true));
            this.ClearToolStripCommandButton.Name = "ClearToolStripCommandButton";
            this.ClearToolStripCommandButton.Size = new System.Drawing.Size(75, 19);
            this.ClearToolStripCommandButton.Text = "Alle löschen";
            // 
            // SaveTtoolStripCommandMenuItem
            // 
            this.SaveTtoolStripCommandMenuItem.Name = "SaveTtoolStripCommandMenuItem";
            this.SaveTtoolStripCommandMenuItem.Size = new System.Drawing.Size(207, 22);
            this.SaveTtoolStripCommandMenuItem.Text = "Speichern und Schliessen";
            this.SaveTtoolStripCommandMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // schliessenToolStripMenuItem
            // 
            this.schliessenToolStripMenuItem.Name = "schliessenToolStripMenuItem";
            this.schliessenToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.schliessenToolStripMenuItem.Text = "Schliessen";
            this.schliessenToolStripMenuItem.ToolTipText = "Schliesst das Fenster";
            this.schliessenToolStripMenuItem.Click += new System.EventHandler(this.schliessenToolStripMenuItem_Click);
            // 
            // bearbeitenToolStripMenuItem
            // 
            this.bearbeitenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hinzufügenToolStripMenuItem,
            this.ändernToolStripMenuItem,
            this.löschenToolStripMenuItem});
            this.bearbeitenToolStripMenuItem.Name = "bearbeitenToolStripMenuItem";
            this.bearbeitenToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.bearbeitenToolStripMenuItem.Text = "Bearbeiten";
            // 
            // hinzufügenToolStripMenuItem
            // 
            this.hinzufügenToolStripMenuItem.Name = "hinzufügenToolStripMenuItem";
            this.hinzufügenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.hinzufügenToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.hinzufügenToolStripMenuItem.Text = "Einfügen";
            this.hinzufügenToolStripMenuItem.Click += new System.EventHandler(this.hinzufügenToolStripMenuItem_Click);
            // 
            // ändernToolStripMenuItem
            // 
            this.ändernToolStripMenuItem.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindSrcDetailsList, "IsEditable", true));
            this.ändernToolStripMenuItem.Name = "ändernToolStripMenuItem";
            this.ändernToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.ändernToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.ändernToolStripMenuItem.Text = "Ändern";
            this.ändernToolStripMenuItem.Click += new System.EventHandler(this.ändernToolStripMenuItem_Click);
            // 
            // löschenToolStripMenuItem
            // 
            this.löschenToolStripMenuItem.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindSrcDetailsList, "IsEditable", true));
            this.löschenToolStripMenuItem.Name = "löschenToolStripMenuItem";
            this.löschenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.löschenToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.löschenToolStripMenuItem.Text = "Löschen";
            this.löschenToolStripMenuItem.Click += new System.EventHandler(this.löschenToolStripMenuItem_Click);
            // 
            // dGVDetails
            // 
            this.dGVDetails.AllowUserToAddRows = false;
            this.dGVDetails.AllowUserToDeleteRows = false;
            this.dGVDetails.AutoGenerateColumns = false;
            this.dGVDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dGVDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.bestellBezugDataGridViewTextBoxColumn,
            this.artikelNrDataGridViewTextBoxColumn,
            this.bezeichnungDataGridViewTextBoxColumn,
            this.mengeDataGridViewTextBoxColumn,
            this.einheitDataGridViewTextBoxColumn,
            this.einzelPreisDataGridViewTextBoxColumn,
            this.rabattDataGridViewTextBoxColumn,
            this.vatSatzDataGridViewTextBoxColumn,
            this.nettoBetragZeileDataGridViewTextBoxColumn});
            this.dGVDetails.ContextMenuStrip = this.contextMenuStrip1;
            this.dGVDetails.DataSource = this.detailsViewListBindingSource;
            this.dGVDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGVDetails.Location = new System.Drawing.Point(0, 24);
            this.dGVDetails.MultiSelect = false;
            this.dGVDetails.Name = "dGVDetails";
            this.dGVDetails.ReadOnly = true;
            this.dGVDetails.RowHeadersVisible = false;
            this.dGVDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dGVDetails.Size = new System.Drawing.Size(934, 392);
            this.dGVDetails.TabIndex = 6;
            this.dGVDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGVDetails_CellDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.einfügenToolStripMenuItem,
            this.ändernToolStripMenuItem1,
            this.löschenToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(156, 70);
            // 
            // einfügenToolStripMenuItem
            // 
            this.einfügenToolStripMenuItem.Name = "einfügenToolStripMenuItem";
            this.einfügenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.einfügenToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.einfügenToolStripMenuItem.Text = "Einfügen";
            this.einfügenToolStripMenuItem.Click += new System.EventHandler(this.hinzufügenToolStripMenuItem_Click);
            // 
            // ändernToolStripMenuItem1
            // 
            this.ändernToolStripMenuItem1.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindSrcDetailsList, "IsEditable", true));
            this.ändernToolStripMenuItem1.Name = "ändernToolStripMenuItem1";
            this.ändernToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.ändernToolStripMenuItem1.Size = new System.Drawing.Size(155, 22);
            this.ändernToolStripMenuItem1.Text = "Ändern";
            this.ändernToolStripMenuItem1.Click += new System.EventHandler(this.ändernToolStripMenuItem_Click);
            // 
            // löschenToolStripMenuItem1
            // 
            this.löschenToolStripMenuItem1.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindSrcDetailsList, "IsEditable", true));
            this.löschenToolStripMenuItem1.Name = "löschenToolStripMenuItem1";
            this.löschenToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.löschenToolStripMenuItem1.Size = new System.Drawing.Size(155, 22);
            this.löschenToolStripMenuItem1.Text = "Löschen";
            this.löschenToolStripMenuItem1.Click += new System.EventHandler(this.löschenToolStripMenuItem_Click);
            // 
            // detailsViewListBindingSource
            // 
            this.detailsViewListBindingSource.DataMember = "DetailsViewList";
            this.detailsViewListBindingSource.DataSource = this.bindSrcDetailsList;
            // 
            // bindSrcDetailsList
            // 
            this.bindSrcDetailsList.DataSource = typeof(ebIViewModels.ViewModels.DetailsViewModels);
            // 
            // bestellBezugDataGridViewTextBoxColumn
            // 
            this.bestellBezugDataGridViewTextBoxColumn.DataPropertyName = "BestellBezug";
            this.bestellBezugDataGridViewTextBoxColumn.HeaderText = "Bestell-Pos.";
            this.bestellBezugDataGridViewTextBoxColumn.Name = "bestellBezugDataGridViewTextBoxColumn";
            this.bestellBezugDataGridViewTextBoxColumn.ReadOnly = true;
            this.bestellBezugDataGridViewTextBoxColumn.Width = 87;
            // 
            // artikelNrDataGridViewTextBoxColumn
            // 
            this.artikelNrDataGridViewTextBoxColumn.DataPropertyName = "ArtikelNr";
            this.artikelNrDataGridViewTextBoxColumn.HeaderText = "Artikel Nr.";
            this.artikelNrDataGridViewTextBoxColumn.Name = "artikelNrDataGridViewTextBoxColumn";
            this.artikelNrDataGridViewTextBoxColumn.ReadOnly = true;
            this.artikelNrDataGridViewTextBoxColumn.Width = 78;
            // 
            // bezeichnungDataGridViewTextBoxColumn
            // 
            this.bezeichnungDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.bezeichnungDataGridViewTextBoxColumn.DataPropertyName = "Bezeichnung";
            this.bezeichnungDataGridViewTextBoxColumn.HeaderText = "Bezeichnung";
            this.bezeichnungDataGridViewTextBoxColumn.Name = "bezeichnungDataGridViewTextBoxColumn";
            this.bezeichnungDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mengeDataGridViewTextBoxColumn
            // 
            this.mengeDataGridViewTextBoxColumn.DataPropertyName = "Menge";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N4";
            dataGridViewCellStyle1.NullValue = null;
            this.mengeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.mengeDataGridViewTextBoxColumn.HeaderText = "Menge";
            this.mengeDataGridViewTextBoxColumn.Name = "mengeDataGridViewTextBoxColumn";
            this.mengeDataGridViewTextBoxColumn.ReadOnly = true;
            this.mengeDataGridViewTextBoxColumn.Width = 65;
            // 
            // einheitDataGridViewTextBoxColumn
            // 
            this.einheitDataGridViewTextBoxColumn.DataPropertyName = "EinheitDisplay";
            this.einheitDataGridViewTextBoxColumn.HeaderText = "Einheit";
            this.einheitDataGridViewTextBoxColumn.Name = "einheitDataGridViewTextBoxColumn";
            this.einheitDataGridViewTextBoxColumn.ReadOnly = true;
            this.einheitDataGridViewTextBoxColumn.Width = 64;
            // 
            // einzelPreisDataGridViewTextBoxColumn
            // 
            this.einzelPreisDataGridViewTextBoxColumn.DataPropertyName = "EinzelPreis";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N4";
            dataGridViewCellStyle2.NullValue = null;
            this.einzelPreisDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.einzelPreisDataGridViewTextBoxColumn.HeaderText = "EinzelPreis";
            this.einzelPreisDataGridViewTextBoxColumn.Name = "einzelPreisDataGridViewTextBoxColumn";
            this.einzelPreisDataGridViewTextBoxColumn.ReadOnly = true;
            this.einzelPreisDataGridViewTextBoxColumn.Width = 83;
            // 
            // rabattDataGridViewTextBoxColumn
            // 
            this.rabattDataGridViewTextBoxColumn.DataPropertyName = "Rabatt";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.rabattDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.rabattDataGridViewTextBoxColumn.HeaderText = "Rabatt";
            this.rabattDataGridViewTextBoxColumn.Name = "rabattDataGridViewTextBoxColumn";
            this.rabattDataGridViewTextBoxColumn.ReadOnly = true;
            this.rabattDataGridViewTextBoxColumn.Width = 64;
            // 
            // vatSatzDataGridViewTextBoxColumn
            // 
            this.vatSatzDataGridViewTextBoxColumn.DataPropertyName = "VatSatz";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            this.vatSatzDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.vatSatzDataGridViewTextBoxColumn.HeaderText = "MwSt.%";
            this.vatSatzDataGridViewTextBoxColumn.Name = "vatSatzDataGridViewTextBoxColumn";
            this.vatSatzDataGridViewTextBoxColumn.ReadOnly = true;
            this.vatSatzDataGridViewTextBoxColumn.Width = 70;
            // 
            // nettoBetragZeileDataGridViewTextBoxColumn
            // 
            this.nettoBetragZeileDataGridViewTextBoxColumn.DataPropertyName = "NettoBetragZeile";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            this.nettoBetragZeileDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.nettoBetragZeileDataGridViewTextBoxColumn.HeaderText = "Netto Betrag";
            this.nettoBetragZeileDataGridViewTextBoxColumn.Name = "nettoBetragZeileDataGridViewTextBoxColumn";
            this.nettoBetragZeileDataGridViewTextBoxColumn.ReadOnly = true;
            this.nettoBetragZeileDataGridViewTextBoxColumn.Width = 92;
            // 
            // FrmDetailsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 416);
            this.Controls.Add(this.dGVDetails);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmDetailsList";
            this.Text = "Rechnungspositionen";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVDetails)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.detailsViewListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindSrcDetailsList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem detailpositionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem schliessenToolStripMenuItem;
        private System.Windows.Forms.DataGridView dGVDetails;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem einfügenToolStripMenuItem;
        private WinFormsMvvm.Controls.ToolStripCommandButton ClearToolStripCommandButton;
        private System.Windows.Forms.BindingSource bindSrcDetailsList;
        private System.Windows.Forms.BindingSource detailsViewListBindingSource;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem SaveTtoolStripCommandMenuItem;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem ändernToolStripMenuItem1;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem löschenToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem bearbeitenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hinzufügenToolStripMenuItem;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem ändernToolStripMenuItem;
        private WinFormsMvvm.Controls.ToolStripCommandMenuItem löschenToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn bestellBezugDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn artikelNrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bezeichnungDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mengeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn einheitDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn einzelPreisDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabattDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vatSatzDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nettoBetragZeileDataGridViewTextBoxColumn;
    }
}