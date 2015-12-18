namespace ebIServices.SendMail
{
    public interface ISendMailService
    {
        string Subject { get; set; }
        string MailBody { get; set; }
        string XmlFilename { get; set; }
        string PdfFileName { get; set; }
        string SendTo { get; set; }
        void SendMail();
    }
}