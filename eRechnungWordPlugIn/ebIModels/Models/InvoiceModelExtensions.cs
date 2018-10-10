using System;
using System.Collections.Generic;
using ExtensionMethods;
using System.Linq;
using ebIModels.Schema;
using ebIModels.Services;
using SettingsManager;
using System.Diagnostics;

namespace ebIModels.Models
{
    public enum EbIVersion
    {
        V4P0,
        V4P1,
        V4P2,
        V4P3,
        V5P0
    }

    /// <summary>
    /// Enthält Hilfsfelder und Methoden für das InvoiceModel
    /// Damit soll es möglich sein, die Model Datei leicht zu ändern
    /// </summary>
    /// <seealso cref="ebIModels.Models.InvoiceModel" />
    /// <seealso cref="ebIModels.Models.IInvoiceModel" />
    public partial class InvoiceModel : IInvoiceModel
    {
        public InvoiceModel()
        {
            this.paymentConditionsField = new PaymentConditionsType();
            this.paymentMethodField = new PaymentMethodType();
            this.taxField = new TaxType();
            this.reductionAndSurchargeDetailsField = new ReductionAndSurchargeDetailsType();
            this.detailsField = new DetailsType();
            this.orderingPartyField = new OrderingPartyType();
            this.invoiceRecipientField = new InvoiceRecipientType();
            this.billerField = new BillerType();
            this.deliveryField = new DeliveryType();
            this.InvoiceCurrency = ebIModels.Mapping.ModelConstants.CurrencyCodeFixed;
            this.cancelledOriginalDocumentField = new CancelledOriginalDocumentType();
            this.relatedDocumentField = new List<RelatedDocumentType>();
            this.additionalInformationField = new List<AdditionalInformationType>();
            this.deliveryField = new DeliveryType();
            this.billerField = new BillerType();
            this.invoiceRecipientField = new InvoiceRecipientType();
            this.orderingPartyField = new OrderingPartyType();
            this.detailsField = new DetailsType();
            this.reductionAndSurchargeDetailsField = new ReductionAndSurchargeDetailsType();
            this.taxField = new TaxType();
            this.paymentMethodField = new PaymentMethodType();
            this.paymentConditionsField = new PaymentConditionsType();
            this.documentTypeField = new DocumentTypeType();
            //this.SetInvoiceVersion();
            //this.CurrentSchemas = _schemaInfo;
            this.Version = Models.EbIVersion.V5P0;

        }

        public InvoiceSubtype InvoiceSubtype { get; set; }

        public EbIVersion Version { get; set; }

        public static readonly Models.EbIVersion LatestVersion = Models.EbIVersion.V5P0;

        //private const string SchemaPath = "ebIModels.Schema.";
        private static string GetTfsString()
        {
            string ret;
            var vInfo = new ProductInfo();
            ret = string.Format(" V{0} ({1}, {2:G})", vInfo.VersionInfo.ChangeSetId, vInfo.VersionInfo.BuildName, vInfo.VersionInfo.CompileTime);
            return ret;
        }

        public bool InitFromSettings { get; set; }
        private decimal _netAmount;
        public decimal NetAmount
        {
            get { return _netAmount.FixedFraction(2); }
            set { _netAmount = value.FixedFraction(2); }
        }

        private decimal _taxAmountTotal;
        public decimal TaxAmountTotal
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

        public EbInterfaceResult IsValidInvoice()
        {
            throw new NotImplementedException();
        }

        //public EbInterfaceResult Save(string filename)
        //{
        //    throw new NotImplementedException();
        //}

        public EbInterfaceResult Save(string filename, EbIVersion version)
        {
            var invoice = (IInvoiceBase)Mapping.MapInvoice.MapToEbInterface(this, version);
            var result = invoice.Save(filename);
            return result;
        }

        public void SaveTemplate(string filename)
        {
            Schema.ebInterface5p0.InvoiceType invoice = (Schema.ebInterface5p0.InvoiceType)Mapping.MapInvoice.MapToEbInterface(this, Models.EbIVersion.V5P0);
            invoice.SaveTemplate(filename);
        }

        //public static string RemoveVorlageText(string text)
        //{
        //    return text.Replace(VorlageString, EbInvoiceNumber);
        //}
    }
    public partial class AccountType
    {
        public AccountType()
        {
            this.bankCodeField = new BankCodeType();
        }
    }

    public partial class UniversalBankTransactionType
    {

