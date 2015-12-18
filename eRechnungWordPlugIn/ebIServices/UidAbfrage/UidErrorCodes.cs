using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace ebIServices.UidAbfrage
{
   public static class UidErrorCodes
    {
        private static List<ErrorCode> _errorCodes;
        static UidErrorCodes()
        {            
            Assembly asm = Assembly.GetExecutingAssembly();
            using (Stream St = asm.GetManifestResourceStream("ebIServices.UidAbfrage.UidErrorCodes.xml"))
            {
                using (StreamReader resRd = new StreamReader(St))
                {
                    string xmlText = resRd.ReadToEnd();
                    XElement xdoc = XElement.Parse(xmlText);
                    IEnumerable<XElement> childs = from xel in xdoc.Elements() select xel;
                    List<ErrorCode> errors = (from xElement in childs
                        select new ErrorCode()
                        {
                            rc = int.Parse(xElement.Element("ErrorID").Value),
                            Message = xElement.Element("Text").Value.TrimStart(Environment.NewLine.ToCharArray()).TrimEnd(Environment.NewLine.ToCharArray()).Trim()
                        }).ToList();
                    //List<VatDefaultValue> vatList = (from xElement in childs
                    //                                 select new VatDefaultValue()
                    //                                 {
                    //                                     MwStSatz = decimal.Parse(xElement.Element("MwStSatz").Value),
                    //                                     Beschreibung = xElement.Element("Beschreibung").Value
                    //                                 }).ToList<VatDefaultValue>();
                    _errorCodes = errors;
                    resRd.Close();
                    resRd.Dispose();
                }
            }
        }
        public static int Count { get { return _errorCodes.Count; } }
        public static string ErrorText(string code)
        {
            int iCode = int.Parse(code);
            ErrorCode error = _errorCodes.Find(p => p.rc == iCode);
            if (error != null)
            {
                string rc = string.Format("UB{0,00000} {1}", error.rc + 10000, error.Message);
                return rc;
            }
            else
            {
                string rc = string.Format("UB00081 Bestätigung: Unbekannter Fehlercode {0}", iCode);
                return rc;                
            }
        }
    }

    public class ErrorCode
    {
        public int rc
        {
            get; set;
            
        }
        public string Message { get; set; }
    }
}
