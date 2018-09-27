using System;
using System.Collections.Generic;
using ExtensionMethods;
using System.Linq;

namespace ebIModels.Models
{
    public partial class InvoiceModel
    {

        // public decimal? NetAmount { get; set; }
        private decimal? _netAmount;

        public decimal? NetAmount
        {
            get { return _netAmount.FixedFraction(2); }
            set { _netAmount = value.FixedFraction(2); }
        }

        private decimal? prepaidAmountField;
        public decimal? PrepaidAmount
        {
            get => this.prepaidAmountField.FixedFraction(2);
            set {
                this.prepaidAmountField = value.FixedFraction(2);
            }
        }
        //public decimal? TaxAmount { get; set; }
        private decimal? _taxAmountTotal;

        public decimal? TaxAmountTotal
        {
            get { return _taxAmountTotal.FixedFraction(2); }
            set { _taxAmountTotal = value.FixedFraction(2); }
        }



        /// <summary>
        /// Berechnet die Gesamtsummen der aktuellen eRechnung
        /// </summary>
        public void CalculateTotals()
        {
            decimal gesamtBetrag = 0;
            decimal nettoBetrag = 0;
            decimal taxAmount = 0;
            if (Tax != null)
            {
                foreach (var vatItem in this.Tax.TaxItem)
                {

                    gesamtBetrag += vatItem.TaxableAmount;
                    nettoBetrag += vatItem.TaxableAmount - vatItem.TaxAmount;
                    taxAmount += vatItem.TaxAmount;
                }

            }
            TotalGrossAmount = gesamtBetrag;

            PayableAmount = gesamtBetrag + PrepaidAmount;
            NetAmount = nettoBetrag;
            TaxAmountTotal = taxAmount;
        }
    }

    public partial class AbstractPartyType
    {

        public void SetFurtherIdenfication(FurtherIdentificationType.SupportedIds id, string value)
        {
            if (FurtherIdentification == null)
            {
                FurtherIdentification = new List<FurtherIdentificationType>();
            }
            var fId = FurtherIdentification.FirstOrDefault(p => p.IdentificationType == id.ToString());
            if (fId != null)
            {
                FurtherIdentification.Remove(fId);
            }
            FurtherIdentification.Add(new FurtherIdentificationType() { IdentificationType = id.ToString(), Value = value });

        }
        public string GetFurtherIdentification(FurtherIdentificationType.SupportedIds id)
        {
            if (FurtherIdentification == null)
            {
                return "";
            }
            var fId = FurtherIdentification.FirstOrDefault(p => p.IdentificationType == id.ToString());
            if (fId == null)
            {
                return "";
            }
            return fId.Value;
        }
    }
    public partial class CountryType
    {
        // private readonly ICountryCodes _countryCodes = new CountryCodesModels();
        public CountryType(CountryCodeType country)
        {
            var cText = CountryCodes.GetFromCode(country.ToString());
        }
        public string CountryCodeText
        {
            get {
                return CountryCode.ToString();
            }
            set {
                if (CountryCode.ToString().Equals(value) == true)
                    return;
                string val = value;
                CountryCode = (CountryCodeType)Enum.Parse(typeof(CountryCodeType), val);
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

            if (itemList.Count == 0)
                return tax;
            // Dictionary für alle USt Angaben
            Dictionary<decimal, TaxItemType> taxItems = new Dictionary<decimal, TaxItemType>();
            foreach (ItemListType itemListType in itemList)
            {
                foreach (ListLineItemType lineItem in itemListType.ListLineItem)
                {
                    if (lineItem.TaxItem != null)
                    {

                        var taxValue = lineItem.TaxItem.TaxPercent;
                        decimal taxVal = taxValue.Value;
                        if (taxItems.ContainsKey(taxVal))
                        {
                            taxItems[taxVal].TaxableAmount += lineItem.LineItemAmount.GetValueOrDefault();
                            //taxItems[taxVal].Amount += (lineItem.LineItemAmount * taxValue.Value / 100); // Bad Idea produces to differences
                        }
                        else
                        {
                            taxItems.Add(taxVal, lineItem.TaxItem);
                        }
                    }

                }
            }
            foreach (var taxItem in taxItems)
            {
                var item = taxItem.Value;
                item.TaxPercent.Value = (item.TaxableAmount * item.TaxPercent.Value / 100).FixedFraction(2);
                tax.TaxItem.Add(item);
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
            set {
                if (_bestellPositionErforderlich == value)
                    return;
                _bestellPositionErforderlich = value;
            }
        }

    }
}
