﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ebIModels.Models.Tests
{
    [TestClass()]
    public class InvoiceSubtypesTests
    {
        [TestMethod()]
        public void GetSubtypeTestIndustryOk()
        {
            var erg1 = InvoiceSubtypes.GetVariant("Wirtschaft");
            Assert.AreEqual(InvoiceSubtypes.ValidationRuleSet.Industries, erg1);
            var erg = InvoiceSubtypes.GetSubtype(InvoiceSubtypes.ValidationRuleSet.Industries);
            Assert.AreEqual(InvoiceSubtypes.ValidationRuleSet.Industries,erg.VariantOption);
        }

        [TestMethod()]
        public void GetListTestOk()
        {
            var erg = InvoiceSubtypes.GetList();
            Assert.IsNotNull(erg);
        }
    }
}
