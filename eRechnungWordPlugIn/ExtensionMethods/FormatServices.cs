using System;

namespace ExtensionMethods
{
    public static class FormatServices
    {
        private const string FmtDecimal2 = "{0:#,##0.00}";
        private const string FmtDecimal4 = "{0:#,##0.0000}";
        private const string FmtPercent2 = "{0:P2}";

        public static string Decimal2(this decimal value)
        {
            return string.Format(FmtDecimal2, value);

        }

        public static string Decimal4(this decimal value)
        {
            return string.Format(FmtDecimal4, value);
        }

        public static string Percent2(this decimal value)
        {
            return string.Format(FmtPercent2,value/100);
        }

        public static decimal FixedFraction(this decimal value, int iFraction)
        {
            var decFract = Math.Round(value, iFraction);
            return decFract;
        }
    }
}
