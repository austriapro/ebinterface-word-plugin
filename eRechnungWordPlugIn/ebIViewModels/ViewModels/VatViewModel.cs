using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsMvvm;
using ExtensionMethods;

namespace ebIViewModels.ViewModels
{
    /// <summary>
    /// Viewmodel für die MwSt Tabelle
    /// </summary>
    public class VatViewModel : ViewModelBase
    {
        public VatViewModel() { }
        public VatViewModel(string taxCode, string taxCodeText, decimal vatPercent, decimal taxedAmount)
        {
            _vatBaseAmount = taxedAmount;
            _vatPercent = vatPercent;
            _vatAmount = ((taxedAmount * vatPercent) / 100).FixedFraction(2);
            _taxCode = taxCode;
            _taxCodeText = taxCodeText;
        }
        private decimal _vatBaseAmount;
        /// <summary>
        /// Basisbetrag für die MwSt Berechnung
        /// </summary>
        public decimal VatBaseAmount
        {
            get { return _vatBaseAmount.FixedFraction(2); }
            set {
                if (_vatBaseAmount == value)
                    return;
                _vatBaseAmount = value.FixedFraction(2);
                OnPropertyChanged();
                //VatTotalAmount = _vatBaseAmount + VatAmount;
                OnPropertyChanged(nameof(VatTotalAmount));
            }
        }

        private decimal _vatAmount;
        /// <summary>
        /// Betrag der MwSt = Basisbetrag * MwSt-Satz
        /// </summary>
        public decimal VatAmount
        {
            get { return _vatAmount.FixedFraction(2); }
            set {
                if (_vatAmount == value)
                    return;
                _vatAmount = value.FixedFraction(2);
                OnPropertyChanged();
                OnPropertyChanged(nameof(VatTotalAmount));
                //VatTotalAmount = _vatAmount + VatBaseAmount;
            }
        }

        private decimal _vatTotalAmount;
        /// <summary>
        /// Gesamtbetrag = Basisbetrag + Betrag der MwSt
        /// </summary>
        public decimal VatTotalAmount
        {
            get {
                return (_vatAmount + VatBaseAmount).FixedFraction(2);
            }
            private set {
                _vatTotalAmount = value;
                OnPropertyChanged();
            }
        }

        private decimal _vatPercent;
        /// <summary>
        /// MwSt Satz
        /// </summary>
        public decimal VatPercent
        {
            get { return _vatPercent; }
            set {
                if (_vatPercent == value)
                    return;
                _vatPercent = value;
                OnPropertyChanged();
                VatAmount = VatBaseAmount * _vatPercent / 100;
            }
        }

        //private bool _taxExemption;
        ///// <summary>
        ///// Gibt an ob steuerbefreit (=true)
        ///// </summary>
        //public bool TaxExemption
        //{
        //    get { return _taxExemption; }
        //    set
        //    {
        //        if (_taxExemption == value)
        //            return;
        //        _taxExemption = value;
        //        OnPropertyChanged();
        //    }
        //}


        private string _taxCode;
        /// <summary>
        /// Code für die Steuerbefreiung
        /// </summary>
        public string TaxCode
        {
            get { return _taxCode; }
            set {
                if (_taxCode == value)
                    return;
                _taxCode = value;
                OnPropertyChanged();
            }
        }


        private string _taxCodeText;
        /// <summary>
        /// Freitext Berschreibung der Steuerbefreiung
        /// </summary>
        public string TaxCodeText
        {
            get { return _taxCodeText; }
            set {
                if (_taxCodeText == value)
                    return;
                _taxCodeText = value;
                OnPropertyChanged();
            }
        }

    }
}
