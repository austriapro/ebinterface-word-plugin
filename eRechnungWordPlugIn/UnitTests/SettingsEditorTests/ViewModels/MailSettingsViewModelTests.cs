using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebICommonTestSetup;
using ebIServices.SendMail;
using ebIServices.SendMail.Tests;
using SettingsEditor.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SettingsManager;
using Microsoft.Practices.Unity;

namespace SettingsEditor.ViewModels.Tests
{
    [TestClass()]
    public class MailSettingsViewModelTests : Common
    {
        [TestMethod()]
        public void MailSettingsViewModelTestMailOk()
        {
            UContainer.RegisterType<ISendMailService, SendMailTestMock>("SendMailTest");
            var sndMail = UContainer.Resolve<ISendMailService>("SendMailTest");
            var mailSetting = UContainer.Resolve<MailSettingsViewModel>(new ParameterOverride("mailService", sndMail));
            mailSetting.IsValid();
            ListResults(mailSetting.Results);
            Assert.IsTrue(mailSetting.Results.IsValid);
            mailSetting.TestenCommand.Execute(null);
            Assert.IsTrue(string.IsNullOrEmpty(sndMail.SendTo));
        }

        [TestMethod()]
        public void SaveSettingsTtestOk()
        {
            UContainer.RegisterType<ISendMailService, SendMailTestMock>("SendMailTest");
            var sndMail = UContainer.Resolve<ISendMailService>("SendMailTest");
            var mailSetting = UContainer.Resolve<MailSettingsViewModel>(new ParameterOverride("mailService", sndMail));
            string testString = "Ihre Rechnung Nr. [RECHNUNGSNR] ausgestellt am [RECHNUNGSDATUM]";
            mailSetting.Subject = testString;
            mailSetting.SaveCommand.Execute(null);
            Assert.AreEqual(testString, PlugInSettings.Default.MailBetreff);
        }
    }
}
