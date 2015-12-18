using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ebIModels.Schema;
using SettingsManager;
using InvoiceType = ebIModels.Schema.ebInterface4p0.InvoiceType;

namespace ebIViewModels.ExtensionMethods
{
    public static class Extensions
    {
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
        public static T2 ConvertEnum<T2>(this Enum inp)
        {
            string val = inp.ToString();
            T2 erg = (T2) Enum.Parse(typeof(T2), val);
            return erg;
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
            if (vat2Verify == PlugInSettings.VatIdDefaultOhneVstBerechtigung)
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
    }
}
