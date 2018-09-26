using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using ebIModels.Mapping;
using ebIModels.Models;

namespace ebIModels.Schema
{
    /// <summary>
    /// Klasse für die Erzeugung von ebInterface InvoiceType Klassen
    /// </summary>
    public static class InvoiceFactory
    {

        private static readonly Dictionary<string, ebInterfaceVersion> InvoiceTypes = new Dictionary<string, ebInterfaceVersion>
        {
            {"http://www.ebinterface.at/schema/4p0/",
                    new ebInterfaceVersion
                        {
                            Version = Schema.InvoiceType.ebIVersion.V4P0,
                            VersionType = typeof(Schema.ebInterface4p0.InvoiceType),
                            IsSaveSupported = false
                        }
            },
            {"http://www.ebinterface.at/schema/4p1/",
                    new ebInterfaceVersion
                        {
                            Version = Schema.InvoiceType.ebIVersion.V4P1,
                            VersionType = typeof(Schema.ebInterface4p1.InvoiceType),
                            IsSaveSupported = true

                        }
            },
                {"http://www.ebinterface.at/schema/4p2/",
                    new ebInterfaceVersion
                        {
                            Version = Schema.InvoiceType.ebIVersion.V4P2,
                            VersionType = typeof(Schema.ebInterface4p2.InvoiceType),
                            IsSaveSupported = true
                        }
                },
                {"http://www.ebinterface.at/schema/4p3/",
                    new ebInterfaceVersion
                        {
                            Version = Schema.InvoiceType.ebIVersion.V4P3,
                            VersionType = typeof(Schema.ebInterface4p3.InvoiceType),
                            IsSaveSupported = true
                        }
                },
                {"http://www.ebinterface.at/schema/5p0/",
                    new ebInterfaceVersion
                        {
                            Version = Schema.InvoiceType.ebIVersion.V5P0,
                            VersionType = typeof(Schema.ebInterface5p0.InvoiceType),
                            IsSaveSupported = true
                        }
                },

            };

        public static readonly InvoiceType.ebIVersion LatestVersion = InvoiceType.ebIVersion.V5P0;
        /// <summary>
        /// Erzeugt eine neue ebInterface Instanz der angegebenen Version 
        /// </summary>
        /// <param name="version">ebInterface Version <seealso cref="Version"/></param>
        /// <returns>Eine neue <see cref="Schema.InvoiceType"/> Instanz</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">version</exception>
        public static Schema.InvoiceType CreateInvoice(Schema.InvoiceType.ebIVersion version)
        {
            Schema.InvoiceType invoice = null;
            switch (version)
            {
                //case Schema.InvoiceType.ebIVersion.V3P02:
                //    invoice = new Schema.ebInterface3p02.InvoiceType();
                //    break;
                case Schema.InvoiceType.ebIVersion.V4P0:
                    invoice = new Schema.ebInterface4p0.InvoiceType();
                    break;
                case Schema.InvoiceType.ebIVersion.V4P1:
                    invoice = new Schema.ebInterface4p1.InvoiceType();
                    break;
                case Schema.InvoiceType.ebIVersion.V4P2:
                    invoice = new Schema.ebInterface4p2.InvoiceType();
                    break;
                case Schema.InvoiceType.ebIVersion.V5P0:
                    invoice = new Schema.ebInterface5p0.InvoiceType();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("version");
            }

            return invoice;
        }

        public static List<string> GetVersionsWithSaveSupported()
        {
            var list = from x in InvoiceTypes where x.Value.IsSaveSupported == true select x.Value.Version.ToString();

            //   List<string> ebiList = new List<string>() { ebIVersion.V4P2.ToString(), ebIVersion.V4P1.ToString() };
            return list.ToList(); ;
        }

        public const string VatIdDefault = "00000000";
        public static IInvoiceType CreateInvoice()
        {
            IInvoiceType invoice = new Models.InvoiceType();
            // invoice.Biller.Address.Country.CountryCodeText = CountryCodeType.AT.ToString();
            invoice.Biller.Address.Country = new CountryType(CountryCodeType.AT);
            invoice.InvoiceRecipient.Address.Country = new CountryType(CountryCodeType.AT);
            invoice.DocumentTitle = "Demo Rechnung";
            invoice.InvoiceDate = DateTime.Today;
            invoice.InvoiceCurrency = ModelConstants.CurrencyCodeFixed;
            invoice.Delivery.Item = null;
            invoice.PaymentConditions.DueDate = DateTime.Today;
            invoice.Biller.OrderReference.ReferenceDateSpecified = false;
            invoice.Biller.VATIdentificationNumber = VatIdDefault;
            invoice.InvoiceRecipient.VATIdentificationNumber = VatIdDefault;
            invoice.DocumentType = DocumentTypeType.Invoice;
            invoice.InvoiceSubtype = InvoiceSubtypes.GetSubtype(InvoiceSubtypes.ValidationRuleSet.Government);
            return invoice;
        }

        //public static void Save(Models.InvoiceType invoice,string filname)
        //{
        //    ebInterface4p1.InvoiceType inv = MappingService.MapModelToV4p1(invoice);            
        //    inv.Save(filname);
        //}

