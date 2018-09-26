using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ebIViewModels.ViewModels;
using ExtensionMethods;
using WinFormsMvvm;
using SettingsManager;
using WinFormsMvvm.DialogService;
using LogService;

namespace ebIViewModels.Views
{
    public partial class FrmDetailsEdit : FormService
    {
        public FrmDetailsEdit(DetailsViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            bindingSource1.DataSource = ViewModel as DetailsViewModel;
            detailsViewValidationProvider.RulesetName = viewModel.RuleSet;
            // this.BindingContext[bindingSource1].SuspendTwoWayBinding();
            Bindings.Add(bindingSource1);
        }

        public override void SetBindingSource(object bindSrc)
        {
            var viewModel = bindSrc as DetailsViewModel;
            ViewModel = viewModel;
            bindingSource1.DataSource = ViewModel as DetailsViewModel;
            // cmBxMwSt.SelectedValue = viewModel.VatSatz;
            multiColumnComboBox1.DataSource = ((DetailsViewModel)ViewModel).UoMList;
            multiColumnComboBox2.DataSource = ((DetailsViewModel)ViewModel).VatList;
            // cmBxMwSt.Format += CmBxMwStOnFormat;

        }

        private void cmdBtnSave_Click(object sender, EventArgs e)
        {

            //  var viewModel = ViewModel as DetailsViewModel;
            //  viewModel.SaveCommand.Execute(null);
            // WriteAllValues();
            Log.TraceWrite(CallerInfo.Create(),textBox10.Text + "-" + ((DetailsViewModel)ViewModel).BestellBezug);
            var var2 = this.ValidateChildren();
            Log.TraceWrite(CallerInfo.Create(),textBox10.Text + "-" + ((DetailsViewModel)ViewModel).BestellBezug);
            if (detailsViewValidationProvider.IsValid)
            {
                // this.BindingContext[bindingSource1].UpdateDataBoundObject();
                Log.TraceWrite(CallerInfo.Create(),textBox10.Text + "-" + ((DetailsViewModel)ViewModel).BestellBezug);
                // this.BindingContext[bindingSource1].ResumeTwoWayBinding();
                DialogResult = DialogResult.OK;
                ((ViewModelBase)ViewModel).ChangePending = false;
                Close();
                
            }
        }

    }
}
