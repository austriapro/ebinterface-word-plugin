using System;
using System.Windows.Forms;
using ebIViewModels.RibbonViewModels;
using WinFormsMvvm.DialogService;

namespace ebIViewModels.RibbonViews
{
  
    /// <summary>
    /// About Formular
    /// </summary>
    public partial class FrmAboutView : FormService
    {
        public FrmAboutView(AboutViewModel aboutView)
        {

            InitializeComponent();
            ViewModel = aboutView;
            aboutViewModelBindingSource.DataSource = ViewModel;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.linkLabel2.Text);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.linkLabel1.Text);
        }

        private void lblNewVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.lblNewVersion.Text);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int colName = dataGridView1.Columns["LicenseColumn"].Index;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (System.Uri.IsWellFormedUriString(row.Cells[colName].Value.ToString(),UriKind.Absolute))
                {
                    row.Cells[colName] = new DataGridViewLinkCell() { Value = row.Cells[colName].Value };
                    //DataGridViewLinkCell c = row.Cells[colName] as DataGridViewLinkCell;
                }
            }
        }
        // And handle the click too
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewLinkCell)
            {
                System.Diagnostics.Process.Start(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as string);
            }
        }
    }
}
