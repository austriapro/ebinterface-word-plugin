using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebIModels.Models
{
   public interface IDocumentTypes
   {
       
       List<DocumentTypeModel> GetDocumentTypes(InvoiceSubtypes.ValidationRuleSet invVariant);

       
   }
}
