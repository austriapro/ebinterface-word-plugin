using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using ebIModels.Mapping;
using ebIModels.Schema;
using ExtensionMethods;

namespace ebIModels.Models
{
    /// <summary>
    /// Internes Datenmodel einer ebInteface Rechnung basierend auf ebInterface 5.0
    /// Die benötigigten Ergänzungen sind in den Dateien 
    /// </summary>
    /// <seealso cref="ebIModels.Models.InvoiceModel" />
    /// <seealso cref="ebIModels.Models.IInvoiceModel" />
    public partial class InvoiceModel :  IInvoiceModel
    {
        private string invoiceNumberField;
        private System.DateTime invoiceDateField;
        private CancelledOriginalDocumentType cancelledOriginalDocumentField;
        private List<RelatedDocumentType> relatedDocumentField;
        private List<AdditionalInformationType> additionalInformationField;
        private DeliveryType deliveryField;
        private BillerType billerField;
        private InvoiceRecipientType invoiceRecipientField;
        private OrderingPartyType orderingPartyField;
        private DetailsType detailsField;
        private ReductionAndSurchargeDetailsType reductionAndSurchargeDetailsField;
        private TaxType taxField;
        private decimal totalGrossAmountField;
        private decimal prepaidAmountField;
        private bool prepaidAmountFieldSpecified;
        private decimal roundingAmountField;
        private bool roundingAmountFieldSpecified;
        private decimal payableAmountField;
        private PaymentMethodType paymentMethodField;
        private PaymentConditionsType paymentConditionsField;
        private string commentField;
        private string generatingSystemField;
        private DocumentTypeType documentTypeField;
        private CurrencyType invoiceCurrencyField;
        private bool manualProcessingField;
        private bool manualProcessingFieldSpecified;
        private string documentTitleField;
        private LanguageType languageField;
        private bool isDuplicateField;
        private bool isDuplicateFieldSpecified;
        /// <remarks/>
        public string InvoiceNumber
        {
            get { return this.invoiceNumberField; }
            set { this.invoiceNumberField = value; }
        }

        /// <remarks/>
        public System.DateTime InvoiceDate
        {
            get { return this.invoiceDateField; }
            set { this.invoiceDateField = value; }
        }

        /// <remarks/>
        public CancelledOriginalDocumentType CancelledOriginalDocument
        {
            get { return this.cancelledOriginalDocumentField; }
            set { this.cancelledOriginalDocumentField = value; }
        }

        /// <remarks/>
        public List<RelatedDocumentType> RelatedDocument
        {
            get { return this.relatedDocumentField; }
            set { this.relatedDocumentField = value; }
        }

        /// <remarks/>
        public List<AdditionalInformationType> AdditionalInformation
        {
            get { return this.additionalInformationField; }
            set { this.additionalInformationField = value; }
        }

        /// <remarks/>
        public DeliveryType Delivery
        {
            get { return this.deliveryField; }
            set { this.deliveryField = value; }
        }

        /// <remarks/>
        public BillerType Biller
        {
            get { return this.billerField; }
            set { this.billerField = value; }
        }

        /// <remarks/>
        public InvoiceRecipientType InvoiceRecipient
        {
            get { return this.invoiceRecipientField; }
            set { this.invoiceRecipientField = value; }
        }

        /// <remarks/>
        public OrderingPartyType OrderingParty
        {
            get { return this.orderingPartyField; }
            set { this.orderingPartyField = value; }
        }

        /// <remarks/>
        public DetailsType Details
        {
            get { return this.detailsField; }
            set { this.detailsField = value; }
        }

        /// <remarks/>
        public ReductionAndSurchargeDetailsType ReductionAndSurchargeDetails
        {
            get { return this.reductionAndSurchargeDetailsField; }
            set { this.reductionAndSurchargeDetailsField = value; }
        }

        /// <remarks/>
        public TaxType Tax
        {
            get { return this.taxField; }
            set { this.taxField = value; }
        }

        /// <remarks/>
        public decimal TotalGrossAmount
        {
            get { return this.totalGrossAmountField; }
            set { this.totalGrossAmountField = value; }
        }

        /// <remarks/>
        public decimal PrepaidAmount
        {
            get { return this.prepaidAmountField; }
            set { this.prepaidAmountField = value;
                // ToDo BelowTheLineItem setzen
            }
        }

        /// <remarks/>
        public bool PrepaidAmountSpecified
        {
            get { return this.prepaidAmountFieldSpecified; }
            set { this.prepaidAmountFieldSpecified = value; }
        }

        /// <remarks/>
        public decimal RoundingAmount
        {
            get { return this.roundingAmountField; }
            set { this.roundingAmountField = value; }
        }

        /// <remarks/>
        public bool RoundingAmountSpecified
        {
            get { return this.roundingAmountFieldSpecified; }
            set { this.roundingAmountFieldSpecified = value; }
        }

        /// <remarks/>
        public decimal PayableAmount
        {
            get { return this.payableAmountField; }
            set { this.payableAmountField = value; }
        }

        /// <remarks/>
        public PaymentMethodType PaymentMethod
        {
            get { return this.paymentMethodField; }
            set { this.paymentMethodField = value; }
        }

