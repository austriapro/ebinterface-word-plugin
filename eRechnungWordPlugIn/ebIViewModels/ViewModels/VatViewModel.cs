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
        public const string FmtDecimal2 = "";
        private decimal _vatBaseAmount;
        /// <summary>
        /// Basisbetrag für die MwSt Berechnung
        /// </summary>
        public decimal VatBaseAmount
        {
            get { return _vatBaseAmount; }
            set
            {
                if (_vatBaseAmount == value)
                    return;
                _vatBaseAmount = value.FixedFraction(2);
                OnPropertyChanged();
                VatTotalAmount = _vatBaseAmount + VatAmount;
            }
        }

        private decimal _vatAmount;
        /// <summary>
        /// Betrag der MwSt = Basisbetrag * MwSt-Satz
        /// </summary>
        public decimal VatAmount
        {
            get { return _vatAmount; }
            set
            {
                if (_vatAmount == value)
                    return;
                _vatAmount = value.FixedFraction(2);
                OnPropertyChanged();
                VatTotalAmount = _vatAmount + VatBaseAmount;
            }
        }

        private decimal _vatTotalAmount;
        /// <summary>
        /// Gesamtbetrag = Basisbetrag + Betrag der MwSt
        /// </summary>
        public decimal VatTotalAmount
        {
            get { return _vatTotalAmount; }
            set
            {
                if (_vatTotalAmount == value)
                    return;
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
            set
            {
                if (_vatPercent == value)
                    return;
                _vatPercent = value;
                OnPropertyChanged();
                VatAmount = VatBaseAmount * _vatPercent / 100;
            }
        }

        private bool _taxExemption;
        /// <summary>
        /// Gibt an ob steuerbefreit (=true)
        /// </summary>
        public bool TaxExemption
        {
            get { return _taxExemption; }
            set
            {
                if (_taxExemption == value)
                    return;
                _taxExemption = value;
                OnPropertyChanged();
            }
        }


        private string _taxCode;
       /// <summary>
       /// Code für die Steuerbefreiung
       /// </summary>
        public string TaxCode
        {
            get { return _taxCode; }
            set
            {
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
            set
            {
                if (_taxCodeText == value)
                    return;
                _taxCodeText = value;
                OnPropertyChanged();
            }
        }
        
    }    
}
