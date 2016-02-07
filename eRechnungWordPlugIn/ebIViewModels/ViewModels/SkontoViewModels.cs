using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ebIModels.Models;
using ebIModels.Schema;
using ebIViewModels.Services;
using ebIViewModels.Views;
using ExtensionMethods;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.Unity;
using Validation = ebIValidation;
using SettingsManager;
using SimpleEventBroker;
using WinFormsMvvm;
using WinFormsMvvm.Controls;
using WinFormsMvvm.DialogService;
using LogService;

namespace ebIViewModels.ViewModels
{
    public class SkontoViewModels : ViewModelBase
    {
        private BindingList<SkontoViewModel> _skontoList;
        /// <summary>
        /// Liste der Skontozeilen
        /// </summary>
        public BindingList<SkontoViewModel> SkontoList
        {
            get { return _skontoList; }
            set
            {
                if (_skontoList == value)
                    return;
                _skontoList = value;
                OnPropertyChanged();
                // HasSkontoElements = _skontoList.Any();

                OnPropertyChanged("HasSkontoElements");
            }
        }

        private bool _hasSkontoElements;
        /// <summary>
        /// Comment
        /// </summary>
        public bool HasSkontoElements
        {
            get
            {
                //Log.LogWrite(CallerInfo.Create(),"Get SkontoList.Any={1}",_skontoList.Any());
                return _skontoList.Any() || _hasSkontoElements;
            }
            set
            {
                if (_hasSkontoElements == value)
                    return;
                Log.TraceWrite(CallerInfo.Create(),"Set HasSkontoElements={0}", _hasSkontoElements);
                _hasSkontoElements = value;
                OnPropertyChanged();
            }
        }

        private int _dueDays;
        /// <summary>
        /// Fälligkeitstage
        /// </summary>
        [RangeValidator(0, RangeBoundaryType.Inclusive, 0, RangeBoundaryType.Ignore, MessageTemplate = "SK00060 Tage darf nicht kleiner Null sein.", Tag = "Zahlungsziel")]
        public int InvoiceDueDays
        {
            get { return _dueDays; }
            set
            {
                if (_dueDays == value)
                    return;
                _dueDays = value;
                OnPropertyChanged();
                _invoiceDueDate = _invoiceDate.AddDays(_dueDays);
                OnPropertyChanged("InvoiceDueDate");
            }
        }


        private decimal _baseAmount;
        /// <summary>
        /// Basisbetrag auf den sich der Skonto bezieht
        /// </summary>
        public decimal BaseAmount
        {
            get { return _baseAmount; }
            set
            {
                if (_baseAmount == value)
                    return;
                _baseAmount = value;
                OnPropertyChanged();
            }
        }

        private DateTime _invoiceDate;
        /// <summary>
        /// Rechnungsdatum
        /// </summary>
        [RelativeDateTimeValidator(-6, DateTimeUnit.Month,
                             6, DateTimeUnit.Month, Tag = "Rechnungsdatum",
                             MessageTemplate = "SK00061 Das Rechnungsdatum darf max. 6 Monate in der Vergangenheit oder in der Zukunft sein.")]
        public DateTime InvoiceDate
        {
            get
            {
                Log.TraceWrite(CallerInfo.Create(),"InvoiceDate={0}", _invoiceDate);
                return _invoiceDate;
            }
            set
            {
                if (_invoiceDate == value)
                    return;
                _invoiceDate = value.Date;
                OnPropertyChanged();
                _invoiceDueDate = _invoiceDate.AddDays(_dueDays);
                OnPropertyChanged("InvoiceDueDate");
                UpdateSkontoList();
                OnPropertyChanged("SkontoList");
            }
        }

