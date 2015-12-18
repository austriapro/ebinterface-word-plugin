﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebICommonTestSetup;
using ebIServices.SendMail;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ebIServices.SendMail.Tests
{
    [TestClass()]
    public class SendMailServiceTests : Common
    {
        [TestMethod()]
        public void SendMailTestOk()
        {
            UContainer.RegisterType<ISendMailService, SendMailTestMock>("SendMailTest");
            var sndMail = UContainer.Resolve<ISendMailService>("SendMailTest");
            sndMail.MailBody = "Servus";
            sndMail.PdfFileName = null;
            sndMail.SendTo = "jbogad@hotmail.com";
            sndMail.Subject = "Nachricht aus Unit-Test";
            sndMail.XmlFilename = null;
            
            sndMail.SendMail(); 
            Assert.IsNotNull(sndMail);
        }
    }
}
