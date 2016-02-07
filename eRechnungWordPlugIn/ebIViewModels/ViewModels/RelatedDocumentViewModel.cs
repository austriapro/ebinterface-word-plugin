using System;
using System.Diagnostics;
using System.Reflection.Emit;
using ebIModels.Models;
using ebIViewModels.Services;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.Unity;
using SimpleEventBroker;
using WinFormsMvvm;
using WinFormsMvvm.DialogService;
using LogService;

namespace ebIViewModels.ViewModels
{
    public class RelatedDocumentViewModel : ViewModelBaseExtension
    {
        private const string Storno = "Storno";
        private const string Verweis = "Verweis";

        public enum RefType
        {
            Keine = 0,
            Storno,
            Verweis
        }

        private RefType _refTypeSelected;
        public RefType RefTypeSelected
        {
            get { return _refTypeSelected; }
            set
            {
                //if (_refTypeSelected == value)
                //    return;
                _refTypeSelected = value;
                OnPropertyChanged();
                OnUpdateSelection(_refTypeSelected.ToString());
            }
        }

        private string _refInvNumber;
        [StringLengthValidator(1, RangeBoundaryType.Inclusive, 35, RangeBoundaryType.Inclusive, MessageTemplate = "VW00050 Verweis: Bei Storno darf die Rechnungsnummer nicht leer sein.", Ruleset = Storno, Tag = "Rechnungsnr.")]
        [StringLengthValidator(1, RangeBoundaryType.Inclusive, 35, RangeBoundaryType.Inclusive, MessageTemplate = "VW00051 Verweis: Bei Verweis darf die Rechnungsnummer nicht leer sein.", Ruleset = Verweis, Tag = "Rechnungsnr.")]
        public string RefInvNumber
        {
            get { return _refInvNumber; }
            set
            {
                if (_refInvNumber == value)
                    return;
                _refInvNumber = value;
                OnPropertyChanged();
            }
        }

        private DateTime _refInvDate;
        [DateTimeRangeValidator("2000-01-01T00:00:00", RangeBoundaryType.Exclusive, "3000-12-31T00:00:00", RangeBoundaryType.Exclusive,
            MessageTemplate = "VW00052 Verweis: Bei Storno darf das Datum nicht vor leer sein.", Ruleset = Storno, Tag = "Datum")]
        public DateTime RefInvDate
        {
            get
            {
                return _refInvDate;
            }
            set
            {
                if (_refInvDate == value)
                    return;
                _refInvDate = value;
                OnPropertyChanged();
            }
        }

        private string _refSelectedDocType;
        public string RefSelectedDocType
        {
            get { return _refSelectedDocType; }
            set
            {
                //if (_refSelectedDocType == value)
                //    return;
                _refSelectedDocType = value;
                OnPropertyChanged();
                OnUpdateSelection(_refSelectedDocType);
            }
        }

        private string _refComment;
        //[ValidatorComposition(CompositionType.Or, Ruleset = STORNO)]
        // [NotNullValidator(Ruleset = STORNO)]
        [StringLengthValidator(0, 255, MessageTemplate = "VW00053 Verweis: Die Anmerkung darf max. {5} Zeichen lang sein.", Ruleset = Storno)]
        [StringLengthValidator(0, 255, MessageTemplate = "VW00053 Verweis: Die Anmerkung darf max. {5} Zeichen lang sein.", Ruleset = Verweis)]
        public string RefComment
        {
            get { 
                return _refComment; 
            }
            set
            {
                if (_refComment == value)
                    return;
                _refComment = value;
                OnPropertyChanged();
            }
        }

        private DropDownListViewModels _refTypeList = new DropDownListViewModels();
        public DropDownListViewModels RefTypeList
        {
            get { return _refTypeList; }
            set
            {
                if (_refTypeList == value)
                    return;
                _refTypeList = value;
                OnPropertyChanged();
            }
        }


        private DropDownListViewModels _refDocTypes = new DropDownListViewModels();
        public DropDownListViewModels RefDocTypes
        {
            get { return _refDocTypes; }
            set
            {
                if (_refDocTypes == value)
                    return;
                _refDocTypes = value;
                OnPropertyChanged();
            }
        }


