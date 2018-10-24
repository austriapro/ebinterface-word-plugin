using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ebICommonTestSetup;
using ebIModels.Models;
using ebIViewModels.ViewModels.Tests;
using ebIViewModelsTests.ViewModels;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using SettingsEditor.ViewModels;
using SettingsManager;

namespace ebIViewModels.SettingsViewModels.Tests
{
    [TestFixture]
    public class BillerSettingsViewModelTests : CommonTestSetup
    {
        [Test]
        public void IsValidCurrencyTest()
        {
            PlugInSettings.Reset();
            BillerSettings = Cmn.UContainer.Resolve<BillerSettingsViewModel>();
            Assert.AreEqual("EUR", BillerSettings.CurrSelected.Code);
        }


        [Test]
        public void VatBerechtigtOkTest()
        {
            SetupSettings();
            BillerSettings.IsValid();
            Cmn.ListResults(BillerSettings.Results);
            Assert.AreEqual(true, BillerSettings.Results.IsValid);
        }
        [Test]
        public void VatBerechtigtNotOkTest()
        {
            SetupSettings();
            BillerSettings.VatId = "0000000";
            BillerSettings.IsValid();
            Cmn.ListResults(BillerSettings.Results);
            Assert.AreEqual(false, BillerSettings.Results.IsValid);
        }

        [Test]
        public void VatBerechtigtNotOkTest2()
        {
            SetupSettings();
            BillerSettings.VatId = PlugInSettings.VatIdDefaultOhneVstBerechtigung;
            BillerSettings.IsValid();
            Cmn.ListResults(BillerSettings.Results);
            Assert.AreEqual(false, BillerSettings.Results.IsValid);
        }

        [Test]
        public void VatNotBerechtigtOkTest()
        {
            SetupSettings();
            BillerSettings.VatId = PlugInSettings.VatIdDefaultOhneVstBerechtigung;
            BillerSettings.IsVatBerechtigt = false;
            BillerSettings.VatText = "ich bin nicht berechtigt";
            BillerSettings.IsValid();
            Cmn.ListResults(BillerSettings.Results);
            Assert.AreEqual(true, BillerSettings.Results.IsValid);
        }
        [Test]
        public void VatNotBerechtigtNotOkTest()
        {
            SetupSettings();
            BillerSettings.IsVatBerechtigt = false;
            BillerSettings.VatId = PlugInSettings.VatIdDefaultMitVstBerechtigung; // Muss für den Test hier stehen, da sonst überschrieben
            BillerSettings.VatText = "";
            BillerSettings.IsValid();
            Cmn.ListResults(BillerSettings.Results);
            Assert.AreEqual(false, BillerSettings.Results.IsValid);
            Assert.AreEqual(2, BillerSettings.Results.Count());
        }

    }
}
