using System;
using System.Windows.Forms;
using Microsoft.Practices.Unity;
using SettingsEditor.Views;
using WinFormsMvvm;
using WinFormsMvvm.Controls;
using WinFormsMvvm.DialogService;

namespace SettingsEditor.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private IUnityContainer _uc;
        public SettingsViewModel(IUnityContainer uc, IDialogService dialog)
        {
            _uc = uc;
            _dlg = dialog;
            
        }

        #region Rechnungssteller Command

        private RibbonCommandButton _editRechnungsstellerCommand;
        public RibbonCommandButton EditEditRechnungsstellerCommand
        {
            get
            {
                _editRechnungsstellerCommand = _editRechnungsstellerCommand ??
                                               new RibbonCommandButton(RechnungsstellerClick);
                return _editRechnungsstellerCommand;
            }
        }

        private void RechnungsstellerClick(object inv)
        {
            // InvoiceViewModel invoice = inv as InvoiceViewModel;
            BillerSettingsViewModel rsModel = _uc.Resolve<BillerSettingsViewModel>();
            var rc = _dlg.ShowDialog<FrmBillerSettingsView>(rsModel);
            if (rc == DialogResult.OK)
            {
                // Save the holy thing?
            }

        }

        #endregion

        #region Konto Command

        private RibbonCommandButton _editKontoCommandButton;
        public RibbonCommandButton EditKontoCommandButton
        {
            get
            {
                _editKontoCommandButton = _editKontoCommandButton ?? new RibbonCommandButton(param => EditKontoCommandButtonClick());
                return _editKontoCommandButton;
            }
        }

        private void EditKontoCommandButtonClick()
        {
            KontoSettingsViewModel ktoModel = _uc.Resolve<KontoSettingsViewModel>();
            var rc = _dlg.ShowDialog<FrmKontoSettingsView>(ktoModel);
        }

        #endregion

        #region Handy Signatur

        private RibbonCommandButton _handySignaturButtonCommand;

        public RibbonCommandButton HandySignaturButtonCommand
        {
            get
            {
                _handySignaturButtonCommand = _handySignaturButtonCommand ??
                                              new RibbonCommandButton(param => HandySignaturButtonClick());
                return _handySignaturButtonCommand;
            }
        }

        private void HandySignaturButtonClick()
        {
            HandySignSettingsViewModel model = new HandySignSettingsViewModel();
            var rc = _dlg.ShowDialog<FrmHandySignSettingsView>(model);
        }

        #endregion

        #region eMail

        private RibbonCommandButton _mailButton;
        public RibbonCommandButton MailButton
        {
            get
            {
                _mailButton = _mailButton ?? new RibbonCommandButton(param => MailClick());
                return _mailButton;
            }
        }

        private void MailClick()
        {
            MailSettingsViewModel model = _uc.Resolve<MailSettingsViewModel>();
            var rc = _dlg.ShowDialog<FrmMailSettingsView>(model);
        }

        #endregion

        #region Uid Abfrage 

        private RibbonCommandButton _uidAbfrageButton;

        public RibbonCommandButton UidAbfrageButton
        {
            get
            {
                _uidAbfrageButton = _uidAbfrageButton ?? new RibbonCommandButton(param => UidAbfrageClick());
                return _uidAbfrageButton;
            }
        }

        private void UidAbfrageClick()
        {
            UidAbfrageSettingsViewModel model = _uc.Resolve<UidAbfrageSettingsViewModel>();
            var rc = _dlg.ShowDialog<FrmUidAbfrageSettingsView>(model);
        }

        #endregion

        #region Speicherorte

        private RibbonCommandButton _saveLocButton;
        public RibbonCommandButton SaveLocButton
        {
            get
            {
                _saveLocButton = _saveLocButton ?? new RibbonCommandButton(param => SaveLocClick());
                return _saveLocButton;
            }
        }

        private void SaveLocClick()
        {
            SaveLocationSettingsViewModel model = _uc.Resolve<SaveLocationSettingsViewModel>();
            var rc = _dlg.ShowDialog<FrmSaveLocationView>(model);
        }

        #endregion

        #region Zustellung

        private RibbonCommandButton _zustellgButton;
        public RibbonCommandButton ZustellgButton
        {
            get
            {
                _zustellgButton = _zustellgButton ?? new RibbonCommandButton(param => ZustellgClick());
                return _zustellgButton;
            }
        }

        private void ZustellgClick()
        {
            ZustellSettingsViewModel model = _uc.Resolve<ZustellSettingsViewModel>();
            var rc = _dlg.ShowDialog<FrmZustellSettingsView>(model);
        }

        #endregion
 
    }
}
