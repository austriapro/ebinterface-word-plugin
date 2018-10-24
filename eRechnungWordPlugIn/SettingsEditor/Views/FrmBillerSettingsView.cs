using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SettingsEditor.ViewModels;
using WinFormsMvvm.DialogService;
using WinFormsMvvm;

namespace SettingsEditor.Views
{
    public partial class FrmBillerSettingsView : FormService
    {
        public FrmBillerSettingsView(BillerSettingsViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = (BillerSettingsViewModel)viewModel;
            bindingSourceReStellerSetting.DataSource = (BillerSettingsViewModel)ViewModel;
            Bindings.Add(bindingSourceReStellerSetting);
            ((BillerSettingsViewModel)ViewModel).PropertyChanged += OnPropertyChanged;

            foreach (GroupBox box in Controls.OfType<GroupBox>())
            {
                SetTextChangedEvent(box);
            }
            ckBxVstBerechtigt.CheckedChanged += tbx_TextChanged;
#if !DEBUG
            cBtnReset.Visible = false;
#endif
        }
        public override void SetBindingSource(object bindSrc)
        {
            multiColumnComboBox1.DataSource = ((BillerSettingsViewModel)ViewModel).VatDefaultList;
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
            // ((BillerSettingsViewModel)ViewModel).AnyTextChanged = true;
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
            if (validationProviderBillerSettings.IsValid && ((BillerSettingsViewModel)ViewModel).Results.IsValid)
            {
                DialogResult = DialogResult.OK;
                ((ViewModelBase)ViewModel).ChangePending = false;
                Close();
            }
        }
    }
}
