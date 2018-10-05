using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebICommonTestSetup;
using ebIModels.Schema;
using ebIViewModels.ViewModels;
using ebIViewModelsTests.ViewModels;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using SettingsManager;

namespace ebIViewModels.ViewModels.Tests
{
    [TestFixture]
    public class DetailsViewModelsTests : CommonTestSetup
    {
        private readonly Common _common;

        public DetailsViewModelsTests()
        {
            _common = Cmn;
        }


        [Test]
        public void DetailsViewModelsTest()
        {
            DetailsListConverter det = _common.UContainer.Resolve<DetailsListConverter>();
            var detv = new DetailsViewModel(null, null, false)
            {
                EinzelPreis = new decimal(60.0125),
                Menge = new decimal(10),
                VatItem = new SettingsManager.VatDefaultValue("S",20,"Testbeschreibung")
            };
            // detv.UpdateTotals();
            det.DetailsList.Add(detv);
            var det2 = det.DetailsList[0];
            Assert.AreEqual((decimal)600.13,det2.NettoBetragZeile);
            Assert.AreEqual((decimal)120.03,det2.MwStBetragZeile);
        }

        [Test]
        public void GetItemListTest()
        {

        }

        [Test]
        public void LoadTest()
        {
            _common.Invoice = InvoiceFactory.LoadTemplate(Common.InvTest);
            var det = DetailsListConverter.Load(_common.Invoice.Details.ItemList, _common.UContainer, false);
            Assert.IsNotNull(det);
            Assert.AreEqual((int)4, det.DetailsList.Count);
            Assert.AreEqual("DAY", det.DetailsList[0].Einheit);
            Assert.AreEqual("LS", det.DetailsList[2].Einheit);
        }

        [Test]
        public void GetItemLoadRabattTestOk()
        {
            _common.Invoice = InvoiceFactory.LoadTemplate(Common.InvTest);

            var det = DetailsListConverter.Load(_common.Invoice.Details.ItemList, _common.UContainer, false).DetailsList;
            var detList = DetailsListConverter.ConvertToItemList(det, "Z01");
            var det2 = DetailsListConverter.Load(detList, _common.UContainer, false).DetailsList;
            
            Assert.AreEqual(det[0].Rabatt,det2[0].Rabatt);
        }

        [Test]
        public void BestPosNotEmptyTestOk()
        {
            var detail = Cmn.UContainer.Resolve<DetailsViewModel>(new ParameterOverride("bestPosRequired",true));
            detail.ArtikelNr = "100";
            detail.Bezeichnung = "Testartikel";
            detail.Menge = 10;
            detail.Einheit = "STK";
            detail.EinzelPreis = 2000;
            detail.VatItem = PlugInSettings.Default.GetValueFromPercent(20);
            detail.BestellBezug = "100";
            detail.SaveCommand.Execute(null);
            Cmn.ListResults(detail.Results);
            Assert.IsTrue(detail.Results.IsValid);
        }
        [Test]
        public void BestPosEmptyTestNotOk()
        {
            var detail = Cmn.UContainer.Resolve<DetailsViewModel>(new ParameterOverride("bestPosRequired", true));
            detail.ArtikelNr = "100";
            detail.Bezeichnung = "Testartikel";
            detail.Menge = 10;
            detail.Einheit = "STK";
            detail.EinzelPreis = 2000;
            detail.VatItem = PlugInSettings.Default.GetValueFromPercent(20);
            detail.BestellBezug = "";
            detail.SaveCommand.Execute(null);
            Cmn.ListResults(detail.Results);
            Assert.IsTrue(!detail.Results.IsValid);
        }
        [Test]
        public void BestPosSpacesTestNotOk()
        {
            var detail = Cmn.UContainer.Resolve<DetailsViewModel>(new ParameterOverride("bestPosRequired", true));
            detail.ArtikelNr = "100";
            detail.Bezeichnung = "Testartikel";
            detail.Menge = 10;
            detail.Einheit = "STK";
            detail.EinzelPreis = 2000;
            detail.VatItem = PlugInSettings.Default.GetValueFromPercent(20);
            detail.BestellBezug = " ";
            detail.SaveCommand.Execute(null);
            Cmn.ListResults(detail.Results);
            Assert.IsTrue(!detail.Results.IsValid);
        }
        [Test]
        public void BestPosBundAlphaTestNotOk()
        {
            var detail = Cmn.UContainer.Resolve<DetailsViewModel>(new ParameterOverride("bestPosRequired", true));
            detail.ArtikelNr = "100";
            detail.Bezeichnung = "Testartikel";
            detail.Menge = 10;
            detail.Einheit = "STK";
            detail.EinzelPreis = 2000;
            detail.VatItem = PlugInSettings.Default.GetValueFromPercent(20);
            detail.BestellBezug = "Abcd";
            detail.SaveCommand.Execute(null);
            Cmn.ListResults(detail.Results);
            Assert.IsTrue(!detail.Results.IsValid);
        }
        [Test]
        public void BestPosWirtschaftAlphaTestOk()
        {
            var detail = Cmn.UContainer.Resolve<DetailsViewModel>(new ParameterOverride("bestPosRequired", false));
            detail.ArtikelNr = "100";
            detail.Bezeichnung = "Testartikel";
            detail.Menge = 10;
            detail.Einheit = "STK";
            detail.EinzelPreis = 2000;
            detail.VatItem = PlugInSettings.Default.GetValueFromPercent(20);
            detail.BestellBezug = "Abcd";
            detail.SaveCommand.Execute(null);
            Cmn.ListResults(detail.Results);
           Assert.IsTrue(detail.Results.IsValid);
        }
        
    }
}
