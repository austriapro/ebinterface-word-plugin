using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SettingsEditor.ViewModels;
using WinFormsMvvm.DialogService;
using WinFormsMvvm;

namespace SettingsEditor.Views
{
    public partial class FrmSaveLocationView : FormService
    {
        public FrmSaveLocationView(SaveLocationSettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            bndSrcPathSettings.DataSource = ViewModel;
        }

        private void commandButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            ((ViewModelBase)ViewModel).ChangePending = false;
            Close();
        }

        private void cmdBtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
