using ebIViewModels.ExtensionMethods;
using ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ebIViewModelsTests.ExtensionMethods
{
    [TestClass()]
    public class DecimalFractionValidatorTests
    {
        [TestMethod()]
        public void CheckDigitsTrueTest2digits()
        {
            var res = new decimal(3.14).CheckDigits(2);
            Assert.AreEqual(true,res);
        }
        [TestMethod()]
        public void CheckDigitsTrueTest0digits()
        {
            var res = new decimal(10000).CheckDigits(2);
            Assert.AreEqual(true, res);
        }
        [TestMethod]
        public void CheckDigitsTrueTest1digits()
        {
            var res = new decimal(3.1).CheckDigits(2);
            Assert.AreEqual(true, res);
        }
        [TestMethod]
        public void CheckDigitsTrueTest1p0digits()
        {
            decimal dec = new decimal(3.000000);
            var res = dec.CheckDigits(2);
            Assert.AreEqual(true, res);
        }
        [TestMethod()]
        public void CheckDigitsFalseTest()
        {
            var res = new decimal(3.1415).CheckDigits(2);
            Assert.AreEqual(false, res);
        }
    }
}
