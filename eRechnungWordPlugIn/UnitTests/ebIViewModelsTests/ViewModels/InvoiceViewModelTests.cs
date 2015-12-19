using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using ebIModels.Models;
using ebIModels.Schema;
using ebIServices.SendMail;
using ebIViewModels.ErrorView;
using ebIViewModels.RibbonViewModels;
using ebIViewModels.RibbonViews;
using ebIViewModels.Services;
using ebIViewModels.ViewModels;
using ebIViewModels.Views;
using ebIViewModelsTests.ViewModels;
using ebICommonTestSetup;
using eRechnung;
using EventBrokerExtension;
using ExtensionMethods;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SettingsEditor.ViewModels;
using SettingsManager;
using WinFormsMvvm.DialogService;
using WinFormsMvvm.DialogService.FrameworkDialogs.FolderBrowse;
using WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile;
using WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile;

namespace ebIViewModels.ViewModels.Tests
{
    [TestClass()]
    public class InvoiceViewModelTests : CommonTestSetup
    {
        private const string EmptyInvoice = @"Daten\EmptyInvoiceTemplate.xml";
        private const string DeliveryDateInvoice = @"Daten\DeliveryDate.xml";
        private const string DeliveryPeriodInvoice = @"Daten\DeliveryPeriod.xml";

        private const string BillerCountry = "/eb:Invoice/eb:Biller/eb:Address/eb:Country";
        private const string BillerName = "/eb:Invoice/eb:Biller/eb:Address/eb:Name";
        private const string DeliveryDateXPath = "/eb:Invoice/eb:Delivery/eb:Date";
        private const string DeliveryFromDateXPath = "/eb:Invoice/eb:Delivery/eb:Period/eb:FromDate";
        private const string DeliveryToDateXPath = "/eb:Invoice/eb:Delivery/eb:Period/eb:ToDate";
        private const string CommentPath = "/eb:Invoice/eb:Comment";

