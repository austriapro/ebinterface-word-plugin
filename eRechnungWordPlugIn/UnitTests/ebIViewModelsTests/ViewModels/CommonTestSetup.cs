using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using ebIViewModels.ErrorView;
using ebIViewModels.ViewModels;
using ebIViewModels.ViewModels.Tests;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ebICommonTestSetup;
using SettingsEditor.ViewModels;

namespace ebIViewModelsTests.ViewModels
{
    [TestClass]
    public class CommonTestSetup
    {
        internal Common Cmn = new Common(Common.InvTemplate);
        internal InvoiceViewModel InvVm;
        internal ErrorActionPaneViewModel ErrorActionPane;
        private string _testData = @"C:\TFS\eRechnung-Business\Testdaten\Billersettings.xml";
        internal BillerSettingsViewModel BillerSettings; // 
        internal XmlNamespaceManager Nspc = new XmlNamespaceManager(new NameTable());

        public CommonTestSetup()
        {
            InitGlobals();
        }

        [TestInitialize]
        public void InitGlobals()
        {
            InvVm = Cmn.UContainer.Resolve<InvoiceViewModel>(new ParameterOverride("invoice", Cmn.Invoice));
            InvVm.NoUpdatePrompt = true;
            ErrorActionPane = Cmn.UContainer.Resolve<ErrorActionPaneViewModel>();
            Nspc.AddNamespace("eb", "http://www.ebinterface.at/schema/4p2/");
        }

        internal void SetupSettings()
        {
            BillerSettings = Cmn.UContainer.Resolve<BillerSettingsViewModel>();
            string xmlData = File.ReadAllText(_testData);
            XElement xEl = XElement.Parse(xmlData);
            var o1 = xEl.Element("Name").Value;
            BillerSettings.Name = xEl.Element("Name").Value;
            BillerSettings.Strasse = xEl.Element("Strasse").Value;
            BillerSettings.Plz = xEl.Element("Plz").Value;
            BillerSettings.Ort = xEl.Element("Ort").Value;
            BillerSettings.Phone = xEl.Element("Phone").Value;
            BillerSettings.Kontakt = xEl.Element("Kontakt").Value;
            BillerSettings.Email = xEl.Element("Email").Value;
            BillerSettings.VatId = xEl.Element("VatId").Value;
            BillerSettings.Gln = xEl.Element("Gln").Value;
            BillerSettings.VatText = xEl.Element("VatText").Value;
            BillerSettings.Bank = xEl.Element("Bank").Value;
            BillerSettings.Inhaber = xEl.Element("Inhaber").Value;
            BillerSettings.Iban = xEl.Element("Iban").Value;
            BillerSettings.Bic = xEl.Element("Bic").Value;
            var countryCode = xEl.Element("CountryCode").Value;
            BillerSettings.CountryCodeSelected = BillerSettings.CountryCodes.Find(p => p.Code == countryCode);

            var vatSelected = decimal.Parse(xEl.Element("VatSelected").Value);
            BillerSettings.VatSelected = BillerSettings.VatDefaultList.Find(p => p.MwStSatz == vatSelected);
            var currency = xEl.Element("Currency").Value;
            BillerSettings.CurrSelected = BillerSettings.CurrencyList.FirstOrDefault(p => p.Code == currency);
            BillerSettings.IsVatBerechtigt = bool.Parse(xEl.Element("IsVatBerechtigt").Value);
        }

        public void ListErrorPanel()
        {
            foreach (ErrorViewModel model in ErrorActionPane.ErrorList)
            {
                Console.WriteLine(model.FieldName + "-" + model.Description);
            }
        }
    }
}