        /// <remarks/>
        public PaymentConditionsType PaymentConditions
        {
            get { return this.paymentConditionsField; }
            set { this.paymentConditionsField = value; }
        }

        /// <remarks/>
        public string Comment
        {
            get { return this.commentField; }
            set { this.commentField = value; }
        }

        /// <remarks/>
        public string GeneratingSystem
        {
            get { return this.generatingSystemField; }
            set { this.generatingSystemField = value; }
        }

        /// <remarks/>
        public DocumentTypeType DocumentType
        {
            get { return this.documentTypeField; }
            set { this.documentTypeField = value; }
        }

        /// <remarks/>
        public CurrencyType InvoiceCurrency
        {
            get { return this.invoiceCurrencyField; }
            set { this.invoiceCurrencyField = value; }
        }

        /// <remarks/>
        public bool ManualProcessing
        {
            get { return this.manualProcessingField; }
            set { this.manualProcessingField = value; }
        }

        /// <remarks/>
        public bool ManualProcessingSpecified
        {
            get { return this.manualProcessingFieldSpecified; }
            set { this.manualProcessingFieldSpecified = value; }
        }

        /// <remarks/>
        public string DocumentTitle
        {
            get { return this.documentTitleField; }
            set { this.documentTitleField = value; }
        }

        /// <remarks/>
        public LanguageType Language
        {
            get { return this.languageField; }
            set { this.languageField = value; }
        }

        /// <remarks/>
        public bool IsDuplicate
        {
            get { return this.isDuplicateField; }
            set { this.isDuplicateField = value; }
        }

        /// <remarks/>
        public bool IsDuplicateSpecified
        {
            get { return this.isDuplicateFieldSpecified; }
            set { this.isDuplicateFieldSpecified = value; }
        }

 
    }

    /// <remarks/>
    public partial class CancelledOriginalDocumentType
    {

        private string invoiceNumberField;
        private System.DateTime invoiceDateField;
        private DocumentTypeType documentTypeField;
        private bool documentTypeFieldSpecified;
        private string commentField;
        /// <remarks/>
        public string InvoiceNumber
        {
            get { return this.invoiceNumberField; }
            set { this.invoiceNumberField = value; }
        }

        /// <remarks/>
        public System.DateTime InvoiceDate
        {
            get { return this.invoiceDateField; }
            set { this.invoiceDateField = value; }
        }

        /// <remarks/>
        public DocumentTypeType DocumentType
        {
            get { return this.documentTypeField; }
            set { this.documentTypeField = value; }
        }

        /// <remarks/>
        public bool DocumentTypeSpecified
        {
            get { return this.documentTypeFieldSpecified; }
            set { this.documentTypeFieldSpecified = value; }
        }

        /// <remarks/>
        public string Comment
        {
            get { return this.commentField; }
            set { this.commentField = value; }
        }
    }

    /// <remarks/>
    public enum DocumentTypeType
    {

        /// <remarks/>
        CreditMemo,

        /// <remarks/>
        FinalSettlement,

        /// <remarks/>
        Invoice,

        /// <remarks/>
        InvoiceForAdvancePayment,

        /// <remarks/>
        InvoiceForPartialDelivery,

        /// <remarks/>
        SelfBilling,

        /// <remarks/>
        SubsequentCredit,

        /// <remarks/>
        SubsequentDebit,
    }

    /// <remarks/>
    public partial class AbstractPartyType
    {

        private string vATIdentificationNumberField;
        private List<FurtherIdentificationType> furtherIdentificationField;
        private OrderReferenceType orderReferenceField;
        private AddressType addressField;
        private ContactType contactField;
        /// <remarks/>
        public string VATIdentificationNumber
        {
            get { return this.vATIdentificationNumberField; }
            set { this.vATIdentificationNumberField = value; }
        }

        /// <remarks/>
        public List<FurtherIdentificationType> FurtherIdentification
        {
            get { return this.furtherIdentificationField; }
            set { this.furtherIdentificationField = value; }
        }

        /// <remarks/>
        public OrderReferenceType OrderReference
        {
            get { return this.orderReferenceField; }
            set { this.orderReferenceField = value; }
        }

        /// <remarks/>
        public AddressType Address
        {
            get { return this.addressField; }
            set { this.addressField = value; }
        }

        /// <remarks/>
        public ContactType Contact
        {
            get { return this.contactField; }
            set { this.contactField = value; }
        }
    }

    /// <remarks/>
    public partial class FurtherIdentificationType
    {

        private string identificationTypeField;
        private string valueField;
        /// <remarks/>
        public string IdentificationType
        {
            get { return this.identificationTypeField; }
            set { this.identificationTypeField = value; }
        }

        /// <remarks/>
        public string Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    public partial class OrderReferenceType
    {

        private string orderIDField;
        private System.DateTime referenceDateField;
        private bool referenceDateFieldSpecified;
        private string descriptionField;
        /// <remarks/>
        public string OrderID
        {
            get { return this.orderIDField; }
            set { this.orderIDField = value; }
        }

        /// <remarks/>
        public System.DateTime ReferenceDate
        {
            get { return this.referenceDateField; }
            set { this.referenceDateField = value; }
        }

        /// <remarks/>
        public bool ReferenceDateSpecified
        {
            get { return this.referenceDateFieldSpecified; }
            set { this.referenceDateFieldSpecified = value; }
        }

        /// <remarks/>
        public string Description
        {
            get { return this.descriptionField; }
            set { this.descriptionField = value; }
        }
    }

