﻿using ebIViewModels.ErrorView;

namespace eRechnung.Views
{
    [System.ComponentModel.ToolboxItemAttribute(false)]
    partial class ErrorActionsPaneControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.allesAuswählenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kopierenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._bscErrorActionPaneViewModel = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblNewVersionAvailable = new System.Windows.Forms.Label();
            this.commandButton1 = new WinFormsMvvm.Controls.CommandButton();
            this.errorViewModelBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.fieldNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._bscErrorActionPaneViewModel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorViewModelBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allesAuswählenToolStripMenuItem,
            this.kopierenToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(201, 48);
            // 
            // allesAuswählenToolStripMenuItem
            // 
            this.allesAuswählenToolStripMenuItem.Name = "allesAuswählenToolStripMenuItem";
            this.allesAuswählenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.allesAuswählenToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.allesAuswählenToolStripMenuItem.Text = "Alles auswählen";
            this.allesAuswählenToolStripMenuItem.Click += new System.EventHandler(this.allesAuswählenToolStripMenuItem_Click);
            // 
            // kopierenToolStripMenuItem
            // 
            this.kopierenToolStripMenuItem.Name = "kopierenToolStripMenuItem";
            this.kopierenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.kopierenToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.kopierenToolStripMenuItem.Text = "Kopieren";
            this.kopierenToolStripMenuItem.Click += new System.EventHandler(this.kopierenToolStripMenuItem_Click);
            // 
            // _bscErrorActionPaneViewModel
            // 
            this._bscErrorActionPaneViewModel.DataMember = "ErrorList";
            this._bscErrorActionPaneViewModel.DataSource = this.errorViewModelBindingSource1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hinweise und Fehler";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.errorViewModelBindingSource1, "Message", true));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(3, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 51);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1\r\noder zwei\r\noder drei";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(320, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(94, 23);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Liste löschen";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fieldNameDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn});
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView1, 2);
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.DataSource = this._bscErrorActionPaneViewModel;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 124);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(411, 473);
            this.dataGridView1.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnClear, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(417, 600);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.commandButton1);
            this.panel1.Controls.Add(this.lblNewVersionAvailable);
            this.panel1.DataBindings.Add(new System.Windows.Forms.Binding("Visible", this.errorViewModelBindingSource1, "IsNewReleaseAvailable", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.panel1.Location = new System.Drawing.Point(3, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(411, 35);
            this.panel1.TabIndex = 9;
            // 
            // lblNewVersionAvailable
            // 
            this.lblNewVersionAvailable.AutoSize = true;
            this.lblNewVersionAvailable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewVersionAvailable.ForeColor = System.Drawing.Color.Red;
            this.lblNewVersionAvailable.Location = new System.Drawing.Point(3, 9);
            this.lblNewVersionAvailable.Name = "lblNewVersionAvailable";
            this.lblNewVersionAvailable.Size = new System.Drawing.Size(189, 13);
            this.lblNewVersionAvailable.TabIndex = 16;
            this.lblNewVersionAvailable.Text = "Eine neue Version ist verfügbar:";
            // 
            // commandButton1
            // 
            this.commandButton1.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.errorViewModelBindingSource1, "DownloadCommand", true));
            this.commandButton1.Location = new System.Drawing.Point(198, 4);
            this.commandButton1.Name = "commandButton1";
            this.commandButton1.Size = new System.Drawing.Size(75, 23);
            this.commandButton1.TabIndex = 17;
            this.commandButton1.Text = "Download";
            this.commandButton1.UseVisualStyleBackColor = true;
            // 
            // errorViewModelBindingSource1
            // 
            this.errorViewModelBindingSource1.DataSource = typeof(ebIViewModels.ErrorView.ErrorActionPaneViewModel);
            // 
            // fieldNameDataGridViewTextBoxColumn
            // 
            this.fieldNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.fieldNameDataGridViewTextBoxColumn.DataPropertyName = "FieldName";
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.fieldNameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.fieldNameDataGridViewTextBoxColumn.HeaderText = "";
            this.fieldNameDataGridViewTextBoxColumn.Name = "fieldNameDataGridViewTextBoxColumn";
            this.fieldNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fieldNameDataGridViewTextBoxColumn.Width = 19;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.descriptionDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.descriptionDataGridViewTextBoxColumn.FillWeight = 90F;
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Beschreibung";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ErrorActionsPaneControl
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ErrorActionsPaneControl";
            this.Size = new System.Drawing.Size(417, 600);
            this.Load += new System.EventHandler(this.ErrorActionsPaneControl_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._bscErrorActionPaneViewModel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorViewModelBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource errorViewModelBindingSource1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource _bscErrorActionPaneViewModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem kopierenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allesAuswählenToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNewVersionAvailable;
        private WinFormsMvvm.Controls.CommandButton commandButton1;
    }
}
