using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ebIViewModels.Validation.Tests
{
    [TestClass()]
    public class GLNValidatorTests
    {
        [TestMethod()]
        public void IsInvalidGlnTest()
        {
            var result = GLNValidator.IsValidGln("123",false);
            Assert.AreEqual(GLNValidator.GLN_ErrorCode.Längefalsch,result);

        }
        [TestMethod()]
        public void IsInvalidBundGlnTest()
        {
            var result = GLNValidator.IsValidGln("12345678901234", true);
            Assert.AreEqual(GLNValidator.GLN_ErrorCode.LängefalschBund, result);

        }

        [TestMethod()]
        public void IsValidGlnTest()
        {
            var result = GLNValidator.IsValidGln("9099999303132", false);
            Assert.AreEqual(GLNValidator.GLN_ErrorCode.OK, result);

        }
        [TestMethod()]
        public void IsWrongPrzGlnTest()
        {
            var result = GLNValidator.IsValidGln("9099999303131", false);
            Assert.AreEqual(GLNValidator.GLN_ErrorCode.Prüfziffer, result);

        }
        [TestMethod]
        public void IsNotNumGlnTest()
        {
            var result = GLNValidator.IsValidGln("9099999A03131", false);
            Assert.AreEqual(GLNValidator.GLN_ErrorCode.NichtNumerisch, result);

        }
    }
}
