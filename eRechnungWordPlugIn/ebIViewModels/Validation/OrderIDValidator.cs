using ebIModels.ExtensionMethods;

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
            /*
             * Regeln:
             * 3-stellig alphanumerisch oder
             * 3-stellig alphanumerisch ":" Irgendwas (Gesamtlänge maximal 35 Stellen)
             * 10-stellig numerisch
             * 10-Stelling numerisch ":" 3-stellig alphanum
             */
            isBestellPosRequired = false;
            message = "";
            int tLen = orderRefId.Length;

            if (tLen == 3)
            {
                if (!orderRefId.IsAlphaNum())
                {
                    message = "Auftragsreferenz nicht drei-stellig alpha-numerisch. (FAQ Fall 3.)";
                    isBestellPosRequired = false;
                    return false;
                }
            }
            else if ((tLen == 10) && (!orderRefId.Contains(":")))
            {
                if (!orderRefId.IsNumeric())
                {
                    message = "Auftragsreferenz ist nicht 10-stellig numerisch. (FAQ Fall 1.)";
                    isBestellPosRequired = false;
                    return false;
                }
                isBestellPosRequired = true;
                return true;
            }
            else
            {
                if (!orderRefId.Contains(":"))
                {
                    message = "Auftragsreferenz ist ungültig.";
                    isBestellPosRequired = false;
                    return false;
                }
                if (tLen > 35)
                {
                    message = "Auftragsreferenz enthält mehr als 35 Zeichen.";
                    isBestellPosRequired = false;
                    return false;
                }
                string[] xSplt = orderRefId.Split(':');

                if (xSplt[0].Length == 3)
                {
                    if (!xSplt[0].IsAlphaNum())
                    {
                        message =
                            "Die ersten drei Zeichen der Auftragsreferenz sind nicht alpha-numerisch. (FAQ Fall 4.)";
                        isBestellPosRequired = false;
                        return false;
                    }
                }
                else
                {
                    if ((xSplt[0].Length != 10) || (!xSplt[0].IsNumeric()))
                    {
                        message = "Die Auftragsreferenz enthält vor den Doppelpunkt nicht 10 numerische Stellen. (FAQ Fall 2.)";
                        isBestellPosRequired = false;
                        return false;
                    }
                    isBestellPosRequired = true;
                    return true;
                }
            }

            message = "";
            return true;

        }
    }
}