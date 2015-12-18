using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Models;
using WinFormsMvvm;

namespace ebIViewModels.Services
{
    public class DropDownListViewModels
    {

        private List<DropDownListViewModel> _dropDownList = new List<DropDownListViewModel>();
        public List<DropDownListViewModel> DropDownList
        {
            get { return _dropDownList; }
            set
            {
                if (_dropDownList == value)
                    return;
                _dropDownList = value;
            }
        }

        public DropDownListViewModels GetList(List<CountryCodeModel> countryCodes)
        {            
            DropDownList = new List<DropDownListViewModel>();
            foreach (var countryCodeModel in countryCodes)
            {
                DropDownList.Add(new DropDownListViewModel
                {
                    Code = countryCodeModel.Code, DisplayText = countryCodeModel.Country
                });
            }
            return this;
        }


        public DropDownListViewModels GetList(List<DocumentTypeModel> docTypes)
        {
            DropDownList = new List<DropDownListViewModel>();
            foreach (DocumentTypeModel docModel in docTypes)
            {
                DropDownList.Add(new DropDownListViewModel
                {
                    Code = docModel.CodeEnglish,
                    DisplayText = docModel.TextGerman
                });
            }
            return this;
        }

        public DropDownListViewModels GetList(List<string> strings )
        {
            DropDownList = new List<DropDownListViewModel>();
            foreach (string cc in strings)
            {
                DropDownList.Add(new DropDownListViewModel() { Code = cc, DisplayText = cc });
            }
            return this;
        }

        public DropDownListViewModels GetList(List<InvoiceSubtype> variants)
        {
            DropDownList = new List<DropDownListViewModel>();
            foreach (InvoiceSubtype cc in variants)
            {
                DropDownList.Add(new DropDownListViewModel() { Code = cc.VariantOption.ToString(), DisplayText = cc.FriendlyName });
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
            return "";
            
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
            return "";
        }
    }
}