        private DateTime _invoiceDueDate;
        /// <summary>
        /// Fälligkeitsdatum
        /// </summary>
        [Validation.PropertyComparisonValidator("InvoiceDate", ComparisonOperator.GreaterThanEqual,
            MessageTemplate = "SK00062 Das Fälligkeitsdatum kann nicht vor dem Rechnungsdatum liegen.", Tag = "Fälligkeitsdatum")]
        public DateTime InvoiceDueDate
        {
            get
            {
                Log.TraceWrite(CallerInfo.Create(),"InvoiceDueDate={0}", _invoiceDueDate);
                return _invoiceDueDate;
            }
            set
            {
                if (_invoiceDueDate == value)
                    return;
                _invoiceDueDate = value.Date;
                OnPropertyChanged();
                _dueDays = _invoiceDueDate.Days(_invoiceDate);
                Log.TraceWrite(CallerInfo.Create(),"@Duedate Duedate: {0:d}, Invoicedate:{1:d}, Days:{2}", _invoiceDueDate, _invoiceDate, _dueDays);
                OnPropertyChanged("InvoiceDueDays");
            }
        }

        private InvoiceSubtypes.ValidationRuleSet _currentValidationRuleset;
        public InvoiceSubtypes.ValidationRuleSet CurrentValidationRuleset
        {
            get
            {
                return _currentValidationRuleset;
            }
            set
            {
                if (_currentValidationRuleset == value)
                    return;
                _currentValidationRuleset = value;
                OnPropertyChanged();
            }
        }

        private readonly IUnityContainer _uc;

        #region Relaycommands

