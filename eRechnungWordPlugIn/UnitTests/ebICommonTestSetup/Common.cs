using System;
using System.Diagnostics;
using ebIModels.Models;
using ebIModels.Schema;
using eRechnung;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using ExtensionMethods;
using System.Reflection;
using System.IO;
using NUnit.Framework;


namespace ebICommonTestSetup
{
    [SetUpFixture]
    public class CommonSetUpClass
    {
        [OneTimeSetUp]
       public void RunBeforeAnyTests()
        {
            var dir = Path.GetDirectoryName(typeof(CommonSetUpClass).Assembly.Location);
            Environment.CurrentDirectory = dir;

            // or
            Directory.SetCurrentDirectory(dir);
        }
    }
    [TestFixture]
    public class Common
    {
        public const string InvTest = @"Daten\Test-ebInterfaceRechn-2014-500-2014-03-19.xml";
        public const string InvTemplate = @"Daten\Test Vorlage 2014-500-2014-03-19.xml";
        private const string DataFolder = "Daten";

        public IUnityContainer UContainer;
        public IInvoiceModel Invoice;

        public Common()
        {
            UContainer = ThisDocument.Register4Unity();
            UpdateDatesInTestTemplates();
        }

        public Common(string fn)
        {
            UContainer = ThisDocument.Register4Unity();
            UpdateDatesInTestTemplates();
            Invoice = InvoiceFactory.LoadTemplate(fn);
        }

        private void UpdateDatesInTestTemplates()
        {
            var dirList = Directory.GetFiles(DataFolder, "*.xml");
            foreach (string templFileName in dirList)
            {
                IInvoiceModel inv = InvoiceFactory.LoadTemplate(templFileName);
                DateTime oldInvDate = inv.InvoiceDate;   // Save Invoicedate
                inv.InvoiceDate = DateTime.Today;
                int days = inv.InvoiceDate.Days(oldInvDate);
                if (inv.Delivery != null)
                {
                    if (inv.Delivery.Item != null)
                    {
                        if (inv.Delivery.Item is DateTime deliveryDate)
                        {
                            inv.Delivery.Item = deliveryDate.AddDays(days);

                        }
                        if (inv.Delivery.Item is PeriodType period)
                        {
                            if (period.FromDate != DateTime.MinValue)
                            {
                                period.FromDate = ((DateTime)period.FromDate).AddDays(days);

                            }
                            if (period.ToDate != DateTime.MinValue)
                            {
                                period.ToDate = ((DateTime)period.ToDate).AddDays(days);
                            }
                            inv.Delivery.Item = period;
                        }
                    }
                }
                if (inv.PaymentConditions != null)
                {
                    inv.PaymentConditions.DueDate = inv.PaymentConditions.DueDate.AddDays(days);
                }
                if (inv.PaymentConditions.Discount != null)
                {
                    foreach (var item2 in inv.PaymentConditions.Discount)
                    {
                        item2.PaymentDate = item2.PaymentDate.AddDays(days);
                    }
                }
                inv.SaveTemplate(templFileName);
            }

        }

        public void Setup(string fn)
        {
            Invoice = InvoiceFactory.LoadTemplate(InvTemplate);
        }
        public void ListResults(ValidationResults results, string headLine)
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
            Console.WriteLine("<-- " + methodBase.Name + " --> " + headLine);
            ListResults(results);
        }
        public void ListResults(ValidationResults results)
        {
            if (!results.Any())
            {
                Console.WriteLine("Keine Fehler");
            }
            foreach (ValidationResult result in results)
            {
                // Logging.LogWrite("Feld:{0}:{1}", result.Key, result.Message);
                Console.WriteLine("Feld:{0}:{1}", result.Key, result.Message);
            }
            Console.WriteLine("-".PadLeft(15, '-'));
        }

        public XElement GetElement(XDocument xdoc, string xName)
        {

            IEnumerable<XElement> xels = xdoc.Descendants();
            var xel = xels.FirstOrDefault(x => x.Name.LocalName == xName);
            return xel;
        }



    }
}