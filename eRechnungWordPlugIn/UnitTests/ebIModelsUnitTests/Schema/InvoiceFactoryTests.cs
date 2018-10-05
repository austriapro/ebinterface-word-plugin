using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ebIModels.Models;
using ebIModels.Schema;
using NUnit.Framework;

namespace ebIModels.Schema.Tests
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
    public class InvoiceFactoryTests
    {
 
        [TestCase(@"Daten\Test-ebInterfaceRechn-2014-500-2014-03-19.xml")]
        [TestCase(@"Daten\DotNetApiCreatedInvoice.xml")]
        [TestCase(@"Daten\Rechng-1-V4p2-20160129V2.xml")]
        [TestCase(@"Daten\Rechng-V4p3-2017-001-neu.xml")]
        [Test]
        public void LoadTemplateTestOk(string rechnungFn)
        {
            string fn = rechnungFn;// @"Daten\Test-ebInterfaceRechn-2014-500-2014-03-19.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn);
            Console.WriteLine($"{invoice.Version.ToString()}:\t{rechnungFn}");
            // invoice.Save(@"Daten\testInvoice.xml");
            Assert.IsNotNull(invoice);
           // Assert.AreEqual(invoice.InvoiceSubtype.VariantOption,InvoiceSubtypes.ValidationRuleSet.Government);            
           // Assert.IsInstanceOf<PeriodType>(invoice.Delivery.Item);
        }

        [Test]
        public void LoadTemplateTestNoGeneratingSystem()
        {
            string fn = @"Daten\DotNetApiCreatedInvoice.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn);

            // invoice.Save(@"Daten\testInvoice.xml");
            Assert.AreEqual(invoice.InvoiceSubtype.VariantOption,InvoiceSubtypes.ValidationRuleSet.Invalid);

        }

        [Test]
        public void SaveTemplateOk()
        {
            string fn = @"Daten\Test-ebInterfaceRechn-2014-500-2014-03-19.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn);

            invoice.SaveTemplate(@"Daten\testTemplateInvoice.xml");
            Assert.IsNotNull(invoice);
        }

        [Test]
        public void SaveTemplateAsIndustryOk()
        {
            string fn = @"Daten\Test-ebInterfaceRechn-2014-500-2014-03-19.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn);
            invoice.InvoiceSubtype = InvoiceSubtypes.GetSubtype(InvoiceSubtypes.ValidationRuleSet.Industries);
            invoice.SaveTemplate(@"Daten\testTemplateInvoiceIndustry.xml");
            Assert.IsNotNull(invoice);
        }
        [Test]
        public void SaveTemplateAsGutschriftIndustryOk()
        {
            string fn = @"Daten\Test-ebInterfaceRechn-2014-500-2014-03-19.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn);
            invoice.InvoiceSubtype = InvoiceSubtypes.GetSubtype(InvoiceSubtypes.ValidationRuleSet.Industries);
            invoice.DocumentType = DocumentTypeType.CreditMemo;
            invoice.SaveTemplate(@"Daten\testTemplateGutschriftIndustry.xml");
            Assert.IsNotNull(invoice);
        }
        [Test]
        public void LoadTemplate4P1WithIndustryVorlageTagOk()
        {
            string fn = @"Daten\testTemplateInvoiceIndustrySample.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn);
            Assert.IsNotNull(invoice);
            Assert.AreEqual(InvoiceSubtypes.ValidationRuleSet.Industries,invoice.InvoiceSubtype.VariantOption);
        }

        [Test]
        public void LoadTemplate4P1WithVorlageTagOk()
        {
            string fn = @"Daten\testTemplateInvoiceTest.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn);
            Assert.IsNotNull(invoice);
        }

        [Test]
        public void LoadTemplate4P0AndSaveAs4P1Ok()
        {
            string fn = @"Daten\Test-ebInterfaceRechn-2014-500-2014-03-19.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn) as InvoiceModel;
            invoice.Save(@"Daten\testSaveInvoice.xml");
            Assert.IsNotNull(invoice);
        }

    }
}
