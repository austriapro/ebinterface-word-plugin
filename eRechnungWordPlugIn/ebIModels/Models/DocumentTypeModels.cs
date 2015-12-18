using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ebIModels.Services;
using IResourceService = ebIModels.Services.IResourceService;

namespace ebIModels.Models
{
    public class DocumentTypeModels : IDocumentTypes
    {
        private List<DocumentTypeModel> _documentTypes; 
        public List<DocumentTypeModel> GetDocumentTypes(InvoiceSubtypes.ValidationRuleSet invcVariant)
        {
            var variants = InvoiceSubtypes.GetSubtype(invcVariant);
            InvoiceSubtype variant2Process = variants;
            IResourceService xmlRes = new ResourceService();
            var xDoc = xmlRes.ReadXmlDocument(variant2Process.FileName);
            IEnumerable<XElement> childs = from xel in xDoc.Elements() select xel;
            List<DocumentTypeModel> documentType = (from xElement in childs
                                                    select new DocumentTypeModel()
                                                    {
                                                        CodeEnglish = xElement.Element("english").Value,
                                                        TextGerman = xElement.Element("german").Value
                                                    }).ToList<DocumentTypeModel>();
            _documentTypes = documentType;
            return documentType;
        }
        public List<DocumentTypeModel> GetReferenceDocumentTypes(InvoiceSubtypes.ValidationRuleSet invcVariant)
        {
            var variants = InvoiceSubtypes.GetSubtype(invcVariant);
            InvoiceSubtype variant2Process = variants;
            IResourceService xmlRes = new ResourceService();
            var xDoc = xmlRes.ReadXmlDocument(variant2Process.FileName);
            IEnumerable<XElement> childs = from xel in xDoc.Elements() select xel;
            List<DocumentTypeModel> documentType = (from xElement in childs where !xElement.Element("english").Value.StartsWith("Cancel")
                                                    select new DocumentTypeModel()
                                                    {
                                                        CodeEnglish = xElement.Element("english").Value,
                                                        TextGerman = xElement.Element("german").Value
                                                    }).ToList<DocumentTypeModel>();
            _documentTypes = documentType;
            return documentType;
        }

    }
}
