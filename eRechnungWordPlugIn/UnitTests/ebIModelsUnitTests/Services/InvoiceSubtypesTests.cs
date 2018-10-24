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
    public class InvoiceSubtypesTests
    {
        [Test]
        public void GetSubtypeTestIndustryOk()
        {
            var erg1 = InvoiceSubtypes.GetVariant("Wirtschaft");
            Assert.AreEqual(InvoiceSubtypes.ValidationRuleSet.Industries, erg1);
            var erg = InvoiceSubtypes.GetSubtype(InvoiceSubtypes.ValidationRuleSet.Industries);
            Assert.AreEqual(InvoiceSubtypes.ValidationRuleSet.Industries,erg.VariantOption);
        }

        [Test]
        public void GetListTestOk()
        {
            var erg = InvoiceSubtypes.GetList();
            Assert.IsNotNull(erg);
        }
    }
}
