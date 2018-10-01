using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Schema.ebInterface5p0;
using Model = ebIModels.Models;
using V5P0 = ebIModels.Schema.ebInterface5p0;

namespace ebIModels.Mapping.V5p0
{
    public static partial class MapInvoice 
    {
        /// <summary>
        /// Maps ebInterface 5p0 InvoiceType to internal InvoiceType Model
        /// </summary>
        /// <param name="source">ebInterface 5p0 InvoiceType</param>
        /// <returns></returns>
        internal static V5P0.InvoiceType MapModelToV5p0(Model.IInvoiceModel source)
        {
            V5P0.InvoiceType invoice = new V5P0.InvoiceType();
            mappingErrors = new List<MappingError>();

            // GeneratingSystem xs:string
            invoice.GeneratingSystem = source.GeneratingSystem;
            // DocumentType DocumentTypeType

            // InvoiceCurrency CurrencyType
            invoice.InvoiceCurrency = source.InvoiceCurrency.ToString();

            // ManualProcessing xs:boolean
            invoice.ManualProcessing = source.ManualProcessing;

            // DocumentTitle xs:string
            invoice.DocumentTitle = source.DocumentTitle;

            // Language LanguageType
            invoice.Language = source.Language.ToString();

            // IsDuplicate xs:boolean
            invoice.IsDuplicate = source.IsDuplicate;

            // InvoiceNumber IDType
            invoice.InvoiceNumber = source.InvoiceNumber;

            // InvoiceDate xs:date
            invoice.InvoiceDate = source.InvoiceDate;

            // CancelledOriginalDocument [0..1] CancelledOriginalDocumentType
            invoice.CancelledOriginalDocument = MapCancelledOriginalDocument(source.CancelledOriginalDocument);

            // RelatedDocument [0..*] RelatedDocumentType
            invoice.RelatedDocument = MapRelatedDocument(source.RelatedDocument);

            // AdditionalInformation [0..*] AdditionalInformationType
            invoice.AdditionalInformation = MapAdditionalInformation(source.AdditionalInformation);

            // Delivery [0..1] DeliveryType
            invoice.Delivery = MapDelivery(source.Delivery);

            // Biller BillerType
            invoice.Biller = MapBiller(source.Biller);

            // InvoiceRecipient InvoiceRecipientType
            invoice.InvoiceRecipient = MapInvoiceRecipient(source.InvoiceRecipient);

            // OrderingParty [0..1] OrderingPartyType
            invoice.OrderingParty = MapOrderingParty(source.OrderingParty);

            // Details DetailsType
            invoice.Details = MapDetails(source.Details);

            // ReductionAndSurchargeDetails [0..1] ReductionAndSurchargeDetailsType
            invoice.ReductionAndSurchargeDetails = MapReductionAndSurchargeDetails(source.ReductionAndSurchargeDetails);

            // Tax TaxType
            invoice.Tax = MapTax(source.Tax);

            // TotalGrossAmount Decimal2Type
            invoice.TotalGrossAmount = source.TotalGrossAmount;

            // PrepaidAmount [0..1] Decimal2Type
            invoice.PrepaidAmount = source.PrepaidAmount;

            // RoundingAmount [0..1] Decimal2Type

            // PayableAmount Decimal2Type
            invoice.PayableAmount = source.PayableAmount;

            // PaymentMethod [0..1] PaymentMethodType
            invoice.PaymentMethod = MapPaymentMethod(source.PaymentMethod);

            // PaymentConditions [0..1] PaymentConditionsType
            invoice.PaymentConditions = MapPaymentConditions(source.PaymentConditions);

            // Comment [0..1] xs:string
            invoice.Comment = source.Comment;
            return invoice;
        }

