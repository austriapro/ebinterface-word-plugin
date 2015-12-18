using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Models;

namespace ebIViewModels.Services
{
    public class InvIndustryEventArgs : EventArgs
    {

        public InvoiceSubtypes.ValidationRuleSet Industry { get; set; }
    }

    public class UpdateDocViewEventArgs : EventArgs
    {
        public bool UpdateDocView;
    }
    public class BestPosRequiredChangedEventArgs : EventArgs
    {
        public bool IsBestPosRequired;
    }

    public class InvoiceDatesChangedEventArgs : EventArgs
    {
        public DateTime InvoiceDate { get; set; }
        public DateTime InvoiceDueDate { get; set; }
        public decimal BaseAmount { get; set; }
    }

    public class SaveAsPdfAndSendMailEventArgs : EventArgs
    {
        public string PdfFilename { get; set; }
        public string XmlFilename { get; set; }
        public string Subject { get; set; }
        public string MailBody { get; set; }
        public string SendTo { get; set; }

    }
}