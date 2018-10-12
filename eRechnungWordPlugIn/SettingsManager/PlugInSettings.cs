using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using SettingsManager.Properties;
using SettingsManager.Services;

namespace SettingsManager
{
    public class PlugInSettings
    {

        private static PlugInSettings _default = Load();
        /// <summary>
        /// Comment
        /// </summary>
        public static PlugInSettings Default
        {
            get { return _default; }

        }

        #region Rechnungssteller

        public string Name
        {
            get { return Settings.Default.SetName; }
            set { Settings.Default.SetName = value; }
        }

        public string Strasse
        {
            get { return Settings.Default.SetStrasse; }
            set { Settings.Default.SetStrasse = value; }
        }

        public string Plz
        {
            get { return Settings.Default.SetPLZ; }
            set { Settings.Default.SetPLZ = value; }
        }

        public string Ort
        {
            get { return Settings.Default.SetOrt; }
            set { Settings.Default.SetOrt = value; }
        }

        public string TelNr
        {
            get { return Settings.Default.SetTelNr; }
            set { Settings.Default.SetTelNr = value; }
        }

        public string Email
        {
            get { return Settings.Default.SetEmail; }
            set { Settings.Default.SetEmail = value; }
        }

        public string Contact
        {
            get { return Settings.Default.SetBillerContact; }
            set { Settings.Default.SetBillerContact = value; }
        }

        public string Land
        {
            get { return Settings.Default.SetLand; }
            set { Settings.Default.SetLand = value; }
        }

        public string BillerGln
        {
            get { return Settings.Default.SetMyGLN; }
            set { Settings.Default.SetMyGLN = value; }
        }

        public string Currency
        {
            get { return Settings.Default.SetWhrg; }
            set { Settings.Default.SetWhrg = value; }
        }

        public string EbInterfaceVersionString
        {
            get { return Settings.Default.SetEbIVersion; }
            set { Settings.Default.SetEbIVersion = value; }
        }
        #endregion

        #region Mehrwertsteuer

        public const string VatIdDefaultOhneVstBerechtigung = "00000000";
        public const string VatIdDefaultMitVstBerechtigung = "ATU00000000";
        public string Vatid
        {
            get { return Settings.Default.SetVATID; }
            set { Settings.Default.SetVATID = value; }
        }
        public bool VStBerechtigt
        {
            get { return Settings.Default.SetVStBerechtigt; }
            set { Settings.Default.SetVStBerechtigt = value; }
        }
        public const string VStBefreitCode = "E";
        public string VStText
        {
            get { return Settings.Default.SetVStText; }
            set { Settings.Default.SetVStText = value;
                _IstNichtVStBerechtigtVatValue.Beschreibung = value; }
        }
        public string MwStTab
        {
            get { return Settings.Default.SetMwStTab; }
            set { Settings.Default.SetMwStTab = value; }
        }
        public decimal MwStDefault { get { return Settings.Default.SetMwStDefault; } set { Settings.Default.SetMwStDefault = value; } }
        public VatDefaultValue MwStDefaultValue { get { return VatDefaultValues.FirstOrDefault(p => p.MwStSatz == MwStDefault); } }
        #endregion

        #region Bank-Konto

        public string Bank
        {
            get { return Settings.Default.SetBank; }
            set { Settings.Default.SetBank = value; }
        }

        public string Kontowortlaut
        {
            get { return Settings.Default.SetKontowortlaut; }
            set { Settings.Default.SetKontowortlaut = value; }
        }

        public string Iban
        {
            get { return Settings.Default.SetIBAN; }
            set { Settings.Default.SetIBAN = value; }
        }

        public string Bic
        {
            get { return Settings.Default.SetBIC; }
            set { Settings.Default.SetBIC = value; }
        }


        #endregion

        #region Mail Settings

        public string DefaultMailBody
        {
            get { return Settings.Default.DefaultMailBody; }
            set { Settings.Default.DefaultMailBody = value; }
        }

        public string DefaultMailSubject
        {
            get { return Settings.Default.DefaultMailSubject; }
            set { Settings.Default.DefaultMailSubject = value; }
        }

        public string MailBetreff
        {
            get { return Settings.Default.SetBetreff; }
            set { Settings.Default.SetBetreff = value; }
        }

        public string MailText
        {
            get { return Settings.Default.SetMailText; }
            set { Settings.Default.SetMailText = value; }
        }


        #endregion

