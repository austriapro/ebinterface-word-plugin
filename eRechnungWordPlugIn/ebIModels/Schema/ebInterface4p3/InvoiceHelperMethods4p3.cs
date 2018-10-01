using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace ebIModels.Schema.ebInterface4p3
{

    public partial class InvoiceType : Schema.InvoiceBase
    {

        public void SetInvoiceVersion()
        {
            base.CurrentSchemas = _schemaInfo;
            base.Version = Models.EbIVersion.V4P3;
            
        }
        public override  XmlDocument ToXmlDocument()
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
            return base.IsValidInvoice(xDoc,this);
        }

        internal static List<EbISchema> _schemaInfo = new List<EbISchema>()
            {
                // new ebISchema(){Prefix = "xsi",Url="http://www.w3.org/2001/XMLSchema-instance",CacheName = "xml.xsd",UseInSchema = false},
                new EbISchema(){Prefix = "eb",Url="http://www.ebinterface.at/schema/4p3/",CacheName = "ebInterface4p3.Invoice.xsd",UseInSchema = true},
                // new ebISchema(){Prefix = "dsig",Url="http://www.w3.org/2000/09/xmldsig#",CacheName = "xmldsig-core-schema.xsd",UseInSchema = true},
                new EbISchema(){Prefix = "ext",Url="http://www.ebinterface.at/schema/4p3/extensions/ext",CacheName = "ebInterface4p3.ebInterfaceExtension.xsd",UseInSchema = true},
                new EbISchema(){Prefix = "sv",Url="http://www.ebinterface.at/schema/4p3/extensions/sv",CacheName = "ebInterface4p3.ebInterfaceExtension_SV.xsd",UseInSchema = true}
            };


    }
}
