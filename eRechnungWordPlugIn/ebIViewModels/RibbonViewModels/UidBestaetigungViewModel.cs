using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using ebIServices.UidAbfrage;
using ebIViewModels.ErrorView;
using ebIViewModels.Services;
using ebIViewModels.ViewModels;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.Unity;
using SettingsManager;
using SimpleEventBroker;
using WinFormsMvvm;
using WinFormsMvvm.Controls;
using WinFormsMvvm.DialogService;

namespace ebIViewModels.RibbonViewModels
{
    public class UidBestaetigungViewModel : ViewModelBaseExtension
    {
        private readonly InvoiceViewModel _invoiceView;
        private readonly IUidAbfrageDienst _uidAbfrage;
        public UidBestaetigungViewModel(IUnityContainer uc, IDialogService dlgService, InvoiceViewModel invoiceView, IUidAbfrageDienst uidAbfrage) : base(uc,dlgService)
        {
            _invoiceView = invoiceView;
            _uidAbfrage = uidAbfrage;
            _billerUid = _invoiceView.VmBillerVatid;
            _receiverUid = _invoiceView.VmRecVatid;
            _teilNehmerId = PlugInSettings.Default.FinOnlTeilnehmer;
            _benutzerId = PlugInSettings.Default.FinOnlBenutzer;
            IsUidValid = false;
        }

        #region Properties

        private string _billerUid;

        [RegexValidator(@"(^[A-Z]{2}\d{9}[B]\d{2})|(^[A-Z]{2,3}\d{8,12})",
            MessageTemplate = "UB00076 Bestätigung: Ihre USt-Id fehlt oder ist nicht gültig.", Tag = "Ust-ID")]
        public string BillerUid
        {
            get
            {
                return _billerUid;
            }
            set
            {
                if (_billerUid == value)
                    return;
                _billerUid = value;
                OnPropertyChanged();
            }
        }

        private string _receiverUid;

        [RegexValidator(@"(^[A-Z]{2}\d{9}[B]\d{2})|(^[A-Z]{2,3}\d{8,12})",
            MessageTemplate = "UB00077 Bestätigung: USt-Id des Rechnungsempfängers fehlt oder ist nicht gültig.",
            Tag = "Ust-ID")]
        public string ReceiverUid
        {
            get { return _receiverUid; }
            set
            {
                if (_receiverUid == value)
                    return;
                _receiverUid = value;
                OnPropertyChanged();
            }
        }

        private string _teilNehmerId;
        //  [NotNullValidator(MessageTemplate = "UB00078 Bestätigung: Die FinOnlTeilnehmer-Identifikation darf nicht leer sein.", Tag = "Tln-Id")]
        [StringLengthValidator(1, 255,
            MessageTemplate = "UB00078 Bestätigung: Die FinOnlTeilnehmer-Identifikation darf nicht leer sein.", Tag = "Tln-Id"
            )]
        public string TeilNehmerId
        {
            get { return _teilNehmerId; }
            set
            {
                if (_teilNehmerId == value)
                    return;
                _teilNehmerId = value;
                OnPropertyChanged();
            }
        }

        private string _benutzerId;

        [StringLengthValidator(1, 255,
            MessageTemplate = "UB00079 Bestätigung: Die FinOnlBenutzer-Identifikation darf nicht leer sein.", Tag = "Ben-Id")]
        public string BenutzerId
        {
            get { return _benutzerId; }
            set
            {
                if (_benutzerId == value)
                    return;
                _benutzerId = value;
                OnPropertyChanged();
            }
        }

        private string _pin;

        [StringLengthValidator(1, 255, MessageTemplate = "UB00080 Bestätigung: Die PIN darf nicht leer sein.",
            Tag = "PIN")]
        public string Pin
        {
            get { return _pin; }
            set
            {
                if (_pin == value)
                    return;
                _pin = value;
                OnPropertyChanged();
            }
        }

        public bool IsUidValid { get; private set; }
        #endregion

        #region Validierung

        public ValidationResults Results { get; set; }

        public bool IsValid()
        {
            ValidationFactory.SetDefaultConfigurationValidatorFactory(new SystemConfigurationSource(false));
            var validator = ValidationFactory.CreateValidator<UidBestaetigungViewModel>();
            Results = validator.Validate(this);
            return Results.IsValid;
        }

        #endregion


        private ICommand _bestaetigenCommand;
        public ICommand BestaetigenCommand
        {
            get
            {
                _bestaetigenCommand = _bestaetigenCommand ?? new RelayCommand(param => BestaetigenClick());
                return _bestaetigenCommand;
            }
        }

        private void BestaetigenClick()
        {
            IsValid();
            if (!Results.IsValid)
            {
                return;
            }
            PublishToPanel("UID Bestätigung", "", "", string.Format("UID Abfrage beginnt {0:g}", DateTime.Now));
            PublishToPanel("", "Antragsteller", "", "Meine UID: " + BillerUid);
            PublishToPanel("", "FinOnlTeilnehmer", "", TeilNehmerId);
            PublishToPanel("", "Erwerber", "", "zu prüfende UID: " + ReceiverUid);
            if (!_uidAbfrage.Login(Pin, TeilNehmerId, BenutzerId))
            {
                PublishToPanel("", "Login", "", "Login nicht erfolgreich.");
                PublishToPanel("","Fehler","",_uidAbfrage.Message);
                return;
            }
            _uidAbfrage.UidAbfrage(ReceiverUid, BillerUid);

            PublishToPanel("", "Ergebnis", "", _uidAbfrage.Message);
            IsUidValid = _uidAbfrage.IsCorrect;
            _uidAbfrage.Logout();
            if (IsUidValid)
            {
                PublishToPanel("", "Firma", "", _uidAbfrage.Name);
                int i = 1;
                foreach (string item in _uidAbfrage.Adrz)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        PublishToPanel("", string.Format("Adresse({0})", i++), "", item); 
                    }
                }
            }
            PublishToPanel("UID Bestätigung", "", "", string.Format("UID Abfrage beendet {0:g}", DateTime.Now));
        }

    }
}
