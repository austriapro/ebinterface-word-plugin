using System;
using System.Collections.Generic;
using System.ComponentModel;
// using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Xml;
using ebIServices.SendMail;
using ebIServices.StartProcess;
using ebIValidation;
using ebIViewModels.ExtensionMethods;
using SettingsEditor.ViewModels;
using Validation = ebIValidation;
using ebIModels.Models;
using ebIModels.Schema;
using ebIViewModels.ErrorView;
using ebIViewModels.Services;
using ebIViewModels.Views;
using ExtensionMethods;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using SettingsManager;
using SimpleEventBroker;
using WinFormsMvvm;


// using ebIModels.Models.InvoiceType;
using WinFormsMvvm.Controls;
using WinFormsMvvm.DialogService;
using WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile;
using WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile;
using DropDownListViewModels = ebIViewModels.Services.DropDownListViewModels;
using InvoiceType = ebIModels.Models.InvoiceType;
using ValidationResult = Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult;
using ebIServices;
using LogService;


namespace ebIViewModels.ViewModels
{

    /// <summary>
    /// Dropdownlisten und zusätzliche Felder
    /// </summary>
    [HasSelfValidation]
    public partial class InvoiceViewModel : ViewModelBaseExtension
    {
        #region Event Publishing

        public const string InvoiceValidationOptionChanged = "InvoiceValidationOptionChanged";
        public const string BestPosRequiredChanged = "BestPosRequiredChanged";
        public const string InvDatesChanged = "InvDatesChanged";
        public const string SendMailEvent = "SendMailEvent";
        public const string SaveAsPdfEvent = "SaveAsPdfEvent";
        public const string DocumentHomeKey = "DocumentHomeKey";


