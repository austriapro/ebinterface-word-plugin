using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Schema.ebInterface5p0;
using VM = ebIModels.Models;
using V5P0 = ebIModels.Schema.ebInterface5p0;

namespace ebIModels.Mapping
{
    public static class ModelToVM
    {
        public static List<MappingError> mappingErrors;
        /// <summary>
        /// Maps ebInterface 5p0 InvoiceType to internal InvoiceType Model
        /// </summary>
        /// <param name="source">ebInterface 5p0 InvoiceType</param>
        /// <returns></returns>
        public static V5P0.InvoiceType MapVMToVm(VM.IInvoiceType source)
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
            invoice.TotalGrossAmount = source.TotalGrossAmount.GetValueOrDefault();

            // PrepaidAmount [0..1] Decimal2Type
            invoice.PrepaidAmount = source.PrepaidAmount.GetValueOrDefault();

            // RoundingAmount [0..1] Decimal2Type

            // PayableAmount Decimal2Type
            invoice.PayableAmount = source.PayableAmount.GetValueOrDefault();

            // PaymentMethod [0..1] PaymentMethodType
            invoice.PaymentMethod = MapPaymentMethod(source.PaymentMethod);

            // PaymentConditions [0..1] PaymentConditionsType
            invoice.PaymentConditions = MapPaymentConditions(source.PaymentConditions);

            // Comment [0..1] xs:string
            invoice.Comment = source.Comment;
            return invoice;
        }

        private static PaymentConditionsType MapPaymentConditions(VM.PaymentConditionsType paymentConditions)
        {
            throw new NotImplementedException();
        }

        private static PaymentMethodType MapPaymentMethod(VM.PaymentMethodType paymentMethod)
        {
            throw new NotImplementedException();
        }

        private static TaxType MapTax(VM.TaxType tax)
        {
            throw new NotImplementedException();
        }

        private static ReductionAndSurchargeDetailsType MapReductionAndSurchargeDetails(VM.ReductionAndSurchargeDetailsType reductionAndSurchargeDetails)
        {
            throw new NotImplementedException();
        }

        private static DetailsType MapDetails(VM.DetailsType details)
        {
            throw new NotImplementedException();
        }

        private static OrderingPartyType MapOrderingParty(VM.OrderingPartyType orderingParty)
        {
            throw new NotImplementedException();
        }

        private static InvoiceRecipientType MapInvoiceRecipient(VM.InvoiceRecipientType invoiceRecipient)
        {
            throw new NotImplementedException();
        }

        private static BillerType MapBiller(VM.BillerType biller)
        {
            throw new NotImplementedException();
        }

        private static DeliveryType MapDelivery(VM.DeliveryType delivery)
        {
            throw new NotImplementedException();
        }

        private static AdditionalInformationType[] MapAdditionalInformation(List<VM.AdditionalInformationType> additionalInformation)
        {
            throw new NotImplementedException();
        }

        private static RelatedDocumentType[] MapRelatedDocument(List<VM.RelatedDocumentType> relatedDocument)
        {
            throw new NotImplementedException();
        }

        private static CancelledOriginalDocumentType MapCancelledOriginalDocument(VM.CancelledOriginalDocumentType cancelledOriginalDocument)
        {
            throw new NotImplementedException();
        }

    }
}
