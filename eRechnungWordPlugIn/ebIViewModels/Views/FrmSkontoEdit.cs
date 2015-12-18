using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ebIViewModels.ViewModels;
using ExtensionMethods;
using WinFormsMvvm.DialogService;
using WinFormsMvvm;

namespace ebIViewModels.Views
{
    public partial class FrmSkontoEdit : FormService
    {
        // private Control _currentCtrl = null;
        public FrmSkontoEdit(SkontoViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
            Binding b = new System.Windows.Forms.Binding("Value", this.bindSkontoEdit, "InvoiceDate", true,
                System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "d");
            dateTimePicker1.DataBindings.Add(b);
            dateTimePicker2.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindSkontoEdit,
                "InvoiceDueDate", true,
                System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "d"));
            dateTimePicker3.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindSkontoEdit,
                "SkontoFaelligDate", true,
                System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "d"));
            Binding prz = new System.Windows.Forms.Binding("Text", this.bindSkontoEdit, "SkontoProzent", true,
                System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "#.00");

            tBxProzent.DataBindings.Add(prz);
            bindSkontoEdit.DataSource = ViewModel;
            Bindings.Add(bindSkontoEdit);
            string ruleSet = ((SkontoViewModel)ViewModel).RuleSet;
            validationProvider1.RulesetName = ruleSet;

        }

        public override void SetBindingSource(object bindSrc)
        {
            var viewModel = bindSrc as SkontoViewModel;
            ViewModel = viewModel;
            bindSkontoEdit.DataSource = ViewModel as SkontoViewModel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // WriteAllValues();
            bindSkontoEdit.EndEdit();
            this.ValidateChildren();

            if (validationProvider1.IsValid)
            {
                DialogResult = DialogResult.OK;
                ((ViewModelBase)ViewModel).ChangePending = false;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
