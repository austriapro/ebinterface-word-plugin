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
using System.Xml.XPath;
using ExtensionMethods;
using KellermanSoftware.CompareNetObjects;

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
        [TestCase(@"Daten\Rechng-V4p3-2017-001-neu.xml", @"Daten\Save5p0von4p3.xml", Models.EbIVersion.V5P0, "http://www.ebinterface.at/schema/5p0/")]
        [TestCase(@"Daten\Test-5p0.xml", @"Daten\Save5p0von5p0.xml", Models.EbIVersion.V5P0, "http://www.ebinterface.at/schema/5p0/")]
        [TestCase(@"Test Vorlage 2014-500-2014-03-19.XML", @"Daten\Save4p1von4p1.xml", Models.EbIVersion.V4P1, "http://www.ebinterface.at/schema/4p1/")]
        [TestCase(@"Test Vorlage 2014-500-2014-03-19.XML", @"Daten\Save4p2von4p1.xml", Models.EbIVersion.V4P2, "http://www.ebinterface.at/schema/4p2/")]
        [TestCase(@"Test Vorlage 2014-500-2014-03-19.XML", @"Daten\Save4p3von4p1.xml", Models.EbIVersion.V4P3, "http://www.ebinterface.at/schema/4p3/")]
        [TestCase(@"Test Vorlage 2014-500-2014-03-19.XML", @"Daten\Save5p0von4p1.xml", Models.EbIVersion.V5P0, "http://www.ebinterface.at/schema/5p0/")]
        [Test]
        public void SaveInvoiceTest(string inputFile, string outputFile, Models.EbIVersion ebIVersion, string expectedAttr)
        {
            var invoice = InvoiceFactory.LoadTemplate(inputFile);
            invoice.PrintDump();
            EbInterfaceResult result = invoice.Save(outputFile, ebIVersion);
            result.PrintDump();
            Assert.That(result.ResultType == ResultType.IsValid, $"Validation Error: {outputFile} ");
            XDocument xInv = XDocument.Load(outputFile);
            var attrs = xInv.Root.Attributes().Where(p => p.IsNamespaceDeclaration == true).FirstOrDefault(x => x.Name.LocalName == "eb");
            Assert.IsNotNull(attrs);
            Assert.AreEqual(expectedAttr, attrs.Value);
        }

        [TestCase(@"Daten\Rechng-V4p3-2017-001-neu.XML",@"Daten\Save4p1von4p3.xml", Models.EbIVersion.V4P1)]
        [TestCase(@"Daten\Rechng-V4p3-2017-001-neu.XML",@"Daten\Save4p2von4p3.xml", Models.EbIVersion.V4P2)]
        [TestCase(@"Daten\Rechng-V4p3-2017-001-neu.XML", @"Daten\Save4p3von4p3.xml", Models.EbIVersion.V4P3)]
        [TestCase(@"Daten\Rechng-V4p3-2017-001-neu.XML", @"Daten\Save5p0von4p3.xml", Models.EbIVersion.V5P0)]
        [TestCase(@"Daten\Rechng-1-V4p2-20160129V2.XML", @"Daten\Save4p1von4p2.xml", Models.EbIVersion.V4P1)]
        [TestCase(@"Daten\Rechng-1-V4p2-20160129V2.XML", @"Daten\Save4p2von4p2.xml", Models.EbIVersion.V4P2)]
        [TestCase(@"Daten\Rechng-1-V4p2-20160129V2.XML", @"Daten\Save4p3von4p2.xml", Models.EbIVersion.V4P3)]
        [TestCase(@"Daten\Rechng-1-V4p2-20160129V2.XML", @"Daten\Save5p0von4p2.xml", Models.EbIVersion.V5P0)]
        [TestCase(@"Daten\Test-ebInterfaceRechn-2014-500-2014-03-19.XML", @"Daten\Save4p1von4p0.xml", Models.EbIVersion.V4P1)]
        [TestCase(@"Daten\Test-ebInterfaceRechn-2014-500-2014-03-19.XML", @"Daten\Save4p2von4p0.xml", Models.EbIVersion.V4P2)]
        [TestCase(@"Daten\Test-ebInterfaceRechn-2014-500-2014-03-19.XML", @"Daten\Save4p3von4p0.xml", Models.EbIVersion.V4P3)]
        [TestCase(@"Daten\Test-ebInterfaceRechn-2014-500-2014-03-19.XML", @"Daten\Save5p0von4p0.xml", Models.EbIVersion.V5P0)]
        public void TaxItemTest(string inputFile,string outputFile, Models.EbIVersion ebIVersion)
        {
            
            var invoice = InvoiceFactory.LoadTemplate(inputFile);
            invoice.PrintDump();
            EbInterfaceResult result = invoice.Save(outputFile, ebIVersion);
            Assert.That(result.ResultType == ResultType.IsValid);
            var inputXml = XDocument.Load(inputFile);
            var savedXml = XDocument.Load(outputFile);
            var inResult = inputXml.Descendants().Where(p1 => p1.Descendants().Count() == 0).Select(p => new NodeList
            {
                NodePath = p.GetPath(),
                Value = p.Value
            }).Distinct().OrderBy(n => n.NodePath).ToList();
            var outResult = savedXml.Descendants().Where(p1=>p1.Descendants().Count()==0).Select(p => new NodeList
            {
                NodePath = p.GetPath(),
                Value = p.Value
            }).Distinct().OrderBy(n => n.NodePath).ToList();
            Console.WriteLine("Nodes not in Output -----------------------");
            var notInOut = inResult.Where(p => !outResult.Any(p2 => p2.NodePath == p.NodePath)).ToList();
            PrintDiff(notInOut);
            Console.WriteLine("Nodes not in Input -----------------------");

            var notInIn = outResult.Where(p => !inResult.Any(p2 => p2.NodePath == p.NodePath)).ToList();
            PrintDiff(notInIn);
            Console.WriteLine("Value different -----------------------");
            var valDif = inResult.Where(p => outResult.Any(p2 => p2.NodePath == p.NodePath && p2.Value != p.Value));
        }
        public void PrintDiff(List<NodeList> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine($"{item.NodePath}\t{item.Value}");
            }
        }



    }
    public class NodeList
    {
        public string NodePath { get; set; }
        public string Value { get; set; }
    }
}
