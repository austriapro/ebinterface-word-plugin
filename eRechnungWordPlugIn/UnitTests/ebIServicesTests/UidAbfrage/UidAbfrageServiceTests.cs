using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebICommonTestSetup;
using ebIServices.UidAbfrage;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Rhino.Mocks;

namespace ebIServices.UidAbfrage.Tests
{
    [TestFixture]
    public class UidAbfrageServiceTests : Common
    {
#if DEBUG
        private IUidAbfrageDienst _abfr;
        private string _webFile = @"C:\Util\BmfWebQuery.txt";
        private string _tln;
        private string _uid;
        private string _pin;
        private string _billerUid;
        private string _okUid = "LU21025032";
        private string _notOkUid = "Lx21025032";
        [SetUp]
        public void UidAbfrageServiceTestsInit()
        {
            
           var cred = (File.ReadAllText(_webFile)+";").Split(';');
            _tln = cred[0];
            _uid = cred[1];
            _pin = cred[2];
            _billerUid = cred[3];

            // Mit dieser Zeile wird die Abfrage wirklich durchgeführt
            // _abfr = UContainer.Resolve<IUidAbfrageDienst>();

            // Mock
            _abfr = MockRepository.GenerateMock<IUidAbfrageDienst>();
            _abfr.Stub(x => x.Login(_pin, _tln, _uid)).Return(true);
            _abfr.Stub(x => x.Message).Return("Message");
            _abfr.Stub(x => x.UidAbfrage(_okUid, _billerUid)).Return(true);
            _abfr.Stub(x => x.UidAbfrage(_notOkUid, _billerUid)).Return(false);

        }
        [Test]
        public void LoginTest()
        {
            var res = _abfr.Login(_pin, _tln, _uid);
            Assert.IsTrue(res);
        }

        [Test]
        public void LogoutTest()
        {
            var res = _abfr.Login(_pin, _tln, _uid);
            Assert.IsTrue(res);
            _abfr.Logout();
        }

        [Test]
        public void UidAbfrageTestOk()
        {
            var res = _abfr.Login(_pin, _tln, _uid);
            Assert.IsTrue(res);
            // var res2 = _abfr.UidAbfrage("ATU54047801", _billerUid);
            var res2 = _abfr.UidAbfrage(_okUid, _billerUid);
            Console.WriteLine(_abfr.Message);
            Assert.IsTrue(res2);            
            _abfr.Logout();

        }

        [Test]
        public void UidAbfrageTestNotOk()
        {
            var res = _abfr.Login(_pin, _tln, _uid);
            Assert.IsTrue(res);
            // var res2 = _abfr.UidAbfrage("ATU54047801", _billerUid);
            var res2 = _abfr.UidAbfrage(_notOkUid, _billerUid);
            Console.WriteLine(_abfr.Message);
            Assert.IsTrue(!res2);
            _abfr.Logout();

        }
#endif
    }
}
