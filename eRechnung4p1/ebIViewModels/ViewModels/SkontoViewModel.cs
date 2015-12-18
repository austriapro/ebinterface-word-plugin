using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Validation = ebIValidation;
using ebIViewModels.Services;
using ExtensionMethods;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using SimpleEventBroker;
using WinFormsMvvm;
using ebIModels.Models;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using WinFormsMvvm.DialogService;
using LogService;

namespace ebIViewModels.ViewModels
{
    public class SkontoViewModel : ViewModelBase
    {

        /// <summary>
        /// Rechnungsdatum
        /// </summary>
        //public readonly DateTime InvoiceDate;

        //private DateTime _invoiceDate;
        /// <summary>
        /// Comment
        /// </summary>        

        private DateTime _invoiceDate;
        public DateTime InvoiceDate
        {
            get
            {
                //return _invoiceDate; 
                return _invoiceDate;
            }
            set
            {
                //  if (_skontoListEntry.InvoiceDate == value)
                //     return;
                _invoiceDate = value;
                OnPropertyChanged();
                _skontoFaelligDate = _invoiceDate.AddDays(_skontoTage);
                OnPropertyChanged("SkontoFaelligDate");
            }
        }


        //public readonly DateTime InvoiceDueDate;
        //private DateTime _invoiceDueDate;
        /// <summary>
        /// Comment
        /// </summary>
        private DateTime _invoiceDueDate;
        public DateTime InvoiceDueDate
        {
            get
            {
                //return _invoiceDueDate; 
                return _invoiceDueDate;
            }
            set
            {
                if (_invoiceDueDate == value)
                    return;
                _invoiceDueDate = value;
                OnPropertyChanged();
            }
        }

        private decimal _skontoProzent;
        /// <summary>
        /// Skonto Prozentsatz
        /// </summary>
        [RangeValidator(typeof(decimal), "0", RangeBoundaryType.Inclusive, "100", RangeBoundaryType.Inclusive, MessageTemplate = "SK00054 Prozent darf nicht kleiner {3} oder größer {5} sein.")]
        [Validation.DecimalFractionValidatorAttribute(2, MessageTemplate = "SK00055 Prozent darf max. 2 Nachkommastellen haben.")]
        [RangeValidator(typeof(decimal), "0", RangeBoundaryType.Exclusive, "100", RangeBoundaryType.Exclusive, MessageTemplate = "SK00054 Prozent muss größer {3} und kleiner {5} sein.", Tag = "Skonto-Prozent", Ruleset = RuleSetBund)]
        [Validation.DecimalFractionValidatorAttribute(2, MessageTemplate = "SK00055 Prozent darf max. 2 Nachkommastellen haben.", Tag = "Skonto-Prozent", Ruleset = RuleSetBund)]
        public decimal SkontoProzent
        {
            get { return _skontoProzent; }
            set
            {
                if (_skontoProzent == value)
                    return;
                _skontoProzent = value;
                OnPropertyChanged();
            }
        }

        // private decimal _skontoBetrag;
        /// <summary>
        /// Skontobetrag. Berechnet: SkontoBasisbetrag * SkontoProzent / 100
        /// </summary>
        public decimal SkontoBetrag
        {
            get { return (SkontoBasisBetrag * SkontoProzent) / 100; }
            //set
            //{
            //    if (_skontoBetrag == value)
            //        return;
            //    _skontoBetrag = value;
            //    OnPropertyChanged();
            //}
        }

        // private decimal _skontoBasisBetrag;
        /// <summary>
        /// Basisbetrag für die Skontozeile
        /// </summary>
        private decimal _skontoBasisBetrag;
        public decimal SkontoBasisBetrag
        {
            get
            {
                return _skontoBasisBetrag;
                //return _skontoBasisBetrag; 
            }
            set
            {
                if (_skontoBasisBetrag == value)
                    return;
                _skontoBasisBetrag = value;
                OnPropertyChanged();
            }
        }

