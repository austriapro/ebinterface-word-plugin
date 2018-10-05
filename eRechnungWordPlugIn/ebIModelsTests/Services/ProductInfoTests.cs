using NUnit.Framework;
using ebIModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebIModels.Services.Tests
{
    [TestFixture]
    public class ProductInfoTests
    {
        [Test]
        public void ProductInfoTest()
        {
            var prodInfo = new ProductInfo();
            Assert.IsNotNull(prodInfo);
        }

    }
}