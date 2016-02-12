using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Models;
using ExtensionMethods;
using Microsoft.Practices.Unity;
using WinFormsMvvm;

namespace ebIViewModels.ViewModels
{
// ToDo: Taxexemption
    public class DetailsListConverter
    {
        private BindingList<DetailsViewModel> _detailsList = new BindingList<DetailsViewModel>();
        /// <summary>
        /// Comment
        /// </summary>
        public BindingList<DetailsViewModel> DetailsList
        {
            get { return _detailsList; }
            set
            {
                if (_detailsList == value)
                    return;
                _detailsList = value;                
            }
        }

        public static List<ItemListType> ConvertToItemList(BindingList<DetailsViewModel> detailsList, string orderId)
        {
            ItemListType itemList = new ItemListType();
            itemList.ListLineItem = new List<ListLineItemType>();
            int posNr = 1;
            foreach (DetailsViewModel details in detailsList)
            {
                ListLineItemType lineItem = new ListLineItemType();
                lineItem.ArticleNumber = new List<ArticleNumberType>(){new ArticleNumberType()
                {
                    Text = new List<string>(){details.ArtikelNr}
                }};
                lineItem.Description = new List<string>() { details.Bezeichnung };
                lineItem.PositionNumber = posNr.ToString();
                posNr++;
                lineItem.InvoiceRecipientsOrderReference.OrderID = orderId;
                lineItem.InvoiceRecipientsOrderReference.OrderPositionNumber = details.BestellBezug;
                
                lineItem.UnitPrice = new UnitPriceType()
                {
                    Value = details.EinzelPreis
                };
                if (!details.Taxexemption)
                {
                    VATRateType rate = new VATRateType();
                    rate.Value = details.VatSatz;
                    rate.TaxCode = details.VatCode;
                    lineItem.Item = rate;
                }
                lineItem.Quantity = new UnitType()
                {
                    Unit = details.Einheit, 
                    Value = details.Menge
                };
                if (details.Rabatt != null)
                {
                    ReductionAndSurchargeListLineItemDetailsType red = new ReductionAndSurchargeListLineItemDetailsType();
                    red.ItemsElementName = new List<ItemsChoiceType>()
                    {
                        ItemsChoiceType.ReductionListLineItem
                    };
                    red.Items = new List<object>();
                    ReductionAndSurchargeBaseType redBase = new ReductionAndSurchargeBaseType();
                    redBase.BaseAmount = details.NettoBasisBetrag;
                    redBase.Percentage = details.Rabatt;
                    redBase.PercentageSpecified = true;
                    red.Items.Add(redBase);
                    lineItem.ReductionAndSurchargeListLineItemDetails = red;
                    lineItem.DiscountFlag = true;
                    lineItem.DiscountFlagSpecified = true;
                }
                lineItem.LineItemAmount = details.NettoBetragZeile;
                itemList.ListLineItem.Add(lineItem);
            }
            List<ItemListType> item = new List<ItemListType>();
            item.Add(itemList);
            return item;
        }


        public static DetailsListConverter Load(List<ItemListType> itemList,IUnityContainer uc, bool bestPosRequired)
        {
            // ItemListType listType = details.ItemList.FirstOrDefault(); // DAs PlugIn hat nur hier nur einen Eintrag

            DetailsListConverter details = uc.Resolve<DetailsListConverter>(); // new DetailsListViewModel();
            if (!itemList.Any())
            {
                return details;
            }
            ItemListType listType = itemList.FirstOrDefault(); // DAs PlugIn hat nur hier nur einen Eintrag

            if (listType == null)
            {
                return details;
            }
            if (!listType.ListLineItem.Any())
            {
                return details;
            }
            foreach (ListLineItemType listLineItem in listType.ListLineItem)
            {
                DetailsViewModel det = uc.Resolve<DetailsViewModel>(new ParameterOverride("bestPosRequired", bestPosRequired)); //new DetailsViewModel();

                det.BestellBezug = listLineItem.InvoiceRecipientsOrderReference.OrderPositionNumber;  // .UnescapeXml();
                var articleNumberType = listLineItem.ArticleNumber.FirstOrDefault();
                if (articleNumberType != null)
                    det.ArtikelNr = articleNumberType.Text.FirstOrDefault();  // .UnescapeXml();
                det.Bezeichnung = listLineItem.Description[0];  // .UnescapeXml();
                det.Einheit = listLineItem.Quantity.Unit;
                det.EinzelPreis = listLineItem.UnitPrice.Value ?? 0;
                // det.GesamtBruttoBetrag = listLineItem.LineItemAmount ?? 0;
                det.Menge = listLineItem.Quantity.Value ?? 0;
                if (listLineItem.Item is TaxExemptionType)
                {
                    det.Taxexemption = true;
                    det.VatSatz = 0;
                }
                else
                {
                    det.Taxexemption = false;
                    VATRateType rate = (VATRateType)listLineItem.Item;
                    if (rate != null)
                        det.VatSatz = rate.Value ?? 0;
                }
                if (listLineItem.ReductionAndSurchargeListLineItemDetails!=null)
                {
                    var red = listLineItem.ReductionAndSurchargeListLineItemDetails.Items.FirstOrDefault() as ReductionAndSurchargeBaseType;
                    if (red != null) det.Rabatt = red.Percentage ?? 0;
                }
                // det.UpdateTotals();
                details.DetailsList.Add(det);
            }
            return details;
        }

    }
}
