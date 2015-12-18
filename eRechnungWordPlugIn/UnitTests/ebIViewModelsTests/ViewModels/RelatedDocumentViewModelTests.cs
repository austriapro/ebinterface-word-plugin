using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebICommonTestSetup;
using ebIModels.Models;
using System.Xml;
using System.Xml.Linq;
using ebIViewModels.ViewModels;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ebIViewModels.ViewModels.Tests
{
    [TestClass()]
    public class RelatedDocumentViewModelTests : Common
    {
        internal RelatedDocumentViewModel _rel;

        [TestMethod()]
        public void RelatedDocumentViewModelKeinSelectedTest()
        {
            _rel = UContainer.Resolve<RelatedDocumentViewModel>();
           bool res = _rel.IsValid();
            Assert.AreEqual(true,res);
        }

        [TestMethod()]
        public void RelatedDocumentViewModelStornoOkTest()
        {
            _rel = UContainer.Resolve<RelatedDocumentViewModel>();
            _rel.RefTypeSelected = RelatedDocumentViewModel.RefType.Storno;
            _rel.RefInvNumber = "123456";
            _rel.RefInvDate = DateTime.Today;
            bool res = _rel.IsValid();
            ListResults(_rel.Results);            
            Assert.AreEqual(true, res);
        }
        [TestMethod()]
        public void RelatedDocumentViewModelStornNotoOkTest()
        {
            _rel = UContainer.Resolve<RelatedDocumentViewModel>();
            _rel.RefTypeSelected = RelatedDocumentViewModel.RefType.Storno;
            _rel.RefInvNumber = "123456";
            bool res = _rel.IsValid();
            ListResults(_rel.Results);
            Assert.AreEqual(false, res);
            _rel = UContainer.Resolve<RelatedDocumentViewModel>();
            _rel.RefTypeSelected = RelatedDocumentViewModel.RefType.Storno;
            _rel.RefInvDate = DateTime.Today;
            res = _rel.IsValid();
            ListResults(_rel.Results);
            Assert.AreEqual(false, res);
            _rel = UContainer.Resolve<RelatedDocumentViewModel>();
            _rel.RefTypeSelected = RelatedDocumentViewModel.RefType.Storno;
            _rel.RefComment = ">".PadRight(300, '.');
            res = _rel.IsValid();
            ListResults(_rel.Results);
            Assert.AreEqual(false, res);
        }
        [TestMethod()]
        public void RelatedDocumentViewModelVerweisOkTest()
        {
            _rel = UContainer.Resolve<RelatedDocumentViewModel>();
            _rel.RefTypeSelected = RelatedDocumentViewModel.RefType.Verweis;            
            _rel.RefInvNumber = "123456";
            _rel.RefInvDate = DateTime.Today;
            bool res = _rel.IsValid();
            Assert.AreEqual(true, res);
        }
        [TestMethod]
        public void RelatedDocumentViewModelVerweisNoDateOkTest()
        {
            _rel = UContainer.Resolve<RelatedDocumentViewModel>();
            _rel.RefTypeSelected = RelatedDocumentViewModel.RefType.Verweis;
            _rel.RefInvNumber = "123456";
            bool res = _rel.IsValid();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void RelatedDocumentViewModelVerweisCommentNotOkTest()
        {
            _rel = UContainer.Resolve<RelatedDocumentViewModel>();
            _rel.RefTypeSelected = RelatedDocumentViewModel.RefType.Verweis;
            _rel.RefInvNumber = "123456";
            _rel.RefComment = ">".PadRight(300, '.');
            bool res = _rel.IsValid();
            ListResults(_rel.Results);
            Assert.AreEqual(false, res);
        }
        
        [TestMethod()]
        public void GetRelatedDocumentEntryCancelDocOkTest()
        {
            _rel = UContainer.Resolve<RelatedDocumentViewModel>();
            _rel.RefTypeSelected = RelatedDocumentViewModel.RefType.Storno;
            _rel.RefInvNumber = "123456";
            _rel.RefInvDate = DateTime.Today;
            bool res = _rel.IsValid();
            ListResults(_rel.Results);
            Assert.AreEqual(true, res);
            var result = _rel.GetRelatedDocumentEntry(InvoiceSubtypes.ValidationRuleSet.Government);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result,typeof(CancelledOriginalDocumentType));

        }
        [TestMethod()]
        public void GetRelatedDocumentEntryRelDocOkTest()
        {
            _rel = UContainer.Resolve<RelatedDocumentViewModel>();
            _rel.RefTypeSelected = RelatedDocumentViewModel.RefType.Verweis;
            _rel.RefInvNumber = "123456";
            _rel.RefInvDate = DateTime.Today;
            bool res = _rel.IsValid();
            ListResults(_rel.Results);
            Assert.AreEqual(true, res);
            var result = _rel.GetRelatedDocumentEntry(InvoiceSubtypes.ValidationRuleSet.Government);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RelatedDocumentType));

        }


    }
}
