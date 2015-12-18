using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ebIModels.Schema.ebInterface4p1
{
    public partial class InvoiceType
    {
        public InvoiceType()
        {
            this.invoiceRootExtensionField = new InvoiceRootExtensionType();
            this.presentationDetailsField = new PresentationDetailsType();
            this.paymentConditionsField = new PaymentConditionsType();
            this.paymentMethodField = new PaymentMethodType();
            this.taxField = new TaxType();
            this.reductionAndSurchargeDetailsField = new ReductionAndSurchargeDetailsType();
            this.detailsField = new DetailsType();
            this.orderingPartyField = new OrderingPartyType();
            this.invoiceRecipientField = new InvoiceRecipientType();
            this.billerField = new BillerType();
            this.deliveryField = new DeliveryType();
            // this.relatedDocumentField = new RelatedDocumentType[];
            // this.cancelledOriginalDocumentField = new CancelledOriginalDocumentType();
            this.SetInvoiceVersion();
        }
    }

    public partial class InvoiceRootExtensionType
    {
        public InvoiceRootExtensionType()
        {
            this.customField = new CustomType();
            this.invoiceRootExtensionField = new InvoiceRootExtensionType1();
        }
    }

    public partial class PresentationDetailsExtensionType
    {

        public PresentationDetailsExtensionType()
        {
            this.customField = new CustomType();
            this.presentationDetailsExtensionField = new PresentationDetailsExtensionType1();
        }
    }

    public partial class PresentationDetailsType
    {

        public PresentationDetailsType()
        {
            this.presentationDetailsExtensionField = new PresentationDetailsExtensionType();
        }
    }

    public partial class PaymentConditionsExtensionType
    {


        public PaymentConditionsExtensionType()
        {
            this.customField = new CustomType();
            this.paymentConditionsExtensionField = new PaymentConditionsExtensionType1();
        }
    }

    public partial class PaymentConditionsType
    {



        public PaymentConditionsType()
        {
            this.paymentConditionsExtensionField = new PaymentConditionsExtensionType();
            // // this\.discountField.=.new.List<DiscountType>();
        }
    }

    public partial class PaymentMethodExtensionType
    {



        public PaymentMethodExtensionType()
        {
            this.customField = new CustomType();
            this.paymentMethodExtensionField = new PaymentMethodExtensionType1();
        }
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
            // // this\.beneficiaryAccountField.=.new.List<AccountType>();
        }
    }

    public partial class PaymentMethodType
    {
        public PaymentMethodType()
        {
            this.paymentMethodExtensionField = new PaymentMethodExtensionType();
        }
    }

    public partial class TaxExtensionType
    {

        public TaxExtensionType()
        {
            this.customField = new CustomType();
            this.taxExtensionField = new TaxExtensionType1();
        }
    }

    public partial class TaxType
    {

        public TaxType()
        {
            this.taxExtensionField = new TaxExtensionType();
            // // this\.otherTaxField.=.new.List<OtherTaxType>();
            // // this\.vATField.=.new.List<VATItemType>();
        }
    }

    public partial class ReductionAndSurchargeDetailsExtensionType
    {

        public ReductionAndSurchargeDetailsExtensionType()
        {
            this.customField = new CustomType();
            this.reductionAndSurchargeDetailsExtensionField = new ReductionAndSurchargeDetailsExtensionType1();
        }
    }

    public partial class ReductionAndSurchargeDetailsType
    {


        public ReductionAndSurchargeDetailsType()
        {
            this.reductionAndSurchargeDetailsExtensionField = new ReductionAndSurchargeDetailsExtensionType();
            // this.itemsElementNameField = new ItemsChoiceType1[];
           //  // this\.itemsField.=.new.List<object>();
        }
    }

    public partial class OtherVATableTaxType : OtherVATableTaxBaseType
    {


        public OtherVATableTaxType()
        {
            this.vATRateField = new VATRateType();
        }

    }

    public partial class ReductionAndSurchargeType : ReductionAndSurchargeBaseType
    {


        public ReductionAndSurchargeType()
        {
            this.vATRateField = new VATRateType();
        }
    }

    public partial class BelowTheLineItemType
    {


        public BelowTheLineItemType()
        {
            this.reasonField = new ReasonType();
        }
    }

    public partial class ListLineItemExtensionType
    {

        public ListLineItemExtensionType()
        {
            this.customField = new CustomType();
            this.listLineItemExtensionField = new ListLineItemExtensionType1();
        }
    }

    public partial class AdditionalInformationType
    {


        public AdditionalInformationType()
        {
            this.weightField = new UnitType();
            this.alternativeQuantityField = new UnitType();
            //// this\.classificationField.=.new.List<ClassificationType>();
            //// this\.chargeNumberField.=.new.List<string>();
            //// this\.serialNumberField.=.new.List<string>();
        }
    }

    public partial class ReductionAndSurchargeListLineItemDetailsType
    {

        public ReductionAndSurchargeListLineItemDetailsType()
        {
            // // this\.itemsElementNameField.=.new.List<ItemsChoiceType>();
            // this\.itemsField.=.new.List<object>();
        }
    }

    public partial class ArticleNumberType
    {

        public ArticleNumberType()
        {
            // this\.textField.=.new.List<string>();
        }
    }

    public partial class ListLineItemType
    {
        public ListLineItemType()
        {
            this.listLineItemExtensionField = new ListLineItemExtensionType();
            this.additionalInformationField = new AdditionalInformationType();
            this.invoiceRecipientsOrderReferenceField = new OrderReferenceDetailType();
            this.billersOrderReferenceField = new OrderReferenceDetailType();
            this.deliveryField = new DeliveryType();
            this.reductionAndSurchargeListLineItemDetailsField = new ReductionAndSurchargeListLineItemDetailsType();
            this.unitPriceField = new UnitPriceType();
            this.quantityField = new UnitType();
            // this\.articleNumberField.=.new.List<ArticleNumberType>();
            // this\.descriptionField.=.new.List<string>();
        }
    }

    public partial class DeliveryType
    {

        public DeliveryType()
        {
            this.deliveryExtensionField = new DeliveryExtensionType();
            this.addressField = new AddressType();
        }
    }

    public partial class AddressType
    {
        public AddressType()
        {
            // this\.addressExtensionField.=.new.List<string>();
            this.countryField = new CountryType();
            // this\.addressIdentifierField.=.new.List<AddressIdentifierType>();
        }
    }

    public partial class CountryType
    {

        public CountryType()
        {
            // this\.textField.=.new.List<string>();
        }
    }

    public partial class DeliveryExtensionType
    {

        public DeliveryExtensionType()
        {
            this.customField = new CustomType();
            this.deliveryExtensionField = new DeliveryExtensionType1();
        }
    }

    public partial class ItemListType
    {


        public ItemListType()
        {
            // this\.listLineItemField.=.new.List<ListLineItemType>();
        }
    }

    public partial class DetailsType
    {


        public DetailsType()
        {
            // this\.belowTheLineItemField.=.new.List<BelowTheLineItemType>();
            // this\.itemListField.=.new.List<ItemListType>();
        }
    }

    public partial class OrderingPartyExtensionType
    {

        public OrderingPartyExtensionType()
        {
            this.customField = new CustomType();
            this.orderingPartyExtensionField = new OrderingPartyExtensionType1();
        }
    }

    public partial class InvoiceRecipientExtensionType
    {

        public InvoiceRecipientExtensionType()
        {
            this.customField = new CustomType();
            this.invoiceRecipientExtensionField = new InvoiceRecipientExtensionType1();
        }
    }

    public partial class BillerExtensionType
    {

        public BillerExtensionType()
        {
            this.customField = new CustomType();
            this.billerExtensionField = new BillerExtensionType1();
        }
    }

    public partial class AbstractPartyType
    {

        public AbstractPartyType()
        {
            this.addressField = new AddressType();
            this.orderReferenceField = new OrderReferenceType();
            // this\.furtherIdentificationField.=.new.List<FurtherIdentificationType>();
        }
    }

    public partial class OrderingPartyType : AbstractPartyType
    {
        public OrderingPartyType()
        {
            this.orderingPartyExtensionField = new OrderingPartyExtensionType();
        }
    }

    public partial class InvoiceRecipientType : AbstractPartyType
    {

        public InvoiceRecipientType()
        {
            this.invoiceRecipientExtensionField = new InvoiceRecipientExtensionType();
        }
    }

    public partial class BillerType : AbstractPartyType
    {

        public BillerType()
        {
            this.billerExtensionField = new BillerExtensionType();
        }
    }

    public partial class VATType
    {

        public VATType()
        {
            // this\.vATItemField.=.new.List<VATItemType>();
        }
    }
}