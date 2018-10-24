using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using ebIModels.Models;
using ExtensionMethods;

namespace ebIModels.Schema.ebInterface4p0
{
    public partial class InvoiceType : Schema.InvoiceBase
    {

        public InvoiceType()
        {
            base.CurrentSchemas = _schemaInfo;
            base.Version = EbIVersion.V4P0;
        }

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
        internal static List<EbISchema> _schemaInfo = new List<EbISchema>()
            {
                // new ebISchema(){Prefix = "xsi",Url="http://www.w3.org/2001/XMLSchema-instance",CacheName = "xml.xsd",UseInSchema = false},
                new EbISchema(){Prefix = "eb",Url="http://www.ebinterface.at/schema/4p0/",CacheName = "ebInterface4p0.Invoice.xsd",UseInSchema = true},
                // new ebISchema(){Prefix = "dsig",Url="http://www.w3.org/2000/09/xmldsig#",CacheName = "xmldsig-core-schema.xsd",UseInSchema = true},
                new EbISchema(){Prefix = "ext",Url="http://www.ebinterface.at/schema/4p0/extensions/ext",CacheName = "ebInterface4p0.ebInterfaceExtension.xsd",UseInSchema = true},
                new EbISchema(){Prefix = "sv",Url="http://www.ebinterface.at/schema/4p0/extensions/sv",CacheName = "ebInterface4p0.ebInterfaceExtension_SV.xsd",UseInSchema = true}
            };


    }
}