        #region Zustellung
        public string DeliveryExePath { get { return Settings.Default.SetZustellExe; } set { Settings.Default.SetZustellExe = value; } }
        public string DeliveryArgs { get { return Settings.Default.SetZustellParm; } set { Settings.Default.SetZustellParm = value; } }
        public string DeliveryWorkDir { get { return Settings.Default.setZustellWorkDir; } set { Settings.Default.setZustellWorkDir = value; } }
        #endregion

        //public bool SendEtresor { get { return Settings.Default.SetSendEtresor; } set { Settings.Default.SetSendEtresor = value; } }
        //public string EtresorMail { get { return Settings.Default.SetEtresorMail; } set { Settings.Default.SetEtresorMail = value; } }
        //public bool SaveLocal { get { return Settings.Default.SetSaveLocal; } set { Settings.Default.SetSaveLocal = value; } }

        #region Pfad Settings

        public const string PathToBase = "eRechnung";
        public const string PathToSigned = "Signiert";
        public const string PathToUnsigned = "Unsigniert";
        public const string PathToTemplate = "Vorlagen";
        private static string GetPath(string target)
        {
            var str = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), PathToBase, target);
            if (!Directory.Exists(str))
            {
                Directory.CreateDirectory(str);
            }
            return str;
        }

        public string PathToUnsignedInvoices
        {
            get {
                string p = Settings.Default.SetLocalPath;
                if (string.IsNullOrEmpty(p))
                {
                    p = GetPath(PathToUnsigned);
                }
                return p;
            }
            set { Settings.Default.SetLocalPath = value; }
        }

        public string PathToInvoiceTemplates
        {
            get {
                string p = Settings.Default.SetTemplatePath;
                if (string.IsNullOrEmpty(p))
                {
                    p = GetPath(PathToTemplate);
                }
                return p;

            }
            set { Settings.Default.SetTemplatePath = value; }
        }

        public string PathToSignedInvoices
        {
            get {
                string p = Settings.Default.SetLocalPathSigned;
                if (string.IsNullOrEmpty(p))
                {
                    p = GetPath(PathToSigned);
                }
                return p;
            }
            set { Settings.Default.SetLocalPathSigned = value; }
        }


        #endregion

        #region UID Abfrage
        public string FinOnlTeilnehmer
        {
            get { return Settings.Default.SetTeilnehmer; }
            set { Settings.Default.SetTeilnehmer = value; }
        }
        public string FinOnlBenutzer
        {
            get { return Settings.Default.SetBenutzer; }
            set { Settings.Default.SetBenutzer = value; }
        }
        #endregion

        #region Handy-Signatur

        public int SignType
        {
            get { return Settings.Default.SetSignType; }
            set { Settings.Default.SetSignType = value; }
        }

        public string HandyNr
        {
            get { return Settings.Default.SetHandyNr; }
            set { Settings.Default.SetHandyNr = value; }
        }

        public string Vorwahl
        {
            get { return Settings.Default.SetVorwahl; }
            set { Settings.Default.SetVorwahl = value; }
        }

        #endregion

        private List<VatDefaultValue> _VatDefaultValues = GetVatDefaultValues();
        public List<VatDefaultValue> VatDefaultValues { get { return _VatDefaultValues; } internal set { _VatDefaultValues = value; } }
        private VatDefaultValue _IstNichtVStBerechtigtVatValue = new VatDefaultValue()
        {
            Code = VStBefreitCode,
            MwStSatz = 0
        };
        public VatDefaultValue GetValueFromPercent(decimal percent)
        {

            return VatDefaultValues.FirstOrDefault(p => p.MwStSatz == percent);
        }

        // Schlechte Idee -> die Codes sind nicht eindeutig, MwSt% schon
        //public VatDefaultValue GetValueFromCode(string code)
        //{

        //    return VatDefaultValues.FirstOrDefault(p => p.Code == code);
        //}

        public VatDefaultValue IstNichtVStBerechtigtVatValue
        {
            get {  _IstNichtVStBerechtigtVatValue.Beschreibung = VStText; return _IstNichtVStBerechtigtVatValue; }
            private set { _IstNichtVStBerechtigtVatValue = value; }
        }

        private static List<VatDefaultValue> GetVatDefaultValues()
        {

            //string xmlVat = Settings.Default.SetMwStTab;

            //if (xmlVat == string.Empty)
            //{

            string xmlVat = ResourceService.ReadXmlString("MwStDefaults.xml");
            //}
            XElement xDoc = XElement.Parse(xmlVat);
            IEnumerable<XElement> childs = from xel in xDoc.Elements() select xel;
            List<VatDefaultValue> vatList = (from xElement in childs
                                             select new VatDefaultValue()
                                             {
                                                 MwStSatz = decimal.Parse(xElement.Element("MwStSatz").Value),
                                                 Beschreibung = xElement.Element("Beschreibung").Value,
                                                 Code = xElement.Element("Code").Value
                                             }).ToList<VatDefaultValue>();
            return new List<VatDefaultValue>(vatList);
        }
        //private set
        //{
        //    var myList = value;
        //    if (myList == null) return;
        //    StringWriter stringWriter = new StringWriter();
        //    XmlDocument xmlDoc = new XmlDocument();
        //    XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);
        //    XmlSerializer serializer = new XmlSerializer(typeof(List<VatDefaultValue>));
        //    serializer.Serialize(xmlWriter, myList);
        //    string xmlResult = stringWriter.ToString();
        //    Settings.Default.SetMwStTab = xmlResult;
        //}
        //}

        public string UnitMeasureDefault
        {
            get { return Settings.Default.SetUoMDefault; }
            set {
                Settings.Default.SetUoMDefault = value;
            }
        }
        public List<UnitOfMeasureEntries> UnitOfMeasures
        {
            get {
                string xmlUoM = Settings.Default.SetUnitOfMeasure;
                if (xmlUoM == string.Empty)
                {
                    string appdir = AppDomain.CurrentDomain.BaseDirectory;
                    xmlUoM = ResourceService.ReadXmlString("UnitOfMeasure.xml");

                }
                //XDocument xDoc = XDocument.Parse(xmlUoM);

                // XNamespace ns = "ns1";
                XElement xDoc = XElement.Parse(xmlUoM);
                XNamespace ns = xDoc.Name.Namespace;
                ////IEnumerable<XElement> childs = from xel in xDoc.Elements() select xel;
                IEnumerable<XElement> childs = xDoc.Elements();
                List<UnitOfMeasureEntries> unitOfMeasure = new List<UnitOfMeasureEntries>();
                foreach (XElement xEl in childs)
                {
                    IEnumerable<XElement> child2 = xEl.Elements();
                    var unit = new UnitOfMeasureEntries();
                    foreach (XElement child in child2)
                    {
                        if (child.Name.LocalName == "Gruppe")
                            unit.Gruppe = child.Value;
                        if (child.Name.LocalName == "ID")
                            unit.ID = child.Value;
                        if (child.Name.LocalName == "Anmerkung")
                            unit.Anmerkung = child.Value;
                        if (child.Name.LocalName == "BeschreibungDE")
                            unit.BeschreibungDE = child.Value;
                        if (child.Name.LocalName == "BeschreibungEN")
                            unit.BeschreibungEN = child.Value;
                        if (child.Name.LocalName == "dtAbk")
                            unit.dtAbk = child.Value;
                        if (child.Name.LocalName == "ZUGFeRD")
                            unit.ZUGFeRD = child.Value;
                        if (child.Name.LocalName == "Reihenfolge")
                            unit.Reihenfolge = child.Value;
                        if (child.Name.LocalName == "Favorite")
                            unit.Favorite = bool.Parse(child.Value);

                    }
                    unitOfMeasure.Add(unit);
                }
                return new List<UnitOfMeasureEntries>(unitOfMeasure);
            }
            set {
                var myList = value;
                if (myList == null)
                    return;
                //var xx = new UnitOfMeasure();
                //xx.Entries = myList;
                //string result = xx.Serialize();
                //Settings.Default.SetUnitOfMeasure = result;
                StringWriter stringWriter = new StringWriter();
                var myUom = new UnitOfMeasures
                {
                    UnitOfMeasure = value
                };
                XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);
                XmlSerializer serializer = new XmlSerializer(typeof(List<UnitOfMeasureEntries>));
                serializer.Serialize(xmlWriter, myList);
                string xmlResult = stringWriter.ToString();
                Settings.Default.SetUnitOfMeasure = xmlResult;
            }
        }

        public static PlugInSettings Load()
        {
            Settings.Default.Reload();
            // Default = new PlugInSettings();

            return new PlugInSettings();
        }

        public static void Reset()
        {
            Settings.Default.Reset();
            _default = PlugInSettings.Load();
        }

        public void Save()
        {
            Settings.Default.Save();
        }
    }
}
