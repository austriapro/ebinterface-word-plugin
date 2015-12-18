using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ebIViewModels.ViewModels;
using WinFormsMvvm.DialogService;
using WinFormsMvvm;

namespace ebIViewModels.Views
{
    public partial class FrmDetailsList : FormService
    {
        // public DetailsViewModels DetailsView;
        public FrmDetailsList(DetailsViewModels viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            bindSrcDetailsList.DataSource = ViewModel;
        }

        public override void SetBindingSource(object bindSrc)
        {
            ViewModel = bindSrc as DetailsViewModels;
            bindSrcDetailsList.DataSource = ViewModel;            
        }

        private void schliessenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ändernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dGVDetails.CurrentRow != null)
            {
                var detailIndex = dGVDetails.CurrentRow.Index;
                ((DetailsViewModels)ViewModel).EditCommand.Execute(detailIndex);
            }
        }

        private void hinzufügenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((DetailsViewModels)ViewModel).AddCommand.Execute(null);
        }

        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dGVDetails.CurrentRow != null)
            {
                var detailIndex = dGVDetails.CurrentRow.Index;
                ((DetailsViewModels)ViewModel).DeleteCommand.Execute(detailIndex);
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            ((ViewModelBase)ViewModel).ChangePending = false;
            this.Close();
        }

        private void dGVDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dGVDetails.CurrentRow != null)
            {
                var detailIndex = dGVDetails.CurrentRow.Index;
                ((DetailsViewModels)ViewModel).EditCommand.Execute(detailIndex);
            }

        }
    }
}
