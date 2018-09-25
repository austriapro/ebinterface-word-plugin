namespace SettingsManager
{
    /// <summary>
    /// VAT Default Values
    /// </summary>
    public class VatDefaultValue
    {
        /// <summary>
        /// Gets or sets the mw st satz.
        /// </summary>
        /// <value>
        /// The mw st satz.
        /// </value>

        public decimal MwStSatz { get; set; }
        /// <summary>
        /// Gets or sets the beschreibung.
        /// </summary>
        /// <value>
        /// The beschreibung.
        /// </value>
        public string Beschreibung { get; set; }

        /// <summary>
        /// Gets or sets the code according to UN/CEFACT Codeliste 5305 in der Version D16B
        /// Siehe auch ebInterface 5p0 Anhang: "Empfohlene Codes für TaxCategoryCode"
        /// </summary>
        /// <value>
        /// Valid Codes: 
        /// Code Beschreibung
        /// S Standardsteuersatz(20%)
        /// AA Reduzierter Steuersatz(10%, 13%, etc.)
        /// O Nicht USt-bar
        /// E, K USt-befreit
        /// Hinweis: K steht für die Innergemeinschaftliche Lieferung(IGL)
        /// </value>
        public string Code { get; set; }
    }
}
