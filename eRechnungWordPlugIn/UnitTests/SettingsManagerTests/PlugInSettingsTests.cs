using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettingsManager;
using NUnit.Framework;
using ebICommonTestSetup;
using System.Xml.Linq;
using System.IO;
namespace SettingsManager.Tests
{
    [TestFixture]
    public class PlugInSettingsTests
    {
        private XElement GetElement(XDocument xdoc, string xName)
            {

            IEnumerable<XElement> xels = xdoc.Descendants();
            var xel = xels.FirstOrDefault(x=>x.Name.LocalName == xName);
            return xel;
            }
        [Test]
        public void GetVatTest()
        {
            
            var vatList = PlugInSettings.Default.VatDefaultValues;
            Assert.IsNotNull(vatList);
        }


        [Test]
        public void LoadUnitOfMeasureTest()
        {
            var uomLIst = PlugInSettings.Default.UnitOfMeasures;
            Assert.IsNotNull(uomLIst);
        }

        [Test]
        public void SaveUnitOfMeasureTest()
        {
            var uomLIst = PlugInSettings.Default.UnitOfMeasures;
            uomLIst[13].Favorite = true;
            PlugInSettings.Default.UnitOfMeasures = uomLIst;
            Assert.IsNotNull(uomLIst);
            
        }

        [Test]
        public void LoadSaveReloadUnitOfMeasureTest()
        {
            var uomLIst = PlugInSettings.Default.UnitOfMeasures;
            uomLIst[13].Favorite = true;
            PlugInSettings.Default.UnitOfMeasures = uomLIst;
            var uomList2 = PlugInSettings.Default.UnitOfMeasures;
            Console.WriteLine(uomList2[13]);
            Assert.IsNotNull(uomLIst);

        }

        [Test]
        public void ResetOkTest()
        {
            PlugInSettings.Reset();
            Assert.AreEqual("EUR",PlugInSettings.Default.Currency);
        }
    }
}
