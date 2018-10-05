using ebIViewModels.ExtensionMethods;
using ExtensionMethods;
using NUnit.Framework;

namespace ebIViewModelsTests.ExtensionMethods
{
    [TestFixture]
    public class DecimalFractionValidatorTests
    {
        [Test]
        public void CheckDigitsTrueTest2digits()
        {
            var res = new decimal(3.14).CheckDigits(2);
            Assert.AreEqual(true,res);
        }
        [Test]
        public void CheckDigitsTrueTest0digits()
        {
            var res = new decimal(10000).CheckDigits(2);
            Assert.AreEqual(true, res);
        }
        [Test]
        public void CheckDigitsTrueTest1digits()
        {
            var res = new decimal(3.1).CheckDigits(2);
            Assert.AreEqual(true, res);
        }
        [Test]
        public void CheckDigitsTrueTest1p0digits()
        {
            decimal dec = new decimal(3.000000);
            var res = dec.CheckDigits(2);
            Assert.AreEqual(true, res);
        }
        [Test]
        public void CheckDigitsFalseTest()
        {
            var res = new decimal(3.1415).CheckDigits(2);
            Assert.AreEqual(false, res);
        }
    }
}
