using Microsoft.VisualStudio.TestTools.UnitTesting;
using ebIViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Models;

namespace ebIViewModels.ViewModels.Tests
{
    [TestClass()]
    public class VatViewModelsTests
    {
        [TestMethod()]
        public void GetTaxTypeTest()
        {
            VatViewModels vat = new VatViewModels();
            TaxType tax = new TaxType()
            {
                VAT = new List<VATItemType>()
                {
                    new VATItemType()
                    {
                        TaxedAmount = new decimal(32.89),
                        Item = new VATRateType()
                        {
                             Value=20
                        },
                        Amount = new decimal(6.578)

                    }
                }
            };
            vat = VatViewModels.Load(tax);
            VatViewModel vatItem = vat.VatViewList.FirstOrDefault();
            Assert.IsNotNull(vatItem);
        }
    }
}