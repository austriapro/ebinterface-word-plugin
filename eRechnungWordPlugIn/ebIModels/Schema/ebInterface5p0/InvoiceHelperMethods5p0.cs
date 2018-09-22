using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ebIModels.Schema.ebInterface5p0 
{
    public class InvoiceHelperMethods5p0
    {

    }

    public partial class InvoiceType : ebIModels.Schema.InvoiceType
    {

        public void SetInvoiceVersion()
        {
            base.CurrentSchema = _schemaInfo;
            base.Version = Schema.InvoiceType.ebIVersion.V5p0;
        }


        internal static List<ebISchema> _schemaInfo = new List<ebISchema>()
            {
                new ebISchema(){Prefix = "eb",Url="http://www.ebinterface.at/schema/5p0/",CacheName = "ebInterface5p0.Invoice.xsd",UseInSchema = true},
            };


    }
}
