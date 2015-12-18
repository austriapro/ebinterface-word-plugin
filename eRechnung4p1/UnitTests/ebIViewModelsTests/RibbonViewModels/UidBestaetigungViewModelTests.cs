using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIServices.UidAbfrage;
using ebIViewModels.ErrorView;
using ebIViewModels.RibbonViewModels;
using ebIViewModels.ViewModels;
using ebIViewModelsTests.ViewModels;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ebIViewModels.RibbonViewModels.Tests
{
    [TestClass()]
    public class UidBestaetigungViewModelTests : CommonTestSetup
    {
        [TestMethod()]
        public void UidBestaetigungViewModelTest()
        {
            InvVm.VmBillerVatid = "ATU62698637";
            InvVm.VmRecVatid = "ATU00000000";
            var uidBest = Cmn.UContainer.Resolve<UidBestaetigungViewModel>(new ParameterOverride("invoiceView", InvVm));
            Assert.AreEqual("ATU62698637", uidBest.BillerUid);
            Assert.AreEqual("ATU00000000", uidBest.ReceiverUid);
        }

        [TestMethod()]
        public void IsValidValidationTestOk()
        {
            InvVm.VmBillerVatid = "ATU62698637";
            InvVm.VmRecVatid = "ATU00000000";
            var uidBest = Cmn.UContainer.Resolve<UidBestaetigungViewModel>(new ParameterOverride("invoiceView", InvVm));
            uidBest.TeilNehmerId = "123456";
            uidBest.BenutzerId = "abcde";
            uidBest.Pin = "98765";
            var result = uidBest.IsValid();
            Assert.AreEqual(true, result);
        }
        [TestMethod()]
        public void IsValidValidationTestNotOk()
        {
            InvVm.VmBillerVatid = "ATUbcd62698637";
            InvVm.VmRecVatid = "00000000";
            var uidBest = Cmn.UContainer.Resolve<UidBestaetigungViewModel>(new ParameterOverride("invoiceView", InvVm));
            uidBest.TeilNehmerId = "";
            uidBest.BenutzerId = "";
            uidBest.Pin = "";
            var result = uidBest.IsValid();
            Cmn.ListResults(uidBest.Results);
            Assert.AreEqual(5, uidBest.Results.Count);
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void DoCheckingTestOk()
        {
            UidAbfrageServiceSetup();
            InvVm.VmBillerVatid = _billerUid;
            InvVm.VmRecVatid = "ATU54047801";
            var uidBest = Cmn.UContainer.Resolve<UidBestaetigungViewModel>(new ParameterOverride("invoiceView", InvVm));
            uidBest.TeilNehmerId = _tln;
            uidBest.BenutzerId = _uid;
            uidBest.Pin = _pin;
            uidBest.BestaetigenCommand.Execute(null);
            ListErrorPanel();
            Assert.AreEqual(true, uidBest.IsUidValid);
        }

        private string _webFile = @"C:\Util\BmfWebQuery.txt";
        private string _tln;
        private string _uid;
        private string _pin;
        private string _billerUid;
        // "ATU54047801"; LU21025032
        public void UidAbfrageServiceSetup()
        {
            var cred = (File.ReadAllText(_webFile) + ";").Split(';');
            _tln = cred[0];
            _uid = cred[1];
            _pin = cred[2];
            _billerUid = cred[3];

        }
    }
}
