using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Services;
using NUnit.Framework;
namespace ebIModels.Services.Tests
{
    [TestFixture]
    public class ResourceServiceTests
    {
        [Test]
        public void ReadResourceServiceTest()
        {
            string file = @"iso_3166-1_laender.xml";
            string xmlContent = "";
            var xmlres = new ResourceService();
            xmlContent = xmlres.ReadXmlString(file);
            Assert.IsNotNull(xmlContent);
        }
    }
}