    /// <remarks/>
    public partial class AddressType
    {

        private List<AddressIdentifierType> addressIdentifierField;
        private string nameField;
        private string tradingNameField;
        private string streetField;
        private string pOBoxField;
        private string townField;
        private string zIPField;
        private CountryType countryField;
        private List<string> phoneField;
        private List<string> emailField;
        private List<string> addressExtensionField;
        /// <remarks/>
        public List<AddressIdentifierType> AddressIdentifier
        {
            get { return this.addressIdentifierField; }
            set { this.addressIdentifierField = value; }
        }

        /// <remarks/>
        public string Name
        {
            get { return this.nameField; }
            set { this.nameField = value; }
        }

        /// <remarks/>
        public string TradingName
        {
            get { return this.tradingNameField; }
            set { this.tradingNameField = value; }
        }

        /// <remarks/>
        public string Street
        {
            get { return this.streetField; }
            set { this.streetField = value; }
        }

        /// <remarks/>
        public string POBox
        {
            get { return this.pOBoxField; }
            set { this.pOBoxField = value; }
        }

        /// <remarks/>
        public string Town
        {
            get { return this.townField; }
            set { this.townField = value; }
        }

        /// <remarks/>
        public string ZIP
        {
            get { return this.zIPField; }
            set { this.zIPField = value; }
        }

        /// <remarks/>
        public CountryType Country
        {
            get { return this.countryField; }
            set { this.countryField = value; }
        }

        /// <remarks/>
        public List<string> Phone
        {
            get { return this.phoneField; }
            set { this.phoneField = value; }
        }

        /// <remarks/>
        public List<string> Email
        {
            get { return this.emailField; }
            set { this.emailField = value; }
        }

        /// <remarks/>
        public List<string> AddressExtension
        {
            get { return this.addressExtensionField; }
            set { this.addressExtensionField = value; }
        }
    }

    /// <remarks/>
    public partial class AddressIdentifierType
    {

        private string addressIdentifierType1Field;
        private string valueField;
        /// <remarks/>
        public string AddressIdentifierType1
        {
            get { return this.addressIdentifierType1Field; }
            set { this.addressIdentifierType1Field = value; }
        }

        /// <remarks/>
        public string Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    public partial class CountryType
    {
        private string countryCodeField;
        private string valueField;
        /// <remarks/>
        public string CountryCode
        {
            get { return this.countryCodeField; }
            set { this.countryCodeField = value; }
        }

        /// <remarks/>
        public string Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }
    public enum CountryCodeType
    {
        AF, AX, AL, DZ, AS, AD, AO, AI, AQ, AG, AR, AM, AW, AU, AT, AZ, BS, BH, BD, BB, BY, BE, BZ, BJ, BM, BT, BO, BQ, BA, BW, BV, BR,
        IO, BN, BG, BF, BI, KH, CM, CA, CV, KY, CF, TD, CL, CN, CX, CC, CO, KM, CG, CD, CK, CR, CI, HR, CU, CW, CY, CZ, DK, DJ, DM, DO,
        EC, EG, SV, GQ, ER, EE, ET, FK, FO, FJ, FI, FR, GF, PF, TF, GA, GM, GE, DE, GH, GI, GR, GL, GD, GP, GU, GT, GG, GN, GW, GY, HT,
        HM, VA, HN, HK, HU, IS, IN, ID, IR, IQ, IE, IM, IL, IT, JM, JP, JE, JO, KZ, KE, KI, KP, KR, KW, KG, LA, LV, LB, LS, LR, LY, LI,
        LT, LU, MO, MK, MG, MW, MY, MV, ML, MT, MH, MQ, MR, MU, YT, MX, FM, MD, MC, MN, ME, MS, MA, MZ, MM, NA, NR, NP, NL, NC, NZ, NI,
        NE, NG, NU, NF, MP, NO, OM, PK, PW, PS, PA, PG, PY, PE, PH, PN, PL, PT, PR, QA, RE, RO, RU, RW, BL, SH, KN, LC, MF, PM, VC, WS,
        SM, ST, SA, SN, RS, SC, SL, SG, SX, SK, SI, SB, SO, ZA, GS, SS, ES, LK, SD, SR, SJ, SZ, SE, CH, SY, TW, TJ, TZ, TH, TL, TG, TK,
        TO, TT, TN, TR, TM, TC, TV, UG, UA, AE, GB, US, UM, UY, UZ, VU, VE, VN, VG, VI, WF, EH, YE, ZM, ZW,
    }

    public enum LanguageType { ger }

    public enum CurrencyType { EUR }
    /// <remarks/>
    public partial class ContactType
    {

        private string salutationField;
        private string nameField;
        private List<string> phoneField;
        private List<string> emailField;
        /// <remarks/>
        public string Salutation
        {
            get { return this.salutationField; }
            set { this.salutationField = value; }
        }

