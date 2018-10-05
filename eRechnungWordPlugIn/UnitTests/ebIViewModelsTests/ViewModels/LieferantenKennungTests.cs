using System.Linq;
using ebICommonTestSetup;
using ebIModels.Models;
using ebIViewModelsTests.ViewModels;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace ebIViewModels.ViewModels.Tests
{
    [TestFixture]
    public class LieferantenKennungTests : CommonTestSetup
    {
        private readonly Common _common = new Common(Common.InvTemplate);

        [Test]
        public void LieferantenKennungLeerTest()
        {
            InvoiceViewModel invoiceView = _common.UContainer.Resolve<InvoiceViewModel>(new ParameterOverride("invoice", _common.Invoice));
            invoiceView.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Industries; 
            //bool result = invoiceView.IsInvoiceValid();
            //Assert.AreEqual(true,result);
            invoiceView.VmLiefantenNr = "";
            bool result = invoiceView.IsInvoiceValid();
            Assert.AreEqual(true, result);
        }

        [Test]
        public void LieferantenKennungBundLeerTest()
        {
            InvoiceViewModel invoiceView = _common.UContainer.Resolve<InvoiceViewModel>();
            invoiceView.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Government;
            //bool result = invoiceView.IsInvoiceValid();
            //Assert.AreEqual(true, result);
            invoiceView.VmLiefantenNr = "";
            bool result = invoiceView.IsInvoiceValid();
            Assert.AreEqual(false, result);
        }

        [Test]
        public void LieferantenKennungBundOkTest()
        {
            InvoiceViewModel invoiceView = _common.UContainer.Resolve<InvoiceViewModel>(new ParameterOverride("invoice", _common.Invoice));
            invoiceView.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Government;
            //bool result = invoiceView.IsInvoiceValid();
            //Assert.AreEqual(true, result);
            invoiceView.VmLiefantenNr = string.Concat(Enumerable.Repeat("abcd", 5)); // 20 Zeichen ist OK
            bool result = invoiceView.IsInvoiceValid();
            Assert.AreEqual(true, result);
        }

        [Test]
        public void LieferantenKennungBundNotOkTest()
        {
            InvoiceViewModel invoiceView = _common.UContainer.Resolve<InvoiceViewModel>();
            invoiceView.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Government;
            //bool result = invoiceView.IsInvoiceValid();
            //Assert.AreEqual(true, result);
            invoiceView.VmLiefantenNr=string.Concat(Enumerable.Repeat("abcd",20));
            bool result = invoiceView.IsInvoiceValid();
            Assert.AreEqual(false, result);
        }
    }
}