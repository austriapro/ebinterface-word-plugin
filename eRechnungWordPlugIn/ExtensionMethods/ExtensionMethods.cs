using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Forms;
using LogService;
using System.ComponentModel;
using System.Collections.Generic;

namespace ExtensionMethods
{
    public static class StringExtensions
    {
        public static T ToEnum<T>(this string inString, T defaultValue) where T : struct, IConvertible
        {
            var outVal = defaultValue;
            if (!Enum.TryParse<T>(inString, out outVal))
            {
                return defaultValue;
            }
            return outVal;
        }

        public static T2 ConvertEnum<T2>(this Enum inp)
        {
            if (!(inp is Enum))
            {
                return default(T2);
            }
            string val = inp.ToString();
            T2[] values =  (T2[])Enum.GetValues(typeof(T2));
            List<T2> list = new List<T2>(values);
            var found = list.FindIndex(p => p.ToString() == val);
            if (found<0)
            {
                return default(T2);
            }
            
            return values[found];
           
        }
        /// <summary>
        /// Konvertiert Sonderzeichen in XML verträgliche Tags
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string EscapeXml(this string s)
        {
            string xml = s;
            Log.TraceWrite(CallerInfo.Create(),"Before:" + xml);
            if (!string.IsNullOrEmpty(xml))
            {
                // replace literal values with entities
                xml = xml.Replace("&", "&amp;");
                xml = xml.Replace("<", "&lt;");
                xml = xml.Replace(">", "&gt;");
                xml = xml.Replace("\"", "&quot;");
                xml = xml.Replace("'", "&apos;");
            }
            Log.TraceWrite(CallerInfo.Create(),"After:" + xml);
            return xml;
        }

        /// <summary>
        /// Konvertiert XML Tags in Sonderzeichen
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string UnescapeXml(this string s)
        {
            string unxml = s;
            Log.TraceWrite(CallerInfo.Create(),"Before:" + unxml);

            if (!string.IsNullOrEmpty(unxml))
            {
                // replace entities with literal values
                unxml = unxml.Replace("&apos;", "'");
                unxml = unxml.Replace("&quot;", "\"");
                unxml = unxml.Replace("&gt;", ">");
                unxml = unxml.Replace("&lt;", "<");
                unxml = unxml.Replace("&amp;", "&");
            }
            Log.TraceWrite(CallerInfo.Create(),"After:" + unxml);
            return unxml;
        }
        /// <summary>
        /// Berechnet die Anzahl von Tagen zwischen zwei Datumsangaben 
        /// z.B. int diff = FutureDate(new DateTime.Today) 
        /// </summary>
        /// <param name="startDate">Datum am Beginn der Periode</param>
        /// <param name="endDate">Datum am Ende der Periode</param>
        /// <returns></returns>
        public static int Days(this DateTime endDate, DateTime startDate)
        {
            TimeSpan span = endDate - startDate;
            return span.Days;
        }

        /// <summary>
        /// Valdidate eMail Address
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true: eMail valid, false: eMail invalid</returns>
        public static bool IsValidEmail(this string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return false;
            }
            string pattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                             + "@"
                             + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
            var regex = new Regex(pattern);
            return regex.IsMatch(email) && !email.EndsWith(".");
        }

