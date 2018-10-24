using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using ebIValidation;
using ebIModels.Models;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.Unity;
using SettingsEditor.Service;
using SettingsEditor.ViewModels;
using SettingsManager;
using SimpleEventBroker;
using WinFormsMvvm;
using WinFormsMvvm.Controls;
using WinFormsMvvm.DialogService;
using LogService;
using static ebIModels.Models.InvoiceModel;
using ebIModels.Schema;
using ebIModels.Mapping;

namespace SettingsEditor.ViewModels
{
    // ebIViewModels.SettingsViewModels.BillerSettingsViewModel
    public class BillerSettingsViewModel : ViewModelBase
    {
        public const string UpdateFromBillerSettings = "UpdateFromBillerSettings";        

        [Publishes(UpdateFromBillerSettings)]
        public event EventHandler UpdateFromBillerSettingsEvent;
        private void UpdateFromBillerSettingsFire()
        {
            UpdateFromBillerSettingsEvent?.Invoke(this, EventArgs.Empty);
        }
        #region Properties

        private string _name;

        [StringLengthValidator(1, 255, MessageTemplate = "BS00014 Name darf nicht leer sein.")]
        [StringLengthValidator(1, 255, MessageTemplate = "BS00014 Name darf nicht leer sein.", Ruleset = KeinVstAbzug)]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _strasse;
        [StringLengthValidator(1, 255, MessageTemplate = "BS00015 Strasse darf nicht leer sein.")]
        [StringLengthValidator(1, 255, MessageTemplate = "BS00015 Strasse darf nicht leer sein.", Ruleset = KeinVstAbzug)]
        public string Strasse
        {
            get { return _strasse; }
            set
            {
                if (_strasse == value)
                    return;
                _strasse = value;
                OnPropertyChanged();
            }
        }

        private string _plz;
        [StringLengthValidator(1, 10, MessageTemplate = "BS00016 PLZ darf nicht leer sein.")]
        [StringLengthValidator(1, 10, MessageTemplate = "BS00016 PLZ darf nicht leer sein.", Ruleset = KeinVstAbzug)]
        public string Plz
        {
            get { return _plz; }
            set
            {
                if (_plz == value)
                    return;
                _plz = value;
                OnPropertyChanged();
            }
        }

        private string _ort;
        [StringLengthValidator(1, 255, MessageTemplate = "BS00017 Ort darf nicht leer sein.")]
        [StringLengthValidator(1, 255, MessageTemplate = "BS00017 Ort darf nicht leer sein.", Ruleset = KeinVstAbzug)]
        public string Ort
        {
            get { return _ort; }
            set
            {
                if (_ort == value)
                    return;
                _ort = value;
                OnPropertyChanged();
            }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (_phone == value)
                    return;
                _phone = value;
                OnPropertyChanged();
            }
        }

        private string _kontakt;
        public string Kontakt
        {
            get { return _kontakt; }
            set
            {
                if (_kontakt == value)
                    return;
                _kontakt = value;
                OnPropertyChanged();
            }
        }

