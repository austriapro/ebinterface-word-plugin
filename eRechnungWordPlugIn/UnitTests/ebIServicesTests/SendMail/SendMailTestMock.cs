using System;

namespace ebIServices.SendMail.Tests
{
    public class SendMailTestMock : ISendMailService
    {
        public string Subject { get; set; }
        public string MailBody { get; set; }
        public string XmlFilename { get; set; }
        public string PdfFileName { get; set; }
        public string SendTo { get; set; }
        public void SendMail()
        {
            Console.WriteLine("SendTo:"+SendTo);
            Console.WriteLine("Subject:"+Subject);
            Console.WriteLine("Body:"+Subject);
            Console.WriteLine("XmlFilename:"+XmlFilename);
            Console.WriteLine("PdfFileName:"+PdfFileName);
        }
    }
}