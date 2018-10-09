using NUnit.Framework;
using ebIModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Schema;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using ServiceStack.Text;

namespace ebIModels.Models.Tests
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
    public class InvoiceTypeTests
    {
        [TestCase(@"Daten\testTemplateInvoiceTest.xml", @"Daten\Save4p1.xml", Models.EbIVersion.V4P1, "http://www.ebinterface.at/schema/4p1/")]
        [TestCase(@"Daten\Rechng-1-V4p2-20160129V2.xml", @"Daten\Save4p2.xml", Models.EbIVersion.V4P2, "http://www.ebinterface.at/schema/4p2/")]
        [TestCase(@"Daten\Rechng-V4p3-2017-001-neu.xml", @"Daten\Save4p3.xml", Models.EbIVersion.V4P3, "http://www.ebinterface.at/schema/4p3/")]
        [TestCase(@"Daten\Rechng-V4p3-2017-001-neu.xml", @"Daten\Save5p0.xml", Models.EbIVersion.V5P0, "http://www.ebinterface.at/schema/5p0/")]
        [Test]
        public void SaveInvoiceTest(string inputFile, string outputFile, Models.EbIVersion ebIVersion, string expectedAttr)
        {
            var invoice = InvoiceFactory.LoadTemplate(inputFile);
            invoice.PrintDump();
            EbInterfaceResult result = invoice.Save(outputFile, ebIVersion);
            result.PrintDump();
            Assert.That(result.ResultType == ResultType.IsValid, $"Validation Error: {outputFile} ");
            XDocument xInv = XDocument.Load(outputFile);
            var attrs = xInv.Root.Attributes().Where(p => p.IsNamespaceDeclaration == true).FirstOrDefault(x=>x.Name.LocalName=="eb");
            Assert.IsNotNull(attrs);            
            Assert.AreEqual(expectedAttr, attrs.Value);
        }
 
    }
}