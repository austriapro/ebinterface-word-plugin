using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIServices.UidAbfrage;
using NUnit.Framework;
namespace ebIServices.UidAbfrage.Tests
{
    [TestFixture]
    public class UidErrorCodesTests
    {
        //[Test]
        //public void UidErrorCodesTest()
        //{
        //    var errorcodes = new UidErrorCodes();
        //    Assert.IsNotNull(errorcodes);
        //    Assert.IsTrue(errorcodes.Count>0);
        //}

        [Test]
        public void ErrorTextTest()
        {
            
            var msg = UidErrorCodes.ErrorText("3");
            Console.WriteLine(msg);
            Assert.AreEqual("UB10003 Die UID-Nummer des Antragstellers ist ungültig.",msg);
        }
    }
}