        [TestMethod]
        public void SaveEmptyInvoiceToTemplateTestOk()
        {
            var invVm = Cmn.UContainer.Resolve<InvoiceViewModel>();
            invVm.SaveTemplateCommand.Execute(EmptyInvoice);
            XDocument xdoc = XDocument.Load(EmptyInvoice);
            var nspm = new XmlNamespaceManager(new NameTable());
            nspm.AddNamespace("eb", "http://www.ebinterface.at/schema/4p2/");
            var xCode = xdoc.XPathSelectElement(BillerCountry, nspm);
            Assert.IsNotNull(xCode);
        }
        [TestMethod()]
        public void FillInvoiceTest()
        {
            // _common.Setup(Common.InvTemplate);   // Test mit Template anfangen           
            Assert.IsNotNull(Cmn.Invoice);
            Assert.IsNotNull(Cmn.Invoice.Details);
            Assert.AreEqual(1, Cmn.Invoice.Details.ItemList.Count);
            Assert.AreEqual(4, Cmn.Invoice.Details.ItemList[0].ListLineItem.Count);

            Assert.IsNotNull(Cmn.Invoice.PaymentConditions);
            Assert.IsNotNull(Cmn.Invoice.PaymentConditions.Discount);
            Assert.AreEqual(2, Cmn.Invoice.PaymentConditions.Discount.Count);

        }
        [TestMethod]
        public void LoadTemplateTest()
        {
            InvVm.LoadTemplateCommand.Execute(ebICommonTestSetup.Common.InvTemplate);
            // MInimum checking ...
            Assert.IsNotNull(InvVm.DetailsView, "Details View");
            Assert.AreEqual(4, InvVm.DetailsView.Count);

            Assert.IsNotNull(InvVm.PaymentConditions, "Payment Conditions");
            Assert.AreEqual(2, InvVm.PaymentConditions.SkontoList.Count);
            Assert.AreEqual((decimal)3.00, InvVm.PaymentConditions.SkontoList[0].SkontoProzent);
            Assert.AreEqual((decimal)42.03, InvVm.PaymentConditions.SkontoList[0].SkontoBetrag);

        }
        [TestMethod]
        public void LieferDatumTest()
        {
            var FromDateExpected = DateTime.Today.ToString("yyyy-MM-dd");// "2014-03-19";

            InvVm.LoadTemplateCommand.Execute(Common.InvTemplate);
            Assert.IsInstanceOfType(Cmn.Invoice.Delivery.Item, typeof(PeriodType),"Delivery Item not null");
            Assert.AreEqual(DateTime.Parse(FromDateExpected), InvVm.VmLieferDatum, "Check Fromdate in InvoiceView");

            // Lieferdatum
            InvVm.SaveTemplateCommand.Execute(DeliveryDateInvoice);
            XDocument xdoc = XDocument.Load(DeliveryDateInvoice);
            var delDate = xdoc.XPathSelectElement(DeliveryDateXPath,Nspc);
            Assert.IsNotNull(delDate,"Deliverydate nach Savetemplate");
            Assert.AreEqual(FromDateExpected, delDate.Value,"Check Deliverydate");
            var fromDate = xdoc.XPathSelectElement(DeliveryFromDateXPath, Nspc);
            Assert.IsNull(fromDate,"Kein Fromdate im XML");
            var toDate = xdoc.XPathSelectElement(DeliveryToDateXPath, Nspc);
            Assert.IsNull(fromDate, "Kein Todate im XML");

        }
        [TestMethod()]
        public void EmptyInvoiceNumberTest()
        {
            InvVm.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Industries;
            InvVm.VmInvNr = "";
            bool result = InvVm.IsInvoiceValid();
            Assert.AreEqual(false, result);
            InvVm.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Government;
            InvVm.VmInvNr = "";
            result = InvVm.IsInvoiceValid();
            Assert.AreEqual(false, result);
        }
        [TestMethod()]
        public void FaelligVorRechnungsDatumTest()
        {
            InvVm.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Industries;
            InvVm.VmInvDate = DateTime.Today;
            InvVm.VmInvDueDate = DateTime.Today.AddDays(-1);
            bool result = InvVm.IsInvoiceValid();
            Assert.AreEqual(false, result);
            InvVm.CurrentSelectedValidation = InvoiceSubtypes.ValidationRuleSet.Government;
            result = InvVm.IsInvoiceValid();
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void PlzEmptyTest()
        {
            InvVm.VmBillerPlz = "";
            bool result = InvVm.IsInvoiceValid();
            foreach (ValidationResult res in InvVm.Results)
            {
                Console.WriteLine(string.Format("{0}: {1}", res.Key, res.Message));
            }
            Assert.AreEqual(false, result);

        }
        public const string InvTemplateSonderz = @"Daten\Sonderzeichen.xmlt";
        [TestMethod]
        public void SonderZeichenLoadTest()
        {
            InvVm.LoadTemplateCommand.Execute(InvTemplateSonderz);
            InvVm.VmBillerName = Sozei;
            foreach (DetailsViewModel model in InvVm.DetailsView)
            {
                Console.WriteLine("ArtNr:{0},Bez:{1},BestPos:{2}", model.ArtikelNr, model.Bezeichnung, model.BestellBezug);
                Assert.AreEqual("ertertasd&/<>", model.ArtikelNr);
            }

        }
        public const string InvTemplateSonderzSave = @"Daten\SonderzeichenSave.xmlt";
        private const string Sozei = "Bogad & Partner Consulting OG mt Sonderzeichen < > / ";

        [TestMethod]
        public void SonderZeichenSaveTest()
        {
            DetailsViewModel dView = Cmn.UContainer.Resolve<DetailsViewModel>(new ParameterOverrides() { 
            { "bestPosRequired", false },
            {"currentRuleSet",InvVm.CurrentSelectedValidation}
            });
            dView.ArtikelNr = "ertertasd&/<>";
            // ertertasd&amp;/&lt;&gt;
            DetailsViewModels dModels = Cmn.UContainer.Resolve<DetailsViewModels>(new ParameterOverrides() { 
            { "bestPosRequired", false },
            {"currentRuleSet",InvVm.CurrentSelectedValidation}
            });
            dModels.DetailsViewList.Add(dView);
            InvVm.DetailsView = dModels.DetailsViewList;
            InvVm.VmBillerName = Sozei;
            InvVm.SaveTemplateCommand.Execute(InvTemplateSonderzSave);
            XDocument xDoc = XDocument.Load(InvTemplateSonderzSave);
            var xEl = xDoc.Root.DescendantsAndSelf().First(p => p.Name.LocalName == "ArticleNumber");
            Assert.AreEqual("ertertasd&/<>", xEl.Value);
            XDocument xdoc = XDocument.Load(InvTemplateSonderzSave);
            var bName = xdoc.XPathSelectElement(BillerName, Nspc).Value;
            Assert.AreEqual(Sozei, bName);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(InvTemplateSonderzSave);
            XmlNode root = xmldoc.DocumentElement;
            XmlNode xName = root.SelectSingleNode(BillerName, Nspc);
            Assert.AreEqual(Sozei.EscapeXml(), xName.InnerXml);
        }

        private const string InvSaveSettingsToTemplate = @"Daten\SaveSettingsToTemplate.xml";
        [TestMethod]
        public void SaveSettingsToTemplateTestOk()
        {
            SetupSettings();
            BillerSettings.Save2Form = true;
            BillerSettings.SaveCommand.Execute(null); // Save Settings 
            InvVm.SaveTemplateCommand.Execute(InvSaveSettingsToTemplate);
            XDocument xDoc = XDocument.Load(InvSaveSettingsToTemplate);
            Assert.IsNotNull(xDoc);
            var xName = xDoc.Root.DescendantsAndSelf().First(p => p.Name.LocalName == "BankName");
            Assert.AreEqual(BillerSettings.Bank, xName.Value);
        }

        private const string InvSaveBankToTemplate = @"Daten\SaveBankToTemplate.xml";
        [TestMethod]
        public void SaveBankToTemplateTestOk()
        {
            InvVm.VmKtoBankName = "TestBank AG";
            InvVm.VmKtoOwner = "Testinhaber";
            InvVm.SaveTemplateCommand.Execute(InvSaveBankToTemplate);
            XDocument xDoc = XDocument.Load(InvSaveBankToTemplate);
            Assert.IsNotNull(xDoc);
            var xName = xDoc.Root.DescendantsAndSelf().First(p => p.Name.LocalName == "BankName");
            Assert.AreEqual("TestBank AG", xName.Value);
            var nspm = new XmlNamespaceManager(new NameTable());
            nspm.AddNamespace("eb", "http://www.ebinterface.at/schema/4p2/");
            var xOwner =
                xDoc.XPathSelectElement(
                    "/eb:Invoice/eb:PaymentMethod/eb:UniversalBankTransaction/eb:BeneficiaryAccount/eb:BankAccountOwner",
                    nspm);
            Assert.AreEqual("Testinhaber", xOwner.Value);
        }

        [TestMethod]
        public void UpdateFromBillerSettingsMitVatTest()
        {
            BillerSettingsViewModel bs = Cmn.UContainer.Resolve<BillerSettingsViewModel>();
            bs.RecalcMwSt = false;
            PlugInSettings.Default.Vatid = "ATU12345678";
            InvVm.OnUpdateFromBillerSettings(bs, null);
            Assert.AreEqual("ATU12345678", InvVm.VmBillerVatid);
        }
        [TestMethod]
        public void UpdateFromBillerSettingsOhneVatTest()
        {
            BillerSettingsViewModel bs = Cmn.UContainer.Resolve<BillerSettingsViewModel>();
            bs.RecalcMwSt = false;
            PlugInSettings.Default.Vatid = "ATU12345678";
            InvVm.OnUpdateFromBillerSettings(bs, null);
            Assert.AreEqual("ATU12345678", InvVm.VmBillerVatid);
        }

        [TestMethod]
        public void StornoRechnungOkTest()
        {
            InvVm.VmDocType = "CancelInvoice";
            InvVm.RelatedDoc = Cmn.UContainer.Resolve<RelatedDocumentViewModel>();
            InvVm.RelatedDoc.RefTypeSelected = RelatedDocumentViewModel.RefType.Storno;
            InvVm.RelatedDoc.RefSelectedDocType = "Invoice";
            InvVm.RelatedDoc.RefInvNumber = "123123";
            InvVm.RelatedDoc.RefInvDate = new DateTime(2013, 12, 13);
            InvVm.IsInvoiceValid();
            Cmn.ListResults(InvVm.Results);
            Assert.AreEqual(true, InvVm.Results.IsValid);
        }
        [TestMethod]
        public void StornoRechnungKeineNotOkTest()
        {
            InvVm.VmDocType = "CancelInvoice";
            InvVm.RelatedDoc = Cmn.UContainer.Resolve<RelatedDocumentViewModel>();
            InvVm.RelatedDoc.RefTypeSelected = RelatedDocumentViewModel.RefType.Keine;
            InvVm.RelatedDoc.RefSelectedDocType = "Invoice";
            InvVm.RelatedDoc.RefInvNumber = "123123";
            InvVm.RelatedDoc.RefInvDate = new DateTime(2013, 12, 13);
            InvVm.IsInvoiceValid();
            Cmn.ListResults(InvVm.Results);
            Assert.AreEqual(false, InvVm.Results.IsValid);
        }
        [TestMethod]
        public void StornoRechnungVerweisNotOkTest()
        {
            InvVm.VmDocType = "CancelInvoice";
            InvVm.RelatedDoc = Cmn.UContainer.Resolve<RelatedDocumentViewModel>();
            InvVm.RelatedDoc.RefTypeSelected = RelatedDocumentViewModel.RefType.Verweis;
            InvVm.RelatedDoc.RefSelectedDocType = "Invoice";
            InvVm.RelatedDoc.RefInvNumber = "123123";
            InvVm.RelatedDoc.RefInvDate = new DateTime(2013, 12, 13);
            InvVm.IsInvoiceValid();
            Cmn.ListResults(InvVm.Results);
            Assert.AreEqual(false, InvVm.Results.IsValid);
        }
        [TestMethod]
        public void StornoGutschriftOkTest()
        {
            InvVm.VmDocType = "CancelCreditMemo";
            InvVm.RelatedDoc = Cmn.UContainer.Resolve<RelatedDocumentViewModel>();
            InvVm.RelatedDoc.RefTypeSelected = RelatedDocumentViewModel.RefType.Storno;
            InvVm.RelatedDoc.RefSelectedDocType = "CreditMemo";
            InvVm.RelatedDoc.RefInvNumber = "123123";
            InvVm.RelatedDoc.RefInvDate = new DateTime(2013, 12, 13);
            InvVm.IsInvoiceValid();
            Cmn.ListResults(InvVm.Results);
            Assert.AreEqual(true, InvVm.Results.IsValid);
        }

        [TestMethod]
        public void ClearTestOk()
        {
            SetupSettings();
            BillerSettings.SaveCommand.Execute(null); // Save Settings 
            InvVm.Clear();
            Assert.AreEqual(PlugInSettings.Default.Name, InvVm.VmBillerName);
            Assert.AreEqual(true, string.IsNullOrEmpty(InvVm.VmRecName));
            Assert.AreEqual(PlugInSettings.Default.Kontowortlaut, InvVm.VmKtoOwner);
        }

        [TestMethod()]
        public void ReplaceTokenTest()
        {
            string result = SharedMethods.ReplaceToken(PlugInSettings.Default.DefaultMailBody, InvVm.VmInvNr, InvVm.VmInvDate, InvVm.VmBillerName,
                 InvVm.VmBillerContact, InvVm.VmBillerphone, InvVm.VmBillerMail);
            Console.WriteLine(PlugInSettings.Default.DefaultMailBody);
            Console.WriteLine(result);
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void SendMailTestOk()
        //{
        //    InvVm.VmRecMail = "jbogad@hotmail.com";
        //    InvVm.SaveAndMailButton.Execute(@"Daten\Invoice2Mail.xml");
        //    Assert.AreEqual(true, InvVm.Results.IsValid);
        //}


        const string SaveCommentTest = @"Daten\SaveCommentTest.xml";
        const string Comment = "Das ist mein Kommentar";
        [TestMethod]
        public void LoadClearCommentTest()
        {
            InvVm.VmComment = Comment;
            InvVm.SaveTemplateCommand.Execute(SaveCommentTest);
            XDocument xdoc = XDocument.Load(SaveCommentTest);
            var nspm = new XmlNamespaceManager(new NameTable());
            nspm.AddNamespace("eb", "http://www.ebinterface.at/schema/4p2/");
            var xCode = xdoc.XPathSelectElement(CommentPath, nspm);
            Assert.AreEqual(Comment, xCode.Value,"Comment has been saved in Template.");
            InvVm.VmComment = "";
            InvVm.LoadTemplateCommand.Execute(Common.InvTest);
            Assert.AreEqual(null, InvVm.VmComment,"Comment has been reloaded.");
        }
    }
}
