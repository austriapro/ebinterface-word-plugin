using System;
using NUnit.Framework;
using ebICommonTestSetup;
using ebIModels.Models;
using ebIViewModels.ViewModels;
using Microsoft.Practices.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ebIViewModelsTests.ViewModels
{
    [TestFixture]
    public class KontoTests : CommonTestSetup
    {
        private void InitializeKontoTests()
        {
            InvVm.LoadTemplateCommand.Execute(Common.InvTemplate);
            // KOntofelder löschen
            InvVm.VmKtoBankName = "";
                InvVm.VmKtoBankName= "";
                InvVm.VmKtoBic= "";
                InvVm.VmKtoIban= "";
                InvVm.VmKtoOwner= "";
                InvVm.VmKtoReference = "";

        }
        [Test]
        public void KontoLeerGutschriftOkTests()
        {
            InitializeKontoTests();
            InvVm.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Government;
            InvVm.VmDocType = DocumentTypeType.CreditMemo.ToString();
            InvVm.IsInvoiceValid();
            Cmn.ListResults(InvVm.Results);
            Assert.IsTrue(InvVm.Results.IsValid);
        }

        [Test]
        public void KontoLeerStornoNotOkTests()
        {
            InitializeKontoTests(); 
            InvVm.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Government;
            InvVm.VmDocType = DocumentTypeType.Invoice.ToString();
            InvVm.RelatedDoc = Cmn.UContainer.Resolve<RelatedDocumentViewModel>();
            InvVm.RelatedDoc.RefSelectedDocType = DocumentTypeType.CreditMemo.ToString();
            InvVm.RelatedDoc.RefInvDate = new DateTime(2013, 12, 15);
            InvVm.RelatedDoc.RefInvNumber = "1234";
            InvVm.RelatedDoc.RefTypeSelected = RelatedDocumentViewModel.RefType.Storno;
            InvVm.IsInvoiceValid();
            Cmn.ListResults(InvVm.Results);
            Assert.IsFalse(InvVm.Results.IsValid);
        }

        [Test]
        public void KontoLeerStornoOkTests()
        {
            InitializeKontoTests();
            InvVm.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Government;
            InvVm.VmDocType = DocumentTypeType.CreditMemo.ToString();
            InvVm.RelatedDoc = Cmn.UContainer.Resolve<RelatedDocumentViewModel>();
            InvVm.RelatedDoc.RefSelectedDocType = DocumentTypeType.Invoice.ToString();
            InvVm.RelatedDoc.RefInvDate = new DateTime(2013, 12, 15);
            InvVm.RelatedDoc.RefInvNumber = "1234";
            InvVm.RelatedDoc.RefTypeSelected = RelatedDocumentViewModel.RefType.Storno;
            InvVm.IsInvoiceValid();
            Cmn.ListResults(InvVm.Results);
            Assert.IsTrue(InvVm.Results.IsValid);
        }
        [Test]
        public void KontoLeerNotOkTests()
        {
            InitializeKontoTests();
            InvVm.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Government;
            InvVm.VmDocType = DocumentTypeType.Invoice.ToString();
            InvVm.IsInvoiceValid();
            Cmn.ListResults(InvVm.Results);
            Assert.IsFalse(InvVm.Results.IsValid);
        }


    }
}