        #region PaymentConditions 
        /// <summary>
        /// Maps the PaymentConditions from Model to ebInterface V5p0
        /// </summary>
        /// <param name="paymentConditions">Payment Conditions from Model</param>
        /// <returns></returns>
        private static PaymentConditionsType MapPaymentConditions(Model.PaymentConditionsType paymentConditions)
        {

            if (paymentConditions == null)
            {
                return null;
            }
            PaymentConditionsType payment = new PaymentConditionsType();
            payment.Comment = paymentConditions.Comment;


            if (paymentConditions.DueDate > DateTime.MinValue)
            {
                payment.DueDate = paymentConditions.DueDate;
                payment.DueDateSpecified = true;
            }
            else
            {
                payment.DueDateSpecified = false;
            }
            if (paymentConditions.Discount != null)
            {
                // inv4P1.PaymentConditions.Discount.Clear();
                var discountList = new List<DiscountType>();
                foreach (Model.DiscountType srcDiscount in paymentConditions.Discount)
                {
                    DiscountType discount = new DiscountType()
                    {
                        Amount = srcDiscount.Amount,
                        AmountSpecified = srcDiscount.AmountSpecified,
                        BaseAmount = srcDiscount.BaseAmount,
                        BaseAmountSpecified = srcDiscount.BaseAmountSpecified,
                        PaymentDate = srcDiscount.PaymentDate,
                        Percentage = srcDiscount.Percentage,
                        PercentageSpecified = srcDiscount.PercentageSpecified
                    };
                    discountList.Add(discount);
                }
                payment.Discount = discountList.ToArray();
            }
            return payment;
        }
        #endregion

        #region PaymentMethod
        /// <summary>
        /// Maps the payment method.
        /// </summary>
        /// <param name="paymentMethod">The payment method.</param>
        /// <returns></returns>
        private static PaymentMethodType MapPaymentMethod(Model.PaymentMethodType paymentMethod)
        {
            PaymentMethodType paymethod = new PaymentMethodType();
            paymethod.Comment = paymentMethod.Comment;
            if (paymentMethod.Item.GetType() == typeof(Model.UniversalBankTransactionType))
            {
                Model.UniversalBankTransactionType txType = paymentMethod.Item as Model.UniversalBankTransactionType;

                paymethod.Item = new UniversalBankTransactionType();
                ((UniversalBankTransactionType)paymethod.Item).BeneficiaryAccount = new AccountType[]
                {
                    new AccountType()
                    {
                        BIC = txType.BeneficiaryAccount.First().BIC,
                        BankName = txType.BeneficiaryAccount.First().BankName,
                        IBAN = txType.BeneficiaryAccount.First().IBAN,
                        BankAccountOwner = txType.BeneficiaryAccount.First().BankAccountOwner
                    }
                };
            }
            return paymethod;
        }
        #endregion

        private static TaxType MapTax(Model.TaxType SourceTax)
        {
            throw new NotImplementedException();
            /*
                        TaxType tax = new TaxType();
                        if (SourceTax.VAT)
                        {

                        } 
                        tax.VAT = new List<VATItemType>();
                        if (source.Tax.VAT != null)
                            foreach (var vatItem in source.Tax.VAT)
                            {

                                VATItemType vatItemNeu = new VATItemType()
                                {
                                    Amount = vatItem.Amount,
                                    TaxedAmount = vatItem.TaxedAmount,
                                    Item = MapVatItemType2Vm(vatItem.Item)
                                };
                                tax.VAT.Add(vatItemNeu);
                            }
                        return tax;
                        */
        }

        private static ReductionAndSurchargeDetailsType MapReductionAndSurchargeDetails(Model.ReductionAndSurchargeDetailsType reductionAndSurchargeDetails)
        {
            throw new NotImplementedException();
        }

        private static DetailsType MapDetails(Model.DetailsType details)
        {
            throw new NotImplementedException();
        }

        private static OrderingPartyType MapOrderingParty(Model.OrderingPartyType orderingParty)
        {
            throw new NotImplementedException();
        }

        private static InvoiceRecipientType MapInvoiceRecipient(Model.InvoiceRecipientType invoiceRecipient)
        {
            throw new NotImplementedException();
        }

        private static BillerType MapBiller(Model.BillerType biller)
        {
            throw new NotImplementedException();
        }

        private static DeliveryType MapDelivery(Model.DeliveryType delivery)
        {
            throw new NotImplementedException();
        }

        private static AdditionalInformationType[] MapAdditionalInformation(List<Model.AdditionalInformationType> additionalInformation)
        {
            throw new NotImplementedException();
        }

        private static RelatedDocumentType[] MapRelatedDocument(List<Model.RelatedDocumentType> relatedDocument)
        {
            throw new NotImplementedException();
        }

        private static CancelledOriginalDocumentType MapCancelledOriginalDocument(Model.CancelledOriginalDocumentType cancelledOriginalDocument)
        {
            throw new NotImplementedException();
        }

    }
}