        [Publishes(DocumentHomeKey)]
        public event EventHandler DocumentHomeKeyEvent;
        private void DocumentHomeKeyFire()
        {
            EventHandler handler = DocumentHomeKeyEvent;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        [Publishes(SaveAsPdfEvent)]
        public event EventHandler SaveAsPdfEventHandler;
        internal virtual void SaveAsPdfEventFire(string xmlFileName, string pdfFileName)
        {
            SaveAsPdfAndSendMailEventArgs arg = new SaveAsPdfAndSendMailEventArgs();
            arg.XmlFilename = xmlFileName;
            arg.PdfFilename = pdfFileName;
            EventHandler handler = SaveAsPdfEventHandler;
            if (handler != null)
            {
                handler(this, (EventArgs)arg);
            }
        }

        [Publishes(BestPosRequiredChanged)]
        public event EventHandler BestPosRequiredChangedEvent;
        private void BestPosRequiredChangedFire()
        {
            EventHandler handler = BestPosRequiredChangedEvent;
            BestPosRequiredChangedEventArgs args = new BestPosRequiredChangedEventArgs();
            args.IsBestPosRequired = IsBestPosRequired;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        [Publishes(InvDatesChanged)]
        public event EventHandler InvDatesChangedEvent;
        private void InvDatesChangedFire()
        {
            EventHandler handler = InvDatesChangedEvent;
            InvoiceDatesChangedEventArgs args = new InvoiceDatesChangedEventArgs();
            args.InvoiceDate = VmInvDate;
            args.InvoiceDueDate = VmInvDueDate;
            _invoice.CalculateTotals();
            args.BaseAmount = VmInvTotalAmountDecimal;

            if (handler != null)
            {
                handler(this, (EventArgs)args);
            }
        }

        [Publishes(InvoiceValidationOptionChanged)]
        public event EventHandler InvoiceValidationOptionChangedEvent;


        [Publishes(SendMailEvent)]
        public event EventHandler SendMailEventHandler;
        internal void SendMailEventFire(string xmlFileName, string pdfFileName,
                                                   string subject,
                                                   string mailBody,
                                                   string sendTo, List<string> attachments)
        {
            SaveAsPdfAndSendMailEventArgs arg = new SaveAsPdfAndSendMailEventArgs();
            arg.XmlFilename = xmlFileName;
            arg.PdfFilename = pdfFileName;
            arg.MailBody = mailBody;
            arg.SendTo = sendTo;
            arg.Subject = subject;

            EventHandler handler = SendMailEventHandler;
            if (handler != null)
            {
                handler(this, (EventArgs)arg);
            }
        }

        #endregion

        #region Properties

        private const string RulesetBund = "Bund";
        #region Kopfdaten

        public string VmDocRef
        {
            get
            {
                string retVal = InvTypes.DropDownList.Find(p => p.Code == _vmDocType).DisplayText;
                return retVal;

            }
        }

        private string _vmDocType;
        public string VmDocType
        {
            get
            {
                // return _invoice.DocumentType.ToString();                 
                return _vmDocType;
            }
            set
            {
                if (_vmDocType == value) return;
                DocumentTypeType invType;

                string val = value;
                if (val == "CancelCreditMemo")
                {
                    val = "Invoice";
                    RelatedDoc.RefTypeSelected = RelatedDocumentViewModel.RefType.Storno;
                }
                if (val == "CancelInvoice")
                {
                    val = "CreditMemo";
                    RelatedDoc.RefTypeSelected = RelatedDocumentViewModel.RefType.Storno;
                }
                if (Enum.TryParse(val, true, out invType))
                {
                    _vmDocType = value;
                    _invoice.DocumentType = invType;
                    OnPropertyChanged();
                    OnProtectedPropertyChanged(VmDocRef, "VmDocRef");
                    OnUpdateSelection(_vmDocType);
                }
            }
        }
        public string VmInvTitle
        {
            get { return _invoice.DocumentTitle; }
            set
            {
                if (_invoice.DocumentTitle == value) return;
                _invoice.DocumentTitle = value;
                OnPropertyChanged();
            }
        }

        // private string _vmComment;
        public string VmComment
        {
            get
            {
                return _invoice.Comment;
            }
            set
            {
                if (_invoice.Comment == value)
                    return;
                if (value != null)
                {
                    var val = value.Split('\v');
                    string res = string.Join(Environment.NewLine, val);
                    _invoice.Comment = res;

                }
                else
                {
                    _invoice.Comment = null;
                }
                OnPropertyChanged();
            }
        }


        //[Required(AllowEmptyStrings = false,ErrorMessage = "Die Rechnungsnummer darf nicht leer sein.")]
        //[DisplayName("Rechnungsstellername")]
        [StringLengthValidator(1, 255, MessageTemplate = "RF00028 Die Rechnungsnummer darf nicht leer sein.", Tag = "Rechnungsnummer")]
        public string VmInvNr
        {
            get { return _invoice.InvoiceNumber; }
            set
            {
                if (_invoice.InvoiceNumber == value) return;
                _invoice.InvoiceNumber = value;
                OnPropertyChanged();
            }
        }

        [RelativeDateTimeValidator(-6, DateTimeUnit.Month,
            6, DateTimeUnit.Month, Tag = "Rechnungsdatum",
            MessageTemplate = "RF00029 Das Rechnungsdatum darf max. 6 Monate in der Vergangenheit oder in der Zukunft sein.")]
        public DateTime VmInvDate
        {
            get { return _invoice.InvoiceDate; }
            set
            {
                if (_invoice.InvoiceDate == value) return;
                _invoice.InvoiceDate = value;
                OnPropertyChanged();
                //_paymentConditions.InvoiceDueDate = value.AddDays(_dueDays);
                _paymentConditions.InvoiceDate = value;
                OnPropertyChanged("VmInvDueDate");
                InvDatesChangedFire();
                OnUpdateDocTable(PaymentConditions, "PaymentConditions");
            }
        }
        #endregion

        #region Biller
        [StringLengthValidator(1, 255, MessageTemplate = "RS00030 Rechnungssteller: {2} darf nicht leer sein.", Tag = "Name")]
        public string VmBillerName
        {
            get { return _invoice.Biller.Address.Name; }
            set
            {
                if (_invoice.Biller.Address.Name == value)
                    return;
                _invoice.Biller.Address.Name = value;
                OnPropertyChanged();
            }
        }

        [StringLengthValidator(1, 255, MessageTemplate = "RS00031 Rechnungssteller: Strasse darf nicht leer sein.", Tag = "Strasse")]
        public string VmBillerStreet
        {
            get { return _invoice.Biller.Address.Street; }
            set
            {
                if (_invoice.Biller.Address.Street == value)
                    return;
                _invoice.Biller.Address.Street = value;
                OnPropertyChanged();
            }
        }

        [StringLengthValidator(1, 10, MessageTemplate = "RS00032 Rechnungssteller: PLZ darf nicht leer und max. {5} Stellen lang sein.", Tag = "PLZ")]
        public string VmBillerPlz
        {
            get { return _invoice.Biller.Address.ZIP; }
            set
            {
                if (_invoice.Biller.Address.ZIP == value)
                    return;
                _invoice.Biller.Address.ZIP = value;
                OnPropertyChanged();
            }
        }

        [StringLengthValidator(1, 255, MessageTemplate = "RS00033 Rechnungssteller: Ort darf nicht leer sein.", Tag = "Ort")]
        public string VmBillerTown
        {
            get { return _invoice.Biller.Address.Town; }
            set
            {
                if (_invoice.Biller.Address.Town == value)
                    return;
                _invoice.Biller.Address.Town = value;
                OnPropertyChanged();
            }
        }

        public string VmBillerCountry
        {
            get { return _invoice.Biller.Address.Country.CountryCodeText; }
            set
            {
                if (_invoice.Biller.Address.Country.CountryCodeText == value)
                    return;
                _invoice.Biller.Address.Country.CountryCodeText = value;
                OnPropertyChanged();
                OnUpdateSelection(_invoice.Biller.Address.Country.CountryCodeText);
            }
        }

        [Validation.GLNValidatorAttribute(MessageTemplate = "RS00034 Rechnungssteller:", Tag = "GLN")]
        [Validation.GLNValidatorAttribute(MessageTemplate = "RS00034 Rechnungssteller:", Tag = "GLN-Bund", Ruleset = RulesetBund)]
        public string VmBillerGln
        {
            get { return GetGln(_invoice.Biller.Address.AddressIdentifier); }
            set
            {
                string gln = GetGln(_invoice.Biller.Address.AddressIdentifier);
                if (gln == value)
                    return;
                _invoice.Biller.Address.AddressIdentifier = SetGln(_invoice.Biller.Address.AddressIdentifier, value);
                OnPropertyChanged();
            }
        }

        public string VmBillerContact
        {
            get { return _invoice.Biller.Address.Contact; }
            set
            {
                if (_invoice.Biller.Address.Contact == value) return;
                _invoice.Biller.Address.Contact = value;
                OnPropertyChanged();
            }
        }

        public string VmBillerphone
        {
            get { return _invoice.Biller.Address.Phone; }
            set
            {
                if (_invoice.Biller.Address.Phone == value) return;
                _invoice.Biller.Address.Phone = value;
                OnPropertyChanged();
            }
        }

        [Validation.EmailAddressValidatorAttribute(true, MessageTemplate = "RS00035 Rechnungssteller: EMail kann leer sein oder muss ein gültiges Format besitzen.", Tag = "EMail")]
        [Validation.EmailAddressValidatorAttribute(false, MessageTemplate = "RS00035 Rechnungssteller: EMail darf nicht leer sein und muss ein gültiges Format besitzen.", Ruleset = RulesetBund, Tag = "EMail")]
        public string VmBillerMail
        {
            get { return _invoice.Biller.Address.Email; }
            set
            {
                if (_invoice.Biller.Address.Email == value) return;
                _invoice.Biller.Address.Email = value;
                OnPropertyChanged();
            }
        }


        [ValidatorComposition(CompositionType.Or, MessageTemplate = "RS00036 Rechnungssteller:  USt-Id '{0}' ist nicht gültig..", Tag = "Ust-Id")]
        [NotNullValidator(Negated = true, MessageTemplate = "RS00037 Rechnungssteller:  USt-Id kann leer sein oder muss ein gültiges Format besitzen.", Tag = "USt-Id")]
        [RegexValidator(@"(^[A-Z]{2}\d{9}[B]\d{2})|(^[A-Z]{2,3}\d{8,12})|([0]{8})", MessageTemplate = "RS00038 Rechnungssteller: USt-Id '{0}' ist nicht gültig.", Tag = "Ust-ID")]
        public string VmBillerVatid
        {
            get { return _invoice.Biller.VATIdentificationNumber; }
            set
            {
                if (_invoice.Biller.VATIdentificationNumber == value) return;
                _invoice.Biller.VATIdentificationNumber = value;
                OnPropertyChanged();
            }
        }

        public string VmRecSalutation
        {
            get { return _invoice.Biller.Address.Salutation; }
            set
            {
                if (_invoice.Biller.Address.Salutation == value) return;
                _invoice.Biller.Address.Salutation = value;
                OnPropertyChanged();
            }
        }

        [StringLengthValidator(0, 35, MessageTemplate = "RS00039 Rechnungssteller: Die Lieferantennummer darf max. {5} Zeichen lang sein.", Tag = "Lieferanten-Nr.")]
        [StringLengthValidator(1, RangeBoundaryType.Inclusive, 35, RangeBoundaryType.Ignore, MessageTemplate = "RS00040 Rechnungssteller: Die Lieferantennummer darf nicht leer sein und muss den Vorgaben lt. eRechnung an die öffentliche Verwaltung entsprechen.", Tag = "Lieferanten-Nr. (Bund)", Ruleset = RulesetBund)]
        public string VmLiefantenNr
        {
            get
            {
                if (_invoice.Biller.InvoiceRecipientsBillerID == null)
                {
                    return string.Empty;
                }
                return _invoice.Biller.InvoiceRecipientsBillerID;
            }
            set
            {
                if (_invoice.Biller.InvoiceRecipientsBillerID == value) return;
                _invoice.Biller.InvoiceRecipientsBillerID = value;
                OnPropertyChanged();
            }
        }

        public string VmOrderReference
        {
            get { return _invoice.InvoiceRecipient.OrderReference.OrderID == null ? "" : _invoice.InvoiceRecipient.OrderReference.OrderID.Trim().ToUpper(); }
            set
            {
                if (_invoice.InvoiceRecipient.OrderReference.OrderID == value) return;
                OnPropertyChanged();
                if (string.IsNullOrEmpty(value))
                {
                    _invoice.InvoiceRecipient.OrderReference.OrderID = value;
                    _invoice.InvoiceRecipient.BestellPositionErforderlich = false;
                    OnPropertyChanged("IsBestPosRequired");
                }
                else
                {
                    _invoice.InvoiceRecipient.OrderReference.OrderID =
                        CurrentSelectedValidation == InvoiceSubtypes.ValidationRuleSet.Government ? value.ToUpper() : value;
                    string msg;
                    bool isReq = IsBestPosRequired;
                    var x = _invoice.InvoiceRecipient.OrderReference.OrderID.IsValidOrderRefBund(out msg, out isReq);
                    _invoice.InvoiceRecipient.BestellPositionErforderlich = isReq;
                    OnPropertyChanged("IsBestPosRequired");
                }
            }
        }

        // private bool _isBestPosRequired;
        /// <summary>
        /// Comment
        /// </summary>
        public bool IsBestPosRequired
        {
            get { return _invoice.InvoiceRecipient.BestellPositionErforderlich; }
            set
            {
                if (_invoice.InvoiceRecipient.BestellPositionErforderlich == value)
                    return;
                _invoice.InvoiceRecipient.BestellPositionErforderlich = value;
                OnPropertyChanged();
                // BestPosRequiredChangedFire();
            }
        }

        public DateTime? VmOrderDate
        {
            get
            {
                if (_invoice.Biller.OrderReference.ReferenceDateSpecified)
                {
                    return _invoice.Biller.OrderReference.ReferenceDate;
                }

                return null;
            }
            set
            {

                if (_invoice.Biller.OrderReference.ReferenceDateSpecified && _invoice.Biller.OrderReference.ReferenceDate == value) return;
                _invoice.Biller.OrderReference.ReferenceDate = value ?? new DateTime();
                _invoice.Biller.OrderReference.ReferenceDateSpecified = !(value == null);
                OnPropertyChanged();
            }
        }

        #endregion

        #region InvoiceReceipient
        [StringLengthValidator(1, 255, MessageTemplate = "RE00041 Rechnungsempfänger: Name darf nicht leer sein.", Tag = "Name")]
        public string VmRecName
        {
            get { return _invoice.InvoiceRecipient.Address.Name; }
            set
            {
                if (_invoice.InvoiceRecipient.Address.Name == value) return;
                _invoice.InvoiceRecipient.Address.Name = value;
                OnPropertyChanged();
            }
        }

        [StringLengthValidator(1, 255, MessageTemplate = "RE00042 Rechnungsempfänger: Strasse darf nicht leer sein.", Tag = "Strasse")]
        public string VmRecStreet
        {
            get { return _invoice.InvoiceRecipient.Address.Street; }
            set
            {
                if (_invoice.InvoiceRecipient.Address.Street == value) return;
                _invoice.InvoiceRecipient.Address.Street = value;
                OnPropertyChanged();
            }
        }

        [StringLengthValidator(1, 255, MessageTemplate = "RE00043 Rechnungsempfänger: PLZ darf nicht leer sein.", Tag = "PLZ")]
        public string VmRecPlz
        {
            get { return _invoice.InvoiceRecipient.Address.ZIP; }
            set
            {
                if (_invoice.InvoiceRecipient.Address.ZIP == value) return;
                _invoice.InvoiceRecipient.Address.ZIP = value;
                OnPropertyChanged();
            }
        }

        [StringLengthValidator(1, 255, MessageTemplate = "RE00044 Rechnungsempfänger: Ort darf nicht leer sein.", Tag = "Ort")]
        public string VmRecTown
        {
            get { return _invoice.InvoiceRecipient.Address.Town; }
            set
            {
                if (_invoice.InvoiceRecipient.Address.Town == value) return;
                _invoice.InvoiceRecipient.Address.Town = value;
                OnPropertyChanged();
            }
        }

        public string VmRecCountry
        {
            get { return _invoice.InvoiceRecipient.Address.Country.CountryCodeText; }
            set
            {
                if (_invoice.InvoiceRecipient.Address.Country.CountryCodeText == value) return;

                _invoice.InvoiceRecipient.Address.Country.CountryCodeText = value;
                OnPropertyChanged();
                OnUpdateSelection(_invoice.InvoiceRecipient.Address.Country.CountryCodeText);
            }
        }

        [Validation.GLNValidatorAttribute(MessageTemplate = "RE00045 Rechnungsempfänger:", Tag = "GLN")]
        [Validation.GLNValidatorAttribute(MessageTemplate = "RE00045 Rechnungsempfänger:", Tag = "GLN-Bund", Ruleset = RulesetBund)]
        public string VmRecGln
        {
            get { return GetGln(_invoice.InvoiceRecipient.Address.AddressIdentifier); ; }
            set
            {
                string gln = GetGln(_invoice.InvoiceRecipient.Address.AddressIdentifier);
                if (gln == value) return;
                _invoice.InvoiceRecipient.Address.AddressIdentifier = SetGln(_invoice.InvoiceRecipient.Address.AddressIdentifier, value);
                OnPropertyChanged();
            }
        }

        public string VmRecContact
        {
            get { return _invoice.InvoiceRecipient.Address.Contact; }
            set
            {
                if (_invoice.InvoiceRecipient.Address.Contact == value) return;
                _invoice.InvoiceRecipient.Address.Contact = value;
                OnPropertyChanged();
            }
        }

        public string VmRecPhone
        {
            get { return _invoice.InvoiceRecipient.Address.Phone; }
            set
            {
                if (_invoice.InvoiceRecipient.Address.Phone == value) return;
                _invoice.InvoiceRecipient.Address.Phone = value;
                OnPropertyChanged();
            }
        }

        [Validation.EmailAddressValidatorAttribute(true, MessageTemplate = "RE00048 Rechnungsempfänger: EMail kann leer sein oder muss ein gültiges Format besitzen.", Tag = "EMail")]
        public string VmRecMail
        {
            get { return _invoice.InvoiceRecipient.Address.Email; }
            set
            {
                if (_invoice.InvoiceRecipient.Address.Email == value) return;
                _invoice.InvoiceRecipient.Address.Email = value;
                OnPropertyChanged();
            }
        }

        [RegexValidator(@"^(?!\s*$).+", MessageTemplate = "RE00095 Rechnungsempfänger: Ust-Id darf nicht leer sein.", Tag = "Ust-ID")]
        [RegexValidator(@"(^[A-Z]{2}\d{9}[B]\d{2})|(^[A-Z]{2,3}\d{8,12})|([0]{8})", MessageTemplate = "RE00094 Rechnungsempfänger: Ust-Id '{0}' ist ungültig.", Tag = "Ust-ID")]
        public string VmRecVatid
        {
            get { return _invoice.InvoiceRecipient.VATIdentificationNumber; }
            set
            {
                if (_invoice.InvoiceRecipient.VATIdentificationNumber == value) return;
                _invoice.InvoiceRecipient.VATIdentificationNumber = value;
                OnPropertyChanged();
            }
        }

        public string VmKundenNr
        {
            get { return _invoice.InvoiceRecipient.BillersInvoiceRecipientID; }
            set
            {
                if (_invoice.InvoiceRecipient.BillersInvoiceRecipientID == value) return;
                _invoice.InvoiceRecipient.BillersInvoiceRecipientID = value;
                OnPropertyChanged();
            }
        }
        public string VmSubOrganisation
        {
            get { return _invoice.InvoiceRecipient.SubOrganizationID; }
            set
            {
                if (_invoice.InvoiceRecipient.SubOrganizationID == value) return;
                _invoice.InvoiceRecipient.SubOrganizationID = value;
                OnPropertyChanged();
            }
        }

        public string VmAcctArea
        {
            get { return _invoice.InvoiceRecipient.AccountingArea; }
            set
            {
                if (_invoice.InvoiceRecipient.AccountingArea == value) return;
                _invoice.InvoiceRecipient.AccountingArea = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Delivery
        [NotNullValidator(MessageTemplate = "RL00000 Das Lieferdatum darf nicht leer sein.", Tag = "Lieferdatum")]
        public DateTime? VmLieferDatum
        {
            get
            {
                if (_invoice.Delivery.Item != null)
                {
                    if (_invoice.Delivery.Item is DateTime)
                    {
                        return (DateTime?)_invoice.Delivery.Item;
                    }
                    else
                    {
                        return ((PeriodType)_invoice.Delivery.Item).FromDate;
                    }
                }
                return null;
            }
            set
            {

                if (_invoice.Delivery.Item == null)
                {
                    _invoice.Delivery.Item = new PeriodType();
                }

                if (_invoice.Delivery.Item is DateTime)
                {
                    DateTime? lieferdatum = _invoice.Delivery.Item as DateTime?;
                    if (lieferdatum == value) return;
                    _invoice.Delivery.Item = new PeriodType() { FromDate = value }; // (DateTime)(value ?? new DateTime());
                }
                else
                {
                    ((PeriodType)_invoice.Delivery.Item).FromDate = value;
                }
                OnPropertyChanged();
            }
        }

        #endregion

        #region Summenteil

        public string VmInvTotalNetAmountText
        {
            get { return PlugInSettings.Default.VStBerechtigt ? _reText1[0] : _reText1[1]; }
        }

        public string VmInvTotalNetAmount
        {
            get { return (_invoice.NetAmount ?? 0).Decimal2(); }
            set
            {
                decimal dec;
                if (decimal.TryParse((string)value, out dec))
                {
                    _invoice.NetAmount = null;
                }
                if (_invoice.NetAmount == dec)
                    return;
                _invoice.NetAmount = dec;
                OnProtectedPropertyChanged(_invoice.NetAmount.Value.Decimal2());
            }
        }

        public string VmInvTaxText
        {
            get { return PlugInSettings.Default.VStBerechtigt ? _reText2[0] : string.Format(_reText2[1], PlugInSettings.Default.VStText); }

        }

        public string VmInvTaxAmount
        {
            get { return PlugInSettings.Default.VStBerechtigt ? ((_invoice.TaxAmount ?? 0).Decimal2()) : "0,00"; }
        }

        public string VmInvTotalAmountText
        {
            get { return PlugInSettings.Default.VStBerechtigt ? _reText3[0] : _reText3[1]; }
        }

        public decimal VmInvTotalAmountDecimal
        {
            get { return _invoice.TotalGrossAmount ?? 0; }
        }
        public string VmInvTotalAmount
        {
            get { return (_invoice.TotalGrossAmount ?? 0).Decimal2(); }
            set
            {
                decimal dec;
                if (decimal.TryParse((string)value, out dec))
                {
                    _invoice.TotalGrossAmount = null;
                }
                if (_invoice.TotalGrossAmount == dec)
                    return;
                _invoice.TotalGrossAmount = dec;
                _paymentConditions.BaseAmount = dec;
                OnProtectedPropertyChanged(_invoice.TotalGrossAmount.Value.Decimal2());
            }
        }

        private string _vmInvCurrency;
        public string VmInvCurrency
        {
            get { return _invoice.InvoiceCurrency.ToString(); }
            set
            {
                if (_vmInvCurrency == value) return;
                CurrencyType curr;
                if (!Enum.TryParse(value, out curr))
                {
                    curr = CurrencyType.EUR;
                }
                _vmInvCurrency = value;
                _invoice.InvoiceCurrency = curr;
                OnPropertyChanged();
                OnUpdateSelection(_vmInvCurrency);
            }
        }
        #endregion

        #region Payment Conditions

        public DateTime? VmInvDueDateNullable
        {
            get
            {
                if (VmInvDueDate == DateTime.MinValue)
                {
                    return null;
                }
                return VmInvDueDate;
            }
            set
            {
                VmInvDueDate = value ?? DateTime.MinValue;
            }
        }

        //[Validation.PropertyComparisonValidator("VmInvDate", ComparisonOperator.GreaterThanEqual, 
        //    MessageTemplate = "RF00049 Das Fälligkeitsdatum darf nicht vor dem Rechnungsdatum liegen.", Tag = "Fälligkeitsdatum")]
        //[Validation.PropertyComparisonValidator("Today", ComparisonOperator.GreaterThanEqual, 
        //    MessageTemplate = "RF00049 Das Fälligkeitsdatum darf nicht vor dem heutigen Tag liegen.", Tag = "Fälligkeitsdatum")]
        public DateTime VmInvDueDate
        {
            get
            {
                if (_paymentConditions == null)
                {
                    return new DateTime();
                }

                return _paymentConditions.InvoiceDueDate;
            }
            set
            {
                // if (_paymentConditions.InvoiceDueDate == value) return;

                _paymentConditions.InvoiceDueDate = value;
                OnPropertyChanged();
                InvDatesChangedFire();
                //OnUpdateDocTable(PaymentConditions, "PaymentConditions");
                // _paymentConditions.InvoiceDueDays = value.Days(_invoice.InvoiceDate);
            }
        }

        //private int _dueDays;
        ///// <summary>
        ///// Comment
        ///// </summary>
        //public int DueDays
        //{
        //    get { return _dueDays; }
        //    set
        //    {
        //        if (_dueDays == value)
        //            return;
        //        _dueDays = value;
        //        OnPropertyChanged();
        //    }
        //}
        private SkontoViewModels _paymentConditions;
        public SkontoViewModels PaymentConditions
        {
            get
            {
                // _paymentConditions.LoadFromInvoice(_invoice);
                return _paymentConditions;
            }
            set
            {
                _paymentConditions = value;
                if (_paymentConditions != null)
                {
                    _invoice.PaymentConditions = _paymentConditions.GetPaymentConditions(VmInvDueDate);
                    OnUpdateDocTable(value);
                    OnPropertyChanged("VmInvDueDate");
                }
            }
        }

        #endregion

        #region Paymentmethode -Bank Details

        public string VmKtoBankName
        {
            get { return _bankTx.BeneficiaryAccount[0].BankName; }
            set
            {
                if (_bankTx.BeneficiaryAccount[0].BankName == value) return;
                _bankTx.BeneficiaryAccount[0].BankName = value;
                OnPropertyChanged();
            }
        }

        public string VmKtoBic
        {
            get { return _bankTx.BeneficiaryAccount[0].BIC; }
            set
            {
                if (_bankTx.BeneficiaryAccount[0].BIC == value) return;
                _bankTx.BeneficiaryAccount[0].BIC = value;
                OnPropertyChanged();
            }
        }

        public string VmKtoIban
        {
            get { return _bankTx.BeneficiaryAccount[0].IBAN; }
            set
            {
                if (_bankTx.BeneficiaryAccount[0].IBAN == value) return;
                _bankTx.BeneficiaryAccount[0].IBAN = value;
                OnPropertyChanged();
            }
        }

        public string VmKtoOwner
        {
            get { return _bankTx.BeneficiaryAccount[0].BankAccountOwner; }
            set
            {
                if (_bankTx.BeneficiaryAccount[0].BankAccountOwner == value) return;
                _bankTx.BeneficiaryAccount[0].BankAccountOwner = value;
                OnPropertyChanged();
            }
        }

        // private string _vmKtoReference;

        public string VmKtoReference
        {
            get
            {
                if (_bankTx.PaymentReference != null)
                    return _bankTx.PaymentReference.Value;
                return null;
            }
            set
            {
                if (_bankTx.PaymentReference == null)
                {
                    _bankTx.PaymentReference = new PaymentReferenceType();
                }
                _bankTx.PaymentReference.Value = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #endregion

        #region DropDownLists

        private DropDownListViewModels _currencyList;
        public DropDownListViewModels CurrencyList
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

        private DropDownListViewModels _countryCodeList;
        public DropDownListViewModels CountryCodeList
        {
            get { return _countryCodeList; }
            set
            {
                if (_countryCodeList == value)
                    return;
                _countryCodeList = value;
                OnPropertyChanged();
            }
        }


        // private InvoiceSubtype.Target _invVariant;
        public InvoiceSubtypes.ValidationRuleSet CurrentSelectedValidation
        {
            get
            {
                return _invoice.InvoiceSubtype.VariantOption;
            }
            set
            {
                //if (_invoice.InvoiceSubtype.VariantOption == value)
                //    return;
                // _invoice.InvoiceSubtype.VariantOption = value;
                var subt = (InvoiceSubtypes.ValidationRuleSet)value;
                //subt = InvoiceSubtypes.ValidationRuleSet.Government;
                _invoice.InvoiceSubtype = InvoiceSubtypes.GetSubtype(subt);
                //    OnUpdateSelection(_invoice.InvoiceSubtype.VariantOption.ToString());
                _invTypes.GetList(_documentTypes.GetDocumentTypes(subt));
                OnInvoiceValidationOptionChanged();
                OnPropertyChanged();
                OnPropertyChanged("InvTypes");
            }
        }

        private DropDownListViewModels _invoiceVariantList;
        public DropDownListViewModels InvoiceVariantList
        {
            get { return _invoiceVariantList; }
            set
            {
                if (_invoiceVariantList == value)
                    return;
                _invoiceVariantList = value;
                OnPropertyChanged();
            }
        }


        private DropDownListViewModels _invTypes;
        public DropDownListViewModels InvTypes
        {
            get { return _invTypes; }
            set
            {
                if (_invTypes == value)
                    return;
                _invTypes = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region ViewModel Lists

        public VatViewModels VatView
        {
            get
            {
                _invoice.Tax = TaxType.GetTaxTypeList(_invoice.Details.ItemList, !PlugInSettings.Default.VStBerechtigt, PlugInSettings.Default.VStText);
                _invoice.CalculateTotals();
                return VatViewModels.Load(_invoice.Tax);
            }
        }


        private RelatedDocumentViewModel _relatedDoc;
        public RelatedDocumentViewModel RelatedDoc
        {
            get { return _relatedDoc; }
            set
            {
                if (_relatedDoc == value)
                    return;
                _relatedDoc = value;
                OnPropertyChanged();
            }
        }


        #region Detailzeilen

        // Kopftext

        public string VmKopfText
        {
            get { return _invoice.Details.HeaderDescription; }
            set
            {
                if (_invoice.Details.HeaderDescription == value)
                    return;
                _invoice.Details.HeaderDescription = value;
                OnPropertyChanged();
            }
        }

        // Fusstext
        public string VmFussText
        {
            get { return _invoice.Details.FooterDescription; }
            set
            {
                if (_invoice.Details.FooterDescription == value)
                    return;
                _invoice.Details.FooterDescription = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Liste der Detailzeilen der Rechnung
        /// </summary>
        public BindingList<DetailsViewModel> DetailsView
        {
            get
            {
                //_invoice.Tax = TaxType.GetTaxTypeList(_invoice.Details.ItemList, !PlugInSettings.Default.VStBerechtigt, PlugInSettings.Default.VStText);
                return DetailsListConverter.Load(_invoice.Details.ItemList, _uc, IsBestPosRequired).DetailsList;
            }
            set
            {
                //if (_detailsView == value)
                //    return;
                //_detailsView = value;
                _invoice.Details.ItemList = DetailsListConverter.ConvertToItemList(value, VmOrderReference);
                _invoice.Tax = TaxType.GetTaxTypeList(_invoice.Details.ItemList, !PlugInSettings.Default.VStBerechtigt, PlugInSettings.Default.VStText);
                _invoice.CalculateTotals();
                //OnUpdateDocTable(value);
                //OnUpdateDocTable(VatView, "VatView");
                // FireProtectedPropertyChanged();
            }
        }

        #endregion

        #endregion

        #region commands
        private RibbonCommandButton _clearDocumentCommand;
        public RibbonCommandButton ClearDocumentCommand
        {
            get
            {
                _clearDocumentCommand = _clearDocumentCommand ?? new RibbonCommandButton(param => ClearDocumentClick());
                return _clearDocumentCommand;
            }
        }

        private RibbonCommandButton _editDetailsCommand;
        public RibbonCommandButton EditDetailsCommand
        {
            get
            {
                _editDetailsCommand = _editDetailsCommand ?? new RibbonCommandButton(param => EditDetails());
                return _editDetailsCommand;
            }
        }

        private RibbonCommandButton _editSkontoCommand;
        public RibbonCommandButton EditSkontoCommand
        {
            get
            {
                _editSkontoCommand = _editSkontoCommand ?? new RibbonCommandButton(param => EditSkontoClick());
                return _editSkontoCommand;
            }
        }

        private RibbonCommandButton _saveEbinterfaceCommand;
        public RibbonCommandButton SaveEbinterfaceCommand
        {
            get
            {
                _saveEbinterfaceCommand = _saveEbinterfaceCommand ?? new RibbonCommandButton(o => SaveEbinterfacClick(o));
                return _saveEbinterfaceCommand;
            }
        }

        private RibbonCommandButton _saveTemplateCommand;
        public RibbonCommandButton SaveTemplateCommand
        {
            get
            {
                _saveTemplateCommand = _saveTemplateCommand ?? new RibbonCommandButton(SaveTemplateClick);
                return _saveTemplateCommand;
            }
        }

        private RibbonCommandButton _loadTemplateCommand;
        public RibbonCommandButton LoadTemplateCommand
        {
            get
            {
                return _loadTemplateCommand = _loadTemplateCommand ?? new RibbonCommandButton(LoadTemplateClick);

            }
        }

        private RibbonCommandButton _verifyCommand;
        public RibbonCommandButton VerifyCommand
        {
            get
            {
                _verifyCommand = _verifyCommand ?? new RibbonCommandButton(VerifyClick);
                return _verifyCommand;
            }
        }

        private RibbonCommandButton _saveAndMailButton;
        public RibbonCommandButton SaveAndMailButton
        {
            get
            {
                _saveAndMailButton = _saveAndMailButton ?? new RibbonCommandButton(SaveAndMailClick);
                return _saveAndMailButton;
            }
        }

        #endregion
        #region Validierung
        public ValidationResults Results { get; set; } = new ValidationResults();
        public List<string> ProcessMessages { get; internal set; }
        #endregion
        #region Private Objekte
        private readonly string[] _reText1 = { "Rechnungsbetrag exkl. MwSt", "Rechnungsbetrag" };
        private readonly string[] _reText2 = { "Summe MwSt", "{0}: Summe MwSt" };
        private readonly string[] _reText3 = { "Rechnungsbetrag inkl. MwSt", "Gesamt" };

        internal IInvoiceType _invoice;
        private UniversalBankTransactionType _bankTx;
        // private readonly IPlugInSettings _settings;
        internal ProgressViewModel _progressView;
        internal ISaveFileDialog _saveDlg;
        internal IOpenFileDialog _openDlg;
        internal DocumentTypeModels _documentTypes;
        internal bool _checkForReceipientEmail = false;
        internal IStartProcessDienst _process;

        #endregion

        #region Konstruktor
        public InvoiceViewModel(

            IUnityContainer uc,
            DocumentTypeModels documentTypes,
            IInvoiceType invoice,
            IDialogService dialog, ISaveFileDialog saveDlg, IOpenFileDialog openDlg,
            RelatedDocumentViewModel relatedDoc, IStartProcessDienst process,
            ProgressViewModel progressView)
            : base(uc, dialog)
        {
            // _settings = settings;
            _invoice = invoice; // InvoiceFactory.CreateInvoice();
            _progressView = progressView;
            _saveDlg = saveDlg;
            _openDlg = openDlg;
            _process = process;
            _documentTypes = documentTypes;
            _countryCodeList = _uc.Resolve<DropDownListViewModels>();
            _countryCodeList.GetList(CountryCodes.GetCountryCodeList());
            _currencyList = _uc.Resolve<DropDownListViewModels>();
            _currencyList.GetList(Enum.GetNames(typeof(CurrencyType)).ToList());
            _invoiceVariantList = _uc.Resolve<DropDownListViewModels>();
            _invoiceVariantList.GetList(InvoiceSubtypes.GetList());
            _invTypes = _uc.Resolve<DropDownListViewModels>();
            _invTypes.GetList(_documentTypes.GetDocumentTypes(CurrentSelectedValidation));
            //CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Government;
            _invoice.InvoiceSubtype = InvoiceSubtypes.GetSubtype(InvoiceSubtypes.ValidationRuleSet.Government);
            _vmDocType = _invoice.DocumentType.ToString();
            // VatView = new VatViewModels();
            _relatedDoc = relatedDoc;
            _relatedDoc.CurrentSelectedValidation = CurrentSelectedValidation;
            _bankTx = GetBankTx();
            if (_invoice == null)
            {
                BillerSettings2Vm();
                // Clear();
            }
            else
            {
                if (_invoice.InitFromSettings)
                {
                    BillerSettings2Vm();
                }

                _invoice.CalculateTotals();

            }
            // FireProtectedPropertyChanged();
            DetailsView = DetailsListConverter.Load(_invoice.Details.ItemList, _uc, IsBestPosRequired).DetailsList;
            _paymentConditions = _uc.Resolve<SkontoViewModels>(new ParameterOverride("invVm", this)); // nötig, damit ein WErt vorhanden ...
            _paymentConditions.LoadFromInvoice(_invoice);
            _process.ProcessFinishedEvent += OnStartProcessDienstProcessFinished;
        }

        #endregion

        #region RibbonCommandButton Methoden
        /// <summary>
        /// Prüft die aktuell erfasse Rechnung auf Gültigkeit und speichert diese dann als XML Datei
        /// </summary>
        internal string SaveEbinterfacClick(object o)
        {
            IsInvoiceValid();
            if (!Results.IsValid)
            {
                ShowErrorResult();
                return null;
            }
            string fn;
            if (o is string)
            {
                fn = (string)o;
            }
            else
            {
                _saveDlg.InitialDirectory = PlugInSettings.Default.PathToUnsignedInvoices;
                _saveDlg.Filter = "ebInterface XML (*.xml)|*.xml|Alle Dateien (*.*)|*.*";
                _saveDlg.DefaultExt = "xml";
                _saveDlg.FileName = MakeFileName("Rechng-", _saveDlg.DefaultExt);
                DialogResult rc = _dlg.ShowSaveFileDialog(_saveDlg);
                if (rc != DialogResult.OK)
                {
                    return null;
                }
                fn = _saveDlg.FileName;
            }
            if (!SaveEbinterface(fn))
            {
                return null;
            }
            ChangePending = false;
            return fn;
        }

        /// <summary>
        /// Zeigt einen Dialog zum Bearbeiten der Skonto Tabelle
        /// </summary>
        private void EditSkontoClick()
        {
            _invoice.CalculateTotals();
            var skontoView = _uc.Resolve<SkontoViewModels>(new ParameterOverride("invVm", this)); // PaymentConditions;
            // skontoView.LoadFromInvoiceViewModel(this);
            // skontoView.LoadFromInvoice(_invoice);
            var rc = _dlg.ShowDialog<FrmSkontoList>(skontoView);
            if (rc == DialogResult.OK)
            {
                PaymentConditions = skontoView;
                VmInvDate = skontoView.InvoiceDate;
                VmInvDueDate = skontoView.InvoiceDueDate;
            }
        }

        /// <summary>
        /// setzt den Inhalt aller Felder auf die Anfangswerte
        /// </summary>
        private void ClearDocumentClick()
        {
            ClearPanel();
            Clear();
        }

        /// <summary>
        /// Öffnet den Dialog zur Bearbeitung der Details
        /// </summary>
        private void EditDetails()
        {
            SetBestPosRequired(CurrentSelectedValidation);
            var detailsViewModel = _uc.Resolve<DetailsViewModels>(new ParameterOverrides() {
            { "bestPosRequired", IsBestPosRequired },
            {"currentRuleSet",CurrentSelectedValidation}
            });
            detailsViewModel.DetailsViewList = DetailsView;
            var rc = _dlg.ShowDialog<FrmDetailsList>(detailsViewModel);
            if (rc == DialogResult.OK)
            {
                DetailsView = detailsViewModel.DetailsViewList;
                UpdateView();
            }
        }
        /// <summary>
        /// Speichert den aktuellen Inhalt als Vorlage
        /// </summary>
        internal void SaveTemplateClick(object o)
        {
            string fn = "";
            if (o is string)
            {
                fn = (string)o;
            }
            else
            {
                _saveDlg.InitialDirectory = PlugInSettings.Default.PathToInvoiceTemplates;
                _saveDlg.Filter = "Vorlagen (*.xmlt,*.xml)|*.xmlt;*.xml|Alle Dateien (*.*)|*.*";
                _saveDlg.DefaultExt = "xmlt";
                _saveDlg.FileName = MakeFileName("Vorlage-", _saveDlg.DefaultExt);
                DialogResult rc = _dlg.ShowSaveFileDialog(_saveDlg);
                if (rc != DialogResult.OK)
                {
                    return;
                }
                fn = _saveDlg.FileName;
            }
            SaveXmlTemplate(fn);
            ChangePending = false;
        }

        /// <summary>
        /// Lädt eine Vorlage
        /// </summary>
        private void LoadTemplateClick(object o)
        {
            string fn = "";
            if (o is string)
            {
                fn = (string)o;
            }
            else
            {
                _openDlg.InitialDirectory = PlugInSettings.Default.PathToInvoiceTemplates;
                _openDlg.Filter = "Vorlagen (*.xmlt,*.xml)|*.xmlt;*xml|All files (*.*)|*.*";
                DialogResult rc = _dlg.ShowOpenFileDialog(_openDlg);
                if (rc != DialogResult.OK)
                {
                    return;
                }
                fn = _openDlg.FileName;
            }
            Clear();
            LoadTemplateWithProgressBar(fn);
            DocumentHomeKeyFire();
        }

        /// <summary>
        /// Prüft die aktuelle Rechnung auf ebInterface Konformität
        /// </summary>
        private void VerifyClick(object o)
        {
            IsInvoiceValid();
            if (o is bool)
            {
                if ((bool)o)
                {
                    return;
                }
            }
            ShowErrorResult();
        }

        /// <summary>
        /// prüft und sspeichert die aktuelle Rechnung, häng diese an eine Mail zum Versand
        /// </summary>
        /// <param name="o"></param>
        internal virtual void SaveAndMailClick(object o)
        {
            _checkForReceipientEmail = true;
            string saveComment = VmComment;
            if (!string.IsNullOrEmpty(VmComment))
            {
                VmComment += Environment.NewLine;
            }
            VmComment += "Diese Rechnung wurde auch in einem anderen Format versendet.";
            string fn = SaveEbinterfacClick(o);
            _checkForReceipientEmail = false;

            if (fn == null)
            {
                VmComment = saveComment;
                return;
            }
            string subj = SharedMethods.ReplaceToken(string.IsNullOrEmpty(PlugInSettings.Default.MailBetreff)
                ? PlugInSettings.Default.DefaultMailSubject
                : PlugInSettings.Default.MailBetreff, VmInvNr, VmInvDate, VmBillerName,
                VmBillerContact, VmBillerphone, VmBillerMail);
            string mailBody = SharedMethods.ReplaceToken(string.IsNullOrEmpty(PlugInSettings.Default.MailText)
                ? PlugInSettings.Default.DefaultMailBody
                : PlugInSettings.Default.MailText, VmInvNr, VmInvDate, VmBillerName,
                VmBillerContact, VmBillerphone, VmBillerMail);

            var path = System.IO.Path.GetDirectoryName(fn);
            var fn2 = System.IO.Path.GetFileNameWithoutExtension(fn) + ".pdf";
            string pdfFileName = System.IO.Path.Combine(path, fn2);
            SaveAsPdfEventFire(fn, pdfFileName);
            SendMailEventFire(fn, pdfFileName, subj, mailBody, VmRecMail, new List<string>());
            VmComment = saveComment;
        }

        /// <summary>
        /// Fügt die Validationresults dem Errorpane hinzu
        /// </summary>
        /// <param name="results"></param>
        /// <param name="tagDefault"></param>
        /// <param name="prefix"></param>
        internal void AddToErrorPane(ValidationResults results, string tagDefault, string prefix)
        {
            if (results.IsValid) return;
            string headLine = "Datenprüfung" + (CurrentSelectedValidation == InvoiceSubtypes.ValidationRuleSet.Government ? " für den Bund" : "");
            foreach (ValidationResult result in results)
            {
                Log.LogWrite(CallerInfo.Create(), Log.LogPriority.Low, "{0}; {1} ({3}); {2}", headLine,
                     string.IsNullOrEmpty(result.Tag) ? tagDefault : result.Tag, result.Message, result.Key);
                PublishToPanel(headLine, string.IsNullOrEmpty(result.Tag) ? tagDefault : result.Tag, "",
                    result.Message);
            }
        }

        private RibbonCommandButton _runZustellDienstButton;
        public RibbonCommandButton RunZustellDienstButton
        {
            get
            {
                _runZustellDienstButton = _runZustellDienstButton ?? new RibbonCommandButton(RunZustellDienstClick);
                return _runZustellDienstButton;
            }
        }

        public bool WaitForProcess = false; // Test Mockup
        private void RunZustellDienstClick(object o)
        {

            if (string.IsNullOrEmpty(PlugInSettings.Default.DeliveryExePath))
            {
                var rc = _dlg.ShowMessageBox("Der Versand via Zustelldienst ist nicht konfiguriert", "Versand", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string fn = SaveEbinterfacClick(o);
            if (!Results.IsValid) return;
            var arg = string.Format(PlugInSettings.Default.DeliveryArgs, fn, VmInvNr, VmBillerMail, VmRecMail);
            RunProcess(arg, PlugInSettings.Default.DeliveryExePath, PlugInSettings.Default.DeliveryWorkDir, "Zustelldienst");
        }

        internal bool RunProcess(string arg, string exePath, string workingDir, string title)
        {
            _waitEvent = new ManualResetEvent(false);
            ProcessMessages = new List<string>();
            var result = _process.Run(exePath, arg, workingDir, true);
            bool isConsole = StartProcessDienst.IsConsoleApplication(exePath);
            if ((WaitForProcess || isConsole) && result)
            {
                _waitEvent.WaitOne(60 * 1000); // max. 1 Minute warten, nötig, damit der Consolen-Output im Testergebnis erscheint
                PublishProcessMsg(_process, title);
            }
            else
            {
                PublishProcessMsg(_process, title);
            }
            return result;
        }

        internal ManualResetEvent _waitEvent;
        public void OnStartProcessDienstProcessFinished(object sender, EventArgs args)
        {
            //IStartProcessDienst sProc = (StartProcessDienst)sender;
            //PublishProcessMsg(sProc);
            _waitEvent.Set();
        }

        private void PublishProcessMsg(IStartProcessDienst sProc, string title)
        {
            string desc;
            desc = "Ausgabe";
            if (sProc.ErrorMessages.Count > 0)
            {
                ProcessMessages.AddRange(sProc.ErrorMessages);

                foreach (string message in sProc.ErrorMessages)
                {
                    PublishToPanel(title, desc, "", message);
                    desc = "";
                }
            }
            if (sProc.Messages.Count > 0)
            {
                ProcessMessages.AddRange(sProc.Messages);
                //desc = "Fehler";
                foreach (string message in sProc.Messages)
                {
                    PublishToPanel(title, desc, "", message);
                    desc = "";
                }
            }
        }
        #endregion

        #region Arbeits Methoden
        bool _clearVat = false;
        public bool NoUpdatePrompt = false;
        internal virtual void LoadTemplateWithProgressBar(string filename)
        {
            Log.TraceWrite(CallerInfo.Create(), "entering filename={0}", filename ?? "(null)");
            InvoiceType inv = InvoiceFactory.LoadTemplate(filename) as InvoiceType;

            DialogResult rc;
            _clearVat = false;
            if (!PlugInSettings.Default.VStBerechtigt)
            {
                if (inv.Biller.VATIdentificationNumber != PlugInSettings.VatIdDefaultOhneVstBerechtigung)
                {
                    rc = _dlg.ShowMessageBox(
                        "Die Vorlage enthält MwSt Daten. In den Einstellungen ist festgelegt, dass kein VSt Abzug erfolgt." + Environment.NewLine +
                        " Soll die Vorlage trotzdem geladen werden?", "Vorlage laden",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rc == DialogResult.No)
                    {
                        return;
                    }
                    _clearVat = true;
                }
            }
            _invoice = inv;
            if (_clearVat)
            {
                VmBillerVatid = PlugInSettings.VatIdDefaultOhneVstBerechtigung;
                VatSatzSetzen(0);
            }
            _invoice.CalculateTotals();
            _paymentConditions.LoadFromInvoice(_invoice);
            if (!NoUpdatePrompt)
            {
                var updateInvView = _uc.Resolve<UpdateInvoiceViewModel>(new ParameterOverride("invoice", _invoice));
                rc = _dlg.ShowDialog<FrmUpdateInvoiceView>(updateInvView);
                if (rc == DialogResult.OK)
                {
                    VmInvNr = updateInvView.InvoiceNumber;
                    VmInvDate = updateInvView.InvoiceDate;
                    VmInvDueDate = updateInvView.InvoiceDueDate;
                    VmLieferDatum = updateInvView.DeliveryDate;
                    // DueDays = updateInvView.DueDays;
                }
            }

            ClearPanel();
            PublishToPanel("Vorlage laden", "Dateiname", "I", filename);
            _progressView.Description = "Vorlage wird geladen ...";
            _progressView.Maximum = 8;
            _progressView.Minimum = 0;
            _progressView.Step = 1;
            _progressView.Value = 0;
            _progressView.CountTracking = "Starting ...";
            _progressView.DoWork = LoadTemplateWithBackGroundWorker;
            _dlg.ShowDialog<FrmShowProgress>(_progressView);
            if (_progressView.ex != null)
            {
                PublishToPanel(null, "Ergebnis", "E", "Datei konnte nicht geladen werden");
                PublishToPanel(null, "Fehler", "E", _progressView.ex.Message);
                PublishToPanel(null, "Stacktrace", "E", _progressView.ex.StackTrace);
                _progressView.ex = null;
            }
            else
            {
                PublishToPanel(null, "Ergebnis", "I", "Vorlage wurde erfolgreich geladen.");
            }
            GetSeparatedFileds();

        }

        internal virtual void GetSeparatedFileds()
        {
            var subtyp = _invoice.InvoiceSubtype;
            _invoice.InvoiceSubtype = InvoiceSubtypes.GetSubtype(InvoiceSubtypes.ValidationRuleSet.Invalid);
            if (subtyp.VariantOption == InvoiceSubtypes.ValidationRuleSet.Invalid)
            {
                PublishToPanel(null, "Rechnungstyp", "",
                    "Der Rechnungstyp konnte nicht erkannt werden und wurde auf 'Wirtschaft' gesetzt.");
                PublishToPanel(null, "", "", "ACHTUNG: Die eRechnung stammt möglicherweise nicht aus dem Word PlugIn.");
                CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Industries;
            }
            else
            {
                CurrentSelectedValidation = subtyp.VariantOption;
            }
            SetBestPosRequired(subtyp.VariantOption);
            if (_invoice.CancelledOriginalDocument != null)
            {
                RelatedDoc.AddRelatedDocument(_invoice.CancelledOriginalDocument);
            }
            else
            {
                if (_invoice.RelatedDocument.Any())
                {
                    if (_invoice.RelatedDocument.Count() > 1)
                    {
                        PublishToPanel(null, "Verweis", "", "Das geladene Dokument enthält mehr als einen Verweis. Es wird nur der erste Verweis geladen alle anderen gehen verloren.");
                    }
                    RelatedDoc.AddRelatedDocument(_invoice.RelatedDocument[0]);
                }
            }

        }

        private void SetBestPosRequired(InvoiceSubtypes.ValidationRuleSet subtyp)
        {
            if (subtyp == InvoiceSubtypes.ValidationRuleSet.Government)
            {
                bool isRequired = false;
                string msg;
                if (!_invoice.InvoiceRecipient.OrderReference.OrderID.IsValidOrderRefBund(out msg, out isRequired))
                {
                    PublishToPanel(null, "Auftragsreferenz", "", msg);
                }
                IsBestPosRequired = isRequired;
            }
        }

        internal void LoadTemplateWithBackGroundWorker(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;

                workerReportProgress(worker);
                string filename = _progressView.PayLoad as string;

                workerReportProgress(worker);
                _vmDocType = _invoice.DocumentType.ToString();
                //_invoice.Tax = TaxType.GetTaxTypeList(_invoice.Details.ItemList, !PlugInSettings.Default.VStBerechtigt, PlugInSettings.Default.VStText);
                _invoice.CalculateTotals();
                _bankTx = GetBankTx();
                // _paymentConditions = _uc.Resolve<SkontoViewModels>(new ParameterOverride("invoice", _invoice));
                workerReportProgress(worker);
                UpdateViewWithBackGroundWorker(sender, e);
            }

            catch (Exception exception)
            {
                _progressView.ex = exception;
                return;
            }

        }

        internal virtual void SaveXmlTemplate(string fileName)
        {
            UpdateInvoiceParts();
            _invoice.SaveTemplate(fileName);
        }

        internal bool SaveEbinterface(string filename)
        {
            ClearPanel();
            ebInterfaceResult ebIResult = _invoice.Save(filename);
            if (ebIResult.ResultType == ResultType.IsValid)
            {
                PublishToPanel(filename + " erfolgreich gespeichert.", "", "", "");
            }
            else
            {
                string msg = string.Format("Es wurden {0} Fehler gefunden.", ebIResult.ResultMessages.Count);
                foreach (ResultMessage result in ebIResult.ResultMessages)
                {
                    PublishToPanel(msg, result.Field, result.Severity.ToString().Substring(0, 1), result.Message);
                    Results.AddResult(new ValidationResult(result.Message, this, "", result.Field, null));
                }
            }
            ShowMessagePanel(true);
            return ebIResult.ResultType == ResultType.IsValid;
        }

        #endregion

        #region Servicemethoden
        public string MakeFileName(string prefix, string ext)
        {
            string fn = CleanFileName(prefix + VmInvNr + "." + ext);
            return fn;
        }
        private static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), ""));
        }
        private void NotImplemented()
        {
            MessageBox.Show("Not implemented");
        }
        private UniversalBankTransactionType GetBankTx()
        {
            if (_invoice.PaymentMethod != null)
            {
                if (_invoice.PaymentMethod.Item != null)
                {
                    if (_invoice.PaymentMethod.Item is UniversalBankTransactionType)
                    {
                        {
                            return (UniversalBankTransactionType)_invoice.PaymentMethod.Item;
                        }
                    }
                }
            }
            return null;
        }

        public virtual void Clear()
        {
            IInvoiceType inv = InvoiceFactory.CreateInvoice();
            _invoice = inv;
            VmLieferDatum = new DateTime();
            RelatedDoc.Clear();
            BillerSettings2Vm();
            VmInvDate = DateTime.Today;
            VmInvDueDate = DateTime.Today;
            PaymentConditions.Clear();
            // UpdateView();

            VmLieferDatum = null;
            DoWorkEventArgs args = new DoWorkEventArgs(true);
            UpdateViewWithBackGroundWorker(null, args);
        }

        /// <summary>
        /// Update aller Teile von _invoice, die nicht direkt verknüpft sind
        /// </summary>
        internal virtual void UpdateInvoiceParts()
        {
            _invoice.PaymentConditions = PaymentConditions.GetPaymentConditions(VmInvDueDate);
            var relDocs = RelatedDoc.GetRelatedDocumentEntry(CurrentSelectedValidation);
            _invoice.CancelledOriginalDocument = null;
            var type = relDocs as CancelledOriginalDocumentType;
            if (type != null)
            {
                _invoice.CancelledOriginalDocument = type;
                return;
            }
            _invoice.RelatedDocument.Clear();
            var docs = relDocs as RelatedDocumentType;
            if (docs != null)
            {
                _invoice.RelatedDocument.Add(docs);
                return;
            }

        }
        public bool IsInvoiceValid()
        {
            UpdateInvoiceParts();
            ValidationFactory.SetDefaultConfigurationValidatorFactory(new SystemConfigurationSource(false));
            ValidationResults results = new ValidationResults();
            Results = new ValidationResults();
            bool result = true;
            ClearPanel();
            if (CurrentSelectedValidation == InvoiceSubtypes.ValidationRuleSet.Government)
            {
                var bundValidator = ValidationFactory.CreateValidator<InvoiceViewModel>(RulesetBund);
                results = bundValidator.Validate(this);
                Results.AddAllResults(results);
                // AddToErrorPane(results, "", "");
                result &= results.IsValid;
            }
            var invoiceValidator = ValidationFactory.CreateValidator<InvoiceViewModel>();
            results = invoiceValidator.Validate(this);
            // Results.AddAllResults(results);
            foreach (ValidationResult validationResult in results)
            {
                string k = validationResult.Key;
                var res = Results.Where(p => p.Key == k);
                if (!res.Any())
                {
                    Results.AddResult(validationResult);
                }
            }
            AddToErrorPane(Results, "", "");
            result &= results.IsValid;

            // RuleSet
            string ruleset = (IsBestPosRequired) ? "BestPosRequired" : "";
            var detailsValidator = ValidationFactory.CreateValidator<DetailsViewModel>(ruleset);
            int i = 0;
            foreach (DetailsViewModel details in DetailsView)
            {
                results = detailsValidator.Validate(details);
                Results.AddAllResults(results);
                AddToErrorPane(results, string.Format("Pos. {0}", ++i), "");
                result &= results.IsValid;
            }
            ruleset = CurrentSelectedValidation == InvoiceSubtypes.ValidationRuleSet.Government ? SkontoViewModel.RuleSetBund : "";
            var skontoValidator = ValidationFactory.CreateValidator<SkontoViewModel>(ruleset);
            i = 0;
            foreach (SkontoViewModel model in PaymentConditions.SkontoList)
            {
                results = skontoValidator.Validate(model);
                Results.AddAllResults(results);
                AddToErrorPane(results, string.Format("Skonto {0}", model.SkontoFaelligDate), "");
                result &= results.IsValid;
            }

            return result;
        }

        internal void ShowErrorResult()
        {

            if (Results.IsValid)
            {
                _dlg.ShowMessageBox("Die eRechnung ist formal korrekt.", "Rechnung prüfen", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
            }
            else
            {
                ShowMessagePanel(true);
                _dlg.ShowMessageBox("Es wurden Fehler gefunden.", "Rechnung prüfen", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
            return;
        }
        internal void UpdateView()
        {
            Log.TraceWrite(CallerInfo.Create(), "Update View");
            UpdateView(true);
        }

        internal virtual void UpdateView(bool updateTables)
        {
            Log.TraceWrite(CallerInfo.Create(), "updateTables={0}", updateTables);
            //_invoice.Tax = TaxType.GetTaxTypeList(_invoice.Details.ItemList, !PlugInSettings.Default.VStBerechtigt, PlugInSettings.Default.VStText);
            _invoice.CalculateTotals();
            _bankTx = GetBankTx();
            CurrentSelectedValidation = _invoice.InvoiceSubtype.VariantOption;
            // ClearPanel();
            _progressView.Description = "Daten werden geladen ...";
            _progressView.Maximum = 6;
            _progressView.Minimum = 0;
            _progressView.Step = 1;
            _progressView.Value = 0;
            _progressView.PayLoad = updateTables;
            _progressView.CountTracking = "Starting ...";
            _progressView.DoWork = UpdateViewWithBackGroundWorker;
            _dlg.ShowDialog<FrmShowProgress>(_progressView);
            if (_progressView.ex != null)
            {
                PublishToPanel(null, "Ergebnis", "E", "Datei konnte nicht geladen werden");
                PublishToPanel(null, "Fehler", "E", _progressView.ex.Message);
                PublishToPanel(null, "Stacktrace", "E", _progressView.ex.StackTrace);
                _progressView.ex = null;
            }
        }

        private void workerReportProgress(BackgroundWorker worker)
        {
            Log.TraceWrite(CallerInfo.Create(), "Update progress");
            if (worker != null) worker.ReportProgress(1);
        }

        internal void UpdateViewWithBackGroundWorker(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = (BackgroundWorker)sender;
                Log.TraceWrite(CallerInfo.Create(), "entering with worker {0} null", (worker == null ? "==" : "!="));
                bool updateTables = true;
                if (e != null && e.Argument != null && e.Argument is bool)
                {
                    updateTables = (bool)e.Argument;
                }
                OnUpdateSelection(VmDocType, "VmDocType");
                OnUpdateSelection(VmBillerCountry, "VmBillerCountry");
                OnUpdateSelection(VmRecCountry, "VmRecCountry");
                OnUpdateSelection(VmInvCurrency, "VmInvCurrency");

                workerReportProgress(worker);

                if (updateTables)
                {
                    OnUpdateDocTable(DetailsView, "DetailsView");
                }
                workerReportProgress(worker);

                if (updateTables)
                {
                    OnUpdateDocTable(VatView, "VatView");
                }
                workerReportProgress(worker);

                if (updateTables)
                {
                    OnUpdateDocTable(PaymentConditions, "PaymentConditions");
                }
                workerReportProgress(worker);

                OnPropertyChanged("RelatedDoc");
                OnPropertyChanged(null);
                workerReportProgress(worker);

                FireProtectedPropertyChanged();
                workerReportProgress(worker);

            }
            catch (Exception ex)
            {

                _progressView.ex = ex;
                return;

            }
        }


        internal virtual void FireProtectedPropertyChanged()
        {
            OnProtectedPropertyChanged(VmInvTotalNetAmountText, "VmInvTotalNetAmountText");
            OnProtectedPropertyChanged(VmInvTotalNetAmount, "VmInvTotalNetAmount");
            OnProtectedPropertyChanged(VmInvTotalAmountText, "VmInvTotalAmountText");
            OnProtectedPropertyChanged(VmInvTotalAmount, "VmInvTotalAmount");
            OnProtectedPropertyChanged(VmInvTaxAmount, "VmInvTaxAmount");
            OnProtectedPropertyChanged(VmInvTaxText, "VmInvTaxText");
            OnProtectedPropertyChanged(VmDocRef, "VmDocRef");
        }


        private List<AddressIdentifierType> SetGln(List<AddressIdentifierType> adrList, string glnValue)
        {
            if (adrList == null)
            {
                adrList = new List<AddressIdentifierType>();
            }
            else
            {
                adrList.RemoveAll(x => (x.AddressIdentifierType1Specified && string.IsNullOrEmpty(x.Value)));
                // Alle GLNnummern entfernen
            }
            adrList.Add(new AddressIdentifierType()
            {
                AddressIdentifierType1Specified = true,
                Value = glnValue
            });
            return adrList;
        }

        private string GetGln(List<AddressIdentifierType> adrList)
        {
            if (adrList == null)
                return "";
            var glnSpec = adrList.Where(x => x.AddressIdentifierType1Specified == true);
            if (!glnSpec.Any())
                return "";

            var gln = glnSpec.First(g => g.AddressIdentifierType1 == AddressIdentifierTypeType.GLN);
            if (gln == null)
                return "";
            return gln.Value;

        }

        #endregion

        #region Eventhandler

        private void OnInvoiceValidationOptionChanged()
        {
            EventHandler handler = InvoiceValidationOptionChangedEvent;
            Log.TraceWrite(CallerInfo.Create(), "Entering, handler=" + (handler == null ? "not null" : "null"));
            if (handler != null)
            {
                Log.TraceWrite(CallerInfo.Create(), "Firing Event ");
                var args = new InvIndustryEventArgs();
                args.Industry = CurrentSelectedValidation;
                handler(this, args);
                Log.TraceWrite(CallerInfo.Create(), "Event finished");
            }
        }

        #endregion

        #region Subscriber

        [SubscribesTo(BillerSettingsViewModel.UpdateFromBillerSettings)]
        public void OnUpdateFromBillerSettings(object sender, EventArgs args)
        {
            BillerSettings2Vm();
            if (sender != null && ((BillerSettingsViewModel)sender).RecalcMwSt)
            {
                VatSatzSetzen(PlugInSettings.Default.MwStDefault);
            }
            UpdateView();
        }

        internal virtual void BillerSettings2Vm()
        {
            _invoice.Biller.Address.Name = PlugInSettings.Default.Name;
            _invoice.Biller.Address.Country.CountryCodeText = PlugInSettings.Default.Land;
            _invoice.Biller.Address.Street = PlugInSettings.Default.Strasse;
            _invoice.Biller.Address.Contact = PlugInSettings.Default.Contact;
            _invoice.Biller.Address.AddressIdentifier = new List<AddressIdentifierType>();
            _invoice.Biller.Address.AddressIdentifier = SetGln(_invoice.Biller.Address.AddressIdentifier, PlugInSettings.Default.BillerGln);
            _invoice.Biller.Address.Email = PlugInSettings.Default.Email;
            _invoice.Biller.Address.ZIP = PlugInSettings.Default.Plz;
            _invoice.Biller.Address.Town = PlugInSettings.Default.Ort;
            _invoice.Biller.Address.Phone = PlugInSettings.Default.TelNr;
            _vmInvCurrency = PlugInSettings.Default.Currency;
            _invoice.InvoiceCurrency = (CurrencyType)Enum.Parse(typeof(CurrencyType), _vmInvCurrency);

            _bankTx.BeneficiaryAccount[0].BankName = PlugInSettings.Default.Bank;
            _bankTx.BeneficiaryAccount[0].BankAccountOwner = PlugInSettings.Default.Kontowortlaut;
            _bankTx.BeneficiaryAccount[0].IBAN = PlugInSettings.Default.Iban;
            _bankTx.BeneficiaryAccount[0].BIC = PlugInSettings.Default.Bic;
            _invoice.PaymentMethod.Item = _bankTx;
            string oldVatId = _invoice.Biller.VATIdentificationNumber;
            _invoice.Biller.VATIdentificationNumber = PlugInSettings.Default.Vatid;

            //if (updateView)
            //{
            //    if ((oldVatId != PlugInSettings.Default.Vatid) &&
            //        (PlugInSettings.Default.Vatid == PlugInSettings.VatIdDefaultOhneVstBerechtigung))
            //    {
            //        VatSatzSetzen();
            //    }
            //    UpdateView();
            //}
        }

        private void VatSatzSetzen(decimal vatSatz)
        {
            var tempDetails = DetailsView;
            foreach (DetailsViewModel model in tempDetails)
            {
                model.VatSatz = vatSatz;
            }
            DetailsView = tempDetails; // Update erfolgt ausserhalb!
        }

        #endregion

        #region Selfvalidation

        [SelfValidation]
        public void InvoiceViewValidation(ValidationResults results)
        {

            #region Rechnungspositionen
            if (DetailsView.Count < 1)
            {
                results.AddResult(new ValidationResult("RE00089 Die Rechnung enthält keine Positionen.", this, "DetailsView", "Details", null));
            }
            if (DetailsView.Count > 999)
            {
                results.AddResult(new ValidationResult("RE00091 Die Rechnung darf max. 999 Positionen enthalten.", this, "DetailsView", "Details", null));
            }
            #endregion

            #region Empfänger Email
            if (_checkForReceipientEmail)
            {
                if (string.IsNullOrEmpty(VmRecMail))
                {
                    results.AddResult(new ValidationResult("RE00066 Rechnungsempfänger: E-Mail Adresse fehlt. Die Rechnung kann nicht versendet werden.", this, "VmRecMail", "E-Mail", null));
                }
            }
            #endregion

            #region Empfänger UStID
            if (!string.IsNullOrEmpty(VmRecVatid))
            {
                if (!VmRecVatid.IsValidVatId())
                {
                    results.AddResult(new ValidationResult("RE00001 Rechnungsempfänger: Ust-Id ist ungültig.", this,
                        "VmRecVatid", "Ust-Id", null));
                }
            }
            else
            {
                if (_invoice.NetAmount >= 10000)
                {
                    results.AddResult(new ValidationResult("RE00002 Rechnungsempfänger: Ust-Id muss bei Rechnungsnettosumme über 10.000 EUR angegeben werden.", this,
                        "VmRecVatid", "Ust-Id", null));
                }
            }
            #endregion

            #region eRechnung an die öffentliche Verwaltung
            if (CurrentSelectedValidation == InvoiceSubtypes.ValidationRuleSet.Government)
            {
                //if (!string.IsNullOrEmpty(VmBillerGln))
                //{
                //    if (VmBillerGln.Length != 13)
                //    {
                //        results.AddResult(new ValidationResult("RE00093 Rechnungsempfänger: Die GLN muss leer oder 13 Stellen lang sein", this,"Rechnungsempfänger", "GLN", null));
                //    }
                //}
                //if (!string.IsNullOrEmpty(VmRecGln))
                //{
                //    if (VmRecGln.Length != 13)
                //    {
                //        results.AddResult(new ValidationResult("RE00092 Rechnungssteller: Die GLN muss leer oder 13 Stellen lang sein", this, "Rechnungssteller", "GLN", null));
                //    }
                //}
                string msg;
                bool isRequired;
                if (PaymentConditions.SkontoList.Count > 2)
                {
                    results.AddResult(new ValidationResult("SK00090 SKonto: Es sind max. zwei Skontozeilen zulässig", this, "SkontoList", "Skonto", null));
                }
                if (!VmOrderReference.IsValidOrderRefBund(out msg, out isRequired))
                {
                    results.AddResult(new ValidationResult("AF00003 Auftragsreferenz: " + msg, this, "VmOrderReference", "Auftragsreferenz", null));
                }
            }
            if (RelatedDoc.RefTypeSelected != RelatedDocumentViewModel.RefType.Keine)
            {
                if (RelatedDoc.IsValid())
                {
                    if (RelatedDoc.RefTypeSelected == RelatedDocumentViewModel.RefType.Storno)
                    {
                        DocumentTypeType doctype = _invoice.DocumentType; // (DocumentTypeType)Enum.Parse(typeof(DocumentTypeType), VmDocType);
                        DocumentTypeType refDocType =
                            (DocumentTypeType)Enum.Parse(typeof(DocumentTypeType), RelatedDoc.RefSelectedDocType);
                        if (refDocType != DocumentTypeType.CreditMemo && doctype != DocumentTypeType.CreditMemo)
                        {

                            results.AddResult(
                                new ValidationResult(
                                    "VW00063 Verweis: Bei Storno einer " + InvTypes.GetText(refDocType.ToString()) + " von muss dieses Dokument eine Gutschrift sein.",
                                    this, "RelatedDoc", "Verweis", null));
                        }
                        else
                        {
                            if (refDocType == DocumentTypeType.CreditMemo && doctype != DocumentTypeType.Invoice)
                            {

                                results.AddResult(
                                    new ValidationResult(
                                        "VW00064 Verweis: Bei Storno einer Gutschrift von muss dieses Dokument eine Rechnung sein.",
                                        this, "RelatedDoc", "Verweis", null));
                            }

                        }
                    }
                }
                else
                {
                    results.AddAllResults(RelatedDoc.Results);
                }
            }
            #endregion

            #region Storno
            if (RelatedDoc.RefTypeSelected != RelatedDocumentViewModel.RefType.Storno && VmDocType.StartsWith("Cancel"))
            {
                results.AddResult(new ValidationResult(
                    string.Format("VW00065 Verweis: Bei Rechnungsart '{0}' muss ein Stornoverweis angegeben werden.", VmDocRef),
                    this, "RelatedDoc", "Verweis", null));
            }
            #endregion

            #region Kontoverbindung
            if (!string.IsNullOrWhiteSpace(VmKtoBankName) ||
                !string.IsNullOrWhiteSpace(VmKtoBic) ||
                !string.IsNullOrWhiteSpace(VmKtoIban) ||
                !string.IsNullOrWhiteSpace(VmKtoOwner) ||
                !string.IsNullOrWhiteSpace(VmKtoReference)
                )
                CheckKontoVerbindung(results);
            else
            {
                // Kein feld der Bankverbindung ist gefüllt: Wenn Bund und Rechnung = Fehler
                if (CurrentSelectedValidation == InvoiceSubtypes.ValidationRuleSet.Government)
                {
                    // Kein Storno und keine Gutschrift
                    if ((RelatedDoc.RefTypeSelected != RelatedDocumentViewModel.RefType.Storno) && (VmDocType != DocumentTypeType.CreditMemo.ToString()))
                    {
                        string reArt = InvTypes.DropDownList.Find(x => x.Code == VmDocType).DisplayText;
                        results.AddResult(new ValidationResult(string.Format("KV00092 Kontoverbindung fehlt für die Rechnungsart '{0}'.", reArt), this, "Konto", "Konto", null));
                    }
                    // Storno einer Gutschrift
                    if ((RelatedDoc.RefTypeSelected == RelatedDocumentViewModel.RefType.Storno) && (RelatedDoc.RefSelectedDocType == DocumentTypeType.CreditMemo.ToString()))
                    {
                        results.AddResult(new ValidationResult("KV00092 Kontoverbindung: Bei Storno einer Gutschrift muss eine Kontoverbindung angegeben werden.", this, "Konto", "Konto", null));
                    }
                }
            }
            #endregion

            #region Vorsteuerberechtigung
            if (PlugInSettings.Default.VStBerechtigt == false) // keine VSt Abzugsberechtigung
            {
                if (VmBillerVatid != PlugInSettings.VatIdDefaultOhneVstBerechtigung)
                {
                    results.AddResult(new ValidationResult(
                        "RS00013 Rechnungssteller: Wenn keine Vst. Abzugsberechtigung besteht muss die USt-ID 8 Nullen enthalten.",
                        this, "VmBillerVatid", "Ust-Id", null));
                }
            }
            #endregion
        }

        internal void CheckKontoVerbindung(ValidationResults results)
        {
            // mindestens ein Feld ist gefüllt.
            if (string.IsNullOrWhiteSpace(VmKtoBankName))
            {
                results.AddResult(
                    new ValidationResult(
                        "KV00004 Kontoverbindung: Wenn eines der Felder angegeben ist, müssen auch alle anderen Felder ausgefüllt werden",
                        this, "VmKtoBankName", "Bank", null));
            }
            else
            {
                const int maxBankName = 255;
                if (VmKtoBankName.Length > maxBankName)
                {
                    results.AddResult(
                        new ValidationResult(
                            string.Format("KV00005 Kontoverbindung: Die Bankbezeichnung darf max. {0} Zeichen lang sein.", maxBankName),
                            this, "VmKtoBankName", "Bank", null));

                }
            }
            if (string.IsNullOrWhiteSpace(VmKtoBic))
            {
                results.AddResult(
                    new ValidationResult(
                        "KV00006 Kontoverbindung: Wenn eines der Felder angegeben ist, müssen auch alle anderen Felder ausgefüllt werden",
                        this, "VmKtoBic", "BIC", null));

            }
            else
            {
                string regEx = "^(?=(?:.{8}|.{11})$)[0-9A-Za-z]{8}([0-9A-Za-z]{3})?";
                var reg = new Regex("[0-9A-Za-z]{8}([0-9A-Za-z]{3})?"); // ([a-zA-Z]{4}[a-zA-Z]{2}[a-zA-Z0-9]{2}([a-zA-Z0-9]{3})?) Pattern aus xsd
                if (!reg.IsMatch(VmKtoBic))
                {
                    results.AddResult(new ValidationResult("KV00007 Kontoverbindung: BIC ist ungültig.",
                        this, "VmKtoBic", "BIC", null));
                }
            }
            if (string.IsNullOrWhiteSpace(VmKtoIban))
            {
                results.AddResult(
                    new ValidationResult(
                        "KV00008 Kontoverbindung: Wenn eines der Felder angegeben ist, müssen auch alle anderen Felder ausgefüllt werden",
                        this, "VmKtoIban", "IBAN", null));

            }
            else
            {
                var status = VmKtoIban.IsIbanValid(true);
                if (!status.IsValid)
                {
                    results.AddResult(
                        new ValidationResult(
                            "KV00009 Kontoverbindung: " + status.Message,  // IBAN Messages ",
                            this, "VmKtoIban", "IBAN", null));
                }
            }
            if (string.IsNullOrWhiteSpace(VmKtoOwner))
            {
                results.AddResult(new ValidationResult("KV00010 Kontoverbindung: Wenn eines der Felder angegeben ist, müssen auch alle anderen Felder ausgefüllt werden",
                        this, "VmKtoOwner", "Kontoinhaber", null));

            }
            else
            {

                const int maxOwnerName = 70;
                if (VmKtoOwner.Length > maxOwnerName)
                {
                    results.AddResult(new ValidationResult(string.Format("KV00011 Kontoverbindung: Kontoinhaber darf max. {0} Zeichen enthalten.", maxOwnerName),
                       this, "VmKtoOwner", "Kontoinhaber", null));
                }
            }
            if (!string.IsNullOrWhiteSpace(VmKtoReference))
            {
                const int maxRefLen = 70;
                if (VmKtoReference.Length > maxRefLen)
                {
                    results.AddResult(new ValidationResult(string.Format("KV00012 Kontoverbindung: Referenz darf max. {0} Zeichen enthalten.", maxRefLen),
                       this, "VmKtoReference", "Referenz", null));
                }
            }

        }
        #endregion
    }
}