        /// <summary>
        /// Prüft die übergebene VATID auf Plausibilität. Die genauen Länderregeln werden nicht geprüft.
        /// Prüfungen:
        /// = "00000000" oder minLänge>=8 beginnend mit 2 stelligem Ländercode gemäss Tabelle
        /// </summary>
        /// <param name="vat2Verify">Zu prüfende VAT ID</param>
        /// <returns></returns>
        public static bool IsValidVatId(this string vat2Verify)
        {
            if (vat2Verify == null)
            {
                return false;
            }
            string regexViesVat =
                    "^((AT)?U[0-9]{8}|" +
                    "(BE)?0?[0-9]{9}|" +
                    "(BG)?[0-9]{9,10}|" +
                    "(CY)[0-9]{8}[A-Z]|" +
                    "(CZ)?[0-9]{8,10}|" +
                    "(DE)?[0-9]{9}|" +
                    "(DK)?[0-9]{8}|" +
                    "(EE)?[0-9]{9}|" +
                    "(EL|GR)?[0-9]{9}|" +
                    "(ES)?[0-9A-Z][0-9]{7}[0-9A-Z]|" +
                    "(FI)?[0-9]{8}|" +
                    "(FR)?[0-9A-Z]{2}[0-9]{9}|" +
                    "(GB)?([0-9]{9}([0-9]{3})?|[A-Z]{2}[0-9]{3})|" +
                    "(HU)?[0-9]{8}|" +
                    "(IE)?[0-9][0-9A-Z][0-9]{5}[A-Z]|" +
                    "(IT)?[0-9]{11}|" +
                    "(LT)?([0-9]{9}|[0-9]{12})|" +
                    "(LU)?[0-9]{8}|" +
                    "(LV)?[0-9]{11}|" +
                    "(MT)?[0-9]{8}|" +
                    "(NL)?[0-9]{9}B[0-9]{2}|" +
                    "(PL)?[0-9]{10}|" +
                    "(PT)?[0-9]{9}|" +
                    "(RO)?[0-9]{2,10}|" +
                    "(SE)?[0-9]{12}|" +
                    "(SI)?[0-9]{8}|" +
                    "(SK)?[0-9]{10})$";
            if (vat2Verify == "00000000")
            {
                return true;
            }
            var regex = new Regex(regexViesVat);
            return regex.IsMatch(vat2Verify);
        }

        public static bool CheckDigits(this decimal value, int digits)
        {
            var str = String.Format("{0}", value);
            var sep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            var parts = str.Split(sep.ToCharArray()[0]);
            if (parts.Length > 1)
            {
                if (parts[1].Length > digits)
                {
                    return false;
                }
            }
            return true;
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

        /// <summary>
        /// To the XML document.
        /// </summary>
        /// <param name="xDocument">The x document.</param>
        /// <returns></returns>
        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }
        public static decimal ToDecimal(this string value)
        {
            decimal dec;
            if (!decimal.TryParse(value, out dec))
            {
                dec = 0;
            }
            return dec;
        }

        /// <summary>
        /// Gets the XPath of the given XElement
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        public static string GetPath(this XElement node)
        {
            string path = node.Name.LocalName;
            XElement currentNode = node;
            while (currentNode.Parent != null)
            {
                currentNode = currentNode.Parent;
                path = currentNode.Name.LocalName + "/" + path;
            }
            return path;
        }

        /// <summary>
        /// To the x document.
        /// </summary>
        /// <param name="xmlDocument">The XML document.</param>
        /// <returns></returns>
        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }
        /// <summary>
        /// Suspends the two way binding.
        /// </summary>
        /// <param name="bindingManager">The binding manager.</param>
        /// <exception cref="ArgumentNullException">bindingManager</exception>
        public static void SuspendTwoWayBinding(this BindingManagerBase bindingManager)
        {
            if (bindingManager == null)
            {
                throw new ArgumentNullException("bindingManager");
            }

            foreach (Binding b in bindingManager.Bindings)
            {
                b.DataSourceUpdateMode = DataSourceUpdateMode.Never;
            }
        }
        /// <summary>
        /// Resumes the two way binding.
        /// </summary>
        /// <param name="bindingManager">The binding manager.</param>
        /// <exception cref="ArgumentNullException">bindingManager</exception>
        public static void ResumeTwoWayBinding(this BindingManagerBase bindingManager)
        {
            if (bindingManager == null)
            {
                throw new ArgumentNullException("bindingManager");
            }

            foreach (Binding b in bindingManager.Bindings)
            {
                b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
            }
        }
        /// <summary>
        /// Updates the data bound object.
        /// </summary>
        /// <param name="bindingManager">The binding manager.</param>
        /// <exception cref="ArgumentNullException">bindingManager</exception>
        public static void UpdateDataBoundObject(this BindingManagerBase bindingManager)
        {
            if (bindingManager == null)
            {
                throw new ArgumentNullException("bindingManager");
            }

            foreach (Binding b in bindingManager.Bindings)
            {
                b.WriteValue();
            }
        }
        /// <summary>
        /// Repeat a string count times
        /// </summary>
        /// <param name="str"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string Repeat(this string str, int count)
        {
            string ret = "";

            for (var x = 0; x < count; x++)
            {
                ret += str;
            }

            return ret;
        }


    }

    /// <summary>
    /// UTF Stringwriter
    /// </summary>
    /// <seealso cref="System.IO.StringWriter" />
    public class StringWriterUtf8 : System.IO.StringWriter
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