        private InvoiceSubtypes.ValidationRuleSet _currentSelectedValidation;
        public InvoiceSubtypes.ValidationRuleSet CurrentSelectedValidation
        {
            get { return _currentSelectedValidation; }
            set
            {
                if (_currentSelectedValidation == value)
                    return;
                _currentSelectedValidation = value;
                _refDocTypes.GetList(_documentTypes.GetReferenceDocumentTypes(CurrentSelectedValidation));
                OnPropertyChanged();
                OnPropertyChanged("RefDocTypes");
            }
        }
        private DocumentTypeModels _documentTypes;
        public RelatedDocumentViewModel(IUnityContainer uc, IDialogService dlg,DocumentTypeModels documentTypes) :base(uc,dlg)
        {
            // _refInvDate = DateTime.Today;
            _refComment = "";
            _refTypeList.DropDownList.Add(new DropDownListViewModel()
            {
                Code = RefType.Keine.ToString(),
                DisplayText = "Kein Verweis"
            });
            _refTypeList.DropDownList.Add(new DropDownListViewModel()
            {
                Code = RefType.Storno.ToString(),
                DisplayText = "Storno"
            });
            _refTypeList.DropDownList.Add(new DropDownListViewModel()
            {
                Code = RefType.Verweis.ToString(),
                DisplayText = "Verweis"
            });
            _refSelectedDocType = "Invoice";
            _documentTypes = documentTypes;
            _refDocTypes.GetList(_documentTypes.GetReferenceDocumentTypes(CurrentSelectedValidation));

        }

        public void Clear()
        {
            RefComment = "";
            RefInvDate = new DateTime();
            RefInvNumber = "";
            RefTypeSelected = RefType.Keine;
            RefSelectedDocType = "Invoice";

        }

        public void AddRelatedDocument<T>(T relDoc)
        {
            if (relDoc == null)
            {
                return;
            }
            if (relDoc is CancelledOriginalDocumentType)
            {
                CancelledOriginalDocumentType cDoc = relDoc as CancelledOriginalDocumentType;
                RefComment = cDoc.Comment??"";
                if (cDoc.DocumentTypeSpecified)
                    RefSelectedDocType = cDoc.DocumentType.ToString();
                RefInvDate = cDoc.InvoiceDate;
                RefInvNumber = cDoc.InvoiceNumber;
                RefTypeSelected = RefType.Storno;
                return;
            }
            if (relDoc is RelatedDocumentType)
            {
                RelatedDocumentType rDoc = relDoc as RelatedDocumentType;
                RefComment = rDoc.Comment??"";
                if (rDoc.DocumentTypeSpecified)
                    RefSelectedDocType = rDoc.DocumentType.ToString();
                if (rDoc.InvoiceDateSpecified)
                    RefInvDate = rDoc.InvoiceDate;
                RefInvNumber = rDoc.InvoiceNumber;
                RefTypeSelected = RefType.Verweis;
                return;               
            }
        }

        public object GetRelatedDocumentEntry(InvoiceSubtypes.ValidationRuleSet formType)
        {
            switch (RefTypeSelected)
            {
                case RefType.Keine:
                    return null;

                case RefType.Storno:
                    CancelledOriginalDocumentType docType = new CancelledOriginalDocumentType();
                    docType.Comment = RefComment;
                    
                    if (!string.IsNullOrEmpty(RefSelectedDocType))
                    {
                        docType.DocumentType = (DocumentTypeType)Enum.Parse(typeof(DocumentTypeType), RefSelectedDocType);
                        docType.DocumentTypeSpecified = true;
                    }
                    docType.InvoiceDate = RefInvDate;
                    docType.InvoiceNumber = RefInvNumber;
                    return docType;
                case RefType.Verweis:
                    RelatedDocumentType relDoc = new RelatedDocumentType();
                    relDoc.Comment = RefComment;

                    if (!string.IsNullOrEmpty(RefSelectedDocType))
                    {
                        relDoc.DocumentType = (DocumentTypeType)Enum.Parse(typeof(DocumentTypeType), RefSelectedDocType);
                        relDoc.DocumentTypeSpecified = true;
                    }
                    if (RefInvDate.CompareTo(new DateTime(2000, 1, 1)) > 0)
                    {
                        relDoc.InvoiceDate = RefInvDate;
                        relDoc.InvoiceDateSpecified = true;
                    }
                    relDoc.InvoiceNumber = RefInvNumber;
                    return relDoc;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public ValidationResults Results { get; internal set; }
        public bool IsValid()
        {
            ValidationFactory.SetDefaultConfigurationValidatorFactory(new SystemConfigurationSource(false));
            string ruleset = RefTypeSelected.ToString();
            var validator = ValidationFactory.CreateValidator<RelatedDocumentViewModel>(ruleset);
            Results = validator.Validate(this);
            Log.LogWrite(CallerInfo.Create(),Log.LogPriority.High, "ValidationResult IsVaild={0}, ErrorCount={1}",Results.IsValid,Results.Count);
            return Results.IsValid;
        }


        [SubscribesTo(InvoiceViewModel.InvoiceValidationOptionChanged)]
        public void OnInvoiceValidationOptionChanged(object sender, EventArgs args)
        {
            InvIndustryEventArgs arg = args as InvIndustryEventArgs;
            Log.TraceWrite(CallerInfo.Create(),"Event: {0}, new Value={1}", InvoiceViewModel.InvoiceValidationOptionChanged,arg.Industry.ToString());
            CurrentSelectedValidation = arg.Industry;
        }
    }
}