        private RelayCommand _editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                _editCommand = _editCommand ?? new RelayCommand(EditClick);
                return _editCommand;
            }
        }

        private RelayCommand _deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                _deleteCommand = _deleteCommand ?? new RelayCommand(DeleteClick);
                return _deleteCommand;
            }
        }


        private RelayCommand _insertCommand;
        public RelayCommand InsertCommand
        {
            get
            {
                _insertCommand = _insertCommand ?? new RelayCommand(param => InsertClick(), CanAddElements);
                return _insertCommand;
            }
        }

        private bool CanAddElements(object obj)
        {
            if (CurrentValidationRuleset != InvoiceSubtypes.ValidationRuleSet.Government) return true;
            if (SkontoList.Count < 2) return true;
            return false;
        }


        private RelayCommand _clearCommand;
        public RelayCommand ClearCommand
        {
            get
            {
                _clearCommand = _clearCommand ?? new RelayCommand(param => clearClick());
                return _clearCommand;
            }
        }

        #endregion

        #region Command Methods
        private void clearClick()
        {
            var rc = _dlg.ShowMessageBox("Wollen sie wirklich alle Skontozeilen löschen?", "Skonto bearbeiten",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rc == DialogResult.Yes)
            {
                Clear();
                // HasSkontoElements = false;
                OnPropertyChanged("HasSkontoElements");
                OnPropertyChanged("SkontoList");
            }
        }

        private void InsertClick()
        {
            var skonto = _uc.Resolve<SkontoViewModel>(new ParameterOverride("skontoEntry", this));
            SetSkontoProperties(skonto);
            var rc = _dlg.ShowDialog<FrmSkontoEdit>(skonto);
            if (rc == DialogResult.OK)
            {
                SkontoList.Add(skonto);
                // HasSkontoElements = true;
                OnPropertyChanged("HasSkontoElements");
                OnPropertyChanged("SkontoList");

            }
        }

        private void SetSkontoProperties(SkontoViewModel skonto)
        {
            //skonto.SkontoBasisBetrag = BaseAmount;
            //skonto.InvoiceDate = InvoiceDate;
            //skonto.InvoiceDueDate = InvoiceDueDate;
            //skonto.CurrentRuleSet = CurrentValidationRuleset; // invoice.InvoiceSubtype.VariantOption;
            //skonto.SkontoBasisBetrag = BaseAmount; //  invoice.TotalGrossAmount ?? 0;
            //skonto.InvoiceDueDays = InvoiceDueDays;
           // skonto.UpdateFromSkontoListEntry(this);
        }


        private void DeleteClick(object o)
        {
            var rc = _dlg.ShowMessageBox("Wollen Sie den Skontoeintrag wirklich löschen?", "Skonto löschen",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rc == DialogResult.Yes)
            {
                int i = (int)o;
                SkontoList.RemoveAt(i);
                // HasSkontoElements = SkontoList.Any();
                OnPropertyChanged("HasSkontoElements");
                OnPropertyChanged("SkontoList");
            }
        }

        private void EditClick(object o)
        {
            int i = (int)o;
            var skonto = SkontoList[i];
            SetSkontoProperties(skonto);
            var rc = _dlg.ShowDialog<FrmSkontoEdit>(skonto);
            if (rc == DialogResult.OK)
            {
                SkontoList[i] = skonto;
                OnPropertyChanged("SkontoList");

            }
        }

        #endregion
        // private IInvoiceType _invoice;
        public SkontoViewModels(IDialogService dialog, IUnityContainer uc, InvoiceViewModel invVm)
        {
            _uc = uc;
            _skontoList = new BindingList<SkontoViewModel>();
            _dlg = dialog;
            // _invoice = invoice;
            LoadFromInvoiceViewModel(invVm);
            // HasSkontoElements = _skontoList.Any();
        }

        public void LoadFromInvoiceViewModel(InvoiceViewModel invVm)
        {
            _invoiceDate = invVm.VmInvDate;
            _baseAmount = invVm.VmInvTotalAmountDecimal;
            _currentValidationRuleset = invVm.CurrentSelectedValidation;
            if (invVm.PaymentConditions == null)
            {
                SkontoList = new BindingList<SkontoViewModel>();
            }
            else
            {
                _dueDays = invVm.VmInvDueDate.Days(invVm.VmInvDate);
                _invoiceDueDate = invVm.VmInvDueDate;
                SkontoList = invVm.PaymentConditions.SkontoList;
            }

        }
        public void LoadFromInvoice(IInvoiceType invoice)
        {
           var _invoice = invoice;
            _invoiceDate = _invoice.InvoiceDate;
            _invoiceDueDate = _invoice.PaymentConditions.DueDate;
            _dueDays = _invoiceDueDate.Days(_invoiceDate);
            _baseAmount = _invoice.TotalGrossAmount ?? 0;
            SetFromPaymentConditions(_invoice.PaymentConditions);
        }
        public void Clear()
        {
            // BaseAmount = 0;
            // InvoiceDueDate = new DateTime();
            SkontoList.Clear();
            // HasSkontoElements = false;
            OnPropertyChanged("HasSkontoElements");
        }

        private void UpdateSkontoList()
        {
            
            foreach (SkontoViewModel skonto in SkontoList)
            {
                UpdateSkonto(skonto);
            }
            OnPropertyChanged("SkontoList");

        }

        private void SetFromPaymentConditions(PaymentConditionsType paymentConditions)
        {
            SkontoList.Clear();
            foreach (DiscountType discount in paymentConditions.Discount)
            {
                SkontoViewModel skontoView = _uc.Resolve<SkontoViewModel>(new ParameterOverrides(){
                                            {"skontoEntry", this},
                                            {"discount",discount}
                                            });
                SkontoList.Add(skontoView);
            }
        }

        public PaymentConditionsType GetPaymentConditions(DateTime dueDate)
        {
            PaymentConditionsType payment = new PaymentConditionsType()
            {
                DueDate = dueDate,
                Discount = new List<DiscountType>(),
                MinimumPaymentSpecified = false

            };

            foreach (SkontoViewModel viewModel in SkontoList)
            {
                payment.Discount.Add(viewModel.GetDiscountType());
            }
            return payment;
        }


        [SubscribesTo(InvoiceViewModel.InvDatesChanged)]
        public void OnInvDatesChanged(object sender, EventArgs args)
        {
            InvoiceDatesChangedEventArgs arg = args as InvoiceDatesChangedEventArgs;
            InvoiceDate = arg.InvoiceDate;
            InvoiceDueDate = arg.InvoiceDueDate;
            BaseAmount = arg.BaseAmount;
        }


        private void UpdateSkonto(SkontoViewModel model)
        {
            Log.TraceWrite(CallerInfo.Create(),"at Entry");
            InvoiceDatesChangedEventArgs args = new InvoiceDatesChangedEventArgs();
            args.InvoiceDate = InvoiceDate;
            args.InvoiceDueDate = InvoiceDueDate;
            args.BaseAmount = BaseAmount;
            model.Update(args);
            return;
        }

    }
}