        /// <remarks/>
        public string Name
        {
            get { return this.nameField; }
            set { this.nameField = value; }
        }

        /// <remarks/>
        public List<string> Phone
        {
            get { return this.phoneField; }
            set { this.phoneField = value; }
        }

        /// <remarks/>
        public List<string> Email
        {
            get { return this.emailField; }
            set { this.emailField = value; }
        }
    }

    /// <remarks/>
    public partial class RelatedDocumentType
    {

        private string invoiceNumberField;
        private System.DateTime invoiceDateField;
        private bool invoiceDateFieldSpecified;
        private DocumentTypeType documentTypeField;
        private bool documentTypeFieldSpecified;
        private string commentField;
        /// <remarks/>
        public string InvoiceNumber
        {
            get { return this.invoiceNumberField; }
            set { this.invoiceNumberField = value; }
        }

        /// <remarks/>
        public System.DateTime InvoiceDate
        {
            get { return this.invoiceDateField; }
            set { this.invoiceDateField = value; }
        }

        /// <remarks/>
        public bool InvoiceDateSpecified
        {
            get { return this.invoiceDateFieldSpecified; }
            set { this.invoiceDateFieldSpecified = value; }
        }

        /// <remarks/>
        public DocumentTypeType DocumentType
        {
            get { return this.documentTypeField; }
            set { this.documentTypeField = value; }
        }

        /// <remarks/>
        public bool DocumentTypeSpecified
        {
            get { return this.documentTypeFieldSpecified; }
            set { this.documentTypeFieldSpecified = value; }
        }

        /// <remarks/>
        public string Comment
        {
            get { return this.commentField; }
            set { this.commentField = value; }
        }
    }

    /// <remarks/>
    public partial class AdditionalInformationType
    {

        private string keyField;
        private string valueField;
        /// <remarks/>
        public string Key
        {
            get { return this.keyField; }
            set { this.keyField = value; }
        }

        /// <remarks/>
        public string Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    public partial class DeliveryType
    {

        private string deliveryIDField;
        private object itemField;
        private AddressType addressField;
        private ContactType contactField;
        private string descriptionField;
        /// <remarks/>
        public string DeliveryID
        {
            get { return this.deliveryIDField; }
            set { this.deliveryIDField = value; }
        }

        /// <remarks/>
        public object Item
        {
            get { return this.itemField; }
            set { this.itemField = value; }
        }

        /// <remarks/>
        public AddressType Address
        {
            get { return this.addressField; }
            set { this.addressField = value; }
        }

        /// <remarks/>
        public ContactType Contact
        {
            get { return this.contactField; }
            set { this.contactField = value; }
        }

        /// <remarks/>
        public string Description
        {
            get { return this.descriptionField; }
            set { this.descriptionField = value; }
        }
    }

    /// <remarks/>
    public partial class PeriodType
    {

        private System.DateTime fromDateField;
        private System.DateTime toDateField;
        /// <remarks/>
        public System.DateTime FromDate
        {
            get { return this.fromDateField; }
            set { this.fromDateField = value; }
        }

        /// <remarks/>
        public System.DateTime ToDate
        {
            get { return this.toDateField; }
            set { this.toDateField = value; }
        }
    }

    /// <remarks/>
    public partial class BillerType : AbstractPartyType
    {

        private string invoiceRecipientsBillerIDField;
        /// <remarks/>
        public string InvoiceRecipientsBillerID
        {
            get { return this.invoiceRecipientsBillerIDField; }
            set { this.invoiceRecipientsBillerIDField = value; }
        }
    }

    /// <remarks/>
    public partial class InvoiceRecipientType : AbstractPartyType
    {

        private string billersInvoiceRecipientIDField;
        private string accountingAreaField;
        private string subOrganizationIDField;
        /// <remarks/>
        public string BillersInvoiceRecipientID
        {
            get { return this.billersInvoiceRecipientIDField; }
            set { this.billersInvoiceRecipientIDField = value; }
        }

        /// <remarks/>
        public string AccountingArea
        {
            get { return this.accountingAreaField; }
            set { this.accountingAreaField = value; }
        }

        /// <remarks/>
        public string SubOrganizationID
        {
            get { return this.subOrganizationIDField; }
            set { this.subOrganizationIDField = value; }
        }
    }

    /// <remarks/>
    public partial class OrderingPartyType : AbstractPartyType
    {

        private string billersOrderingPartyIDField;
        /// <remarks/>
        public string BillersOrderingPartyID
        {
            get { return this.billersOrderingPartyIDField; }
            set { this.billersOrderingPartyIDField = value; }
        }
    }

    /// <remarks/>
    public partial class DetailsType
    {

        private string headerDescriptionField;
        private List<ItemListType> itemListField;
        private string footerDescriptionField;
        /// <remarks/>
        public string HeaderDescription
        {
            get { return this.headerDescriptionField; }
            set { this.headerDescriptionField = value; }
        }

        /// <remarks/>
        public List<ItemListType> ItemList
        {
            get { return this.itemListField; }
            set { this.itemListField = value; }
        }

