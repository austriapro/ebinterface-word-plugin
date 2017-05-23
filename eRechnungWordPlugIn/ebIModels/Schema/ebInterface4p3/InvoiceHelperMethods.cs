using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ebIModels.Schema.ebInterface4p3
{
    public class InvoiceHelperMethods
    {

    }

    public partial class InvoiceType : ebIModels.Schema.InvoiceType
    {

        public void SetInvoiceVersion()
        {
            base.CurrentSchema = _schemaInfo;
            base.Version = Schema.InvoiceType.ebIVersion.V4P3;
        }


        internal static List<ebISchema> _schemaInfo = new List<ebISchema>()
            {
                // new ebISchema(){Prefix = "xsi",Url="http://www.w3.org/2001/XMLSchema-instance",CacheName = "xml.xsd",UseInSchema = false},
                new ebISchema(){Prefix = "eb",Url="http://www.ebinterface.at/schema/4p3/",CacheName = "ebInterface4p3.Invoice.xsd",UseInSchema = true},
                // new ebISchema(){Prefix = "dsig",Url="http://www.w3.org/2000/09/xmldsig#",CacheName = "xmldsig-core-schema.xsd",UseInSchema = true},
                new ebISchema(){Prefix = "ext",Url="http://www.ebinterface.at/schema/4p3/extensions/ext",CacheName = "ebInterface4p3.ebInterfaceExtension.xsd",UseInSchema = true},
                new ebISchema(){Prefix = "sv",Url="http://www.ebinterface.at/schema/4p3/extensions/sv",CacheName = "ebInterface4p3.ebInterfaceExtension_SV.xsd",UseInSchema = true}
            };


    }
}
