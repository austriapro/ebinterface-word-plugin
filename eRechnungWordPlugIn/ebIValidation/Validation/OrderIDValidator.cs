using ExtensionMethods;
using System.Text.RegularExpressions;

namespace ebIViewModels.ExtensionMethods
{
    public static class OrderIdValidator
    {
        /// <summary>
        /// Prüft den angegebenen String auf Gültige OrderReferenez für den Bund
        /// </summary>
        /// <param name="orderRefId">zu prüfende Orderreferenz</param>
        /// <param name="message">Fehlermeldung wenn nicht korrekt</param>
        /// <param name="isBestellPosRequired">out: true wenn in den Detailzeilen die Bestellposition erforderlich ist</param>
        /// <returns>true=ist korrekt, false=nicht korrekt</returns>
        public static bool IsValidOrderRefBund(this string orderRefId, out string message, out bool isBestellPosRequired)
        {

            string rule01 = "^[0-9]{10}$";                // 10stelliger numerischer Wert, z.B. 4700000001 
            string rule02 = "^[0-9]{10}:[A-Z0-9]{3}$";    // 10stelliger numerischer Wert mit zusätzlicher Einkäufergruppe (EKG), z.B. 4700000001:Z01 
            //string rule02a = "^[0-9]+:.*";               // 10stelliger numerischer Wert mit zusätzlicher Einkäufergruppe (EKG), z.B. 4700000001:Z01 
            string rule03 = "^[A-Z0-9]{3}$";              // 3stelliger alphanumerischer Wert, z.B. Z01 (Einkäufergruppe) 
            string rule04 = "^[A-Z0-9]{3}:.{0,50}$";           // 3stelliger alphanumerischer Wert mit zusätzlicher interner Referenz, z.B. Z01:111599-0099-V-3-2099 (Einkäufergruppe:interne Referenz) 
            //string rule05 = @"^[A-Z0-9]+\/";             // Zumindest 3stelliger alphanumerischer Wert, z.B. Z0/ (Verwaltungskennzeichen - VKZ) 
            string rule06 = @"^[A-Z0-9]+\/.{0,50}$";           // Zumindest 3stelliger Wert mit zusätzlicher interner Referenz, z.B. Z0/interne Referenz (Verwaltungskennzeichen/interne Referenz) 
            Regex regEx;
            isBestellPosRequired = false;

            if (string.IsNullOrWhiteSpace(orderRefId))
            {
                message = "Auftragsreferenz ist leer";
                isBestellPosRequired = false;
                return false;
            }
            int tLen = orderRefId.Length;
            if (tLen > 54)
            {
                message = "Auftragsreferenz ist zu lang (max. 54 Zeichen erlaubt)";
                isBestellPosRequired = false;
                return false;

            }

            if (tLen == 3 && (!orderRefId.Contains("/")))
            {
                regEx = new Regex(rule03);
                if (regEx.IsMatch(orderRefId))
                {
                    message = "";
                    return true;
                }
                message = "Auftragsreferenz für den Bund ist nicht drei-stellig alpha-numerisch.";
                isBestellPosRequired = false;
                return false;
            }

            if (tLen == 10 && (!orderRefId.Contains("/")) && (!orderRefId.Contains(":")))
            {
                regEx = new Regex(rule01);
                if (regEx.IsMatch(orderRefId))
                {
                    message = "";
                    isBestellPosRequired = true;
                    return true;
                }
                message = "Auftragsreferenz ist nicht 10-stellig numerisch.";
                isBestellPosRequired = false;
                return false;
            }


            if (orderRefId.Contains(":"))
            {
                regEx = new Regex(rule04);
                if (regEx.IsMatch(orderRefId))
                {
                    message = "";
                    return true;
                }
                if (tLen == 14)
                {
                    regEx = new Regex(rule02);
                    if (regEx.IsMatch(orderRefId))
                    {
                        message = "";
                        isBestellPosRequired = true;
                        return true;
                    }

                    message =
                        "Die Auftragsreferenz enthält vor dem Doppelpunkt nicht 10 numerische Stellen oder ist nach danach nicht alphanumerisch.";
                    isBestellPosRequired = false;
                    return false;
                }
                message =
                    "Die Auftragsreferenz sind nicht alpha-numerisch oder ungültig.";
                isBestellPosRequired = false;
                return false;
            }
            if (orderRefId.Contains("/"))
            {
                if (tLen >= 3)
                {
                    regEx = new Regex(rule06);
                    if (regEx.IsMatch(orderRefId))
                    {
                        message = "";
                        return true;
                    }
                    message =
                        "Das Verwaltungskennzeichen ist nicht alpha-numerisch oder ungültig.";
                    isBestellPosRequired = false;
                    return false;
                }
            }
            message = "Die Auftragsreferenz ist ungültig.";
            isBestellPosRequired = false;
            return false;


        }
    }
}