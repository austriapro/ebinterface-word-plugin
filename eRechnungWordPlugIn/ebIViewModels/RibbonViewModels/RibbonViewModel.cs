using System;
using System.Windows.Forms;
using ebIViewModels.RibbonViews;
using ebIViewModels.ViewModels;
using ebIViewModels.Views;
using Microsoft.Practices.Unity;
using WinFormsMvvm;
using WinFormsMvvm.Controls;
using WinFormsMvvm.DialogService;

namespace ebIViewModels.RibbonViewModels
{
    /// <summary>
    /// Diese Klassen implementiert die Commands für das Ribbon.
    /// </summary>
    public class RibbonViewModel : ViewModelBase
    {

        private RibbonCommandButton _btnAboutCommand;
        public RibbonCommandButton BtnAboutCommand
        {
            get
            {
                _btnAboutCommand = _btnAboutCommand ?? new RibbonCommandButton(param => btnAboutClick());
                return _btnAboutCommand;
            }
        }
        private void btnAboutClick()
        {
            AboutViewModel aboutView = _uc.Resolve<AboutViewModel>();
            _dlg.ShowDialog<FrmAboutView>(aboutView);
        }

        private RibbonCommandButton _ebInterfaceLinkButton;
        public RibbonCommandButton EbInterfaceLinkButton
        {
            get
            {
                _ebInterfaceLinkButton = _ebInterfaceLinkButton ?? new RibbonCommandButton(param => ebInterfaceLinkClick());
                return _ebInterfaceLinkButton;
            }
        }
        private void ebInterfaceLinkClick()
        {
            System.Diagnostics.Process.Start("http://www.ebinterface.at");
        }

        private RibbonCommandButton _austriaProButton;
        public RibbonCommandButton AustriaProButton
        {
            get
            {
                _austriaProButton = _austriaProButton ?? new RibbonCommandButton(param => austriaProClick());
                return _austriaProButton;
            }
        }
        private void austriaProClick()
        {
            System.Diagnostics.Process.Start("http://www.austriapro.at");
        }

        private RibbonCommandButton _erbGvAtButton;
        public RibbonCommandButton ErbGvAtButton
        {
            get
            {
                _erbGvAtButton = _erbGvAtButton ?? new RibbonCommandButton(param => erbGvAtClick());
                return _erbGvAtButton;
            }
        }
        private void erbGvAtClick()
        {
            System.Diagnostics.Process.Start("https://www.erb.gv.at");
        }

        private RibbonCommandButton _signaturButton;
        public RibbonCommandButton SignaturButton
        {
            get
            {
                _signaturButton = _signaturButton ?? new RibbonCommandButton(param => SignaturClick());
                return _signaturButton;
            }
        }
        private void SignaturClick()
        {
            NotImplemented();
        }

        private RibbonCommandButton _verifySignatureButton;
        public RibbonCommandButton VerifySignatureButton
        {
            get
            {
                _verifySignatureButton = _verifySignatureButton ?? new RibbonCommandButton(param => VerifySignatureClick());
                return _verifySignatureButton;
            }
        }
        private void VerifySignatureClick()
        {
            NotImplemented();
        }

        private RibbonCommandButton _helpButton;
        public RibbonCommandButton HelpButton
        {
            get
            {
                _helpButton = _helpButton ?? new RibbonCommandButton(param => HelpClick());
                return _helpButton;
            }
        }
        private void HelpClick()
        {
            _dlg.ShowMessageBox("Weitere Hilfe finden Sie im mitgelieferten Dokument 'Ausfüllhilfe.pdf'" + Environment.NewLine + "oder im Forum http://www.ebinterface.org.","Hilfe",MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private RibbonCommandButton _supportButton;
        public RibbonCommandButton SupportButton
        {
            get
            {
                _supportButton = _supportButton ?? new RibbonCommandButton(param => supportClick());
                return _supportButton;
            }
        }
        private void supportClick()
        {
            System.Diagnostics.Process.Start("http://www.ebinterface.org");
        }


        private IUnityContainer _uc;

        public RibbonViewModel(IUnityContainer uc,IDialogService dialog)
        {
            _uc = uc;
            _dlg = dialog;
        }

        private void NotImplemented()
        {
            MessageBox.Show("Not implemented");
        }
    }
}
