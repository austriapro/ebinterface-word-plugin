using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
// using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using ebIModels.Models;
using ebIViewModels.Services;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.Unity;
using SettingsManager;
using SimpleEventBroker;
using WinFormsMvvm;
using WinFormsMvvm.Controls;
using WinFormsMvvm.DialogService;
using ExtensionMethods;

namespace ebIViewModels.ViewModels
{
    
    public class DetailsViewModel : ViewModelBase
    {

        private string _artikelNr;
        /// <summary>
        /// Artikelnummer
        /// </summary>
        public string ArtikelNr
        {
            get { return _artikelNr; }
            set
            {
                if (_artikelNr == value)
                    return;
                _artikelNr = value;
                OnPropertyChanged();
            }
        }

        private string _bezeichnung;
        /// <summary>
        /// Artikelbezeichung
        /// </summary>
        // [Required(AllowEmptyStrings = false, ErrorMessage = "Es muss eine Bezeichnung angegeben werden.")]
        [StringLengthValidator(1, 255, MessageTemplate = "DT00024 Die Artikelbezeichnung ist erforderlich und darf max. {5} Zeichen lang sein.")]
        [StringLengthValidator(1, 255, MessageTemplate = "DT00024 Die Artikelbezeichnung ist erforderlich und darf max. {5} Zeichen lang sein.", Ruleset = "BestPosRequired")]
        public string Bezeichnung
        {
            get { return _bezeichnung; }
            set
            {
                if (_bezeichnung == value)
                    return;
                // Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = "Bezeichnung" });
                _bezeichnung = value;
                OnPropertyChanged();
            }
        }

        private decimal _menge;
        /// <summary>
        /// Menge
        /// </summary>
       // [Range(1, 999999, ErrorMessage = "Die Menge muss grösser 0 sein.")]                       
        public decimal Menge
        {
            get { return _menge; }
            set
            {
                if (_menge == value)
                    return;
                _menge = value.FixedFraction(4);
                OnPropertyChanged();
                UpdateLineItemTotals();
            }
        }

        public string EinheitDisplay
        {
            get
            {
                //var abk = UomEntries.FirstOrDefault(p => p.ID == _einheit);
                //if (abk == null) return Einheit;
                //return abk.dtAbk;
                if (_uomSelected == null)
                {
                    return "??";
                }
                else
                {
                    return _uomSelected.Description;
                }
            }
        }

        private string _einheit;
        /// <summary>
        /// Einheit in der der Stückpreis angegeben wird
        /// </summary>
        public string Einheit
        {
            get { return _einheit; }
            set
            {
                if (_einheit == value)
                    return;
                _einheit = value;
                OnPropertyChanged();
                _uomSelected = _uoMList.FirstOrDefault(p => p.Id == _einheit);
                OnPropertyChanged(nameof(UomSelected));
            }
        }

        private decimal _rabatt;
        /// <summary>
        /// Rabatt-Prozentsatz. Kann positiv (Rabatt) oder negativ (Zuschlag) sein
        /// </summary>
        public decimal Rabatt
        {
            get { return _rabatt; }
            set
            {
                if (_rabatt == value)
                    return;
                _rabatt = value;
                OnPropertyChanged();
                UpdateLineItemTotals();
            }
        }

        private decimal _einzelPreis;
        /// <summary>
        /// Stückpreis dieser Position
        /// </summary>
        public decimal EinzelPreis
        {
            get { return _einzelPreis.FixedFraction(4); }
            set
            {
                if (_einzelPreis == value)
                    return;
                _einzelPreis = value.FixedFraction(4);
                OnPropertyChanged();
                UpdateLineItemTotals();
            }
        }

        //private bool _taxexemption;
        /// <summary>
        /// Zeigt eine Steuerbefreiung an.
        /// </summary>
        //public bool Taxexemption
        //{
        //    get { return _taxexemption; }
        //    set
        //    {
        //        if (_taxexemption == value)
        //            return;
        //        _taxexemption = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private string _vatCode;
        ///// <summary>
        ///// Code für den MwSt Satz        
        ///// </summary>
        //public string VatCode
        //{
        //    get { return _vatCode; }
        //    set
        //    {
        //        if (_vatCode == value)
        //            return;
        //        _vatCode = value;
        //        OnPropertyChanged();
        //    }
        //}

        private BindingList<VatDefaultValue> _vatList;
        /// <summary>
        /// Comment
        /// </summary>
        public BindingList<VatDefaultValue> VatList
        {
            get { return _vatList; }
            set
            {
                if (_vatList == value)
                    return;
                _vatList = value;
                OnPropertyChanged();
            }
        }

