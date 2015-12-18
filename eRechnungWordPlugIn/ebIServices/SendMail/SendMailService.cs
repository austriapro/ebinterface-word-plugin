using Microsoft.Office.Interop.Outlook;

namespace ebIServices.SendMail
{
    public class SendMailService : ISendMailService
    {
        public string Subject{get;set;}
        public string MailBody{get;set;}
        public string XmlFilename{get;set;}
        public string PdfFileName{get;set;}
        public string SendTo{get;set;}
        private Application _outlook;

        public SendMailService()
        {
            _outlook = new Application();
        }

        public void SendMail()
        {
         
            var mail = (_MailItem) _outlook.CreateItem(OlItemType.olMailItem);
            mail.To = SendTo;
            mail.Subject = Subject;
            mail.Body = MailBody;
            if (!string.IsNullOrEmpty(XmlFilename))
            {
                mail.Attachments.Add(XmlFilename, OlAttachmentType.olByValue, 1, null);
            }
            if (!string.IsNullOrEmpty(PdfFileName))
            {
                mail.Attachments.Add(PdfFileName, OlAttachmentType.olByValue, 1, null);
            }
            mail.Display(true);
            return;
        }
    }
}