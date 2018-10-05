using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIValidation;
using NUnit.Framework;

namespace ebIViewModels.Validation.Tests
{
    [TestFixture]
    public class GLNValidatorTests
    {
        [Test]
        public void IsInvalidGlnTest()
        {
            var result = GLNValidator.IsValidGln("123",false);
            Assert.AreEqual(GLNValidator.GLN_ErrorCode.Längefalsch,result);

        }
        [Test]
        public void IsInvalidBundGlnTest()
        {
            var result = GLNValidator.IsValidGln("12345678901234", true);
            Assert.AreEqual(GLNValidator.GLN_ErrorCode.LängefalschBund, result);

        }

        [Test]
        public void IsValidGlnTest()
        {
            var result = GLNValidator.IsValidGln("9099999303132", false);
            Assert.AreEqual(GLNValidator.GLN_ErrorCode.OK, result);

        }
        [Test]
        public void IsWrongPrzGlnTest()
        {
            var result = GLNValidator.IsValidGln("9099999303131", false);
            Assert.AreEqual(GLNValidator.GLN_ErrorCode.Prüfziffer, result);

        }
        [Test]
        public void IsNotNumGlnTest()
        {
            var result = GLNValidator.IsValidGln("9099999A03131", false);
            Assert.AreEqual(GLNValidator.GLN_ErrorCode.NichtNumerisch, result);

        }
    }
}
