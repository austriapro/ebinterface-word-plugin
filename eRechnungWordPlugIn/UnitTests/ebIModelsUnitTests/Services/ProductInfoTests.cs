using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ebIModels.Services.Tests
{
    [TestClass()]
    public class ProductInfoTests
    {
        [TestMethod()]
        public void GetTfsInfoTest()
        {
            var tfsInfo = ProductInfo.GetTfsInfoString();
            Assert.IsNotNull(tfsInfo);
        }
        [TestMethod()]
        public void GetProductInfoTest()
        {
            var prod = ProductInfo.GetProductInfo();
            Assert.IsNotNull(prod);
        }
    }
}
