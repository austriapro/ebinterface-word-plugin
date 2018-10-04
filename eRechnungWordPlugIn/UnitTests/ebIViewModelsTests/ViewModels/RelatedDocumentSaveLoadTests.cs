using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using ebIModels.Models;
using ebIViewModels.ViewModels;
using ebIViewModels.ViewModels.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ebIViewModelsTests.ViewModels
{
    [TestClass]
    public class RelatedDocumentSaveLoadTests : CommonTestSetup
    {
        const string SaveTempCancelled = @"Daten\RelatedDocCancelled.xml";
        const string SaveTempCancelledSample = @"Daten\RelatedDocCancelledSample.xml";
        const string SaveTempNoCancelledElement = @"Daten\RelatedDocNotCancelled.xml";
        const string SaveTempRelated = @"Daten\RelatedDoc.xml";
        const string SaveEmptyCommentRelated = @"Daten\RelatedDocSaveEmptyComment.xml";
        const string RelatedSample = @"Daten\RelatedDocSample.xml";
        private XNamespace ns = @"http://www.ebinterface.at/schema/4p2/";

        private XElement GetElement(XDocument xdoc, string xName)
        {

            IEnumerable<XElement> xels = xdoc.Descendants();
            var xel = xels.FirstOrDefault(x =>x.Name.NamespaceName==ns.NamespaceName && x.Name.LocalName == xName);
            return xel;
        }
        [TestMethod]
        public void SaveNoCancelledDocTestOk()
        {
            InvVm.SaveTemplateCommand.Execute(SaveTempNoCancelledElement);
            XDocument xDoc = XDocument.Load(SaveTempNoCancelledElement);
            XElement xel = GetElement(xDoc, "InvoiceNumber");
            Assert.IsNotNull(xel);
            // Assert.AreNotEqual(0, xel.);
        }


        [TestMethod]
        public void SaveCancelledDocTestOk()
        {
            SaveCancelledDoc();
            XDocument xDoc = XDocument.Load(SaveTempCancelled);
            XElement xel = GetElement(xDoc, "CancelledOriginalDocument");
            Assert.IsNotNull(xel);
           // Assert.AreNotEqual(0,xel.Count);
        }

        [TestMethod]
        public void SaveRelatedDocTestOk()
        {
            SaveRelatedDoc("REL11111",new DateTime(2013,11,12),"Teilrechnung von damals",DocumentTypeType.InvoiceForPartialDelivery);
            XDocument xDoc = XDocument.Load(SaveTempRelated);
            XElement xel = GetElement(xDoc, "RelatedDocument");
            Assert.IsNotNull(xel);
            // Assert.AreNotEqual(0,xel.Count);
        }
        [TestMethod]
        public void RelatedDocValidTestOk()
        {
            InvVm.VmDocType = DocumentTypeType.CreditMemo.ToString();
            SaveCancelledDoc();
            InvVm.IsInvoiceValid();
            Assert.AreEqual(true,InvVm.Results.IsValid);
        }
        [TestMethod]
        public void RelatedDocInvalidInvoiceDocTypeTestOk()
        {
            InvVm.VmDocType = DocumentTypeType.FinalSettlement.ToString();
            SetRelatedDocument(RelatedDocumentViewModel.RefType.Storno, DocumentTypeType.InvoiceForAdvancePayment);            
            InvVm.IsInvoiceValid();
            Cmn.ListResults(InvVm.Results);
            Assert.AreEqual(false, InvVm.Results.IsValid);
        }
        [TestMethod]
        public void RelatedDocInvalidRelDocTypeTestOk()
        {
            InvVm.VmDocType = DocumentTypeType.CreditMemo.ToString();
            SetRelatedDocument(RelatedDocumentViewModel.RefType.Storno, DocumentTypeType.CreditMemo);
            InvVm.IsInvoiceValid();
            Cmn.ListResults(InvVm.Results);
            Assert.AreEqual(false, InvVm.Results.IsValid);
        }

        [TestMethod]
        public void LoadCancelledDocTestOk()
        {
            InvVm.LoadTemplateCommand.Execute(SaveTempCancelledSample);
            Assert.AreEqual(DocumentTypeType.FinalSettlement.ToString(),InvVm.RelatedDoc.RefSelectedDocType);
            Assert.AreEqual("STORNO123123",InvVm.RelatedDoc.RefInvNumber);
        }
        [TestMethod]
        public void LoadRelatedDocTestOk()
        {
            InvVm.LoadTemplateCommand.Execute(RelatedSample);
            Assert.AreEqual(DocumentTypeType.InvoiceForPartialDelivery.ToString(), InvVm.RelatedDoc.RefSelectedDocType);
            Assert.AreEqual("REL11111", InvVm.RelatedDoc.RefInvNumber);
        }
        [TestMethod]
        public void SaveAndLoadEmptyCommentRelatedDocTestOk()
        {
            SaveRelatedDoc("REL11111", new DateTime(2013, 11, 12), "", DocumentTypeType.InvoiceForPartialDelivery);
            XDocument xDoc = XDocument.Load(SaveTempRelated);
            XElement xel = GetElement(xDoc, "RelatedDocument");
            Assert.IsNotNull(xel,"Related Doc exists in XML");
            // Assert.AreNotEqual(0,xel.Count);
            InvVm.LoadTemplateCommand.Execute(SaveTempRelated);
            InvVm.IsInvoiceValid();
            Cmn.ListResults(InvVm.Results);
            Assert.IsTrue(InvVm.Results.IsValid,"Invoice is valid");

        }

        private void SaveCancelledDoc()
        {
            SetRelatedDocument(RelatedDocumentViewModel.RefType.Storno,DocumentTypeType.FinalSettlement);
            InvVm.SaveTemplateCommand.Execute(SaveTempCancelled);
        }

        private void SetRelatedDocument(RelatedDocumentViewModel.RefType typ,DocumentTypeType docTyp)
        {
            InvVm.RelatedDoc.RefTypeSelected = typ;
            InvVm.RelatedDoc.RefInvNumber = "STORNO123123";
            InvVm.RelatedDoc.RefInvDate = new DateTime(2014, 1, 20);
            InvVm.RelatedDoc.RefSelectedDocType = docTyp.ToString();
            InvVm.RelatedDoc.RefComment = "Diese Schlussrechnung war leider falsch";
        }

        private void SaveRelatedDoc(string invNr, DateTime date, string comment,DocumentTypeType dType)
        {
            InvVm.RelatedDoc.RefTypeSelected = RelatedDocumentViewModel.RefType.Verweis;
            InvVm.RelatedDoc.RefInvNumber = invNr;
            InvVm.RelatedDoc.RefInvDate = date;
            InvVm.RelatedDoc.RefSelectedDocType = dType.ToString();
            InvVm.RelatedDoc.RefComment = comment;
            InvVm.SaveTemplateCommand.Execute(SaveTempRelated);
        }
    }
}
