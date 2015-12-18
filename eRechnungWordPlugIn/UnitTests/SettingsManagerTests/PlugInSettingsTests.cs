using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettingsManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ebICommonTestSetup;
using System.Xml.Linq;
using System.IO;
namespace SettingsManager.Tests
{
    [TestClass()]
    public class PlugInSettingsTests
    {
        private XElement getElement(XDocument xdoc, string xName)
            {

            IEnumerable<XElement> xels = xdoc.Descendants();
            var xel = xels.FirstOrDefault(x=>x.Name.LocalName == xName);
            return xel;
            }
        [TestMethod()]
        public void GetVatTest()
        {
            
            var vatList = PlugInSettings.Default.VatDefaultValues;
            Assert.IsNotNull(vatList);
        }

        [TestMethod]
        public void SetVatTest()
        {

            var vatList = PlugInSettings.Default.VatDefaultValues;
            PlugInSettings.Default.VatDefaultValues = vatList;
            Console.WriteLine(PlugInSettings.Default.MwStTab);
            Assert.IsNotNull(vatList);
            
        }

        [TestMethod()]
        public void LoadUnitOfMeasureTest()
        {
            var uomLIst = PlugInSettings.Default.UnitOfMeasures;
            Assert.IsNotNull(uomLIst);
        }

        [TestMethod()]
        public void SaveUnitOfMeasureTest()
        {
            var uomLIst = PlugInSettings.Default.UnitOfMeasures;
            uomLIst[13].Favorite = true;
            PlugInSettings.Default.UnitOfMeasures = uomLIst;
            Assert.IsNotNull(uomLIst);
            
        }

        [TestMethod()]
        public void LoadSaveReloadUnitOfMeasureTest()
        {
            var uomLIst = PlugInSettings.Default.UnitOfMeasures;
            uomLIst[13].Favorite = true;
            PlugInSettings.Default.UnitOfMeasures = uomLIst;
            var uomList2 = PlugInSettings.Default.UnitOfMeasures;
            Console.WriteLine(uomList2[13]);
            Assert.IsNotNull(uomLIst);

        }

        [TestMethod]
        public void ResetOkTest()
        {
            PlugInSettings.Reset();
            Assert.AreEqual("EUR",PlugInSettings.Default.Currency);
        }
    }
}