        private VatDefaultValue _vatItem;
        /// <summary>
        /// Selected VAT Item
        /// </summary>
        public VatDefaultValue VatItem
        {
            get { return _vatItem; }
            set
            {
                if (_vatItem == value)
                    return;
                if (_vatItem == null)
                {
                    _vatItem = PlugInSettings.Default.MwStDefaultValue;

                }
                else
                {
                    _vatItem = value;
                }
                OnPropertyChanged();
                UpdateLineItemTotals();
            }
        }

        public bool IsVatBerechtigt
        {
            get { return PlugInSettings.Default.VStBerechtigt; }
        }

        //private decimal _vatSatz;
        ///// <summary>
        ///// Mehrwertsteuersatz
        ///// </summary>
        //public decimal VatSatz
        //{
        //    get { return _vatSatz; }
        //    set
        //    {
        //        if (_vatSatz == value)
        //            return;
        //        // var item = _vatList.First(p => p.MwStSatz == value);
        //        _vatSatz = value;
        //        OnPropertyChanged();
        //        _vatItem = _vatList.FirstOrDefault(p => p.MwStSatz == _vatSatz);
        //        OnPropertyChanged(nameof(VatItem));
        //        UpdateTotals();
        //    }
        //}

        private string _bestellBezug;
        /// <summary>
        /// Positionsnr in der Bestellung für diese Zeile
        /// </summary>
        [StringLengthValidator(0, RangeBoundaryType.Ignore, 255, RangeBoundaryType.Inclusive, MessageTemplate = "DT00025 Die Bestellposition darf max {5} Zeichen lang sein.")]
        [StringLengthValidator(1, RangeBoundaryType.Inclusive, 255, RangeBoundaryType.Ignore, MessageTemplate = "DT00026 Aufgrund der Auftragsreferenz darf die Bestellposition nicht leer sein", Ruleset = "BestPosRequired")]
        [StringLengthValidator(0, RangeBoundaryType.Ignore, 255, RangeBoundaryType.Inclusive, MessageTemplate = "DT00027 Bestellposition: Max. Anzahl Zeichen ist {5}", Ruleset = "BestPosRequired")]
        [RegexValidator(@"^\d+$",MessageTemplate="DT00088 Die Bestellpositionsnummer muss numerisch sein", Ruleset = "BestPosRequired")]
        public string BestellBezug
        {
            get
            {
                if (_bestellBezug == null)
                {
                    return "";
                }
                return _bestellBezug.Trim();
            }
            set
            {
                if (_bestellBezug == value)
                    return;
                _bestellBezug = (value ?? "").Trim(); ;
                OnPropertyChanged();
            }
        }

        private decimal _gesamtNetto;
        /// <summary>
        /// Nettobetrag der Zeile = Einzelpreis * Menge
        /// </summary>
        public decimal NettoBetragZeile
        {
            get { return _gesamtNetto.FixedFraction(2); }
            private set
            {
                if (_gesamtNetto == value)
                    return;
                _gesamtNetto = value.FixedFraction(2);
                OnPropertyChanged();
            }
        }

        private decimal _gesamtMwStBetrag;
        /// <summary>
        /// GesamtNetto - RabattBetrag
        /// </summary>
        public decimal MwStBetragZeile
        {
            get { return _gesamtMwStBetrag.FixedFraction(2); }
            private set
            {
                if (_gesamtMwStBetrag == value)
                    return;
                _gesamtMwStBetrag = value.FixedFraction(2);
                OnPropertyChanged();
            }
        }

        private decimal _gesamtBruttoBetrag;
        /// <summary>
        /// Gesamtbruttobetrag der Zeile
        /// </summary>
        public decimal BruttoBetragZeile
        {
            get { return _gesamtBruttoBetrag.FixedFraction(2); }
            private set
            {
                if (_gesamtBruttoBetrag == value)
                    return;
                _gesamtBruttoBetrag = value.FixedFraction(2);
                OnPropertyChanged();
            }
        }

        private decimal _rabattBetrag;
        /// <summary>
        /// GesamtNetto * RabattProzent
        /// </summary>
        public decimal RabattBetragZeile
        {
            get { return _rabattBetrag.FixedFraction(2); }
            private set
            {
                if (_rabattBetrag == value)
                    return;
                _rabattBetrag = value.FixedFraction(2);
                OnPropertyChanged();
            }
        }

        private decimal _nettoBasisBetrag;
        public decimal NettoBasisBetrag
        {
            get { return _nettoBasisBetrag.FixedFraction(2); }
            private set
            {
                if (_nettoBasisBetrag == value)
                    return;
                _nettoBasisBetrag = value.FixedFraction(2);
                OnPropertyChanged();
            }
        }