        /// <remarks/>
        public string FooterDescription
        {
            get { return this.footerDescriptionField; }
            set { this.footerDescriptionField = value; }
        }
    }

    /// <remarks/>
    public partial class ItemListType
    {

        private string headerDescriptionField;
        private List<ListLineItemType> listLineItemField;
        private string footerDescriptionField;
        /// <remarks/>
        public string HeaderDescription
        {
            get { return this.headerDescriptionField; }
            set { this.headerDescriptionField = value; }
        }

        /// <remarks/>
        public List<ListLineItemType> ListLineItem
        {
            get { return this.listLineItemField; }
            set { this.listLineItemField = value; }
        }

        /// <remarks/>
        public string FooterDescription
        {
            get { return this.footerDescriptionField; }
            set { this.footerDescriptionField = value; }
        }
    }

    /// <remarks/>
    public partial class ListLineItemType
    {

        private string positionNumberField;
        private List<string> descriptionField;
        private List<ArticleNumberType> articleNumberField;
        private UnitType quantityField;
        private UnitPriceType unitPriceField;
        private ReductionAndSurchargeListLineItemDetailsType reductionAndSurchargeListLineItemDetailsField;
        private DeliveryType deliveryField;
        private OrderReferenceDetailType billersOrderReferenceField;
        private OrderReferenceDetailType invoiceRecipientsOrderReferenceField;
        private List<ClassificationType> classificationField;
        private List<AdditionalInformationType> additionalInformationField;
        private TaxItemType taxItemField;
        private decimal lineItemAmountField;
        /// <remarks/>
        public string PositionNumber
        {
            get { return this.positionNumberField; }
            set { this.positionNumberField = value; }
        }

        /// <remarks/>
        public List<string> Description
        {
            get { return this.descriptionField; }
            set { this.descriptionField = value; }
        }

        /// <remarks/>
        public List<ArticleNumberType> ArticleNumber
        {
            get { return this.articleNumberField; }
            set { this.articleNumberField = value; }
        }

        /// <remarks/>
        public UnitType Quantity
        {
            get { return this.quantityField; }
            set { this.quantityField = value; }
        }

        /// <remarks/>
        public UnitPriceType UnitPrice
        {
            get { return this.unitPriceField; }
            set { this.unitPriceField = value; }
        }

        /// <remarks/>
        public ReductionAndSurchargeListLineItemDetailsType ReductionAndSurchargeListLineItemDetails
        {
            get { return this.reductionAndSurchargeListLineItemDetailsField; }
            set { this.reductionAndSurchargeListLineItemDetailsField = value; }
        }

        /// <remarks/>
        public DeliveryType Delivery
        {
            get { return this.deliveryField; }
            set { this.deliveryField = value; }
        }

        /// <remarks/>
        public OrderReferenceDetailType BillersOrderReference
        {
            get { return this.billersOrderReferenceField; }
            set { this.billersOrderReferenceField = value; }
        }

        /// <remarks/>
        public OrderReferenceDetailType InvoiceRecipientsOrderReference
        {
            get { return this.invoiceRecipientsOrderReferenceField; }
            set { this.invoiceRecipientsOrderReferenceField = value; }
        }

        /// <remarks/>
        public List<ClassificationType> Classification
        {
            get { return this.classificationField; }
            set { this.classificationField = value; }
        }

        /// <remarks/>
        public List<AdditionalInformationType> AdditionalInformation
        {
            get { return this.additionalInformationField; }
            set { this.additionalInformationField = value; }
        }

        /// <remarks/>
        public TaxItemType TaxItem
        {
            get { return this.taxItemField; }
            set { this.taxItemField = value; }
        }

        /// <remarks/>
        public decimal LineItemAmount
        {
            get { return this.lineItemAmountField; }
            set { this.lineItemAmountField = value; }
        }
    }

    /// <remarks/>
    public partial class ArticleNumberType
    {

        private ArticleNumberTypeType articleNumberType1Field;
        private bool articleNumberType1FieldSpecified;
        private string valueField;
        /// <remarks/>
        public ArticleNumberTypeType ArticleNumberType1
        {
            get { return this.articleNumberType1Field; }
            set { this.articleNumberType1Field = value; }
        }

        /// <remarks/>
        public bool ArticleNumberType1Specified
        {
            get { return this.articleNumberType1FieldSpecified; }
            set { this.articleNumberType1FieldSpecified = value; }
        }

        /// <remarks/>
        public string Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    public enum ArticleNumberTypeType
    {

        /// <remarks/>
        PZN,

        /// <remarks/>
        GTIN,

        /// <remarks/>
        InvoiceRecipientsArticleNumber,

        /// <remarks/>
        BillersArticleNumber,
    }

    /// <remarks/>
    public partial class UnitType
    {

        private string unitField;
        private decimal valueField;
        /// <remarks/>
        public string Unit
        {
            get { return this.unitField; }
            set { this.unitField = value; }
        }

