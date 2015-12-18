using System;
using System.Collections.Generic;
using System.ComponentModel;
using ebICommonTestSetup;
using ebIModels.Models;
using ebIViewModels.ViewModels;
using ebIViewModels.ViewModels.Tests;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SettingsManager;

namespace ebIViewModelsTests.ViewModels
{
    [TestClass]
    public class UIDNrTests : CommonTestSetup
    {
       // private readonly Common Cmn = new Common();
        const string UidFile = @"Daten\UidTest.xml";
        [TestMethod]
        public void UIDNrValidSimpleTests()
        {
            Cmn.Setup(Common.InvTemplate);   // Test mit Template anfangen
            InvoiceViewModel invoiceView = Cmn.UContainer.Resolve<InvoiceViewModel>(new ParameterOverride("invoice", Cmn.Invoice));
            invoiceView.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Industries;
            invoiceView.VmRecVatid = PlugInSettings.VatIdDefaultMitVstBerechtigung;
            bool result = invoiceView.IsInvoiceValid();
            Assert.AreEqual(true, result, "VatIdDefaultMitVstBerechtigung");
            invoiceView.SaveEbinterfaceCommand.Execute(UidFile);
            Cmn.ListResults(invoiceView.Results);
            Assert.IsTrue(invoiceView.Results.IsValid, "IsInvoiceValid nach Save ebInterface");
            invoiceView.VmRecVatid = PlugInSettings.VatIdDefaultOhneVstBerechtigung;
            result = invoiceView.IsInvoiceValid();
            Cmn.ListResults(invoiceView.Results);
            Assert.IsTrue(result, "IsInvoiceValid vor Save ebInterface");
            invoiceView.VmBillerVatid = "ABSCD"; // False UstId
            result = invoiceView.IsInvoiceValid();
            Cmn.ListResults(invoiceView.Results);
            Assert.IsFalse(result, "UStId falsch");
        }

        [TestMethod]
        public void UIDNrMissingBelow10000OkTest()
        {
            Cmn.Setup(Common.InvTemplate);   // Test mit Template anfangen
            InvoiceViewModel invoiceView = Cmn.UContainer.Resolve<InvoiceViewModel>(new ParameterOverride("invoice", Cmn.Invoice));
            invoiceView.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Industries;
            invoiceView.VmRecVatid = "";
            bool result = invoiceView.IsInvoiceValid();
            Cmn.ListResults(invoiceView.Results);
            Assert.IsFalse(result, "IsInvoiceValid vor Save ebInterface");
        }

        [TestMethod]
        public void UIDNrMissingAbove10000NotOkTest()
        {
            Cmn.Setup(Common.InvTemplate);   // Test mit Template anfangen
            InvoiceViewModel invoiceView = Cmn.UContainer.Resolve<InvoiceViewModel>(new ParameterOverride("invoice", Cmn.Invoice));
            invoiceView.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Industries;
            invoiceView.VmRecVatid = "";
            List<DetailsViewModel> detailsView = new List<DetailsViewModel>();
            DetailsViewModel dModel = Cmn.UContainer.Resolve<DetailsViewModel>(new ParameterOverride("bestPosRequired",invoiceView.IsBestPosRequired));
            dModel.Menge = 10;
            dModel.Einheit = "STK";
            dModel.EinzelPreis = 2000;
            dModel.VatSatz = 20;
            dModel.Bezeichnung = "Musterartikel";
            detailsView.Add(dModel);
            invoiceView.DetailsView = new BindingList<DetailsViewModel>(detailsView);
            Assert.AreEqual("20.000,00",invoiceView.VmInvTotalNetAmount);
            bool result = invoiceView.IsInvoiceValid();
            Cmn.ListResults(invoiceView.Results);
            Assert.IsFalse(result, "IsInvoiceValid vor Save ebInterface");
        }

    }
}
