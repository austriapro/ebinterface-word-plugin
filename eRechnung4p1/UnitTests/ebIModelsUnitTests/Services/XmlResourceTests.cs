using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ebIModels.Services.Tests
{
    [TestClass()]
    public class ResourceServiceTests
    {
        [TestMethod()]
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
