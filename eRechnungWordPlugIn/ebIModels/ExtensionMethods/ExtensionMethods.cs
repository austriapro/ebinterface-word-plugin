// using System.Threading.Tasks;

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace ebIModels.ExtensionMethods
{
    /// <summary>
    /// Diese Klasse enthält die Erweiterungsmethoden für DotNetApi für ebInterface
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Erzeugt ein Decimal mit der angegebenen Anzahl von Nachkommastellen
        /// </summary>
        /// <param name="value"></param>
        /// <param name="iFraction"></param>
        /// <returns></returns>
       public static decimal FixedFraction(this decimal value, int iFraction)
        {
            var decFract = Math.Round(value, iFraction);
            return decFract;
        }
        /// <summary>
        /// Prüft den angegebenen text auf alphanumerisch
        /// </summary>
        /// <param name="text"></param>
        /// <returns>
        /// true: Der Text besteht ausschliesslich aus den Zeichen a-z, A-Z, 0-9, *, $
        /// false: Der Text enthält auch andere Zeichen
        /// </returns>
        public static bool IsAlphaNum(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            if (r.IsMatch(text))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Prüft ob der angegebene text numerisch ist
        /// </summary>
        /// <param name="text">The string.</param>
        /// <returns>
        /// true: Der Text enthält nur Ziffern
        /// false: Der Text enthält auch andere Zeichen
        /// </returns>
        public static bool IsNumeric(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            Regex r = new Regex("^[0-9]*$");
            if (r.IsMatch(text))
            {
                return true;
            }
            return false;
        }

        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }

        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }
    }

    /// <summary>
    /// UTF Stringwriter
    /// </summary>
    class StringWriterUtf8 : System.IO.StringWriter
    {
        public override Encoding Encoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }
        
    }
}
