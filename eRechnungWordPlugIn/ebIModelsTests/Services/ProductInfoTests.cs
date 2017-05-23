using Microsoft.VisualStudio.TestTools.UnitTesting;
using ebIModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebIModels.Services.Tests
{
    [TestClass()]
    public class ProductInfoTests
    {
        [TestMethod()]
        public void ProductInfoTest()
        {
            var prodInfo = new ProductInfo();
            Assert.IsNotNull(prodInfo);
        }

    }
}