        /// <remarks/>
        public decimal Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    public partial class UnitPriceType
    {

        private decimal baseQuantityField;
        private bool baseQuantityFieldSpecified;
        private decimal valueField;
        /// <remarks/>
        public decimal BaseQuantity
        {
            get { return this.baseQuantityField; }
            set { this.baseQuantityField = value; }
        }

        /// <remarks/>
        public bool BaseQuantitySpecified
        {
            get { return this.baseQuantityFieldSpecified; }
            set { this.baseQuantityFieldSpecified = value; }
        }

        /// <remarks/>
        public decimal Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    public partial class ReductionAndSurchargeListLineItemDetailsType
    {

        private List<object> itemsField;
        private List<ItemsChoiceType> itemsElementNameField;
        /// <remarks/>
        public List<object> Items
        {
            get { return this.itemsField; }
            set { this.itemsField = value; }
        }

        /// <remarks/>
        public List<ItemsChoiceType> ItemsElementName
        {
            get { return this.itemsElementNameField; }
            set { this.itemsElementNameField = value; }
        }
    }

    /// <remarks/>
    public partial class OtherVATableTaxType : TaxItemType
    {

        private string taxIDField;
        /// <remarks/>
        public string TaxID
        {
            get { return this.taxIDField; }
            set { this.taxIDField = value; }
        }
    }

    /// <remarks/>
    public partial class TaxItemType
    {

        private decimal taxableAmountField;
        private TaxPercentType taxPercentField;
        private decimal taxAmountField;
        private bool taxAmountFieldSpecified;
        private AccountingCurrencyAmountType accountingCurrencyAmountField;
        private string commentField;
        /// <remarks/>
        public decimal TaxableAmount
        {
            get { return this.taxableAmountField; }
            set { this.taxableAmountField = value; }
        }

        /// <remarks/>
        public TaxPercentType TaxPercent
        {
            get { return this.taxPercentField; }
            set { this.taxPercentField = value; }
        }

        /// <remarks/>
        public decimal TaxAmount
        {
            get { return this.taxAmountField; }
            set { this.taxAmountField = value; }
        }

        /// <remarks/>
        public bool TaxAmountSpecified
        {
            get { return this.taxAmountFieldSpecified; }
            set { this.taxAmountFieldSpecified = value; }
        }

        /// <remarks/>
        public AccountingCurrencyAmountType AccountingCurrencyAmount
        {
            get { return this.accountingCurrencyAmountField; }
            set { this.accountingCurrencyAmountField = value; }
        }

        /// <remarks/>
        public string Comment
        {
            get { return this.commentField; }
            set { this.commentField = value; }
        }
    }

    /// <remarks/>
    public partial class TaxPercentType
    {

        private string taxCategoryCodeField;
        private decimal valueField;
        /// <remarks/>
        public string TaxCategoryCode
        {
            get { return this.taxCategoryCodeField; }
            set { this.taxCategoryCodeField = value; }
        }

        /// <remarks/>
        public decimal Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    public partial class AccountingCurrencyAmountType
    {

        private CurrencyType currencyField;
        private decimal valueField;
        /// <remarks/>
        public CurrencyType Currency
        {
            get { return this.currencyField; }
            set { this.currencyField = value; }
        }

        /// <remarks/>
        public decimal Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    public partial class ReductionAndSurchargeBaseType
    {

        private decimal baseAmountField;
        private decimal percentageField;
        private bool percentageFieldSpecified;
        private decimal amountField;
        private bool amountFieldSpecified;
        private string commentField;
        private ClassificationType classificationField;
        /// <remarks/>
        public decimal BaseAmount
        {
            get { return this.baseAmountField; }
            set { this.baseAmountField = value; }
        }

        /// <remarks/>
        public decimal Percentage
        {
            get { return this.percentageField; }
            set { this.percentageField = value; }
        }

        /// <remarks/>
        public bool PercentageSpecified
        {
            get { return this.percentageFieldSpecified; }
            set { this.percentageFieldSpecified = value; }
        }

        /// <remarks/>
        public decimal Amount
        {
            get { return this.amountField; }
            set { this.amountField = value; }
        }

        /// <remarks/>
        public bool AmountSpecified
        {
            get { return this.amountFieldSpecified; }
            set { this.amountFieldSpecified = value; }
        }

        /// <remarks/>
        public string Comment
        {
            get { return this.commentField; }
            set { this.commentField = value; }
        }

        /// <remarks/>
        public ClassificationType Classification
        {
            get { return this.classificationField; }
            set { this.classificationField = value; }
        }
    }

    /// <remarks/>
    public partial class ClassificationType
    {

        private string classificationSchemaField;
        private string valueField;
        /// <remarks/>
        public string ClassificationSchema
        {
            get { return this.classificationSchemaField; }
            set { this.classificationSchemaField = value; }
        }

        /// <remarks/>
        public string Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    public enum ItemsChoiceType
    {

        /// <remarks/>
        OtherVATableTaxListLineItem,

        /// <remarks/>
        ReductionListLineItem,

        /// <remarks/>
        SurchargeListLineItem,
    }

    /// <remarks/>
    public partial class OrderReferenceDetailType : OrderReferenceType
    {

        private string orderPositionNumberField;
        /// <remarks/>
        public string OrderPositionNumber
        {
            get { return this.orderPositionNumberField; }
            set { this.orderPositionNumberField = value; }
        }
    }

    /// <remarks/>
    public partial class ReductionAndSurchargeDetailsType
    {

        private List<object> itemsField;
        private List<ItemsChoiceType1> itemsElementNameField;
        /// <remarks/>
        public List<object> Items
        {
            get { return this.itemsField; }
            set { this.itemsField = value; }
        }

        /// <remarks/>
        public List<ItemsChoiceType1> ItemsElementName
        {
            get { return this.itemsElementNameField; }
            set { this.itemsElementNameField = value; }
        }
    }

    /// <remarks/>
    public partial class ReductionAndSurchargeType : ReductionAndSurchargeBaseType
    {

        private TaxItemType taxItemField;
        /// <remarks/>
        public TaxItemType TaxItem
        {
            get { return this.taxItemField; }
            set { this.taxItemField = value; }
        }
    }

    /// <remarks/>
    public enum ItemsChoiceType1
    {

        /// <remarks/>
        OtherVATableTax,

        /// <remarks/>
        Reduction,

        /// <remarks/>
        Surcharge,
    }

    /// <remarks/>
    public partial class TaxType
    {

        private List<TaxItemType> taxItemField;
        private List<OtherTaxType> otherTaxField;
        /// <remarks/>
        public List<TaxItemType> TaxItem
        {
            get { return this.taxItemField; }
            set { this.taxItemField = value; }
        }

        /// <remarks/>
        public List<OtherTaxType> OtherTax
        {
            get { return this.otherTaxField; }
            set { this.otherTaxField = value; }
        }
    }

    /// <remarks/>
    public partial class OtherTaxType
    {

        private string commentField;
        private decimal amountField;
        /// <remarks/>
        public string Comment
        {
            get { return this.commentField; }
            set { this.commentField = value; }
        }

        /// <remarks/>
        public decimal Amount
        {
            get { return this.amountField; }
            set { this.amountField = value; }
        }
    }

    /// <remarks/>
    public partial class PaymentMethodType
    {

        private string commentField;
        private object itemField;
        /// <remarks/>
        public string Comment
        {
            get { return this.commentField; }
            set { this.commentField = value; }
        }

        /// <remarks/>
        public object Item
        {
            get { return this.itemField; }
            set { this.itemField = value; }
        }
    }

    /// <remarks/>
    public partial class NoPaymentType
    {
    }

    /// <remarks/>
    public partial class OtherPaymentType
    {
    }

    /// <remarks/>
    public partial class PaymentCardType
    {

        private string primaryAccountNumberField;
        private string cardHolderNameField;
        /// <remarks/>
        public string PrimaryAccountNumber
        {
            get { return this.primaryAccountNumberField; }
            set { this.primaryAccountNumberField = value; }
        }

        /// <remarks/>
        public string CardHolderName
        {
            get { return this.cardHolderNameField; }
            set { this.cardHolderNameField = value; }
        }
    }

    /// <remarks/>
    public partial class SEPADirectDebitType
    {

        private SEPADirectDebitTypeType typeField;
        private bool typeFieldSpecified;
        private string bICField;
        private string iBANField;
        private string bankAccountOwnerField;
        private string creditorIDField;
        private string mandateReferenceField;
        private System.DateTime debitCollectionDateField;
        private bool debitCollectionDateFieldSpecified;
        /// <remarks/>
        public SEPADirectDebitTypeType Type
        {
            get { return this.typeField; }
            set { this.typeField = value; }
        }

        /// <remarks/>
        public bool TypeSpecified
        {
            get { return this.typeFieldSpecified; }
            set { this.typeFieldSpecified = value; }
        }

        /// <remarks/>
        public string BIC
        {
            get { return this.bICField; }
            set { this.bICField = value; }
        }

        /// <remarks/>
        public string IBAN
        {
            get { return this.iBANField; }
            set { this.iBANField = value; }
        }

        /// <remarks/>
        public string BankAccountOwner
        {
            get { return this.bankAccountOwnerField; }
            set { this.bankAccountOwnerField = value; }
        }

        /// <remarks/>
        public string CreditorID
        {
            get { return this.creditorIDField; }
            set { this.creditorIDField = value; }
        }

        /// <remarks/>
        public string MandateReference
        {
            get { return this.mandateReferenceField; }
            set { this.mandateReferenceField = value; }
        }

        /// <remarks/>
        public System.DateTime DebitCollectionDate
        {
            get { return this.debitCollectionDateField; }
            set { this.debitCollectionDateField = value; }
        }

        /// <remarks/>
        public bool DebitCollectionDateSpecified
        {
            get { return this.debitCollectionDateFieldSpecified; }
            set { this.debitCollectionDateFieldSpecified = value; }
        }
    }

    /// <remarks/>
    public enum SEPADirectDebitTypeType
    {

        /// <remarks/>
        B2C,

        /// <remarks/>
        B2B,
    }

    /// <remarks/>
    public partial class UniversalBankTransactionType
    {

        private List<AccountType> beneficiaryAccountField;
        private PaymentReferenceType paymentReferenceField;
        private bool consolidatorPayableField;
        private bool consolidatorPayableFieldSpecified;
        /// <remarks/>
        public List<AccountType> BeneficiaryAccount
        {
            get { return this.beneficiaryAccountField; }
            set { this.beneficiaryAccountField = value; }
        }

        /// <remarks/>
        public PaymentReferenceType PaymentReference
        {
            get { return this.paymentReferenceField; }
            set { this.paymentReferenceField = value; }
        }

        /// <remarks/>
        public bool ConsolidatorPayable
        {
            get { return this.consolidatorPayableField; }
            set { this.consolidatorPayableField = value; }
        }

        /// <remarks/>
        public bool ConsolidatorPayableSpecified
        {
            get { return this.consolidatorPayableFieldSpecified; }
            set { this.consolidatorPayableFieldSpecified = value; }
        }
    }

    /// <remarks/>
    public partial class AccountType
    {

        private string bankNameField;
        private BankCodeType bankCodeField;
        private string bICField;
        private string bankAccountNrField;
        private string iBANField;
        private string bankAccountOwnerField;
        /// <remarks/>
        public string BankName
        {
            get { return this.bankNameField; }
            set { this.bankNameField = value; }
        }

        /// <remarks/>
        public BankCodeType BankCode
        {
            get { return this.bankCodeField; }
            set { this.bankCodeField = value; }
        }

        /// <remarks/>
        public string BIC
        {
            get { return this.bICField; }
            set { this.bICField = value; }
        }

        /// <remarks/>
        public string BankAccountNr
        {
            get { return this.bankAccountNrField; }
            set { this.bankAccountNrField = value; }
        }

        /// <remarks/>
        public string IBAN
        {
            get { return this.iBANField; }
            set { this.iBANField = value; }
        }

        /// <remarks/>
        public string BankAccountOwner
        {
            get { return this.bankAccountOwnerField; }
            set { this.bankAccountOwnerField = value; }
        }
    }

    /// <remarks/>
    public partial class BankCodeType
    {

        private string bankCodeType1Field;
        private string valueField;
        /// <remarks/>
        public string BankCodeType1
        {
            get { return this.bankCodeType1Field; }
            set { this.bankCodeType1Field = value; }
        }

        /// <remarks/>
        public string Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    public partial class PaymentReferenceType
    {

        private string checkSumField;
        private string valueField;
        /// <remarks/>
        public string CheckSum
        {
            get { return this.checkSumField; }
            set { this.checkSumField = value; }
        }

        /// <remarks/>
        public string Value
        {
            get { return this.valueField; }
            set { this.valueField = value; }
        }
    }

    /// <remarks/>
    public partial class PaymentConditionsType
    {

        private System.DateTime dueDateField;
        private bool dueDateFieldSpecified;
        private List<DiscountType> discountField;
        private decimal minimumPaymentField;
        private bool minimumPaymentFieldSpecified;
        private string commentField;
        /// <remarks/>
        public System.DateTime DueDate
        {
            get { return this.dueDateField; }
            set { this.dueDateField = value; }
        }

        /// <remarks/>
        public bool DueDateSpecified
        {
            get { return this.dueDateFieldSpecified; }
            set { this.dueDateFieldSpecified = value; }
        }

        /// <remarks/>
        public List<DiscountType> Discount
        {
            get { return this.discountField; }
            set { this.discountField = value; }
        }

        /// <remarks/>
        public decimal MinimumPayment
        {
            get { return this.minimumPaymentField; }
            set { this.minimumPaymentField = value; }
        }

        /// <remarks/>
        public bool MinimumPaymentSpecified
        {
            get { return this.minimumPaymentFieldSpecified; }
            set { this.minimumPaymentFieldSpecified = value; }
        }

        /// <remarks/>
        public string Comment
        {
            get { return this.commentField; }
            set { this.commentField = value; }
        }
    }

    /// <remarks/>
    public partial class DiscountType
    {

        private System.DateTime paymentDateField;
        private decimal baseAmountField;
        private bool baseAmountFieldSpecified;
        private decimal percentageField;
        private bool percentageFieldSpecified;
        private decimal amountField;
        private bool amountFieldSpecified;
        private string commentField;
        /// <remarks/>
        public System.DateTime PaymentDate
        {
            get { return this.paymentDateField; }
            set { this.paymentDateField = value; }
        }

        /// <remarks/>
        public decimal BaseAmount
        {
            get { return this.baseAmountField; }
            set { this.baseAmountField = value; }
        }

        /// <remarks/>
        public bool BaseAmountSpecified
        {
            get { return this.baseAmountFieldSpecified; }
            set { this.baseAmountFieldSpecified = value; }
        }

        /// <remarks/>
        public decimal Percentage
        {
            get { return this.percentageField; }
            set { this.percentageField = value; }
        }

        /// <remarks/>
        public bool PercentageSpecified
        {
            get { return this.percentageFieldSpecified; }
            set { this.percentageFieldSpecified = value; }
        }

        /// <remarks/>
        public decimal Amount
        {
            get { return this.amountField; }
            set { this.amountField = value; }
        }

        /// <remarks/>
        public bool AmountSpecified
        {
            get { return this.amountFieldSpecified; }
            set { this.amountFieldSpecified = value; }
        }

        /// <remarks/>
        public string Comment
        {
            get { return this.commentField; }
            set { this.commentField = value; }
        }
    }

}
