using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Models;
using ebIModels.Schema;
using ebIViewModels.ViewModels;
using NUnit.Framework;
namespace ebIViewModels.ViewModels.Tests
{
    [TestFixture]
    public class MappingServiceTests
    {
        [Test]
        public void MapV4P1ToVmTest()
        {
            string fn = @"Daten\Test-ebInterfaceRechn-2014-500-2014-03-19.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn);
            // var invVm = DocumentViewModel.MapV4P1ToVm(invoice as ebIModels.Schema.ebInterface4p1.InvoiceType);
            // var inv4p1 = MappingService.MapVMToV4p1(invVm);
            //Assert.AreEqual(new DateTime(2014, 04, 19), invoice.PaymentConditions.DueDate);
            Assert.AreEqual(CountryCodeType.AT, invoice.Biller.Address.Country.CountryCode);
            Assert.AreEqual("Österreich", invoice.Biller.Address.Country.Value);
            invoice.SaveTemplate(@"Daten\ConvertedInvoice.xml");
            Assert.IsNotNull(invoice);
        }
    }
}
