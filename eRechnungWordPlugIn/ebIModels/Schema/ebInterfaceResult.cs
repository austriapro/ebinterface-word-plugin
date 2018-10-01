using System.Collections.Generic;

namespace ebIModels.Schema
{
    ///// <summary>
    ///// Auflistung der Komponenten die Messages zurückliefern
    ///// </summary>
    public enum ResultType
    {
        /// <summary>
        /// Der Vorgang war erfolgreich
        /// </summary>
        IsValid = 0,
        /// <summary>
        /// Die Xml Validierung lieferte Fehler oder Warnungen
        /// </summary>
        XmlValidationIssue,

        /// <summary>
        /// Die Prüfung der erb Spezifika lieferte Fehler. <seealso cref="ebInterface4p0.InvoiceType.IsValidErbInvoice()">Hier finden Sie eine Beschreibung der Prüfungen</seealso>
        /// </summary>
        ErbValidationIssue,
        /// <summary>
        /// Der Upload zu Erb.gv.at lieferte Fehler zurück
        /// </summary>
        UploadToErbIssue,
    }
    /// <summary>
    /// Ergebnis einer Validierungs- oder Upload Operation
    /// </summary>
    public class EbInterfaceResult
    {
        /// <summary>
        /// Initialisiert eine neue Instanze der <see cref="EbInterfaceResult"/> Klasse.
        /// </summary>
        public EbInterfaceResult()
        {
            ResultMessages = new List<ResultMessage>();

        }
        /// <summary>
        /// Gibt an, welcher Vorgang den Fehler gemeldet hat
        /// </summary>
        public ResultType ResultType;

        /// <summary>
        /// Liste der zurückgegebenen Fehler oder Warnungen
        /// </summary>
        public readonly List<ResultMessage> ResultMessages;
    }
}