using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ExtensionMethods.Tests
{
    [TestClass()]
    public class StringExtensionsTests
    {
        [TestMethod()]
        public void ToDecimalTest()
        {
            string num = "100";
            decimal dec = num.ToDecimal();
            Assert.AreEqual((decimal)100, dec);
        }

        [TestMethod()]
        public void EscapeXmlTest()
        {
            string input = "yxcyxc&/<>";
            string expected = "yxcyxc&amp;/&lt;&gt;";
            string output = input.EscapeXml();
            Assert.AreEqual(expected,output);
        }

        [TestMethod()]
        public void UnescapeXmlTest()
        {
            string input = "yxcyxc&/<>";
            string expected = "yxcyxc&amp;/&lt;&gt;";
            string output = expected.UnescapeXml();
            Assert.AreEqual(input, output);

        }
    }
}
