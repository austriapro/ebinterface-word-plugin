using System.Windows.Forms;
using ebIViewModels.ErrorView;

namespace eRechnung.Views
{
    public partial class ErrorActionsPaneControl : UserControl
    {
        public ErrorActionPaneViewModel ErrorActionPaneView;
        public ErrorActionsPaneControl(ErrorActionPaneViewModel errorActionPaneView)
        {
            InitializeComponent();
            ErrorActionPaneView = errorActionPaneView;
            errorViewModelBindingSource1.DataSource = ErrorActionPaneView;
            
        }

        private void btnClear_Click(object sender, System.EventArgs e)
        {
            ErrorActionPaneView.ClearCommand.Execute(null);
            
        }
        private void lblNewVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.lblNewVersion.Text);
        }
        private void kopierenToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (this.dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                try
                {
                    // Add the selection to the clipboard.
                    Clipboard.SetDataObject(
                        this.dataGridView1.GetClipboardContent());

                    // Replace the text box contents with the clipboard text.
                }
                catch (System.Runtime.InteropServices.ExternalException)
                {
                    
                }
            }

        }

        private void allesAuswählenToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            dataGridView1.SelectAll();
        }

        private void ErrorActionsPaneControl_Load(object sender, System.EventArgs e)
        {
            this.Dock = DockStyle.Fill;
        }
    }
}
