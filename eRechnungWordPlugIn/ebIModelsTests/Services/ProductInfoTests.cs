using NUnit.Framework;
using ebIModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ServiceStack.Text;
using System.Net;
using System.IO;
using System.Xml.Schema;
using XML = System.Xml.Serialization;

namespace ebIModels.Services.Tests
{
    [SetUpFixture]
    public class CommonSetUpClass
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var dir = Path.GetDirectoryName(typeof(CommonSetUpClass).Assembly.Location);
            Environment.CurrentDirectory = dir;
            Directory.SetCurrentDirectory(dir);
            Console.WriteLine($"Directory:{dir}");
        }
    }
    [TestFixture]
    public class ProductInfoTests
    {
        [Test]
        public void ProductInfoTest()
        {
            var prodInfo = new ProductInfo();
            
            prodInfo.PrintDump();
            Assert.IsNotNull(prodInfo);
        }

    }
}