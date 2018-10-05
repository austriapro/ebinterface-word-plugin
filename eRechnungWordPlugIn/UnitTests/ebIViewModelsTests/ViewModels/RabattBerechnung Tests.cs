using System;
using ebIViewModels.ViewModels;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using SettingsManager;

namespace ebIViewModelsTests.ViewModels
{
    [TestFixture]
    public class RabattBerechnungTests : CommonTestSetup
    {
        [Test]
        public void RabattBerechnungTestOk()
        {
            DetailsViewModel dView = Cmn.UContainer.Resolve<DetailsViewModel>(new ParameterOverrides() { 
            { "bestPosRequired", false },
            {"currentRuleSet",InvVm.CurrentSelectedValidation}
            });
            dView.ArtikelNr = "1001";
            dView.Bezeichnung = "Mister Blister";
            dView.Menge = new decimal(10.0005);
            dView.EinzelPreis = new decimal(5.00);
            dView.VatItem= PlugInSettings.Default.MwStDefaultValue;
            dView.Einheit = "STK";
            dView.Rabatt = 10;
            dView.BestellBezug = "22";
            dView.SaveCommand.Execute(null);
            Cmn.ListResults(dView.Results);
            Assert.IsTrue(dView.Results.IsValid);
            DetailsViewModels dModels = Cmn.UContainer.Resolve<DetailsViewModels>(new ParameterOverrides() { 
            { "bestPosRequired", false },
            {"currentRuleSet",InvVm.CurrentSelectedValidation}
            });
            dModels.DetailsViewList.Add(dView);
            InvVm.DetailsView = dModels.DetailsViewList;
            InvVm.IsInvoiceValid();
            Cmn.ListResults(InvVm.Results);
            Assert.IsTrue(InvVm.Results.IsValid);
            InvVm.SaveEbinterfaceCommand.Execute(@"Daten\RabattSaved.xml");
        }

        [Test]
        public void RabattSaveAndReloadTest()
        {
            const string RabattSaveReload = @"Daten\RabattSaveReload.xml";
            InvVm.SaveEbinterfaceCommand.Execute(RabattSaveReload);
            var inv2 = Cmn.UContainer.Resolve<InvoiceViewModel>();
            inv2.LoadTemplateCommand.Execute(RabattSaveReload);
            Assert.AreEqual(InvVm.DetailsView[0].Rabatt, inv2.DetailsView[0].Rabatt);
        }
    }
}
