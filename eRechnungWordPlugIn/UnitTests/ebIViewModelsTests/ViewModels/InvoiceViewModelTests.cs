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
using NUnit.Framework;
using SettingsEditor.ViewModels;
using SettingsManager;
using WinFormsMvvm.DialogService;
using WinFormsMvvm.DialogService.FrameworkDialogs.FolderBrowse;
using WinFormsMvvm.DialogService.FrameworkDialogs.OpenFile;
using WinFormsMvvm.DialogService.FrameworkDialogs.SaveFile;
using ebISaveFileDialog;
using System.IO;
using ServiceStack.Text;

namespace ebIViewModels.ViewModels.Tests
{
    [SetUpFixture]
    public class CommonSetUpClass
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var dir = Path.GetDirectoryName(typeof(CommonSetUpClass).Assembly.Location);
            Environment.CurrentDirectory = dir;
            Directory.SetCurrentDirectory(dir);
            Console.WriteLine($"Directory:{dir}");
        }
    }
    [TestFixture]
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

        [Test]
        public void SaveEmptyInvoiceToTemplateTestOk()
        {
            var invVm = Cmn.UContainer.Resolve<InvoiceViewModel>();
            invVm.SaveTemplateCommand.Execute(EmptyInvoice);
            XDocument xdoc = XDocument.Load(EmptyInvoice);
            var nspm = new XmlNamespaceManager(new NameTable());
            nspm.AddNamespace("eb", "http://www.ebinterface.at/schema/5p0/");
            var xCode = xdoc.XPathSelectElement(BillerCountry, nspm);
            Assert.IsNotNull(xCode);
        }
        [Test]
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
        [Test]
        public void CalculateTotalsTest()
        {
            string testInv = @"C:\GitHub\ebinterface-word-plugin\eRechnungWordPlugIn\UnitTests\ebIViewModelsTests\Daten\Test Vorlage 2014-500-2014-03-19.XML";
            InvVm.LoadTemplateCommand.Execute(testInv);
            var controllInvoice = InvoiceFactory.LoadTemplate(testInv);
            Assert.AreEqual(1, controllInvoice.Details.ItemList.Count, "Count Details.ItemList");
            Assert.AreEqual(controllInvoice.Details.ItemList[0].ListLineItem.Count, InvVm.DetailsView.Count, "Details Count");
            Assert.Multiple(() =>
            {
                for (int i = 0; i < InvVm.DetailsView.Count; i++)
                {
                    var vmDetail = InvVm.DetailsView[i];
                    vmDetail.UomEntries = new List<UnitOfMeasureEntries>(); // um den Object Dump klein zu halten
                    vmDetail.UoMList = new System.ComponentModel.BindingList<UnitOfMeasureViewModel>(); // um den Object Dump klein zu halten
                    vmDetail.VatList = new System.ComponentModel.BindingList<VatDefaultValue>();

                    var cDetail = controllInvoice.Details.ItemList[0].ListLineItem[i];
                    Console.WriteLine($"in Template Pos {cDetail.PositionNumber} ****************************************");
                    cDetail.PrintDump();

                    Console.WriteLine($"Zeile {i} in ViewModel, Pos {cDetail.PositionNumber} ****************************************");
                    vmDetail.PrintDump();

                    Assert.AreEqual(cDetail.ArticleNumber[0].Value, vmDetail.ArtikelNr, $"Pos {cDetail.PositionNumber}Artikelnr");
                    Assert.AreEqual(cDetail.LineItemAmount, vmDetail.NettoBetragZeile, $"Pos {cDetail.PositionNumber}, LineItemAmount");
                    Assert.AreEqual(cDetail.TaxItem.TaxAmount, vmDetail.MwStBetragZeile, $"Pos {cDetail.PositionNumber}, TaxAmount");
                    Assert.AreEqual(cDetail.TaxItem.TaxableAmount, vmDetail.NettoBetragZeile, $"Pos {cDetail.PositionNumber}, TaxableAmount");
                    Assert.AreEqual(cDetail.TaxItem.TaxPercent.Value, vmDetail.VatItem.MwStSatz, $"Pos {cDetail.PositionNumber}, TaxPercent.Value");
                }
            });
            Assert.AreEqual(controllInvoice.Tax.TaxItem.Count, InvVm.VatView.VatViewList.Count, "Tax.TaxItem.Count");
            Assert.Multiple(() =>
            {
                for (int i = 0; i < InvVm.VatView.VatViewList.Count; i++)
                {
                    var vmVat = InvVm.VatView.VatViewList[i];
                    var cTaxItem = controllInvoice.Tax.TaxItem[i];
                    Assert.AreEqual(cTaxItem.TaxPercent.TaxCategoryCode, vmVat.TaxCode, "TaxPercent.TaxCategoryCode");
                    Assert.AreEqual(cTaxItem.TaxPercent.Value, vmVat.VatPercent, "TaxPercent.Value");
                    Assert.AreEqual(cTaxItem.TaxableAmount, vmVat.VatBaseAmount, "TaxableAmount");
                    Assert.AreEqual(cTaxItem.TaxAmount, vmVat.VatAmount, "TaxAmount");
                }
            });
            //Console.WriteLine("VAT ***********************************");
            //InvVm.VatView.VatViewList.PrintDump();
            //controllInvoice.Tax.TaxItem.PrintDump();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(controllInvoice.TotalGrossAmount, InvVm.VmInvTotalAmountDecimal, nameof(controllInvoice.TotalGrossAmount));
                Assert.AreEqual(controllInvoice.NetAmount, InvVm.VmInvTotalNetAmount.ToDecimal(), nameof(controllInvoice.NetAmount));
                Assert.AreEqual(controllInvoice.TaxAmountTotal, InvVm.VmInvTaxAmount.ToDecimal(), nameof(controllInvoice.TaxAmountTotal));
            });
        }
        [Test]
        public void LoadTemplateTest()
        {
            InvVm.LoadTemplateCommand.Execute(ebICommonTestSetup.Common.InvTemplate);
            //InvVm.PrintDump();
            // MInimum checking ...
            Assert.IsNotNull(InvVm.DetailsView, "Details View");
            Assert.AreEqual(4, InvVm.DetailsView.Count);
            InvVm.PaymentConditions.PrintDump();
            Assert.IsNotNull(InvVm.PaymentConditions, "Payment Conditions");
            Assert.AreEqual(2, InvVm.PaymentConditions.SkontoList.Count);
            Assert.AreEqual((decimal)3.00, InvVm.PaymentConditions.SkontoList[0].SkontoProzent);
            Assert.AreEqual((decimal)35.25, InvVm.PaymentConditions.SkontoList[0].SkontoBetrag);

        }
        [Test]
        public void LieferDatumTest()
        {
            var FromDateExpected = DateTime.Today.ToString("yyyy-MM-dd");// "2014-03-19";

            InvVm.LoadTemplateCommand.Execute(Common.InvTemplate);
            InvVm.PrintDump();
            //Assert.IsInstanceOf<PeriodType>(Cmn.Invoice.Delivery.Item, "Delivery Item not null");
            Assert.AreEqual(DateTime.Parse(FromDateExpected), InvVm.VmLieferDatum, "Check Fromdate in InvoiceView");

            // Lieferdatum
            InvVm.SaveTemplateCommand.Execute(DeliveryDateInvoice);
            XDocument xdoc = XDocument.Load(DeliveryDateInvoice);
            var delDate = xdoc.XPathSelectElement(DeliveryDateXPath, Nspc);
            Assert.IsNotNull(delDate, "Deliverydate nach Savetemplate");
            Assert.AreEqual(FromDateExpected, delDate.Value, "Check Deliverydate");
            var fromDate = xdoc.XPathSelectElement(DeliveryFromDateXPath, Nspc);
            Assert.IsNull(fromDate, "Kein Fromdate im XML");
            var toDate = xdoc.XPathSelectElement(DeliveryToDateXPath, Nspc);
            Assert.IsNull(fromDate, "Kein Todate im XML");

        }
        [Test]
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
        [Test]
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

        [Test]
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
        [Test]
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

        [Test]
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
        [Test]
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
        [Test]
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
            nspm.AddNamespace("eb", "http://www.ebinterface.at/schema/5p0/");
            var xOwner =
                xDoc.XPathSelectElement(
                    "/eb:Invoice/eb:PaymentMethod/eb:UniversalBankTransaction/eb:BeneficiaryAccount/eb:BankAccountOwner",
                    nspm);
            Assert.AreEqual("Testinhaber", xOwner.Value);
        }

        [Test]
        public void UpdateFromBillerSettingsMitVatTest()
        {
            BillerSettingsViewModel bs = Cmn.UContainer.Resolve<BillerSettingsViewModel>();
            bs.RecalcMwSt = false;
            PlugInSettings.Default.Vatid = "ATU12345678";
            InvVm.OnUpdateFromBillerSettings(bs, null);
            Assert.AreEqual("ATU12345678", InvVm.VmBillerVatid);
        }
        [Test]
        public void UpdateFromBillerSettingsOhneVatTest()
        {
            BillerSettingsViewModel bs = Cmn.UContainer.Resolve<BillerSettingsViewModel>();
            bs.RecalcMwSt = false;
            PlugInSettings.Default.Vatid = "ATU12345678";
            InvVm.OnUpdateFromBillerSettings(bs, null);
            Assert.AreEqual("ATU12345678", InvVm.VmBillerVatid);
        }

        [Test]
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
        [Test]
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
        [Test]
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
        [Test]
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

        [Test]
        public void ClearTestOk()
        {
            SetupSettings();
            BillerSettings.SaveCommand.Execute(null); // Save Settings 
            InvVm.Clear();
            Assert.AreEqual(PlugInSettings.Default.Name, InvVm.VmBillerName);
            Assert.AreEqual(true, string.IsNullOrEmpty(InvVm.VmRecName));
            Assert.AreEqual(PlugInSettings.Default.Kontowortlaut, InvVm.VmKtoOwner);
        }

        [Test]
        public void ReplaceTokenTest()
        {
            string result = SharedMethods.ReplaceToken(PlugInSettings.Default.DefaultMailBody, InvVm.VmInvNr, InvVm.VmInvDate, InvVm.VmBillerName,
                 InvVm.VmBillerContact, InvVm.VmBillerphone, InvVm.VmBillerMail);
            Console.WriteLine(PlugInSettings.Default.DefaultMailBody);
            Console.WriteLine(result);
            Assert.IsNotNull(result);
        }

        //[Test]
        //public void SendMailTestOk()
        //{
        //    InvVm.VmRecMail = "jbogad@hotmail.com";
        //    InvVm.SaveAndMailButton.Execute(@"Daten\Invoice2Mail.xml");
        //    Assert.AreEqual(true, InvVm.Results.IsValid);
        //}


        const string SaveCommentTest = @"Daten\SaveCommentTest.xml";
        const string Comment = "Das ist mein Kommentar";
        [Test]
        public void LoadClearCommentTest()
        {
            InvVm.VmComment = Comment;
            InvVm.SaveTemplateCommand.Execute(SaveCommentTest);
            XDocument xdoc = XDocument.Load(SaveCommentTest);
            var nspm = new XmlNamespaceManager(new NameTable());
            nspm.AddNamespace("eb", "http://www.ebinterface.at/schema/5p0/");
            var xCode = xdoc.XPathSelectElement(CommentPath, nspm);
            Assert.AreEqual(Comment, xCode.Value, "Comment has been saved in Template.");
            InvVm.VmComment = "";
            InvVm.LoadTemplateCommand.Execute(Common.InvTest);
            Assert.AreEqual(null, InvVm.VmComment, "Comment has been reloaded.");
        }

        //[Test]
        //public void CheckKontoVerbindungTest()
        //{
        //    InvVm.Results = new ValidationResults();
        //    PrivateObject privateInv = new PrivateObject(InvVm);
        //    privateInv.Invoke("CheckKontoVerbindung", InvVm.Results);
        //    Assert.IsTrue(InvVm.Results.IsValid);
        //}
    }
}
