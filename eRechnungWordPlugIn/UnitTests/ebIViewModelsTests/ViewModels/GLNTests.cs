using System;
using ebICommonTestSetup;
using ebIModels.Models;
using ebIViewModels.ViewModels;
using ebIViewModels.ViewModels.Tests;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;


namespace ebIViewModelsTests.ViewModels
{
    [TestFixture]
    public class GLNTests : CommonTestSetup
    {
      //  private readonly Common _common = new Common(Common.InvTemplate);

        [Test]
        public void GLNValidTest()
        {
            // 9099999303132
            InvoiceViewModel invoiceView = Cmn.UContainer.Resolve<InvoiceViewModel>(new ParameterOverride("invoice", Cmn.Invoice));
            invoiceView.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Industries;
            invoiceView.VmRecGln = "9099999303132";
            invoiceView.VmBillerGln= "9099999303132";
            bool result = invoiceView.IsInvoiceValid();
            Assert.AreEqual(true, result);

        }
        [Test]
        public void GLNBundInvalidTest()
        {
            // 9099999303132
            InvoiceViewModel invoiceView = Cmn.UContainer.Resolve<InvoiceViewModel>(new ParameterOverride("invoice", Cmn.Invoice));
            invoiceView.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Government;
            invoiceView.VmRecGln = "909999031324";
            invoiceView.VmBillerGln = "909999031324";
            bool result = invoiceView.IsInvoiceValid();
            // ListErrorPanel();
            Cmn.ListResults(invoiceView.Results);
            Assert.IsFalse(invoiceView.Results.IsValid);
        }
        [Test]
        public void GlnZuKurzNotOkTest()
        {
            InvoiceViewModel invoiceView = Cmn.UContainer.Resolve<InvoiceViewModel>(new ParameterOverride("invoice", Cmn.Invoice));
            invoiceView.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Industries;
            invoiceView.VmRecGln = "123";
            bool result = invoiceView.IsInvoiceValid();
            Assert.AreEqual(false, result);

        }

        internal const string SampleGln = "9099999303132";
        private const string BillerGlnPath = "/eb:Invoice/eb:Biller/eb:Address/eb:AddressIdentifier";
        const string GlnSaveAndLoadFile = @"Daten\GlnSaveAndLoadFile.xml";

        [Test]
        public void GlnSaveAndLoadTest()
        {
            InvVm.VmBillerGln = SampleGln;
            InvVm.SaveTemplateCommand.Execute(GlnSaveAndLoadFile);
            XDocument xdoc = XDocument.Load(GlnSaveAndLoadFile);
            var nspm = new XmlNamespaceManager(new NameTable());
            nspm.AddNamespace("eb", "http://www.ebinterface.at/schema/4p2/");
            var gln = xdoc.XPathSelectElement(BillerGlnPath, nspm).Value;
            Assert.AreEqual(SampleGln, gln,"GLN aus XML File");

            InvVm.LoadTemplateCommand.Execute(Common.InvTest);
            Assert.AreEqual("", InvVm.VmBillerGln,"GLN nach Load Template ohne GLN");

            InvVm.LoadTemplateCommand.Execute(GlnSaveAndLoadFile);
            Assert.AreEqual(SampleGln, InvVm.VmBillerGln,"GLN nach Load Template 4p1 mit GLN");
        }
    }
}
