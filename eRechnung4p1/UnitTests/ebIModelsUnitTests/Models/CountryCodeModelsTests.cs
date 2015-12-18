using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ebIModels.Models.Tests
{
    [TestClass()]
    public class CountryCodeModelsTests
    {
        [TestMethod()]
        public void GetCountryCodeListTest()
        {
            // var cc = new CountryCodesModels();
            List<CountryCodeModel> ccList = CountryCodes.GetCountryCodeList();
            Assert.IsNotNull(ccList);
        }
    }
}
