namespace ebIViewModels.Views
{
    partial class FrmShowProgress
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
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.progressViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblProgressText = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.progressViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pgBar
            // 
            this.pgBar.DataBindings.Add(new System.Windows.Forms.Binding("Maximum", this.progressViewModelBindingSource, "Maximum", true));
            this.pgBar.DataBindings.Add(new System.Windows.Forms.Binding("Minimum", this.progressViewModelBindingSource, "Minimum", true));
            this.pgBar.DataBindings.Add(new System.Windows.Forms.Binding("Step", this.progressViewModelBindingSource, "Step", true));
            this.pgBar.DataBindings.Add(new System.Windows.Forms.Binding("Style", this.progressViewModelBindingSource, "Style", true));
            this.pgBar.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.progressViewModelBindingSource, "Value", true));
            this.pgBar.Location = new System.Drawing.Point(12, 33);
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(260, 23);
            this.pgBar.TabIndex = 0;
            // 
            // progressViewModelBindingSource
            // 
            this.progressViewModelBindingSource.DataSource = typeof(ebIViewModels.ViewModels.ProgressViewModel);
            // 
            // lblProgressText
            // 
            this.lblProgressText.AutoSize = true;
            this.lblProgressText.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.progressViewModelBindingSource, "Description", true));
            this.lblProgressText.Location = new System.Drawing.Point(9, 9);
            this.lblProgressText.Name = "lblProgressText";
            this.lblProgressText.Size = new System.Drawing.Size(35, 13);
            this.lblProgressText.TabIndex = 1;
            this.lblProgressText.Text = "label1";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(197, 66);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.progressViewModelBindingSource, "CountTracking", true));
            this.label1.Location = new System.Drawing.Point(10, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // FrmShowProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(284, 101);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblProgressText);
            this.Controls.Add(this.pgBar);
            this.Name = "FrmShowProgress";
            this.Text = "In Arbeit";
            this.Load += new System.EventHandler(this.FrmShowProgress_Load);
            ((System.ComponentModel.ISupportInitialize)(this.progressViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Label lblProgressText;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.BindingSource progressViewModelBindingSource;
        private System.Windows.Forms.Label label1;
    }
}