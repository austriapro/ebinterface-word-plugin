using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace ebIModels.Services
{

    /// <summary>
    /// Klasse zum Validieren von XML-Dokumenten
    /// </summary>
    internal class XmlValidator
    {
        /// <summary>
        /// Verwaltet alle beim Validieren aufgetretenen Fehler
        /// </summary>
        public readonly List<ValidatorMessage> Errors;

        /// <summary>
        /// Verwaltet alle beim Validieren aufgetretenen Warnungen
        /// </summary>
        public readonly List<ValidatorMessage> Warnings;
        /// <summary>
        /// Gibt das zu verwendente SchemaSet an
        /// </summary>
        public XmlSchemaSet SchemaSet;

        /// <summary>
        /// Gibt die Proxy-Adresse (gegebenenfalls mit Port) für den Internet-Zugang über einen Proxy an
        /// </summary>
        public string ProxyAddress = null;

        /// <summary>
        /// Gibt den Proxy-Benutzernamen für den Internet-Zugang über einen Proxy an,
        /// der eine Authentifizierung erfordert
        /// </summary>
        public string ProxyUserName = null;

        /// <summary>
        /// Gibt das Proxy-Passwort für den Internet-Zugang über einen Proxy an,
        /// der eine Authentifizierung erfordert
        /// </summary>
        public string ProxyUserPassword = null;

        /// <summary>
        /// Gibt die FinOnlBenutzer-Domäne für den Internet-Zugang über einen Proxy an,
        /// der eine Authentifizierung erfordert
        /// </summary>
        public string ProxyUserDomain = null;

        /// <summary>
        /// Rückgabetyp der ValidateXml-Methoden
        /// </summary>
        public enum XmlValidatorResult
        {
            /// <summary>
            /// Das Dokument ist gültig
            /// </summary>
            Valid,

            /// <summary>
            /// Es sind Fehler aufgetreten
            /// </summary>
            ErrorsExists,

            /// <summary>
            /// Es sind Warnungen gemeldet worden 
            /// </summary>
            WarningsExists
        }

        private XmlReader validationReader = null;
        private string nodeArray;

        /// <summary>
        /// Konstruktor 
        /// </summary>
        public XmlValidator()
        {
            Errors = new List<ValidatorMessage>();
            Warnings = new List<ValidatorMessage>();
            this.SchemaSet = new XmlSchemaSet();
        }

        /// <summary>
        /// Behandlungsmethode für das Validation-Ereignis 
        /// </summary>
        private void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            // Fehler bzw. Warnung hinzufügen

            if (args.Severity == XmlSeverityType.Error)
            {
                Errors.Add(new ValidatorMessage()
                    {
                        Field = nodeArray,
                        Message = args.Message
                    });
            }
            else
            {
                Warnings.Add(new ValidatorMessage()
                {
                    Field = nodeArray,
                    Message = args.Message
                });
            }
        }

        /// <summary>
        /// Private Klasse zum Auflösen von Proxy-Problemen
        /// </summary>
        private class XmlUrlProxyResolver : XmlUrlResolver
        {
            /// <summary>
            /// Verwaltet die Proxy-Informationen
            /// </summary>
            public new IWebProxy Proxy = new WebProxy();

            /// <summary>
            /// Liefert einen Stream, der die Datender über das
            /// Argument absoluteUri definierten Datei enthält
            /// </summary>
            public override object GetEntity(Uri absoluteUri, string role,
               Type ofObjectToReturn)
            {
                // Neuen Request erzeugen
                WebRequest request = WebRequest.Create(absoluteUri);

                // Proxy übergeben
                request.Proxy = this.Proxy;

                // Response und Response-Stream holen und zurückgeben
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                return stream;
            }
        }

        /// <summary>
        /// Private Methode zum Validieren 
        /// </summary>
        /// <param name="xmlReader">Referenz auf einen XmlReader, der die XML-Daten enthält</param>
        /// <param name="validationType">Angabe des Validier-Typs</param>
        /// <param name="schemaUri">URI der Schemadatei, falls gegen eine solche Validierung werden soll</param>
        /// <returns>Gibt einen Wert der XmlValidatorResult-Auflistung zurück</returns>
        private XmlValidatorResult ValidateXml(XmlReader xmlReader,
           ValidationType validationType, string schemaUri)
        {
            // Eventuelle alte Fehler und Warnungen löschen 
            Errors.Clear();
            Warnings.Clear();

            // XmlReaderSettings erzeugen und den Validier-Typ festlegen 
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = validationType;


            // Das externe Schema hinzufügen, wenn ein Dateiname angegeben wurde
            if (schemaUri != null)
            {
                try
                {
                    settings.Schemas.Add(null, schemaUri);
                }
                catch (Exception ex)
                {
                    throw new XmlException("Fehler beim Hinzufügen des externen " +
                       "Schemas ': " + schemaUri + "' " + ex.Message, ex);
                }
            }
            else
            {
                if (SchemaSet.Count > 0)
                {

                    try
                    {
                        settings.Schemas.Add(SchemaSet);
                    }
                    catch (Exception ex)
                    {
                        throw new XmlException("Fehler beim Hinzufügen des XmlSchemaSet ':" + ex.Message);
                    }
                }
            }

            // Proxy-Informationen über die eigene Resolver-Klasse übergeben
            if (this.ProxyAddress != null)
            {
                XmlUrlProxyResolver resolver = new XmlUrlProxyResolver();
                ICredentials credentials = new NetworkCredential(this.ProxyUserName,
                   this.ProxyUserPassword, this.ProxyUserDomain);
                resolver.Proxy = new WebProxy(this.ProxyAddress, true, new
                   string[] { }, credentials);
                settings.XmlResolver = resolver;
            }

            // Delegat mit der Methode für das Validation-Ereignis übergeben
            settings.ValidationEventHandler += this.ValidationCallBack;

            // XmlReader mit den Einstellungen erzeugen
            try
            {
                validationReader = XmlReader.Create(xmlReader, settings);
            }
            catch (XmlException ex)
            {
                throw new XmlException("Fehler beim Einlesen des XML-Dokuments: " +
                   ex.Message, ex);
            }
            nodeArray = string.Empty;
            // Daten einlesen und validieren
            try
            {
                while (validationReader.Read())
                {
                    if (validationReader.NodeType == XmlNodeType.Element)
                    {
                        if (validationReader.Depth == 0)
                        {
                            nodeArray = "/";
                        }
                        else
                        {
                            string[] temp = nodeArray.Split('/');
                            nodeArray = "";
                            for (int i = 0; i < (validationReader.Depth); i++)
                            {
                                nodeArray = nodeArray + "/" + temp[i + 1];
                            }
                            nodeArray = nodeArray + "/" + validationReader.Name;
                        }
                    }

                    //if (validationReader.NodeType != XmlNodeType.Whitespace)
                    //{
                    //    Log.LogWrite("'" + validationReader.Name + "';'" + validationReader.Value + "';" +
                    //"'" + validationReader.ValueType.ToString() + "'");

                    //}
                }
            }
            catch (Exception ex)
            {
                Errors.Add(new ValidatorMessage()
                    {
                        Field = "--> Exeption",
                        Message = ex.Message
                    });
            }

            // Ergebnis zurückgeben
            if (this.Errors.Count == 0 && this.Warnings.Count == 0)
            {
                return XmlValidatorResult.Valid;
            }
            else if (this.Errors.Count == 0)
            {
                return XmlValidatorResult.WarningsExists;
            }
            else
            {
                return XmlValidatorResult.ErrorsExists;
            }
        }

        /// <summary>
        /// Validierung eine XML-Datei
        /// </summary>
        /// <param name="xmlFileName">Pfad zu der Datei</param>
        /// <param name="validationType">Angabe des Validierung-Typs</param>
        /// <param name="schemaUri">URI der Schemadatei, falls gegen eine solche Validierung werden soll</param>
        /// <returns>Gibt einen Wert der XmlValidatorResult-Auflistung zurück</returns>
        public XmlValidatorResult ValidateXmlFile(string xmlFileName,
         ValidationType validationType, string schemaUri)
        {
            // XmlReader erzeugen und damit ValidateXml aufrufen
            return ValidateXml(XmlReader.Create(xmlFileName),
               validationType, schemaUri);
        }

        /// <summary>
        /// Validierung ein XElement
        /// </summary>
        /// <param name="element">Referenz auf das Element</param>
        /// <param name="validationType">Angabe des Validierung-Typs</param>
        /// <param name="schemaUri">URI der Schemadatei, falls gegen eine solche Validierung werden soll</param>
        /// <returns>Gibt einen Wert der XmlValidatorResult-Auflistung zurück</returns>
        public XmlValidatorResult ValidateXElement(XElement element,
           ValidationType validationType, string schemaUri)
        {
            return ValidateXml(element.CreateReader(), validationType, schemaUri);
        }

        /// <summary>
        /// Validierung eines XML-Dokument
        /// </summary>
        /// <param name="xmlDoc">Referenz auf das Dokument</param>
        /// <param name="validationType">Angabe des Validierung-Typs</param>
        /// <param name="schemaUri">URI der Schemadatei, falls gegen eine solche Validierung werden soll</param>
        /// <returns>Gibt einen Wert der XmlValidatorResult-Auflistung zurück</returns>
        public XmlValidatorResult ValidateXmlDocument(XmlDocument xmlDoc,
           ValidationType validationType, string schemaUri)
        {
            //MemoryStream aus dem Xml-Dokument erzeugen
            //MemoryStream xmlStream = new MemoryStream();
            //xmlDoc.Save(xmlStream);
            //xmlStream.Seek(0, SeekOrigin.Begin);
            StringReader xmlStream = new StringReader(xmlDoc.InnerXml);
            // Mit dem MemoryStream eine XmlReader-Instanz erzeugen
            XmlReader xrdr = XmlReader.Create(xmlStream);
           // xmlStream.Seek(0, SeekOrigin.Begin);

            return ValidateXml(xrdr, validationType, schemaUri);


        }
    }

    /// <summary>
    /// Klasse für die Validierungsfehler und Warnungen
    /// </summary>
    internal class ValidatorMessage
    {
        /// <summary>
        /// XPath zum fehlerhaften Xml Element
        /// </summary>
        public string Field;

        /// <summary>
        /// Message zum Feld
        /// </summary>
        public string Message;
    }
}
