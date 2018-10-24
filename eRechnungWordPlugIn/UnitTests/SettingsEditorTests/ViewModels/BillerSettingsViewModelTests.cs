using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebICommonTestSetup;
using SettingsEditor.ViewModels;
using NUnit.Framework;
using Microsoft.Practices.Unity;
using SettingsManager;

namespace SettingsEditor.ViewModels.Tests
{
    [TestFixture]
    public class BillerSettingsViewModelTests : Common
    {
        [Test]
        public void IsValidTest()
        {

        }

        [Test]
        public void BillerSettingsViewModelTestNewOk()
        {
            var bsVm = UContainer.Resolve<BillerSettingsViewModel>();
            Assert.AreEqual("EUR",bsVm.CurrSelected.Code);
        }

        [Test]
        public void BicIbanOkTests()
        {
            var bsVm = UContainer.Resolve<BillerSettingsViewModel>();
            bsVm.Name = "Tester";
            bsVm.Strasse = "Verkäuferstrasse";
            bsVm.Ort = "Testort";
            bsVm.Plz = "4711";
            bsVm.VatId = PlugInSettings.VatIdDefaultMitVstBerechtigung;
            bool bsRc = bsVm.IsValid();
            Assert.IsTrue(bsRc, "IsValid nach Basis-Setup");
            bsVm.Bic = "VBWIATW1";
            bsVm.Iban = "AT611904300234573201";
            bsRc = bsVm.IsValid();
            ListResults(bsVm.Results);
            Assert.IsTrue(bsRc, "IsValid nach BIC und IBAN");
            bsVm.Bic = "";
            bsRc = bsVm.IsValid();
            ListResults(bsVm.Results);
            Assert.IsTrue(bsRc, "IsValid nach BIC ist leer");
            bsVm.Bic = "VBWIATW1";
            bsVm.Iban = "";
            bsRc = bsVm.IsValid();
            ListResults(bsVm.Results);
            Assert.IsTrue(bsRc, "IsValid nach IBAN ist leer");
        }
        [Test]
        public void BicIbanNotOkTests()
        {
            var bsVm = UContainer.Resolve<BillerSettingsViewModel>();
            bsVm.Name = "Tester";
            bsVm.Strasse = "Verkäuferstrasse";
            bsVm.Ort = "Testort";
            bsVm.Plz = "4711";
            bsVm.VatId = PlugInSettings.VatIdDefaultMitVstBerechtigung;
            bool bsRc = bsVm.IsValid();
            Assert.IsTrue(bsRc, "IsValid nach Basis-Setup");
            bsVm.Bic = "VBWI12121212ATW1";
            bsRc = bsVm.IsValid();
            ListResults(bsVm.Results);
            Assert.IsFalse(bsRc, "IsValid nach BIC falsch");
            bsVm.Iban = "AT5043000xx91919191919";
            bsRc = bsVm.IsValid();
            ListResults(bsVm.Results);
            Assert.IsFalse(bsRc, "IsValid nach BIC und Iban falsch");

        }
    }
}
