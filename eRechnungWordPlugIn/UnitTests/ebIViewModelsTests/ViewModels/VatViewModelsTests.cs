using NUnit.Framework;
using ebIViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Models;

namespace ebIViewModels.ViewModels.Tests
{
    [TestFixture]
    public class VatViewModelsTests
    {
        [Test]
        public void GetTaxTypeTest()
        {
            VatViewModels vat = new VatViewModels();
            TaxType tax = new TaxType()
            {
                 TaxItem = new List<TaxItemType>()
                 {
                     new TaxItemType()
                     {
                          TaxableAmount = 100,
                          TaxPercent = new TaxPercentType()
                          {
                               TaxCategoryCode = "S", Value=20
                          },
                          TaxAmount = 20,
                          TaxAmountSpecified = true
                     }
                 }
            };
            vat = VatViewModels.Load(tax);
            VatViewModel vatItem = vat.VatViewList.FirstOrDefault();
            Assert.IsNotNull(vatItem);
        }
    }
}