        public UniversalBankTransactionType()
        {
            this.paymentReferenceField = new PaymentReferenceType();
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
                return CountryCode?.ToString();
            }
            set {
                if (CountryCode.ToString().Equals(value) == true)
                    return;
                string val = value;
                CountryCode = ((CountryCodeType)Enum.Parse(typeof(CountryCodeType), val)).ToString();
            }
        }

    }


    public partial class DetailsType
    {

        public void RecalcItemList()
        {
            if (ItemList == null)
            {
                return;
            }
            foreach (ItemListType item in ItemList)
            {
                foreach (ListLineItemType lineItem in item.ListLineItem)
                {
                    lineItem.ReCalcLineItemAmount();
                }
            }
        }
    }

    public partial class ListLineItemType
    {
        public ListLineItemType()
        {
            this.additionalInformationField = new List<AdditionalInformationType>();
            this.invoiceRecipientsOrderReferenceField = new OrderReferenceDetailType();
            this.billersOrderReferenceField = new OrderReferenceDetailType();
            this.deliveryField = new DeliveryType();
            this.reductionAndSurchargeListLineItemDetailsField = new ReductionAndSurchargeListLineItemDetailsType();
            this.unitPriceField = new UnitPriceType();
            this.quantityField = new UnitType();
            // this\.articleNumberField.=.new.List<ArticleNumberType>();
            // this\.descriptionField.=.new.List<string>();
        }
        public void ReCalcLineItemAmount()
        {
            decimal baseAmount = UnitPrice.Value * Quantity.Value;
            decimal netAmount = baseAmount;
            if ((ReductionAndSurchargeListLineItemDetails != null) && (ReductionAndSurchargeListLineItemDetails.Items.Any()))
            {
                decimal rabattProzent = ((ReductionAndSurchargeBaseType)ReductionAndSurchargeListLineItemDetails.Items[0]).Percentage;
                decimal rabatt = (baseAmount * rabattProzent / 100);
                netAmount = baseAmount - rabatt;
            }
            LineItemAmount = netAmount.FixedFraction(2);
        }
    }
    public partial class ReductionAndSurchargeListLineItemDetailsType
    {
        public ReductionAndSurchargeListLineItemDetailsType()
        {
            ItemsElementName = new List<ItemsChoiceType>();
            Items = new List<object>();
        }
    }
    public partial class DeliveryType
    {

        public DeliveryType()
        {
            this.addressField = new AddressType();
        }
    }

    public partial class AddressType
    {
        public AddressType()
        {
            this.countryField = new CountryType(CountryCodeType.AT);
        }
    }

    public partial class AbstractPartyType
    {

        public AbstractPartyType()
        {
            this.addressField = new AddressType();
            this.orderReferenceField = new OrderReferenceType();
        }
    }
    public partial class PaymentConditionsType
    {
        public PaymentConditionsType()
        {
            Discount = new List<DiscountType>();
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

    public partial class TaxItemType
    {
        public static VatDefaultValue GetVatValueFromTaxItem(TaxItemType tax, bool VatBerechtigt)
        {
            if (!VatBerechtigt)
            {
                return PlugInSettings.Default.IstNichtVStBerechtigtVatValue;
            }
            VatDefaultValue vatDefault = PlugInSettings.Default.VatDefaultValues.FirstOrDefault(p => p.Code == tax.TaxPercent.TaxCategoryCode);
            return vatDefault;
        }

    }
    public partial class TaxType
    {
        public TaxType()
        {
            TaxItem = new List<TaxItemType>();
        }
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
                        Debug.WriteLine($"Amount:{lineItem.LineItemAmount} - {lineItem.TaxItem.TaxPercent.Value}");
                        if (!taxItems.ContainsKey(taxVal))
                        {
                            taxItems.Add(taxVal, lineItem.TaxItem);
                        }
                        taxItems[taxVal].TaxableAmount += lineItem.LineItemAmount;
                    }

                }
            }
            foreach (var taxItem in taxItems)
            {
                var item = taxItem.Value;
                item.TaxPercent.Value = taxItem.Key;
                item.TaxAmount = item.TaxableAmount * item.TaxPercent.Value / 100;
                tax.TaxItem.Add(item);
            }
            return tax;
        }
    }
    public partial class ReductionAndSurchargeDetailsType
    {


        public ReductionAndSurchargeDetailsType()
        {
            Items = new List<object>();
            ItemsElementName = new List<ItemsChoiceType1>();
        }
    }
}