        private DateTime _skontoFaelligDate;
        /// <summary>
        /// Skonto Fälligkeitsdatum. Berechnet: Rechnungsdatum + Skontotage oder eingegeben
        /// </summary>
        [Validation.PropertyComparisonValidator("InvoiceDate", ComparisonOperator.GreaterThanEqual,
            MessageTemplate = "SK00056 Das Datum kann nicht vor dem Rechnungsdatum liegen.", Tag = "Skonto-Datum")]
        [Validation.PropertyComparisonValidator("InvoiceDueDate", ComparisonOperator.LessThanEqual,
          MessageTemplate = "SK00057 Das Datum darf nicht nach dem Fälligkeitsdatum der Rechnung liegen.", Tag = "Skonto-Datum")]
        [Validation.PropertyComparisonValidator("InvoiceDate", ComparisonOperator.GreaterThanEqual,
            MessageTemplate = "SK00056 Das Datum kann nicht vor dem Rechnungsdatum liegen.", Tag = "Skonto-Datum", Ruleset = RuleSetBund)]
        [Validation.PropertyComparisonValidator("InvoiceDueDate", ComparisonOperator.LessThanEqual,
          MessageTemplate = "SK00057 Das Datum darf nicht nach dem Fälligkeitsdatum der Rechnung liegen.", Tag = "Skonto-Datum", Ruleset = RuleSetBund)]
        public DateTime SkontoFaelligDate
        {
            get { return _skontoFaelligDate; }
            set
            {
                if (_skontoFaelligDate == value)
                    return;
                _skontoFaelligDate = value;
                OnPropertyChanged();
                _skontoTage = _skontoFaelligDate.Days(_invoiceDate);
                OnPropertyChanged("SkontoTage");
            }
        }

