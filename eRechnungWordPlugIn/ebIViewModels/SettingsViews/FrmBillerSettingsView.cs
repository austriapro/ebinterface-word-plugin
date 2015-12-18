using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ebIViewModels.SettingsViewModels;
using WinFormsMvvm.DialogService;

namespace ebIViewModels.SettingsViews
{
    public partial class FrmBillerSettingsView : FormService
    {
        public FrmBillerSettingsView(BillerSettingsViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = (BillerSettingsViewModel)viewModel;
            bindingSourceReStellerSetting.DataSource = (BillerSettingsViewModel)ViewModel;
            // currListBindingSource.DataSource = ((BillerSettingsViewModel) ViewModel).CurrencyList;
            ((BillerSettingsViewModel)ViewModel).PropertyChanged += OnPropertyChanged;
            ((BillerSettingsViewModel)ViewModel).AnyTextChanged = false;
           // SetTextChangedEvent();
            foreach (GroupBox box in Controls.OfType<GroupBox>())
            {
                SetTextChangedEvent(box);
            }
            ckBxVstBerechtigt.CheckedChanged += tbx_TextChanged;
        }

        private void SetTextChangedEvent(GroupBox grp)
        {
            foreach (TextBox control in grp.Controls.OfType<TextBox>())
            {
                TextBox tbx = control;
                tbx.TextChanged += tbx_TextChanged;
            }
            foreach (ComboBox control in grp.Controls.OfType<ComboBox>())
            {
                ComboBox cbx = control;
                cbx.SelectedIndexChanged += tbx_TextChanged;
            }
        }

        void tbx_TextChanged(object sender, EventArgs e)
        {
            ((BillerSettingsViewModel)ViewModel).AnyTextChanged = true;            
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsVatBerechtigt")
            {
                validationProviderBillerSettings.RulesetName = ((BillerSettingsViewModel)ViewModel).RuleSet;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            validationProviderBillerSettings.RulesetName = ((BillerSettingsViewModel) ViewModel).RuleSet;
            this.ValidateChildren();
            if (validationProviderBillerSettings.IsValid)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
