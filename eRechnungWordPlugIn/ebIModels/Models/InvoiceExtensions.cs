using System;
using System.Collections.Generic;

namespace ebIModels.Models
{
    public partial class InvoiceType
    {

        public decimal? NetAmount { get; set; }

        public decimal? TaxAmount { get; set; }



        /// <summary>
        /// Berechnet die Gesamtsummen der aktuellen eRechnung
        /// </summary>
        public void CalculateTotals()
        {
            decimal gesamtBetrag = 0;
            decimal nettoBetrag = 0;
            decimal amount = 0;
            if (Tax != null && Tax.VAT!=null)
            {
                foreach (var vatItem in this.Tax.VAT)
                {
                    if (vatItem.Item is VATRateType)
                    {
                        VATRateType vatRate = vatItem.Item as VATRateType;
                        gesamtBetrag += (vatItem.TaxedAmount ?? 0) + (vatItem.Amount ?? 0);
                        nettoBetrag += (vatItem.TaxedAmount ?? 0);
                        amount += (vatItem.Amount ?? 0);
                    }
                    if (vatItem.Item is TaxExemptionType)
                    {
                        gesamtBetrag += (vatItem.TaxedAmount ?? 0);
                        nettoBetrag += (vatItem.TaxedAmount ?? 0);
                        //amount += (vatItem.Amount ?? 0);
                    }
                }

            }
            TotalGrossAmount = decimal.Round(gesamtBetrag,2);
            
            PayableAmount = decimal.Round(gesamtBetrag,2);
            if (Details.BelowTheLineItem.Count > 0)
            {
                foreach (BelowTheLineItemType item in Details.BelowTheLineItem)
                {
                    PayableAmount += item.LineItemAmount;
                }
            }
            NetAmount = decimal.Round(nettoBetrag,2);
            TaxAmount = amount;
        }
    }

    public partial class CountryType
    {
        // private readonly ICountryCodes _countryCodes = new CountryCodesModels();
        public CountryType(CountryCodeType country)
        {
            countryCodeField = country;
            countryCodeFieldSpecified = true;
            textField = new List<string>();
            var cText = CountryCodes.GetFromCode(country.ToString());
            textField.Add(cText.Country);
        }
        public string CountryCodeText
        {
            get
            {
                return CountryCode.ToString();
            }
            set
            {
                if (CountryCode.ToString().Equals(value)==true)
                    return;
                string val = value;
                CountryCode = (CountryCodeType) Enum.Parse(typeof(CountryCodeType), val);
            }
        }

    }

    public partial class FurtherIdentificationType
    {
        public enum SupportedIds
        {
            FS = 0,
            FN,
            FBG
        }
    }
   
    public partial class TaxType
    {
        /// <summary>
        /// Berechnet die Steuergesamtsummen der Rechnung
        /// </summary>
        /// <param name="itemList">Liste der Detailzeilenlisten</param>
        /// <returns>TaxType</returns>
        public static TaxType GetTaxTypeList(List<ItemListType> itemList, bool isTaxExemption, string vatText)
        {
            TaxType tax = new TaxType();
            tax.VAT = new List<VATItemType>();
            if (isTaxExemption)
            {
                TaxExemptionType taxEx = new TaxExemptionType()
                {
                    TaxExemptionCode = null,
                    Value=vatText
                };
                
            }
            if (itemList.Count == 0)
                return tax;
            // Dictionary für alle USt Angaben
            Dictionary<decimal, VATItemType> taxItems = new Dictionary<decimal, VATItemType>();
            foreach (ItemListType itemListType in itemList)
            {
                foreach (ListLineItemType lineItem in itemListType.ListLineItem)
                {
                    if (lineItem.Item != null)
                    {
                        if (lineItem.Item is VATRateType && !isTaxExemption)
                        {
                            var taxValue = lineItem.Item as VATRateType;
                            decimal taxVal = taxValue.Value ?? -1;
                            if (taxItems.ContainsKey(taxVal))
                            {
                                taxItems[taxVal].TaxedAmount += lineItem.LineItemAmount;
                                taxItems[taxVal].Amount += (lineItem.LineItemAmount * taxValue.Value / 100);
                            }
                            else
                            {
                                taxItems.Add(taxVal, new VATItemType()
                                {
                                    Amount = (lineItem.LineItemAmount * taxValue.Value / 100),
                                    Item = new VATRateType() { TaxCode = taxValue.TaxCode, Value = taxValue.Value },
                                    TaxedAmount = lineItem.LineItemAmount
                                });
                            }
                        }
                        if (lineItem.Item is TaxExemptionType)
                        {
                            decimal taxVal = 0;
                            if (taxItems.ContainsKey(taxVal))
                            {
                                taxItems[taxVal].TaxedAmount += lineItem.LineItemAmount;
                                taxItems[taxVal].Amount =0;
                            }
                            else
                            {
                                taxItems.Add(taxVal, new VATItemType()
                                {
                                    Amount = 0,
                                    Item = new TaxExemptionType() { TaxExemptionCode=null, Value=vatText },
                                    TaxedAmount = lineItem.LineItemAmount
                                });
                            }
                        }
                    }

                }
            }
            foreach (var taxItem in taxItems)
            {
                tax.VAT.Add(taxItem.Value);
            }
            return tax;
        }
    }

    public partial class InvoiceRecipientType
    {

        private bool _bestellPositionErforderlich;
        public bool BestellPositionErforderlich
        {
            get { return _bestellPositionErforderlich; }
            set
            {
                if (_bestellPositionErforderlich == value)
                    return;
                _bestellPositionErforderlich = value;
            }
        }

    }
}
