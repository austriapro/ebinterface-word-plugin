using ebIViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsMvvm.DialogService;

namespace ebIViewModels.Views
{
    public partial class FrmUpdateInvoiceView : FormService
    {
        public FrmUpdateInvoiceView(UpdateInvoiceViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            bindingSource1.DataSource = ViewModel;
        }
    }
}
