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
using WinFormsMvvm.DialogService;
using WinFormsMvvm;

namespace ebIViewModels.Views
{
    public partial class FrmSkontoList : FormService
    {
        public FrmSkontoList(SkontoViewModels viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            Binding b = new System.Windows.Forms.Binding("Value", this.bindSrcSkonto, "InvoiceDate", true,
                System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "d");
            invoiceDateTimePicker.DataBindings.Add(b);
            bindSrcSkonto.DataSource = (SkontoViewModels) ViewModel;
        }

 
        //public override void SetBindingSource(object bindSrc)
        //{
        //    var viewModel = bindSrc as SkontoViewModels;
        //    ViewModel = viewModel;
        //    bindSrcSkonto.DataSource = ViewModel;
        //}

        public override void SetBindingSource(object bindSrc)
        {
            var viewModel = bindSrc as SkontoViewModels;
            ViewModel = viewModel;
            bindSrcSkonto.DataSource = ViewModel;
        }

        private void speichernUndSchliessenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bindSrcSkonto.EndEdit();
            this.ValidateChildren();

            if (validationProvider1.IsValid)
            {
                DialogResult = DialogResult.OK;
                ((ViewModelBase)ViewModel).ChangePending = false;
                Close();
            }
        }

        private void schliessenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var index = dataGridView1.CurrentRow.Index;
                ((SkontoViewModels)ViewModel).DeleteCommand.Execute(index);
            }
        }

        private void editToolStripCommandMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var index = dataGridView1.CurrentRow.Index;
                ((SkontoViewModels)ViewModel).EditCommand.Execute(index);
            }

        }
    }
}
