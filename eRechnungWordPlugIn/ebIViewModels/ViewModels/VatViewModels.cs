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
                VatViewModel vatModel = new VatViewModel(PlugInSettings.Default.IstNichtVStBerechtigtVatValue.Code,
                    PlugInSettings.Default.IstNichtVStBerechtigtVatValue.Beschreibung,
                    PlugInSettings.Default.IstNichtVStBerechtigtVatValue.MwStSatz,
                    taxType.TaxItem.FirstOrDefault().TaxableAmount);
                vatView.VatViewList.Add(vatModel);
                return vatView;
            }
            if (taxType == null)
            {
                return vatView;
            }
            if (!taxType.TaxItem.Any())
            {
                return vatView;
            }
            
            foreach (var taxItem in taxType.TaxItem)
            {
                VatViewModel vatModel = new VatViewModel(
                    taxItem.TaxPercent.TaxCategoryCode, taxItem.Comment,
                    taxItem.TaxPercent.Value, taxItem.TaxableAmount);
                vatView.VatViewList.Add(vatModel);
            }

            return vatView;
        }
    }
}
