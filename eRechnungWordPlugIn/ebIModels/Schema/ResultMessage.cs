using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebIModels.Schema
{
    /// <summary>
    /// Gibt die Art der Message an
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Information
        /// </summary>
        Information = 0,
        /// <summary>
        /// Warnung
        /// </summary>
        Warning,
        /// <summary>
        /// Fehler
        /// </summary>
        Error
    }

    /// <summary>
    /// Enthält die Fehler und Warn Nachrichten aus der Xml Validierung oder dem Upload zu erb.gv.at
    /// </summary>
    public class ResultMessage
    {
        /// <summary>
        /// Gibt die Art des Eintrages an 
        /// </summary>
        public MessageType Severity;
        /// <summary>
        /// Bezeichenet das fehlerhafte Feld
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// Nachricht zum Fehlerhaften Feld
        /// </summary>
        public string Message { get; set; }
    }
}