        //  private int _dueDays;
        /// <summary>
        /// Comment
        /// </summary>
        private int _invoiceDueDays;
        public int InvoiceDueDays
        {
            get
            {
                //return _dueDays; 
                return _invoiceDueDays;
            }
            set
            {
                if (_invoiceDueDays == value) return;
                _invoiceDueDays = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Skontotage. Berechnet: Skontofällig-Rechnungsdatum oder eingegeben
        /// </summary>
        private int _skontoTage;
        [RangeValidator(0, RangeBoundaryType.Inclusive, 0, RangeBoundaryType.Ignore,
            MessageTemplate = "SK00058 Tage darf nicht kleiner Null sein.", Tag = "Skonto-Tage")]
        [Validation.PropertyComparisonValidator("InvoiceDueDays", ComparisonOperator.LessThan,
            MessageTemplate = "SK00059 Die Skontofälligkeit kann nicht nach der Fälligkeit der Rechnung liegen.")]
        [RangeValidator(0, RangeBoundaryType.Inclusive, 0, RangeBoundaryType.Ignore,
            MessageTemplate = "SK00058 Tage darf nicht kleiner Null sein.", Tag = "Skonto-Tage", Ruleset = RuleSetBund)]
        [Validation.PropertyComparisonValidator("InvoiceDueDays", ComparisonOperator.LessThan,
            MessageTemplate = "SK00059 Die Skontofälligkeit kann nicht nach der Fälligkeit der Rechnung liegen.", Ruleset = RuleSetBund)]
        public int SkontoTage
        {
            get { return _skontoTage; }
            set
            {
                //  if (_skontoTage == value) return;
                        _skontoTage = value;
                OnPropertyChanged();
                _skontoFaelligDate = _invoiceDate.AddDays(value);
                OnPropertyChanged("SkontoFaelligDate");
            }
        }
        public const string RuleSetBund = "Bund";
        public string RuleSet { get { return CurrentRuleSet == InvoiceSubtypes.ValidationRuleSet.Government ? "Bund" : ""; } }
        private InvoiceSubtypes.ValidationRuleSet _currentRuleSet;
        public InvoiceSubtypes.ValidationRuleSet CurrentRuleSet
        {
            get
            {
                return _currentRuleSet;
            }
            set
            {
                if (_currentRuleSet == value)
                    return;
                _currentRuleSet = value;
                OnPropertyChanged();
            }
        }
        // private SkontoViewModels _skontoListEntry;
        public SkontoViewModel(IDialogService dlg, SkontoViewModels skontoEntry, DiscountType discount)
            : base(dlg)
        {
            // _skontoListEntry = skontoEntry;
            _invoiceDate = skontoEntry.InvoiceDate;
            _invoiceDueDate = skontoEntry.InvoiceDueDate;
            _invoiceDueDays = skontoEntry.InvoiceDueDays;
            _skontoBasisBetrag = skontoEntry.BaseAmount;
            if (discount.PaymentDate!=DateTime.MinValue)
            {
                _skontoFaelligDate = discount.PaymentDate;
                _skontoTage = _skontoFaelligDate.Days(_invoiceDate);

            }
            else
            {
                _skontoFaelligDate = _invoiceDate;
                _skontoTage = 0;
            }
            _skontoProzent = discount.PercentageSpecified ? (discount.Percentage ?? 0) : 0;

           
        }

        //public void UpdateFromSkontoListEntry(SkontoViewModels skontoEntry)
        //{
        //    _skontoListEntry = skontoEntry;
        //}
        //public void SetFromInvoice(IInvoiceType invoice)
        //{
        //    _invoiceDate = invoice.InvoiceDate;
        //    _invoiceDueDate = invoice.PaymentConditions.InvoiceDueDate;
        //    _dueDays = _invoiceDueDate.Days(_invoiceDate);
        //    CurrentRuleSet = invoice.InvoiceSubtype.VariantOption;
        //    _skontoBasisBetrag = invoice.TotalGrossAmount ?? 0;
        //}

        public ValidationResults Results { get; private set; }
        public bool IsValidSkonto()
        {
            ValidationFactory.SetDefaultConfigurationValidatorFactory(new SystemConfigurationSource(false));
            string ruleset = CurrentRuleSet == InvoiceSubtypes.ValidationRuleSet.Government ? SkontoViewModel.RuleSetBund : "";
            var skontoValidator = ValidationFactory.CreateValidator<SkontoViewModel>(ruleset);
            Results = skontoValidator.Validate(this);
            return Results.IsValid;
        }

        public DiscountType GetDiscountType()
        {
            DiscountType discount = new DiscountType();
            discount.AmountSpecified = false;
            discount.PercentageSpecified = true;
            discount.Percentage = SkontoProzent;
            discount.PaymentDate = SkontoFaelligDate;
            discount.BaseAmountSpecified = false; // Das macht uns unabhängig von Detailzeilen!
            return discount;
        }

        [SubscribesTo(InvoiceViewModel.InvoiceValidationOptionChanged)]
        public void OnInvoiceValidationOptionChanged(object sender, EventArgs args)
        {
            InvIndustryEventArgs arg = (InvIndustryEventArgs)args;
            CurrentRuleSet = arg.Industry;
        }
        [SubscribesTo(InvoiceViewModel.InvDatesChanged)]
        public void OnInvDueDateChanged(object sender, EventArgs args)
        {

            InvoiceDatesChangedEventArgs arg = args as InvoiceDatesChangedEventArgs;
            Update(arg);
        }

        public void Update(InvoiceDatesChangedEventArgs arg)
        {
            Log.TraceWrite("at Entry");
            int skontoTageSave = _skontoFaelligDate.Days(_invoiceDate); // Alte SKontotage
            InvoiceDate = arg.InvoiceDate;
            SkontoTage = skontoTageSave;
            InvoiceDueDate = arg.InvoiceDueDate;
        }

    }
}
