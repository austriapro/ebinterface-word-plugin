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
using SettingsManager;

namespace ebIViewModels.ViewModels
{

    public class DetailsListConverter
    {
        private BindingList<DetailsViewModel> _detailsList = new BindingList<DetailsViewModel>();
        /// <summary>
        /// Comment
        /// </summary>
        public BindingList<DetailsViewModel> DetailsList
        {
            get { return _detailsList; }
            set {
                if (_detailsList == value)
                    return;
                _detailsList = value;
            }
        }

        /// <summary>
        /// Converts the Viewmodel DetailsViewModelList to Model ItemListType
        /// </summary>
        /// <param name="detailsList">The details list.</param>
        /// <param name="orderId">The order identifier.</param>
        /// <returns>List of ItemListType</returns>
        public static List<ItemListType> ConvertToItemList(BindingList<DetailsViewModel> detailsList, string orderId)
        {
            ItemListType itemList = new ItemListType
            {
                ListLineItem = new List<ListLineItemType>()
            };
            int posNr = 1;
            foreach (DetailsViewModel details in detailsList)
            {
                ListLineItemType lineItem = new ListLineItemType
                {
                    ArticleNumber = new List<ArticleNumberType>(){new ArticleNumberType()
                {
                    Value = details.ArtikelNr
                }},
                    Description = new List<string>() { details.Bezeichnung },
                    PositionNumber = posNr.ToString()
                };
                posNr++;
                lineItem.InvoiceRecipientsOrderReference.OrderID = orderId;
                lineItem.InvoiceRecipientsOrderReference.OrderPositionNumber = details.BestellBezug;

                lineItem.UnitPrice = new UnitPriceType()
                {
                    Value = details.EinzelPreis
                };
                TaxItemType taxItem = new TaxItemType()
                {
                    TaxableAmount = lineItem.LineItemAmount,
                    TaxPercent = new TaxPercentType()
                    {
                        TaxCategoryCode = details.VatItem.Code,
                        Value = details.VatItem.MwStSatz
                    },
                    TaxAmount = (lineItem.LineItemAmount * details.VatItem.MwStSatz / 100).FixedFraction(2)
                };
                lineItem.TaxItem = taxItem;
                lineItem.Quantity = new UnitType()
                {
                    Unit = details.Einheit,
                    Value = details.Menge
                };
                if (details.Rabatt != 0)
                {
                    ReductionAndSurchargeListLineItemDetailsType red = new ReductionAndSurchargeListLineItemDetailsType
                    {
                        ItemsElementName = new List<ItemsChoiceType>()
                    {
                        ItemsChoiceType.ReductionListLineItem
                    },
                        Items = new List<object>()
                    };
                    ReductionAndSurchargeBaseType redBase = new ReductionAndSurchargeBaseType
                    {
                        BaseAmount = details.NettoBasisBetrag,
                        Percentage = details.Rabatt,
                        PercentageSpecified = true
                    };
                    red.Items.Add(redBase);
                    lineItem.ReductionAndSurchargeListLineItemDetails = red;
                }
                lineItem.LineItemAmount = details.NettoBetragZeile;
                itemList.ListLineItem.Add(lineItem);
            }
            List<ItemListType> item = new List<ItemListType>
            {
                itemList
            };
            return item;
        }

        /// <summary>
        /// Loads Model List of ItemListType into ViewModel DetailsViewModelList List
        /// </summary>
        /// <param name="itemList">The item list.</param>
        /// <param name="uc">The uc.</param>
        /// <param name="bestPosRequired">if set to <c>true</c> [best position required].</param>
        /// <returns></returns>
        public static DetailsListConverter Load(List<ItemListType> itemList, IUnityContainer uc, bool bestPosRequired)
        {
            // ItemListType listType = details.ItemList.FirstOrDefault(); // DAs PlugIn hat nur hier nur einen Eintrag

            DetailsListConverter details = uc.Resolve<DetailsListConverter>(); // new DetailsListViewModel();

            if (itemList==null)
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
                DetailsViewModel detailsVM = uc.Resolve<DetailsViewModel>(new ParameterOverride("bestPosRequired", bestPosRequired)); //new DetailsViewModel();

                detailsVM.BestellBezug = listLineItem.InvoiceRecipientsOrderReference.OrderPositionNumber;  // .UnescapeXml();
                var articleNumberType = listLineItem.ArticleNumber.FirstOrDefault();
                if (articleNumberType != null)
                    detailsVM.ArtikelNr = articleNumberType.Value;
                detailsVM.Bezeichnung = listLineItem.Description[0];  // .UnescapeXml();

                // Muss vor Zuweisung des Einzelpreis stehen damit UpdateTotal nicht stirbt

                detailsVM.VatItem = TaxItemType.GetVatValueFromTaxItem(listLineItem.TaxItem, PlugInSettings.Default.VStBerechtigt);

                detailsVM.Einheit = listLineItem.Quantity.Unit;
                detailsVM.EinzelPreis = listLineItem.UnitPrice.Value;
                // det.GesamtBruttoBetrag = listLineItem.LineItemAmount;
                detailsVM.Menge = listLineItem.Quantity.Value;

                if (listLineItem.ReductionAndSurchargeListLineItemDetails != null)
                {
                    if (listLineItem.ReductionAndSurchargeListLineItemDetails.Items.FirstOrDefault() is ReductionAndSurchargeBaseType red)
                        detailsVM.Rabatt = red.Percentage;
                }
                // det.UpdateTotals();
                details.DetailsList.Add(detailsVM);
            }
            return details;
        }

    }
}
