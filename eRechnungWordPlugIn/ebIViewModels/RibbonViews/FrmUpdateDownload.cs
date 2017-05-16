using ebIViewModels.RibbonViewModels;
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

namespace ebIViewModels.RibbonViews
{
    public partial class FrmUpdateDownload : FormService
    {
        public FrmUpdateDownload(DownloadViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            downloadViewModelBindingSource.DataSource = ViewModel;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((DownloadViewModel)ViewModel).DownloadCommand.Execute(null);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((DownloadViewModel)ViewModel).ShowAllCommand.Execute(null);
        }
    }
}
