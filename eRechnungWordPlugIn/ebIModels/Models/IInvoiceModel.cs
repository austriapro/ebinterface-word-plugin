using System;
using System.Collections.Generic;
using ebIModels.Schema;


namespace ebIModels.Models
{
    public interface IInvoiceModel 
    {
        Models.EbIVersion Version { get; set; }

        List<AdditionalInformationType> AdditionalInformation { get; set; }
        BillerType Biller { get; set; }
        CancelledOriginalDocumentType CancelledOriginalDocument { get; set; }
        string Comment { get; set; }
        DeliveryType Delivery { get; set; }
        DetailsType Details { get; set; }
        string DocumentTitle { get; set; }
        DocumentTypeType DocumentType { get; set; }
        string GeneratingSystem { get; set; }
        CurrencyType InvoiceCurrency { get; set; }
        DateTime InvoiceDate { get; set; }
        string InvoiceNumber { get; set; }
        InvoiceRecipientType InvoiceRecipient { get; set; }
        bool IsDuplicate { get; set; }
        bool IsDuplicateSpecified { get; set; }
        LanguageType Language { get; set; }
        bool ManualProcessing { get; set; }
        bool ManualProcessingSpecified { get; set; }
        decimal NetAmount { get; set; }
        OrderingPartyType OrderingParty { get; set; }
        decimal PayableAmount { get; set; }
        PaymentConditionsType PaymentConditions { get; set; }
        PaymentMethodType PaymentMethod { get; set; }
        decimal PrepaidAmount { get; set; }
        bool PrepaidAmountSpecified { get; set; }
        ReductionAndSurchargeDetailsType ReductionAndSurchargeDetails { get; set; }
        List<RelatedDocumentType> RelatedDocument { get; set; }
        decimal RoundingAmount { get; set; }
        bool RoundingAmountSpecified { get; set; }
        TaxType Tax { get; set; }
        decimal TaxAmountTotal { get; set; }
        decimal TotalGrossAmount { get; set; }

        InvoiceSubtype InvoiceSubtype { get; set; }
       // bool InitFromSettings { get; set; }
        void CalculateTotals();
        void SaveTemplate(string filename);
        EbInterfaceResult IsValidInvoice(); 
        EbInterfaceResult IsValidErbInvoice();
        EbInterfaceResult Save(string filename);
        EbInterfaceResult Save(string filename, EbIVersion version);

    }
}