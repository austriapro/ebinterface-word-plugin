﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ebIModels.Schema;

namespace ebIModels.Models
{
    public interface IInvoiceType
    {
        Schema.InvoiceType.ebIVersion Version { get; set; }
    
        string InvoiceNumber { get; set; }

        System.DateTime InvoiceDate { get; set; }

        CancelledOriginalDocumentType CancelledOriginalDocument { get; set; }

        List<RelatedDocumentType> RelatedDocument { get; set; }

        DeliveryType Delivery { get; set; }

        BillerType Biller { get; set; }

        InvoiceRecipientType InvoiceRecipient { get; set; }

        OrderingPartyType OrderingParty { get; set; }

        DetailsType Details { get; set; }

        ReductionAndSurchargeDetailsType ReductionAndSurchargeDetails { get; set; }

        TaxType Tax { get; set; }

        /// <summary>
        /// Gesamtbetrag der Rechnung inkl. Steuern
        /// </summary>
        decimal? TotalGrossAmount { get; set; }

        /// <summary>
        /// Zu zahlender Betrag der Rechnung
        /// </summary>
        decimal? PayableAmount { get; set; }

        /// <summary>
        /// Nettobetrag der Rechnung
        /// </summary>
        decimal? NetAmount { get; set; }

        /// <summary>
        /// Summe aller Vat Beträge
        /// </summary>
        decimal? TaxAmount { get; set; }


        PaymentMethodType PaymentMethod { get; set; }

        PaymentConditionsType PaymentConditions { get; set; }

        PresentationDetailsType PresentationDetails { get; set; }

        string Comment { get; set; }

        InvoiceRootExtensionType InvoiceRootExtension { get; set; }

        string GeneratingSystem { get; set; }

        DocumentTypeType DocumentType { get; set; }

        CurrencyType InvoiceCurrency { get; set; }

        bool ManualProcessing { get; set; }

        [XmlIgnore()]
        bool ManualProcessingSpecified { get; set; }

        string DocumentTitle { get; set; }

        LanguageType Language { get; set; }

        [XmlIgnore()]
        bool LanguageSpecified { get; set; }

        bool IsDuplicate { get; set; }

        [XmlIgnore()]
        bool IsDuplicateSpecified { get; set; }

        InvoiceSubtype InvoiceSubtype { get; set; }
        bool InitFromSettings { get; set; }
            
        void CalculateTotals();

        void SaveTemplate(string filename);

        ebInterfaceResult Save(string filename);
    }
}
