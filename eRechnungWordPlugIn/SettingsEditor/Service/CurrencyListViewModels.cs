using System.Collections.Generic;
using System.Linq;
using ebIModels.Models;
using WinFormsMvvm;

namespace SettingsEditor.Service
{
    public class CurrencyListViewModels : ViewModelBase
    {

        private List<CurrencyListViewModel> _dropDownList = new List<CurrencyListViewModel>();
        public List<CurrencyListViewModel> DropDownList
        {
            get { return _dropDownList; }
            set
            {
                if (_dropDownList == value)
                    return;
                _dropDownList = value;
                OnPropertyChanged();
            }
        }

        public CurrencyListViewModels GetList(List<CountryCodeModel> countryCodes)
        {            
            DropDownList = new List<CurrencyListViewModel>();
            foreach (var countryCodeModel in countryCodes)
            {
                DropDownList.Add(new CurrencyListViewModel
                {
                    Code = countryCodeModel.Code, DisplayText = countryCodeModel.Country
                });
            }
            return this;
        }


        public CurrencyListViewModels GetList(List<DocumentTypeModel> docTypes)
        {
            DropDownList = new List<CurrencyListViewModel>();
            foreach (DocumentTypeModel docModel in docTypes)
            {
                DropDownList.Add(new CurrencyListViewModel
                {
                    Code = docModel.CodeEnglish,
                    DisplayText = docModel.TextGerman
                });
            }
            return this;
        }

        public CurrencyListViewModels GetList(List<string> strings )
        {
            DropDownList = new List<CurrencyListViewModel>();
            foreach (string cc in strings)
            {
                DropDownList.Add(new CurrencyListViewModel() { Code = cc, DisplayText = cc });
            }
            return this;
        }

        public CurrencyListViewModels GetList(List<InvoiceSubtype> variants)
        {
            DropDownList = new List<CurrencyListViewModel>();
            foreach (InvoiceSubtype cc in variants)
            {
                DropDownList.Add(new CurrencyListViewModel() { Code = cc.VariantOption.ToString(), DisplayText = cc.FriendlyName });
            }
            return this;
            
        }

        public string GetText(string code)
        {
            var findCode = from text in _dropDownList
                           where text.Code == code
                           select text.DisplayText;
            if (findCode.Any())
            {
                return findCode.FirstOrDefault();
            }
            return null;
            
        }

        public string GetCode(string selectedText)
        {
            var findCode = from text in _dropDownList
                           where text.DisplayText == selectedText
                           select text.Code;
            if (findCode.Any())
            {
                return findCode.FirstOrDefault();
            }
            return null;
        }
    }
}
