using ebIModels.Models;
using ebIModels.Schema;
using SettingsManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebIModels.Mapping
{
    public static partial class MapInvoice
    {
        public static List<MappingError> mappingErrors = new List<MappingError>();
        public static Models.IInvoiceModel MapToModel(object Invoice)
        {
            Models.IInvoiceModel invoiceModel = null;
            mappingErrors.Clear();
            switch (((IInvoiceBase)Invoice).Version)
            {
                case Models.EbIVersion.V4P0:
                    invoiceModel = V4p0.MapInvoice.MapV4P0ToVm((Schema.ebInterface4p0.InvoiceType)Invoice);                    
                    break;
                case Models.EbIVersion.V4P1:
                    invoiceModel = V4p1.MapInvoice.MapV4P1ToVm((Schema.ebInterface4p1.InvoiceType) Invoice);
                    break;
                case Models.EbIVersion.V4P2:
                    invoiceModel = V4p2.MapInvoice.MapV4P2ToVm((Schema.ebInterface4p2.InvoiceType)Invoice);
                    break;
                case Models.EbIVersion.V4P3:
                    invoiceModel = V4p3.MapInvoice.MapV4P3ToVm((Schema.ebInterface4p3.InvoiceType)Invoice);
                    break;
                case Models.EbIVersion.V5P0:
                    invoiceModel = V5p0.MapInvoice.MapV5p0ToVm((Schema.ebInterface5p0.InvoiceType)Invoice);
                    break;
                default:
                    break;
            }
            invoiceModel.Version = ((IInvoiceBase)Invoice).Version;
            return invoiceModel;
        }

        public static object MapToEbInterface(Models.IInvoiceModel invoiceModel, EbIVersion ebIVersion)
        {
            switch (ebIVersion)
            {
                case EbIVersion.V4P1:
                    return V4p1.MapInvoice.MapModelToV4p1(invoiceModel);
                case EbIVersion.V4P2:
                    return V4p2.MapInvoice.MapModelToV4p2(invoiceModel);
                case EbIVersion.V4P3:
                    return V4p3.MapInvoice.MapModelToV4p3(invoiceModel);
                case EbIVersion.V5P0:
                    return V5p0.MapInvoice.MapModelToV5p0(invoiceModel);
                default:
                    return null;
            }
        }
    }
}
