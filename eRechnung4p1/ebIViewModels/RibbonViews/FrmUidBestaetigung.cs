using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ebIViewModels.RibbonViewModels;
using WinFormsMvvm.DialogService;

namespace ebIViewModels.RibbonViews
{
    public partial class FrmUidBestaetigung : FormService
    {
        public FrmUidBestaetigung(UidBestaetigungViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            bSrcUidQuery.DataSource = ViewModel;
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            this.ValidateChildren();
            if (!validationProvider1.IsValid)
            {
                return;
            }
            Close();
        }
    }
}
