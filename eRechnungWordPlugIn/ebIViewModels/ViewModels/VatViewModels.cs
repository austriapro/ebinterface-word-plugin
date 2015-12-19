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

        public TaxType GetTaxType()
        {
            if (VatViewList.Count == 0)
                return null;
            TaxType tax = new TaxType();            
            tax.VAT = new List<VATItemType>();
            foreach (VatViewModel vatView in VatViewList)
            {
                VATItemType vatItem = new VATItemType();
                vatItem.Amount = vatView.VatAmount;
                vatItem.TaxedAmount = vatView.VatBaseAmount;
                if (vatView.TaxExemption == true)
                {
                    TaxExemptionType taxex = new TaxExemptionType();
                    taxex.TaxExemptionCode = vatView.TaxCode;
                    taxex.Value = vatView.TaxCodeText;
                    vatItem.Item = taxex;
                }
                else
                {
                    VATRateType vatRate = new VATRateType();
                    vatRate.TaxCode = vatView.TaxCode;
                    vatRate.Value = vatView.VatPercent;
                    vatItem.Item = vatRate;
                }
                tax.VAT.Add(vatItem);
            }
            return tax;
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
                vatModel.VatBaseAmount = vatItemType.TaxedAmount ?? 0;
                vatModel.VatAmount = vatItemType.Amount ?? 0;
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
                    vatModel.VatPercent = rate.Value ?? 0;
                    vatModel.TaxCode = rate.TaxCode;
                }
                vatView.VatViewList.Add(vatModel);
            }

            return vatView;
        }
    }
}
