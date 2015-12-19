using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ZustellDienstSample
{
    class Program
    {
        static void Main()
        {
            const string BillerName = "/eb:Invoice/eb:Biller/eb:Address/eb:Name";
            const string BillerMail = "/eb:Invoice/eb:Biller/eb:Address/eb:Email";
            const string InvoiceNr = "/eb:Invoice/eb:InvoiceNumber";


            string[] args = Environment.GetCommandLineArgs();

            if (args.Length < 2)
            {
                Console.WriteLine("Aufruf:");
                Console.WriteLine("ZustellDienstSample <Name der eInterface XML Rechnung> [/stop]");
                Console.WriteLine("/stop ist optional und gibt an, dass das Programm vor der Beendigung warten soll.");
                DoReadKey(true);
                return;
            }
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine(string.Format("arg[{0}]={1}", i, args[i]));
            }

            bool readKey = false;
            if (args.Length > 2)
            {
                if (args[2] == "/stop")
                {
                    readKey = true;
                }
            }
            string fn = args[1];
            if (!File.Exists(fn))
            {
                Console.WriteLine("Datei "+fn+" nicht gefunden.");
                DoReadKey(readKey);
                return;
            }
            XDocument xdoc = XDocument.Load(fn);
            XmlNamespaceManager Nspc = new XmlNamespaceManager(new NameTable());
            Nspc.AddNamespace("eb", "http://www.ebinterface.at/schema/4p2/");
            var invNr = xdoc.XPathSelectElement(InvoiceNr, Nspc).Value;
            var billerName = xdoc.XPathSelectElement(BillerName, Nspc).Value;
            var billerMail = xdoc.XPathSelectElement(BillerMail, Nspc).Value;
            Console.WriteLine("RechnungsNr:"+invNr);
            Console.WriteLine("Rechnungssteller:"+billerName);
            Console.WriteLine("eMail:"+billerMail);
            DoReadKey(readKey);
        }

        private static void DoReadKey(bool readKey)
        {
            if (readKey)
            {
                Console.ReadKey();
            }
        }
    }
}
