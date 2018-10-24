using System;
using System.Collections.Generic;
using ExtensionMethods;
using System.Linq;
using ebIModels.Schema;
using ebIModels.Services;
using SettingsManager;
using System.Diagnostics;
using LogService;

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
            this.InvoiceCurrency = ebIModels.Mapping.ModelConstants.CurrencyCodeFixed;
            this.additionalInformationField = new List<AdditionalInformationType>();
            this.billerField = new BillerType();
            this.cancelledOriginalDocumentField = new CancelledOriginalDocumentType();
            this.deliveryField = new DeliveryType();
            this.detailsField = new DetailsType();
            this.documentTypeField = new DocumentTypeType();
            this.invoiceRecipientField = new InvoiceRecipientType();
            this.orderingPartyField = new OrderingPartyType();
            this.paymentConditionsField = new PaymentConditionsType();
            this.paymentMethodField = new PaymentMethodType();
            this.reductionAndSurchargeDetailsField = new ReductionAndSurchargeDetailsType();
            this.relatedDocumentField = new List<RelatedDocumentType>();
            this.taxField = new TaxType();
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

        internal void UpdateTaxTypeList(List<ItemListType> itemList) //, bool isTaxExemption, string vatText)
        {

            if (itemList == null || itemList.Count == 0)
            {
                this.taxField = new TaxType();
                return;
            }

            // Dictionary für alle USt Angaben
            var tax = itemList[0].ListLineItem.GroupBy(s => new { Prozent = s.TaxItem.TaxPercent.Value, Code = s.TaxItem.TaxPercent.TaxCategoryCode })
                      .Select(p => new TaxItemType
                      {
                          TaxPercent = new TaxPercentType()
                          {
                              Value = p.Key.Prozent,
                              TaxCategoryCode = p.Key.Code
                          },
                          TaxableAmount = p.Sum(x => x.LineItemAmount),
                          TaxAmount = p.Sum(x => x.LineItemAmount) * p.Key.Prozent / 100,
                          Comment = p.FirstOrDefault().TaxItem.Comment
                      });


            //Log.DumpToLog(CallerInfo.Create(), tax);
            this.taxField = new TaxType() { TaxItem = tax.ToList() };
        }
        /// <summary>
        /// Berechnet die Gesamtsummen der aktuellen eRechnung
        /// </summary>
        public void CalculateTotals()
        {
            TotalGrossAmount = 0;
            PayableAmount = 0;
            NetAmount = 0;
            TaxAmountTotal = 0;
            if (this.detailsField == null || this.detailsField.ItemList == null)
            {
                return;
            }
            UpdateTaxTypeList(this.detailsField.ItemList);


            // Es gibt nur ein Element in der Itemlist
            var totals = from a in this.Details.ItemList[0].ListLineItem
                         group a by 1 into g
                         select new
                         {
                             netto = g.Sum(x => x.LineItemAmount),
                             ustGesamt = g.Sum(x => x.TaxItem.TaxPercent.Value * x.LineItemAmount / 100).FixedFraction(2)
                         };

            decimal nettoBetrag = 0; //            totals.FirstOrDefault().netto.FixedFraction(2);
            decimal taxAmount = 0;   //            totals.FirstOrDefault().ustGesamt.FixedFraction(2);
            if (totals.Count()!=0)
            {
                nettoBetrag = totals.FirstOrDefault().netto.FixedFraction(2);
                taxAmount = totals.FirstOrDefault().ustGesamt.FixedFraction(2);

            }
            TotalGrossAmount = nettoBetrag + taxAmount;
            PayableAmount = TotalGrossAmount + PrepaidAmount;
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
            Log.LogWrite(CallerInfo.Create(), Log.LogPriority.Medium, $"Save Invoice Version {version.ToString()}");
            var invoice = (IInvoiceBase)Mapping.MapInvoice.MapToEbInterface(this, version);
            // var result = invoice.Save(filename);
            EbInterfaceResult result = null;
            switch (version)
            {
                case EbIVersion.V4P0:
                    result = ((ebIModels.Schema.ebInterface4p0.InvoiceType)invoice).Save(filename);
                    break;
                case EbIVersion.V4P1:
                    result = ((ebIModels.Schema.ebInterface4p1.InvoiceType)invoice).Save(filename);
                    break;
                case EbIVersion.V4P2:
                    result = ((ebIModels.Schema.ebInterface4p2.InvoiceType)invoice).Save(filename);

                    break;
                case EbIVersion.V4P3:
                    result = ((ebIModels.Schema.ebInterface4p3.InvoiceType)invoice).Save(filename);

                    break;
                case EbIVersion.V5P0:
                    result = ((ebIModels.Schema.ebInterface5p0.InvoiceType)invoice).Save(filename);

                    break;
                default:
                    break;
            }
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

    public partial class BillerType
    {
        public BillerType()
        {


        }
    }
    public partial class AbstractPartyType
    {
        public AbstractPartyType()
        {
            this.addressField = new AddressType();
            this.orderReferenceField = new OrderReferenceType();
            this.contactField = new ContactType()
            {
                Email = new List<string>(),
                Phone = new List<string>()
            };
        }

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
            this.lineItemAmountField = netAmount.FixedFraction(2);
            //Log.DumpToLog(CallerInfo.Create(), this);
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
            this.itemField = null;
        }
    }

    public partial class AddressType
    {
        public AddressType()
        {
            this.countryField = new CountryType(CountryCodeType.AT);
        }
    }


    public partial class PaymentConditionsType
    {
        public PaymentConditionsType()
        {
            this.discountField = new List<DiscountType>();
            this.dueDateField = DateTime.Today;
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
        public TaxItemType()
        {
            TaxPercent = new TaxPercentType()
            {
                TaxCategoryCode = PlugInSettings.Default.MwStDefaultValue.Code,
                Value = PlugInSettings.Default.MwStDefaultValue.MwStSatz
            };
        }


    }
    public partial class TaxType
    {

        public TaxType()
        {
            this.taxItemField = new List<TaxItemType>();
            this.otherTaxField = null;

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

