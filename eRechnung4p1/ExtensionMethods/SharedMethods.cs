using System;

namespace ExtensionMethods
{
    public static class SharedMethods
    {
        public static string ReplaceToken(string source, string vmInvNr, DateTime vmInvDate, string vmBillerName, string vmBillerContact, string vmBillerPhone, string vmBillerMail)
        {
            string[] tokens = {
                                  "[RECHNUNGSNR]", "[RECHNUNGSDATUM]", "[RECHNUNGSSTELLER]", "[KONTAKT]", "[TELEFON]",
                                  "[EMAIL]"
                              };
            string tmpString = source;
            for (int i = 0; i < tokens.Length; i++)
            {
                string replText = "{" + string.Format("{0}", i) + "}";
                tmpString = tmpString.Replace(tokens[i], replText);
            }
            string finalString = string.Format(tmpString, vmInvNr, vmInvDate.ToString("d"), vmBillerName,
                vmBillerContact, vmBillerPhone, vmBillerMail);
            return finalString;
        }
 
    }
}