        private string _email;
        [EmailAddressValidator(true, MessageTemplate = "BS00018 Ungültige eMail Adresse")]
        [EmailAddressValidator(true, MessageTemplate = "BS00018 Ungültige eMail Adresse", Ruleset = KeinVstAbzug)]
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email == value)
                    return;
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _vatId;
        public const string VatRegex = "^((AT)?U[0-9]{8}|" +
                                       "(BE)?0?[0-9]{9}|" +
                                       "(BG)?[0-9]{9,10}|" +
                                       "(CY)[0-9]{8}[A-Z]|" +
                                       "(CZ)?[0-9]{8,10}|" +
                                       "(DE)?[0-9]{9}|" +
                                       "(DK)?[0-9]{8}|" +
                                       "(EE)?[0-9]{9}|" +
                                       "(EL|GR)?[0-9]{9}|" +
                                       "(ES)?[0-9A-Z][0-9]{7}[0-9A-Z]|" +
                                       "(FI)?[0-9]{8}|" +
                                       "(FR)?[0-9A-Z]{2}[0-9]{9}|" +
                                       "(GB)?([0-9]{9}([0-9]{3})?|[A-Z]{2}[0-9]{3})|" +
                                       "(HU)?[0-9]{8}|" +
                                       "(IE)?[0-9][0-9A-Z][0-9]{5}[A-Z]|" +
                                       "(IT)?[0-9]{11}|" +
                                       "(LT)?([0-9]{9}|[0-9]{12})|" +
                                       "(LU)?[0-9]{8}|" +
                                       "(LV)?[0-9]{11}|" +
                                       "(MT)?[0-9]{8}|" +
                                       "(NL)?[0-9]{9}B[0-9]{2}|" +
                                       "(PL)?[0-9]{10}|" +
                                       "(PT)?[0-9]{9}|" +
                                       "(RO)?[0-9]{2,10}|" +
                                       "(SE)?[0-9]{12}|" +
                                       "(SI)?[0-9]{8}|" +
                                       "(SK)?[0-9]{10})$";
        private const string VatRegex8x0 = "^[0]{8}$"; // Keine VSt Berechtigung
        // [StringLengthValidator(8, RangeBoundaryType.Inclusive, 8, RangeBoundaryType.Inclusive, MessageTemplate = "Wenn keine VSt. Abzugsberechtigung besteht, muss die USt-Id  8 Nullen enthalten.", Ruleset = KeinVstAbzug)]
        [RegexValidator(VatRegex, MessageTemplate = "BS00019 Die USt-ID ist ungültig. Wenn Sie noch keine USt-Id haben, geben Sie 'ATU00000000' ein.")]
        [RegexValidator("^[0]+$", MessageTemplate = "BS00020 Wenn eine VSt. Abzugsberechtigung besteht, darf USt-Id nicht aus Nullen bestehen.", Negated = true)]
        [RegexValidator("^[0]{8}$", MessageTemplate = "BS00021 Wenn keine VSt. Abzugsberechtigung besteht, muss die USt-Id  8 Nullen enthalten.", Ruleset = KeinVstAbzug)]
        public string VatId
        {
            get { return _vatId; }
            set
            {
                if (_vatId == value)
                    return;
                _vatId = value;
                OnPropertyChanged();
            }
        }

        private string _gln;
        [GLNValidator(MessageTemplate = "BS00022 Ungültige Global Location Number")]
        [GLNValidator(MessageTemplate = "BS00022 Ungültige Global Location Number", Ruleset = KeinVstAbzug)]
        public string Gln
        {
            get { return _gln; }
            set
            {
                if (_gln == value)
                    return;
                _gln = value;
                OnPropertyChanged();
            }
        }

        private bool _save2Form;
        public bool Save2Form
        {
            get { return _save2Form; }
            set
            {
                if (_save2Form == value)
                    return;
                _save2Form = value;
                OnPropertyChanged();
            }
        }

        public const string KeinVstAbzug = "KeinVStAbzug";
        public string RuleSet
        {
            get { return IsVatBerechtigt ? "" : KeinVstAbzug; }
        }

        #region SelectedVersion - string
        private string _SelectedVersion;
        public string SelectedVersion
        {
            get { return _SelectedVersion; }
            set
            {
                if (_SelectedVersion == value)
                    return;
                _SelectedVersion = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region ebIVersions - List<ebIVersion>
        private List<string> _ebIVersions;
        public List<string> ebIVersions
        {
            get { return _ebIVersions; }
            set
            {
                if (_ebIVersions == value)
                    return;
                _ebIVersions = value;
                OnPropertyChanged();
            }
        }
        #endregion


        public bool IsNotVatBerechtigt { get { return !IsVatBerechtigt; } }
        private bool _isVatBerechtigt;

        public bool IsVatBerechtigt
        {
            get { return _isVatBerechtigt; }
            set
            {
                if (_isVatBerechtigt == value)
                    return;
                bool newValue = value;
                if (!newValue)
                {
                    VatId = PlugInSettings.VatIdDefaultOhneVstBerechtigung;
                    VatSelected = PlugInSettings.Default.IstNichtVStBerechtigtVatValue;
                }
                else
                {
                    VatId = PlugInSettings.VatIdDefaultMitVstBerechtigung;
                    VatSelected = PlugInSettings.Default.IstNichtVStBerechtigtVatValue;
                }
                _isVatBerechtigt = value;
                OnPropertyChanged();
                OnPropertyChanged("RuleSet");
            }
        }

        private string _vatText;

        [StringLengthValidator(1, RangeBoundaryType.Inclusive, 1, RangeBoundaryType.Ignore, 
            MessageTemplate = "BS00023 Wenn keine Berechtigung zum Vorsteuerabzugbesteht muss ein Wert eingegeben werden", Ruleset = KeinVstAbzug)]
        public string VatText
        {
            get { return _vatText; }
            set
            {
                if (_vatText == value)
                    return;
                _vatText = value;
                OnPropertyChanged();
            }
        }

        private string _bank;
        public string Bank
        {
            get { return _bank; }
            set
            {
                if (_bank == value)
                    return;
                _bank = value;
                OnPropertyChanged();
            }
        }

        private string _inhaber;
        public string Inhaber
        {
            get { return _inhaber; }
            set
            {
                if (_inhaber == value)
                    return;
                _inhaber = value;
                OnPropertyChanged();
            }
        }

        private string _iban;
        [IbanValidator(MessageTemplate="KV00009 Kontoverbindung: ")]
        [IbanValidator(MessageTemplate = "KV00009 Kontoverbindung: ", Ruleset = KeinVstAbzug)]
        public string Iban
        {
            get { return _iban; }
            set
            {
                if (_iban == value)
                    return;
                _iban = value;
                OnPropertyChanged();
            }
        }

        private string _bic;
        [BicValidator(MessageTemplate="KV00007 Kontoverbindung: BIC ist ungültig.")]
        [BicValidator(MessageTemplate = "KV00007 Kontoverbindung: BIC ist ungültig.", Ruleset = KeinVstAbzug)]
        public string Bic
        {
            get { return _bic; }
            set
            {
                if (_bic == value)
                    return;
                _bic = value;
                OnPropertyChanged();
            }
        }

        private CountryCodeModel _countryCodeSelected;
        public CountryCodeModel CountryCodeSelected
        {
            get { return _countryCodeSelected; }
            set
            {
                if (_countryCodeSelected == value)
                    return;
                _countryCodeSelected = value;
                OnPropertyChanged();
            }
        }

        private List<CountryCodeModel> _countryCodes;
        public List<CountryCodeModel> CountryCodes
        {
            get { return _countryCodes; }
            set
            {
                if (_countryCodes == value)
                    return;
                _countryCodes = value;
                OnPropertyChanged();
            }
        }

        // ToDo VatList im GUI erweitern
        private VatDefaultValue _vatSelected;
        public VatDefaultValue VatSelected
        {
            get { return _vatSelected; }
            set
            {
                if (_vatSelected == value)
                    return;
                _vatSelected = value;
                OnPropertyChanged();
            }
        }

        private BindingList<VatDefaultValue> _vatDefaultList;
        public BindingList<VatDefaultValue> VatDefaultList
        {
            get { return _vatDefaultList; }
            set
            {
                if (_vatDefaultList == value)
                    return;
                _vatDefaultList = value;
                OnPropertyChanged();
            }
        }


        private string _cSel;
        public string CSel
        {
            get { return _cSel; }
            set
            {
                if (_cSel == value)
                    return;
                _cSel = value;
                OnPropertyChanged();
            }
        }


        private CurrencyListViewModel _currSelected;
        public CurrencyListViewModel CurrSelected
        {
            get
            {
                Log.TraceWrite(CallerInfo.Create(),"get:" + (_currSelected != null ? _currSelected.Code : "null"));
                    return _currSelected;
            }
            set
            {
                if (_currSelected != null)
                {
                    if (value != null)
                    {
                        Log.TraceWrite(CallerInfo.Create(),"new:" + value.Code + ", old:" + _currSelected.Code);
                    }
                }
                if (_currSelected == value)
                    return;
                _currSelected = value;
                _cSel = _currSelected.Code;
                OnPropertyChanged();
            }
        }

        public bool RecalcMwSt = false;
        private BindingList<CurrencyListViewModel> _currencyList;
        public BindingList<CurrencyListViewModel> CurrencyList
        {
            get { return _currencyList; }
            set
            {
                if (_currencyList == value)
                    return;
                _currencyList = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region Commands
        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                _saveCommand = _saveCommand ?? new RelayCommand(param => SaveCommandClick());
                return _saveCommand;
            }
        }

        internal virtual void SaveCommandClick()
        {

            if (IsValid())
            {
                FilltoSettings();
                PlugInSettings.Default.Save();
                if (ChangePending && !Save2Form)
                {
                    string msg = RecalcMwSt == true ? "Sollen die Änderungen in das Formular übernommen werden und die MwSt in den Detailzeilen neu berechnet werden?" : "Sollen die Änderungen in das Formular übernommen werden?";
                    DialogResult rc = _dlg.ShowMessageBox(
                        msg, "Einstellungen",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    Save2Form = (rc == DialogResult.Yes);
                }
                if (Save2Form)
                {
                    UpdateFromBillerSettingsFire();
                }
            }
        }


        private ICommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                _clearCommand = _clearCommand ?? new RelayCommand(param => ClearClick());
                return _clearCommand;
            }
        }

        private void ClearClick()
        {
            var rc =
                _dlg.ShowMessageBox(
                    "Wollen Sie wirklich die Einstellungen auf die Auslieferungswerte zurücksetzen?",
                    "Einstellungen zurücksetzen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rc != DialogResult.Yes)
            {
                return;
            }
            PlugInSettings.Reset();
            FillFromSettings();
            OnPropertyChanged();
        }

        public ValidationResults Results { get; internal set; }
        public bool IsValid()
        {
            Validator<BillerSettingsViewModel> validator = ValidationFactory.CreateValidator<BillerSettingsViewModel>(RuleSet);
            Results = validator.Validate(this);
            return Results.IsValid;
        }
        #endregion

        #region private Properties

        private IUnityContainer _uc;

        #endregion
        public BillerSettingsViewModel(IUnityContainer uc, IDialogService dialog
            )
        {
            _uc = uc;
            _dlg = dialog;
            _countryCodes = ebIModels.Models.CountryCodes.GetCountryCodeList();
            Results = new ValidationResults();
           var  cList = _uc.Resolve<CurrencyListViewModels>();
            //cList.GetList(Enum.GetNames(typeof(CurrencyType)).ToList());
            cList.GetList(new List<string>() { ModelConstants.CurrencyCodeFixed.ToString() });
            _currencyList = new BindingList<CurrencyListViewModel>(cList.DropDownList);            
            string defCurr = PlugInSettings.Default.Currency;
            _currSelected = _currencyList.FirstOrDefault(p => p.Code == defCurr);
            _ebIVersions = InvoiceFactory.GetVersionsWithSaveSupported();
            FillFromSettings();

        }

        internal virtual void FillFromSettings()
        {
            _name = PlugInSettings.Default.Name;
            _strasse = PlugInSettings.Default.Strasse;
            _plz = PlugInSettings.Default.Plz;
            _ort = PlugInSettings.Default.Ort;
            _phone = PlugInSettings.Default.TelNr;
            _kontakt = PlugInSettings.Default.Contact;
            _email = PlugInSettings.Default.Email;
            _vatId = PlugInSettings.Default.Vatid;
            _gln = PlugInSettings.Default.BillerGln;
            _save2Form = false;
            _countryCodeSelected = _countryCodes.Find(p => p.Code == PlugInSettings.Default.Land);
            _vatDefaultList = new BindingList<VatDefaultValue> (PlugInSettings.Default.VatDefaultValues);
            _vatSelected = PlugInSettings.Default.MwStDefaultValue;
            _vatText = PlugInSettings.Default.VStText;
            _isVatBerechtigt = PlugInSettings.Default.VStBerechtigt;
            _currSelected = new CurrencyListViewModel();
            _currSelected = _currencyList.FirstOrDefault(p => p.Code == PlugInSettings.Default.Currency);
            _cSel = PlugInSettings.Default.Currency;
            _bank = PlugInSettings.Default.Bank;
            _inhaber = PlugInSettings.Default.Kontowortlaut;
            _iban = PlugInSettings.Default.Iban;
            _bic = PlugInSettings.Default.Bic;
            _SelectedVersion = PlugInSettings.Default.EbInterfaceVersionString;
            // _anyTextChanged = false;
        }

        internal virtual void FilltoSettings()
        {
            PlugInSettings.Default.Name = Name;
            PlugInSettings.Default.Strasse = Strasse;
            PlugInSettings.Default.Plz = Plz;
            PlugInSettings.Default.Ort = Ort;
            PlugInSettings.Default.TelNr = Phone;
            PlugInSettings.Default.Contact = Kontakt;
            PlugInSettings.Default.Email = Email;
            PlugInSettings.Default.Vatid = VatId;
            PlugInSettings.Default.BillerGln = Gln;
            //if (IsVatBerechtigt != PlugInSettings.Default.VStBerechtigt)
            //{
            //    string msgText;
            //    msgText = string.Format("Soll die MwSt in den Detailzeilen mit dem Satz {0}% neu berechnet werden ?", VatSelected.MwStSatz);
            //    var rc = _dlg.ShowMessageBox(msgText, "MwSt berechnen", MessageBoxButtons.YesNo,
            //        MessageBoxIcon.Question);
            //    RecalcMwSt = rc == DialogResult.Yes;
            //}
            if (!IsVatBerechtigt && (PlugInSettings.Default.VStBerechtigt==true))
            {
                string msgText;
                msgText = string.Format("Soll die MwSt in den Detailzeilen mit dem Satz {0}% neu berechnet werden ?", VatSelected.MwStSatz);
                var rc = _dlg.ShowMessageBox(msgText, "MwSt berechnen", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                RecalcMwSt = rc == DialogResult.Yes;
            }
            PlugInSettings.Default.VStBerechtigt = IsVatBerechtigt;
            // CountryCodeSelected = CountryCodes.Find(p => p.Code == PlugInSettings.Default.SetLand);
            PlugInSettings.Default.Land = CountryCodeSelected.Code;
            //PlugInSettings.Default.VatDefaultValues = VatDefaultList;
            // VatSelected = VatDefaultList.Find(p => p.MwStSatz == PlugInSettings.Default.SetMwStDefault);
            PlugInSettings.Default.MwStDefault = VatSelected.MwStSatz;
            PlugInSettings.Default.VStText = VatText;
            PlugInSettings.Default.Currency = CurrSelected.Code;

            PlugInSettings.Default.Bank = Bank;
            PlugInSettings.Default.Kontowortlaut = Inhaber;
            PlugInSettings.Default.Iban = Iban;
            PlugInSettings.Default.Bic = Bic;
            PlugInSettings.Default.EbInterfaceVersionString = SelectedVersion;
        }

        
    }
}