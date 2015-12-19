using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ebIModels.Models;
using ebIModels.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ebIModels.Schema.Tests
{
    [TestClass()]
    public class InvoiceFactoryTests
    {
        [TestMethod()]
        public void LoadTemplateTestOk()
        {
            string fn = @"Daten\Test-ebInterfaceRechn-2014-500-2014-03-19.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn);
            
            // invoice.Save(@"Daten\testInvoice.xml");
            Assert.IsNotNull(invoice);
            Assert.AreEqual(invoice.InvoiceSubtype.VariantOption,InvoiceSubtypes.ValidationRuleSet.Government);            
            Assert.IsInstanceOfType(invoice.Delivery.Item, typeof(PeriodType));
        }

        [TestMethod()]
        public void LoadTemplateTestNoGeneratingSystem()
        {
            string fn = @"Daten\DotNetApiCreatedInvoice.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn);

            // invoice.Save(@"Daten\testInvoice.xml");
            Assert.AreEqual(invoice.InvoiceSubtype.VariantOption,InvoiceSubtypes.ValidationRuleSet.Invalid);

        }

        [TestMethod()]
        public void SaveTemplateOk()
        {
            string fn = @"Daten\Test-ebInterfaceRechn-2014-500-2014-03-19.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn);

            invoice.SaveTemplate(@"Daten\testTemplateInvoice.xml");
            Assert.IsNotNull(invoice);
        }

        [TestMethod()]
        public void SaveTemplateAsIndustryOk()
        {
            string fn = @"Daten\Test-ebInterfaceRechn-2014-500-2014-03-19.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn);
            invoice.InvoiceSubtype = InvoiceSubtypes.GetSubtype(InvoiceSubtypes.ValidationRuleSet.Industries);
            invoice.SaveTemplate(@"Daten\testTemplateInvoiceIndustry.xml");
            Assert.IsNotNull(invoice);
        }
        [TestMethod()]
        public void SaveTemplateAsGutschriftIndustryOk()
        {
            string fn = @"Daten\Test-ebInterfaceRechn-2014-500-2014-03-19.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn);
            invoice.InvoiceSubtype = InvoiceSubtypes.GetSubtype(InvoiceSubtypes.ValidationRuleSet.Industries);
            invoice.DocumentType = DocumentTypeType.CreditMemo;
            invoice.SaveTemplate(@"Daten\testTemplateGutschriftIndustry.xml");
            Assert.IsNotNull(invoice);
        }
        [TestMethod()]
        public void LoadTemplate4P1WithIndustryVorlageTagOk()
        {
            string fn = @"Daten\testTemplateInvoiceIndustrySample.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn);
            Assert.IsNotNull(invoice);
            Assert.AreEqual(InvoiceSubtypes.ValidationRuleSet.Industries,invoice.InvoiceSubtype.VariantOption);
        }

        [TestMethod()]
        public void LoadTemplate4P1WithVorlageTagOk()
        {
            string fn = @"Daten\testTemplateInvoiceTest.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn);
            Assert.IsNotNull(invoice);
        }

        [TestMethod()]
        public void LoadTemplate4P0AndSaveAs4P1Ok()
        {
            string fn = @"Daten\Test-ebInterfaceRechn-2014-500-2014-03-19.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn) as InvoiceType;
            invoice.Save(@"Daten\testSaveInvoice.xml");
            Assert.IsNotNull(invoice);
        }

    }
}
