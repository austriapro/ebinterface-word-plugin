using System.Windows.Forms;
using ebIModels.Models;
using ebIViewModels.SettingsViews;
using ebIViewModels.ViewModels;
using Microsoft.Practices.Unity;
using WinFormsMvvm;
using WinFormsMvvm.Controls;
using WinFormsMvvm.DialogService;

namespace ebIViewModels.SettingsViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private IUnityContainer _uc;
        private IDialogService _dialog;
        public SettingsViewModel(IUnityContainer uc, IDialogService dialog)
        {
            _uc = uc;
            _dialog = dialog;
        }

        private RibbonCommandButton _editRechnungsstellerCommand;
        public RibbonCommandButton EditEditRechnungsstellerCommand
        {
            get
            {
                _editRechnungsstellerCommand = _editRechnungsstellerCommand ?? new RibbonCommandButton(RechnungsstellerClick);
                return _editRechnungsstellerCommand;
            }
        }

        private void RechnungsstellerClick(object inv)
        {
            InvoiceViewModel invoice = inv as InvoiceViewModel;
            BillerSettingsViewModel rsModel = _uc.Resolve<BillerSettingsViewModel>(new ParameterOverride("invoiceView",invoice));
            var rc = _dialog.ShowDialog<FrmBillerSettingsView>(rsModel);
            if (rc == DialogResult.OK)
            {
                // Save the holy thing?
            }

        }
    }
}
