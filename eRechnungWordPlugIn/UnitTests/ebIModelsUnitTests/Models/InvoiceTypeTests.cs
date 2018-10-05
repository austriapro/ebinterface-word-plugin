﻿using NUnit.Framework;
using ebIModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Schema;
using System.Xml;
using System.Xml.Linq;

namespace ebIModels.Models.Tests
{
    [TestFixture]
    public class InvoiceTypeTests
    {
        [Test]
        public void Save4p1Test()
        {
            const string fn = @"Daten\testTemplateInvoiceTest.xml";
            const string save4p1Fn = @"Daten\Save4p1.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn);
            invoice.Save(save4p1Fn, Models.EbIVersion.V4P1);

            XDocument xInv = XDocument.Load(save4p1Fn);
            var attrs = xInv.Root.Attributes().Where(p => p.IsNamespaceDeclaration == true).FirstOrDefault(x=>x.Name.LocalName=="eb");
            Assert.IsNotNull(attrs);
            const string expectedString = "http://www.ebinterface.at/schema/4p1/";
            Assert.AreEqual(expectedString, attrs.Value);
        }
        [Test]
        public void Save4p2Test()
        {
            const string fn = @"Daten\testTemplateInvoiceTest.xml";
            const string save4p1Fn = @"Daten\Save4p2.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn);
            invoice.Save(save4p1Fn, Models.EbIVersion.V4P2);

            XDocument xInv = XDocument.Load(save4p1Fn);
            var attrs = xInv.Root.Attributes().Where(p => p.IsNamespaceDeclaration == true).FirstOrDefault(x => x.Name.LocalName == "eb");
            Assert.IsNotNull(attrs);
            const string expectedString = "http://www.ebinterface.at/schema/4p2/";
            Assert.AreEqual(expectedString, attrs.Value);
        }

        [Test]
        public void Save4p3Test()
        {
            const string fn = @"Daten\testTemplateInvoiceTest.xml";
            const string save4p1Fn = @"Daten\Save4p3.xml";
            var invoice = InvoiceFactory.LoadTemplate(fn);
            invoice.Save(save4p1Fn, Models.EbIVersion.V4P3);

            XDocument xInv = XDocument.Load(save4p1Fn);
            var attrs = xInv.Root.Attributes().Where(p => p.IsNamespaceDeclaration == true).FirstOrDefault(x => x.Name.LocalName == "eb");
            Assert.IsNotNull(attrs);
            const string expectedString = "http://www.ebinterface.at/schema/4p3/";
            Assert.AreEqual(expectedString, attrs.Value);
        }
    }
}