        private UnitOfMeasureViewModel _uomSelected;
        /// <summary>
        /// Comment
        /// </summary>
        public UnitOfMeasureViewModel UomSelected
        {
            get { return _uomSelected; }
            set
            {
                if (_uomSelected == value)
                    return;
                _uomSelected = value;
                OnPropertyChanged();
                _einheit = _uomSelected.Id;
                OnPropertyChanged(nameof(Einheit));
            }
        }

        private BindingList<UnitOfMeasureViewModel> _uoMList;
        /// <summary>
        /// Comment
        /// </summary>
        public BindingList<UnitOfMeasureViewModel> UoMList
        {
            get { return _uoMList; }
            set
            {
                if (_uoMList == value)
                    return;
                _uoMList = value;
                OnPropertyChanged();
            }
        }

        private List<UnitOfMeasureEntries> _uomEntries;
        /// <summary>
        /// Comment
        /// </summary>
        public List<UnitOfMeasureEntries> UomEntries
        {
            get { return _uomEntries; }
            set
            {
                if (_uomEntries == value)
                    return;
                _uomEntries = value;
                OnPropertyChanged();
            }
        }

        private bool _isValidForm;
        /// <summary>
        /// Comment
        /// </summary>
        public bool IsValidForm
        {
            get { return _isValidForm; }
            set
            {
                if (_isValidForm == value)
                    return;
                _isValidForm = value;
                OnPropertyChanged();
            }
        }

        private IUnityContainer _uc;
        private bool _ruleSet;

        public string RuleSet
        {
            get
            {
                return _ruleSet ? "BestPosRequired" : string.Empty;
            }
        }

        public ValidationResults Results { get; private set; }
        public DetailsViewModel(IUnityContainer uc, IDialogService dlg, bool bestPosRequired) : base(dlg)
        {
            _uc = uc;
            _ruleSet = bestPosRequired;
            _vatList = new BindingList<VatDefaultValue>(PlugInSettings.Default.VatDefaultValues);
            _uomEntries = PlugInSettings.Default.UnitOfMeasures;

            BindingList<UnitOfMeasureViewModel> uomViewList = GetUomList();
            _uoMList = new BindingList<UnitOfMeasureViewModel>(uomViewList);
            UomSelected = _uoMList.FirstOrDefault(x => x.Id == PlugInSettings.Default.UnitMeasureDefault); //.Find(x => x.Id == PlugInSettings.Default.UnitMeasureDefault);
            VatItem = PlugInSettings.Default.MwStDefaultValue;

        }

        public BindingList<UnitOfMeasureViewModel> GetUomList()
        {
            bool filter = false;
            BindingList<UnitOfMeasureViewModel> uomViewBindingList = new BindingList<UnitOfMeasureViewModel>();
            bool hasFavorites = _uomEntries.Where(p => p.Favorite == true).Any();
            if (filter == true && hasFavorites)
            {
            List<UnitOfMeasureViewModel> uomViewList = (from li in _uomEntries
                                                            where li.Favorite == true
                                                        select new UnitOfMeasureViewModel()
                                                        {
                                                            Description = li.BeschreibungDE,
                                                            ShortCut = li.dtAbk,
                                                            Id = li.ID
                                                        }).ToList();
                uomViewBindingList = new BindingList<UnitOfMeasureViewModel>(uomViewList);
        }
            else
        {
                List<UnitOfMeasureViewModel> uomViewList = (from li in _uomEntries
                                                            select new UnitOfMeasureViewModel()
                                                            {
                                                                Description = li.BeschreibungDE,
                                                                ShortCut = li.dtAbk,
                                                                Id = li.ID
                                                            }).ToList();
                uomViewBindingList = new BindingList<UnitOfMeasureViewModel>(uomViewList);
        }

            return uomViewBindingList;
        }
        
        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                _saveCommand = _saveCommand ?? new RelayCommand(param => SaveClick());
                return _saveCommand;
            }
        }

        private void SaveClick()
        {
            
            var validator = ValidationFactory.CreateValidator<DetailsViewModel>(RuleSet);
            Results = validator.Validate(this);
            IsValidForm = Results.IsValid;
        }

        private void UpdateLineItemTotals()
        {
            NettoBasisBetrag = Menge*EinzelPreis;
            RabattBetragZeile = NettoBasisBetrag * Rabatt / 100;
            NettoBetragZeile = NettoBasisBetrag - RabattBetragZeile;
            MwStBetragZeile = NettoBetragZeile * (VatItem.MwStSatz / 100);
            BruttoBetragZeile = NettoBetragZeile + MwStBetragZeile;
        }

        //[SubscribesTo(InvoiceViewModel.BestPosRequiredChanged)]
        //public void OnBestPosRequiredChanged(object sender, EventArgs args)
        //{
        //    BestPosRequiredChangedEventArgs arg = args as BestPosRequiredChangedEventArgs;
        //    _ruleSet = arg.IsBestPosRequired;
        //}
       
    }
}
