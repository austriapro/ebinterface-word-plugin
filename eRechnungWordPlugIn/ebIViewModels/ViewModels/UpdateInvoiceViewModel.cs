using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ebIModels.Models;
using WinFormsMvvm;
using WinFormsMvvm.Controls;
using ExtensionMethods;
using WinFormsMvvm.DialogService;


namespace ebIViewModels.ViewModels
{
    public class UpdateInvoiceViewModel : ViewModelBase
    {
        private string _invoiceNumber;
        public string InvoiceNumber
        {
            get { return _invoiceNumber; }
            set
            {
                _invoiceNumber = value;
                OnPropertyChanged();
            }
        }

        private DateTime _invoiceDate = new DateTime(1900,1,1);
        public DateTime InvoiceDate
        {
            get
            {
                return _invoiceDate;
            }
            set
            {
                if (_invoiceDate == value)
                    return;
                _invoiceDate = value;
                OnPropertyChanged();
                _invoiceDueDate = _invoiceDate.AddDays(_dueDays);
                OnPropertyChanged(nameof(InvoiceDueDate));
            }
        }


        private DateTime _invoiceDueDate;
        public DateTime InvoiceDueDate
        {
            get
            {
                return _invoiceDueDate;
            }
            set
            {
                if (_invoiceDueDate == value) return;
                _invoiceDueDate = value;
                OnPropertyChanged();
                _dueDays = value.Days(_invoiceDate);
                OnPropertyChanged(nameof(DueDays));
            }
        }

        private DateTime? _deliveryDate;
        public DateTime? DeliveryDate
        {
            get
            {
                return _deliveryDate;
            }
            set
            {
                _deliveryDate = value;
                OnPropertyChanged();
            }
        }

        private int _dueDays;
        [RangeValidator(0, RangeBoundaryType.Inclusive, 0, RangeBoundaryType.Ignore, MessageTemplate = "RE00060 Tage darf nicht kleiner Null sein.", Tag = "Zahlungsziel")]
        public int DueDays
        {
            get
            {
                return _dueDays;
            }
            set
            {
                if (_dueDays == value)
                    return;
                _dueDays = value;
                OnPropertyChanged();
                _invoiceDueDate = _invoiceDate.AddDays(_dueDays);
                OnPropertyChanged(nameof(InvoiceDueDate));
            }
        }
        public ValidationResults Results { get; private set; }
        public bool Validate()
        {
            ValidationFactory.SetDefaultConfigurationValidatorFactory(new SystemConfigurationSource(false));
            var updateValidator = ValidationFactory.CreateValidator<UpdateInvoiceViewModel>();
            Results = updateValidator.Validate(this);
            return Results.IsValid;
        }

        private UnityContainer _uc;

        public UpdateInvoiceViewModel(UnityContainer uc,IDialogService dlg, IInvoiceModel invoice) : base(dlg)
        {
            
            _uc = uc;
            _invoiceDate = invoice.InvoiceDate;
            _invoiceDueDate = invoice.PaymentConditions.DueDate;
            _invoiceNumber = invoice.InvoiceNumber;
            if (invoice.Delivery.Item != null)
            {
                if (invoice.Delivery.Item is DateTime)
                {
                    _deliveryDate = (DateTime?)invoice.Delivery.Item;
                }
                else
                {
                    _deliveryDate = ((PeriodType)invoice.Delivery.Item).FromDate;
                }
            }

            _dueDays = InvoiceDueDate.Days(InvoiceDate);
        }

        #region Speichern Command

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                _saveCommand = _saveCommand ?? new RelayCommand(param => saveClick());
                return _saveCommand;
            }
        }

        private void saveClick()
        {
            // throw new NotImplementedException();

        }

        #endregion
    }
}
