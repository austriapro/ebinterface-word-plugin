using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using ebICommonTestSetup;
using ebIModels.Models;
using ebIViewModels.ViewModels;
using ebIViewModels.ViewModels.Tests;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using System.Xml.Linq;
using System.IO;

namespace ebIViewModelsTests.ViewModels
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
    public class AuftragsreferenzTests : CommonTestSetup
    {
        // private readonly Common Cmn = new Common();
        readonly string[] aRefBund = new string[] { 
        "4700000001",  // Bestellpos erforderlich
        "4700000001:Z01", // Bestellpos erforderlich
        "Z01", // ohne Bestellpos
        "Z01:111599-0099-V-3-2099", // ohne Bestellpos
        "L4/interne Referenz"
        };
        [Test]
        public void AuftragsReferenzWirtschaftOkTest()
        {
            Cmn.Setup(Common.InvTemplate); // Test mit Template anfangen
            InvoiceViewModel invoiceView =
              Cmn.UContainer.Resolve<InvoiceViewModel>(new ParameterOverride("invoice", Cmn.Invoice));
            invoiceView.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Industries;
            invoiceView.VmOrderReference = "Irgendetwas";
            bool result = invoiceView.IsInvoiceValid();
            Assert.AreEqual(true, result);
        }

        [Test]
        public void AuftragsReferenzWirtschaftLeerOkTest()
        {
            Cmn.Setup(Common.InvTemplate); // Test mit Template anfangen
            InvoiceViewModel invoiceView =
              Cmn.UContainer.Resolve<InvoiceViewModel>(new ParameterOverride("invoice", Cmn.Invoice));
            invoiceView.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Industries;
            invoiceView.VmOrderReference = "";
            bool result = invoiceView.IsInvoiceValid();
            Cmn.ListResults(invoiceView.Results);
            Assert.AreEqual(true, result);

        }
        [Test]
        public void AuftragsReferenzBundOhneBestposOkTest()
        {
            Cmn.Setup(Common.InvTemplate); // Test mit Template anfangen
            InvoiceViewModel invoiceView =
              Cmn.UContainer.Resolve<InvoiceViewModel>(new ParameterOverride("invoice", Cmn.Invoice));
            invoiceView.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Government;
            invoiceView.VmOrderReference = aRefBund[2];
            bool result = invoiceView.IsInvoiceValid();
            Cmn.ListResults(invoiceView.Results);
            Assert.AreEqual(true, result,string.Format("Auftr.Ref={0}",invoiceView.VmOrderReference));
            invoiceView.VmOrderReference = aRefBund[3];
            result = invoiceView.IsInvoiceValid();
            Cmn.ListResults(invoiceView.Results);
            Assert.AreEqual(true, result, string.Format("Auftr.Ref={0}", invoiceView.VmOrderReference));
            invoiceView.VmOrderReference = aRefBund[4];
            result = invoiceView.IsInvoiceValid();
            Cmn.ListResults(invoiceView.Results);
            Assert.AreEqual(true, result, string.Format("Auftr.Ref={0}", invoiceView.VmOrderReference));
        }
        [Test]
        public void AuftragsReferenzBundMitBestposOkTest()
        {
            Cmn.Setup(Common.InvTemplate); // Test mit Template anfangen
            InvoiceViewModel invoiceView =
              Cmn.UContainer.Resolve<InvoiceViewModel>(new ParameterOverride("invoice", Cmn.Invoice));
            invoiceView.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Government;
            int bestPos = 100;
            List<DetailsViewModel> detailsView = new List<DetailsViewModel>();
            foreach (DetailsViewModel model in invoiceView.DetailsView)
            {
                DetailsViewModel dModel = model;
                dModel.BestellBezug = string.Format("{0}", bestPos);
                bestPos++;
                detailsView.Add(dModel);
            }
            invoiceView.DetailsView = new BindingList<DetailsViewModel>(detailsView);
            Console.WriteLine("Test mit " + aRefBund[0]);
            invoiceView.VmOrderReference = aRefBund[0];
            bool result = invoiceView.IsInvoiceValid();
            Cmn.ListResults(invoiceView.Results);
            Assert.AreEqual(true, result);
            Console.WriteLine("Test mit " + aRefBund[1]);
            invoiceView.VmOrderReference = aRefBund[1];
            result = invoiceView.IsInvoiceValid();
            Cmn.ListResults(invoiceView.Results);
            Assert.AreEqual(true, result);
            const string fnBestPos = @"Daten\BestPosTest.xml";
            invoiceView.SaveEbinterfaceCommand.Execute(fnBestPos);
            XDocument xdoc = XDocument.Load(fnBestPos);
            var res = Cmn.GetElement(xdoc, "Details");
            Assert.IsNotNull(res);
        }
        [Test]
        public void AuftragsReferenzBundBestposFehltTest()
        {
            Cmn.Setup(Common.InvTemplate); // Test mit Template anfangen
            InvoiceViewModel invoiceView =
              Cmn.UContainer.Resolve<InvoiceViewModel>(new ParameterOverride("invoice", Cmn.Invoice));
            invoiceView.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Government;
            invoiceView.VmOrderReference = aRefBund[0];
            List<DetailsViewModel> models = new List<DetailsViewModel>();
            DetailsViewModel model = invoiceView.DetailsView.FirstOrDefault();
            model.BestellBezug = "";
            models.Add(model);
            invoiceView.DetailsView = new BindingList<DetailsViewModel>(models);
            bool result = invoiceView.IsInvoiceValid();
            Assert.AreEqual(false, result);
            invoiceView.VmOrderReference = aRefBund[1];
            result = invoiceView.IsInvoiceValid();
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AuftragsReferenzBundLeerNotOkTest()
        {
            Cmn.Setup(Common.InvTemplate); // Test mit Template anfangen
            InvoiceViewModel invoiceView =
              Cmn.UContainer.Resolve<InvoiceViewModel>(new ParameterOverride("invoice", Cmn.Invoice));
            invoiceView.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Government;
            invoiceView.VmOrderReference = "";
            bool result = invoiceView.IsInvoiceValid();
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AuftragsReferenzBundLeerRefillOkTest()
        {
            Cmn.Setup(Common.InvTemplate); // Test mit Template anfangen
            InvoiceViewModel invoiceView =
              Cmn.UContainer.Resolve<InvoiceViewModel>(new ParameterOverride("invoice", Cmn.Invoice));
            invoiceView.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Government;
            invoiceView.VmOrderReference = "";
            bool result = invoiceView.IsInvoiceValid();
            Assert.AreEqual(false, result);
            invoiceView.VmOrderReference = "z01";
            Assert.AreEqual("Z01", Cmn.Invoice.InvoiceRecipient.OrderReference.OrderID);
        }

        [Test]
        public void IsBestPosRequiredLoadTemplateTest()
        {
            const string fn = @"Daten\Bestellposnr.xml";
            InvVm.LoadTemplateCommand.Execute(fn);
            Assert.IsTrue(InvVm.IsBestPosRequired);
        }
        [Test]
        public void IsBestPosRequiredOrderRefChangedNotFalseTestOK()
        {
            const string fn = @"Daten\Bestellposnr.xml";
            InvVm.LoadTemplateCommand.Execute(fn);
            InvVm.VmOrderReference = "Z01";
            Assert.IsTrue(!InvVm.IsBestPosRequired);

        }
    }
}
