using ebIModels.Models;
using V5P0 = ebIModels.Schema.ebInterface5p0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Schema;

namespace ebIModels.Mapping.V5p0
{
    public static partial class MapInvoice
    {

        public static List<MappingError> mappingErrors;
        /// <summary>
        /// Maps ebInterface 5p0 InvoiceType to internal InvoiceType Model
        /// </summary>
        /// <param name="source">ebInterface 5p0 InvoiceType</param>
        /// <returns></returns>
        internal static IInvoiceModel MapV5p0ToVm(V5P0.InvoiceType source)
        {
            IInvoiceModel invoice = InvoiceFactory.CreateInvoice();
            mappingErrors = new List<MappingError>();

            // GeneratingSystem xs:string
            invoice.GeneratingSystem = source.GeneratingSystem;
            // DocumentType DocumentTypeType

            // InvoiceCurrency CurrencyType
            invoice.InvoiceCurrency = MapCurrency(source.InvoiceCurrency);

            // ManualProcessing xs:boolean
            invoice.ManualProcessing = source.ManualProcessing;

            // DocumentTitle xs:string
            invoice.DocumentTitle = source.DocumentTitle;

            // Language LanguageType
            invoice.Language = MapLanguage(source.Language);

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

        private static PaymentConditionsType MapPaymentConditions(V5P0.PaymentConditionsType paymentConditions)
        {
            throw new NotImplementedException();
        }

        private static PaymentMethodType MapPaymentMethod(V5P0.PaymentMethodType paymentMethod)
        {
            throw new NotImplementedException();
        }

        private static TaxType MapTax(V5P0.TaxType tax)
        {
            throw new NotImplementedException();
        }

        private static ReductionAndSurchargeDetailsType MapReductionAndSurchargeDetails(V5P0.ReductionAndSurchargeDetailsType reductionAndSurchargeDetails)
        {
            throw new NotImplementedException();
        }

        private static DetailsType MapDetails(V5P0.DetailsType details)
        {
            throw new NotImplementedException();
        }

        private static OrderingPartyType MapOrderingParty(V5P0.OrderingPartyType orderingParty)
        {
            throw new NotImplementedException();
        }

        private static InvoiceRecipientType MapInvoiceRecipient(V5P0.InvoiceRecipientType invoiceRecipient)
        {
            throw new NotImplementedException();
        }

        private static BillerType MapBiller(V5P0.BillerType biller)
        {
            throw new NotImplementedException();
        }

        private static DeliveryType MapDelivery(V5P0.DeliveryType delivery)
        {
            throw new NotImplementedException();
        }

        private static List<AdditionalInformationType> MapAdditionalInformation(V5P0.AdditionalInformationType[] additionalInformation)
        {
            throw new NotImplementedException();
        }

        private static List<RelatedDocumentType> MapRelatedDocument(V5P0.RelatedDocumentType[] relatedDocument)
        {
            throw new NotImplementedException();
        }

        private static CancelledOriginalDocumentType MapCancelledOriginalDocument(V5P0.CancelledOriginalDocumentType cancelledOriginalDocument)
        {
            throw new NotImplementedException();
        }

        private static LanguageType MapLanguage(string language)
        {
            throw new NotImplementedException();
        }

        private static CurrencyType MapCurrency(string invoiceCurrency)
        {
            throw new NotImplementedException();
        }
    }
}
