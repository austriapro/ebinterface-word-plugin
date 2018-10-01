using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Models;
using WinFormsMvvm;
using SettingsManager;

namespace ebIViewModels.ViewModels
{
    public class VatViewModels : ViewModelBase
    {
        private List<VatViewModel> _vatViewList;
        /// <summary>
        /// Liste der VatViews
        /// </summary>
        public List<VatViewModel> VatViewList
        {
            get { return _vatViewList; }
            set
            {
                if (_vatViewList == value)
                    return;
                _vatViewList = value;
                OnPropertyChanged();
            }
        }

        public VatViewModels()
        {
            _vatViewList = new List<VatViewModel>();
        }

        public static VatViewModels Load(TaxType taxType)
        {
            VatViewModels vatView = new VatViewModels();
            if (!PlugInSettings.Default.VStBerechtigt)
            {
                VatViewModel vatModel = new VatViewModel();
                vatModel.TaxExemption = true;
                vatModel.TaxCodeText = PlugInSettings.Default.VStText;
                vatModel.VatAmount = 0;
                vatModel.VatPercent = 0;
                vatModel.TaxCode = null;
                vatView.VatViewList.Add(vatModel);
                return vatView;
            }
            if (taxType == null)
            {
                return vatView;
            }
            if (taxType.VAT == null)
            {
                return vatView;
            }
            
            foreach (VATItemType vatItemType in taxType.VAT)
            {
                VatViewModel vatModel = new VatViewModel();
                vatModel.VatBaseAmount = vatItemType.TaxedAmount;
                vatModel.VatAmount = vatItemType.Amount;
                if (vatItemType.Item is TaxExemptionType) // Steuerbefreit ...
                {
                    TaxExemptionType taxExemption = (TaxExemptionType)vatItemType.Item;
                    vatModel.VatPercent = 0;
                    vatModel.TaxExemption = true;
                    vatModel.TaxCode = taxExemption.TaxExemptionCode;
                    vatModel.TaxCodeText = taxExemption.Value;
                }
                else // MwSt Satz
                {
                    vatModel.TaxExemption = false;
                    VATRateType rate = (VATRateType)vatItemType.Item;
                    vatModel.VatPercent = rate.Value;
                    vatModel.TaxCode = rate.TaxCode;
                    

                    
                }
                vatView.VatViewList.Add(vatModel);
            }

            return vatView;
        }
    }
}
