using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ebIModels.Models;
using System.Xml;

namespace ebIModels.Schema.ebInterface5p0
{
    public class InvoiceHelperMethods5p0
    {

    }

    public partial class InvoiceType : InvoiceBase
    {

        public void SetInvoiceVersion()
        {
            base.CurrentSchemas = _schemaInfo;
            base.Version = Models.EbIVersion.V5P0;

        }

        internal static List<EbISchema> _schemaInfo = new List<EbISchema>()
        {
                new EbISchema() {
                Prefix = "eb",
                Url ="http://www.ebinterface.at/schema/5p0/",
                CacheName = "ebInterface5p0.Invoice.xsd",
                UseInSchema = true},
        };

        public override XmlDocument ToXmlDocument()
        {
            
            return base.ToXmlDocument(this);
        }
        public override EbInterfaceResult Save(string file)
        {
            return base.Save(file, this);
        }

        public override EbInterfaceResult IsValidInvoice()
        {
            XmlDocument xDoc = ToXmlDocument(this);

            EbInterfaceResult result = IsValidInvoice(xDoc);
            return result;
        }

        public override void SaveTemplate(string file)
        {

            base.SaveTemplate(file, this);
        }
        public override EbInterfaceResult IsValidInvoice(XmlDocument xDoc)
        {
            return base.IsValidInvoice(xDoc, this);
        }
    }
}
