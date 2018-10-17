using System;
using System.IO;
using System.Linq;
using ebIModels.Models;
using ebIModels.Schema;
using ExtensionMethods;
using NUnit.Framework;
using ServiceStack.Text;

namespace ebIModelsUnitTests.Models.Mapping
{
    [SetUpFixture]
    public class CommonSetUpClass
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var dir = Path.GetDirectoryName(typeof(CommonSetUpClass).Assembly.Location);
            Environment.CurrentDirectory = dir;

            // or
            Directory.SetCurrentDirectory(dir);
        }
    }

    [TestFixture]
    public class MappingTest
    {
        [TestCase(@"Daten\Test-ebInterfaceRechn-2014-500-2014-03-19.xml")]
        [TestCase(@"Daten\DotNetApiCreatedInvoice.xml")]
        [TestCase(@"Daten\Rechng-1-V4p2-20160129V2.xml")]
        [TestCase(@"Daten\Rechng-V4p3-2017-001-neu.xml")]
        [TestCase(@"Daten\Test-5p0.xml")]
        [Test]
        public void TotalsAndTaxTest(string inputFn)
        {
            var invoice = InvoiceFactory.LoadTemplate(inputFn);

            var totals = from a in invoice.Details.ItemList[0].ListLineItem
                         group a by 1 into g
                         select new
                         {
                             netto = g.Sum(x => x.LineItemAmount),
                             ustGesamt = g.Sum(x => x.TaxItem.TaxPercent.Value * x.LineItemAmount / 100).FixedFraction(2)
                         };
            totals.PrintDump();
            // ToDo V5p0 Rechnung richtig stellen
            var tax = invoice.Details.ItemList[0].ListLineItem.GroupBy(s => new { Prozent = s.TaxItem.TaxPercent.Value, Code = s.TaxItem.TaxPercent.TaxCategoryCode })
                      .Select(p => new TaxItemType
                      {
                          TaxPercent = new TaxPercentType()
                          {
                              TaxCategoryCode = p.Key.Code,
                              Value = p.Key.Prozent
                          },                                                    
                          TaxableAmount = p.Sum(x => x.LineItemAmount),
                          TaxAmount = (p.Sum(x => x.LineItemAmount) * p.Key.Prozent / 100),                          
                      });
            tax.PrintDump();
            var totale = totals.FirstOrDefault();
            Assert.AreEqual(totale.ustGesamt, invoice.TaxAmountTotal, "USt Gesamt");
            Assert.AreEqual(totale.ustGesamt + totale.netto, invoice.TotalGrossAmount, "Gesamt Betrag");
            Assert.AreEqual(tax.Count(), invoice.Tax.TaxItem.Count,"TaxItem Count");
            Assert.Multiple(() =>
            {
                var taxLIst = tax.ToList();
                for (int i = 0; i < taxLIst.Count; i++)
                {
                    Assert.AreEqual(taxLIst[i].TaxableAmount, invoice.Tax.TaxItem[i].TaxableAmount,"TaxableAmount");
                    Assert.AreEqual(taxLIst[i].TaxAmount, invoice.Tax.TaxItem[i].TaxAmount,"Amount");
                    Assert.AreEqual(taxLIst[i].TaxPercent.TaxCategoryCode, invoice.Tax.TaxItem[i].TaxPercent.TaxCategoryCode,"Percent");
                    Assert.AreEqual(taxLIst[i].TaxPercent.Value, invoice.Tax.TaxItem[i].TaxPercent.Value,"Value");
                }
            });
        }
    }
}