        public static IInvoiceType LoadXmlVm(string xmlInvoice)
        {
            ebInterfaceVersion version = GetVersion(xmlInvoice);
            Schema.InvoiceType inv = Deserialize(xmlInvoice, version.VersionType);
            Models.IInvoiceType invModel;

            switch (inv.Version)
            {
                //case Schema.InvoiceType.ebIVersion.V3P02:
                //    throw new NotImplementedException("Version 3p02 derzeit nicht unterstützt");
                // break;
                case Schema.InvoiceType.ebIVersion.V4P0:
                    invModel = MappingService4p0ToVm.MapV4p0ToModel(inv as Schema.ebInterface4p0.InvoiceType);
                    invModel.Version = InvoiceType.ebIVersion.V4P0;
                    return invModel;
                // break;
                case Schema.InvoiceType.ebIVersion.V4P1:
                    invModel = MappingService4p1ToVm.MapV4P1ToVm(inv as Schema.ebInterface4p1.InvoiceType);
                    invModel.Version = InvoiceType.ebIVersion.V4P1;
                    return invModel;
                // break;
                case Schema.InvoiceType.ebIVersion.V4P2:
                    invModel = MappingService4p2ToVm.MapV4P2ToVm(inv as Schema.ebInterface4p2.InvoiceType);
                    invModel.Version = InvoiceType.ebIVersion.V4P2;
                    return invModel;
                // break;
                case Schema.InvoiceType.ebIVersion.V4P3:
                    invModel = MappingService4p3ToVm.MapV4P3ToVm(inv as Schema.ebInterface4p3.InvoiceType);
                    invModel.Version = InvoiceType.ebIVersion.V4P3;
                    return invModel;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            // return (InvoiceType)inv;
        }

        /// <summary>
        /// Validiert den Xml String gemäß ebInterface Standard und lädt das ebInterface Object aus derm Xml String
        /// </summary>
        /// <param name="xmlInvoice">ebInterface Xml String</param>
        /// <returns>Eine neue <see cref="Schema.InvoiceType"/> Instanz
        /// </returns>        
        public static Schema.InvoiceType LoadXml(string xmlInvoice)
        {
            ebInterfaceVersion version = GetVersion(xmlInvoice);
            Schema.InvoiceType inv = Deserialize(xmlInvoice, version.VersionType);
            return inv;
        }

        private static Schema.InvoiceType Deserialize(string xmlInvoice, Type currentType)
        {
            StringReader stringReader = new StringReader(xmlInvoice);
            XmlSerializer serializer = new XmlSerializer(currentType);
            Schema.InvoiceType inv = (Schema.InvoiceType)serializer.Deserialize(XmlReader.Create(stringReader));
            return inv;
        }

        internal static ebInterfaceVersion GetVersion(XmlDocument xDoc)
        {
            string nspUri;
            if (xDoc.DocumentElement != null)
            {
                nspUri = xDoc.DocumentElement.NamespaceURI;
            }
            else
            {
                throw new NotSupportedException("Der String enthält kein DocumentElement.");
            }
            if (!InvoiceTypes.ContainsKey(nspUri))
            {
                throw new NotSupportedException("Das XmlDokument wird nicht unterstützt");
            }

            return InvoiceTypes[nspUri];

        }

        internal static ebInterfaceVersion GetVersion(string xmlString)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xmlString);
            return GetVersion(xDoc);

        }



        /// <summary>
        /// Validiert den Xml String gemäß ebInterface Standard und lädt das ebInterface Object aus derm Xml String
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// Ein neue <see cref="Schema.InvoiceType" /> Instanz
        /// </returns>
        public static Schema.InvoiceType Load(string fileName)
        {
            string xmlInvoice = File.ReadAllText(fileName);
            Schema.InvoiceType inv = LoadXml(xmlInvoice);
            return inv;
        }

        /// <summary>
        /// Validiert den Xml String gemäß ebInterface Standard und lädt das ebInterface Object aus derm Xml String
        /// </summary>
        /// <param name="file">Stream der die ebInterface Xml Rechnung enthält</param>
        /// <returns>Ein neue <see cref="Schema.InvoiceType"/> Instanz
        /// </returns>        
        public static Schema.InvoiceType Load(StreamReader file)
        {
            string xmlInvoice = file.ReadToEnd();
            return LoadXml(xmlInvoice);
        }

        /// <summary>
        /// Validiert den Xml String gemäß ebInterface Standard und lädt das ebInterface Object aus derm Xml String
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// Ein neue <see cref="Schema.InvoiceType" /> Instanz
        /// </returns>
        public static Models.IInvoiceType LoadTemplate(string fileName)
        {
            string text = File.ReadAllText(fileName);
            string xmlInvoice = Schema.InvoiceType.RemoveVorlageText(text);
            var xmlClean = RemoveEmptyNodes(xmlInvoice);
            Check4ebInterface(xmlClean);
            Models.IInvoiceType inv = LoadXmlVm(xmlClean);
            inv.InvoiceSubtype = InvoiceSubtypes.GetVariantFromGeneratingSystem(inv.GeneratingSystem);
            inv.Details.RecalcItemList();
            return inv;
        }

        private static void Check4ebInterface(string xmlData)
        {
            var xDoc = new XmlDocument();
            xDoc.LoadXml(xmlData);
            XmlAttributeCollection attrColl = xDoc.DocumentElement.Attributes;
            foreach (XmlAttribute att in attrColl)
            {
                string uri = att.Value.ToLower();
                if (uri.StartsWith("http://www.ebinterface.at/schema/") && (att.OwnerElement.LocalName == "Invoice"))
                {
                    return;
                }
            }
            throw new FileLoadException("Die angegebene Datei entspricht nicht dem ebInterface XML Standard");
        }

        private static string RemoveEmptyNodes(string source)
        {
            var doc = XDocument.Parse(source);
            doc.Descendants()
                .Where(e => e.IsEmpty || String.IsNullOrWhiteSpace(e.Value))
                .Remove();

            var outDoc = doc.ToString();

            return outDoc;
        }


    }

}
