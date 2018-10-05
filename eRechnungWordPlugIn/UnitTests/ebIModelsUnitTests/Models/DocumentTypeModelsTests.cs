using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Models;
using NUnit.Framework;
namespace ebIModels.Models.Tests
{
    [TestFixture]
    public class DocumentTypeModelsTests
    {
        [Test]
        public void GetDocumentTypesTest()
        {
            var docType = new DocumentTypeModels();
            var docTypes = docType.GetDocumentTypes(InvoiceSubtypes.ValidationRuleSet.Government);
            Assert.IsNotNull(docTypes);
        }

        [Test]
        public void GetREfDocTypesTest()
        {
            var docType = new DocumentTypeModels();
            var docTypes = docType.GetReferenceDocumentTypes(InvoiceSubtypes.ValidationRuleSet.Government);
            Assert.IsNotNull(docTypes);
            var sel = docTypes.Where(x => x.CodeEnglish.StartsWith("Cancel"));
            Assert.AreEqual(0, sel.Count());
        }
    }
}
