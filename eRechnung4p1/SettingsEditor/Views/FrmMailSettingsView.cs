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
    public partial class FrmMailSettingsView : FormService
    {
        public FrmMailSettingsView(MailSettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            bindingSource1.DataSource = ViewModel;
        }

        private void cBtnSave_Click(object sender, EventArgs e)
        {
            ValidateChildren();
            if (!validationProvider1.IsValid)
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
            ((ViewModelBase)ViewModel).ChangePending = false;
            Close();
        }

        private void cmdBtnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        //private void cBtnTest_Click(object sender, EventArgs e)
        //{

        //}

    }
}
