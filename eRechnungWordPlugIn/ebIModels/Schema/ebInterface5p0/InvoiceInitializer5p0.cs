using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExtensionMethods;

namespace ebIModels.Schema.ebInterface5p0
{
    public partial class InvoiceType
    {
        public InvoiceType()
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
            this.InvoiceCurrency = ebIModels.Mapping.ModelConstants.CurrencyCodeFixed.ToString();
            this.cancelledOriginalDocumentField = null;
            this.relatedDocumentField = null;
            this.additionalInformationField = new AdditionalInformationType[] { };
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
            this.SetInvoiceVersion();
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
        }
    }

    public partial class PaymentMethodType
    {
        public PaymentMethodType()
        {

        }
    }

    public partial class TaxType
    {

        public TaxType()
        {

        }
    }

    public partial class ReductionAndSurchargeDetailsType
    {


        public ReductionAndSurchargeDetailsType()
        {

        }
    }


    public partial class ReductionAndSurchargeType : ReductionAndSurchargeBaseType
    {


    }

    public partial class ListLineItemType
    {
        public ListLineItemType()
        {
            this.additionalInformationField = new AdditionalInformationType[] { };
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
            if (ReductionAndSurchargeListLineItemDetails.Items.Any())
            {
                decimal rabattProzent = ((ReductionAndSurchargeBaseType)ReductionAndSurchargeListLineItemDetails.Items[0]).Percentage;
                decimal rabatt = (baseAmount * rabattProzent / 100).FixedFraction(2);
                netAmount = baseAmount - rabatt;
            }
            LineItemAmount = netAmount;
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
            this.countryField = new CountryType();
        }
    }

    public partial class CountryType
    {

        public CountryType()
        {
            // this\.textField.=.new.List<string>();
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
}
