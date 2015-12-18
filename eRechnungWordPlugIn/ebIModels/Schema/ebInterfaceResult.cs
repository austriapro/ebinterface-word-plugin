using System.Collections.Generic;

namespace ebIModels.Schema
{
    /// <summary>
    /// Ergebnis einer Validierungs- oder Upload Operation
    /// </summary>
    public class ebInterfaceResult
    {
        /// <summary>
        /// Initialisiert eine neue Instanze der <see cref="ebInterfaceResult"/> Klasse.
        /// </summary>
        public ebInterfaceResult()
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