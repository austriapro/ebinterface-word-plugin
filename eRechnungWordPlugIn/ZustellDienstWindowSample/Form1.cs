using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ZustellDienstWindowSample
{
    public partial class Form1 : Form
    {
        const string BillerMail = "/eb:Invoice/eb:Biller/eb:Address/eb:Email";
        const string RecMail = "/eb:Invoice/eb:Receipient/eb:Address/eb:Email";
        const string InvoiceNr = "/eb:Invoice/eb:InvoiceNumber";

        public Form1()
        {
            InitializeComponent();
            label5.Text = "";
            txtBiller.Enabled = false;
            txtFn.Enabled = false;
            txtReNr.Enabled = false;
            txtReceipient.Enabled = false;
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length < 2)
            {
                label5.Text = "Zuwenig Parameter.";
                return;
            }
            string fn = args[1];
            if (!File.Exists(fn))
            {
                label5.Text="Datei " + fn + " nicht gefunden.";
                return;
            }
            txtFn.Text = Path.GetFileNameWithoutExtension(fn);
            XDocument xdoc = XDocument.Load(fn);
            if (!xdoc.Elements().Any())
            {
                label5.Text = "Problem beim Laden der Datei " + fn;
                return;
            }
            XmlNamespaceManager Nspc = new XmlNamespaceManager(new NameTable());
            Nspc.AddNamespace("eb", "http://www.ebinterface.at/schema/4p2/");
            txtReNr.Text = GetXmlValue(xdoc,Nspc,InvoiceNr);
            txtReceipient.Text = GetXmlValue(xdoc, Nspc, RecMail);
            txtBiller.Text = GetXmlValue(xdoc, Nspc, BillerMail);
        }

        private string GetXmlValue(XDocument xdoc, XmlNamespaceManager nspc, string xPath)
        {
            var xEl = xdoc.XPathSelectElement(xPath, nspc);
            if (xEl==null) return "";
            return xEl.Value;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
