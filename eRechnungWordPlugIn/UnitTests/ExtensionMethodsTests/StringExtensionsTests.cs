using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;
using NUnit.Framework;
namespace ExtensionMethods.Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void ToDecimalTest()
        {
            string num = "100";
            decimal dec = num.ToDecimal();
            Assert.AreEqual((decimal)100, dec);
        }

        [Test]
        public void EscapeXmlTest()
        {
            string input = "yxcyxc&/<>";
            string expected = "yxcyxc&amp;/&lt;&gt;";
            string output = input.EscapeXml();
            Assert.AreEqual(expected,output);
        }

        [Test]
        public void UnescapeXmlTest()
        {
            string input = "yxcyxc&/<>";
            string expected = "yxcyxc&amp;/&lt;&gt;";
            string output = expected.UnescapeXml();
            Assert.AreEqual(input, output);

        }
    }
}
