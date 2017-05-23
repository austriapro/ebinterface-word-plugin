using Microsoft.VisualStudio.TestTools.UnitTesting;
using ebIModels.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebIModels.Schema.Tests
{
    [TestClass()]
    public class InvoiceFactoryTests
    {
        [TestMethod()]
        public void GetVersionsWithSaveSupportedTest()
        {
            var supportedLIst = InvoiceFactory.GetVersionsWithSaveSupported();
            Assert.IsNotNull(supportedLIst);
            Assert.IsFalse(supportedLIst.Contains("V4P0"));
        }
    }
}