using System.Collections.Generic;
using System.Xml;
using ebIModels.Models;

namespace ebIModels.Schema
{
    public interface IInvoiceBase
    {
        List<EbISchema> CurrentSchemas { get; }
        Models.EbIVersion Version { get; }
        InvoiceSubtype InvoiceSubtype { get; }

        EbInterfaceResult IsValidInvoice();
        EbInterfaceResult IsValidInvoice(XmlDocument xDoc);
        EbInterfaceResult Save(string file);
        void SaveTemplate(string file);
        //void SetSubtype(InvoiceSubtype invoiceSubtype);
        XmlDocument ToXmlDocument();
    }
}