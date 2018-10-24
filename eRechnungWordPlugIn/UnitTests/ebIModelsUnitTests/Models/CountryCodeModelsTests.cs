using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Models;
using NUnit.Framework;
namespace ebIModels.Models.Tests
{
    [TestFixture]
    public class CountryCodeModelsTests
    {
        [Test]
        public void GetCountryCodeListTest()
        {
            // var cc = new CountryCodesModels();
            List<CountryCodeModel> ccList = CountryCodes.GetCountryCodeList();
            Assert.IsNotNull(ccList);
        }
    }
}
