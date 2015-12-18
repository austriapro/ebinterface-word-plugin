using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Models;
using ebIViewModels.ExtensionMethods;
using ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ebIViewModels.ExtensionMethods.Tests
{
    [TestClass()]
    public class StringExtensionsTests
    {
        [TestMethod()]
        public void ConvertEnumTest()
        {
            CountryCodeType ct = CountryCodeType.AT;
            ebIModels.Schema.ebInterface4p1.CountryCodeType ctErg;
            ctErg = ct.ConvertEnum<ebIModels.Schema.ebInterface4p1.CountryCodeType>();
            //var ctx = ct.ConvertEnum<CountryCodeType, ebIModels.
            // Assert.Fail();
        }
    }
}
