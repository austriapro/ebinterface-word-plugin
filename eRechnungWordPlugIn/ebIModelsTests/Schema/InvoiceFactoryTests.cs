using NUnit.Framework;
using ebIModels.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebIModels.Schema.Tests
{
    [TestFixture]
    public class InvoiceFactoryTests
    {
        [Test]
        public void GetVersionsWithSaveSupportedTest()
        {
            var supportedLIst = InvoiceFactory.GetVersionsWithSaveSupported();
            Assert.IsNotNull(supportedLIst);
            Assert.IsFalse(supportedLIst.Contains("V4P0"));
        }
    }
}