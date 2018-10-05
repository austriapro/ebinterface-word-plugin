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
                            Version = Models.EbIVersion.V4P0,
                            VersionType = typeof(Schema.ebInterface4p0.InvoiceType),
                            IsSaveSupported = false
                        }
            },

            {"http://www.ebinterface.at/schema/4p1/",
                    new ebInterfaceVersion
                        {
                            Version = Models.EbIVersion.V4P1,
                            VersionType = typeof(Schema.ebInterface4p1.InvoiceType),
                            IsSaveSupported = true

                        }
            },
                {"http://www.ebinterface.at/schema/4p2/",
                    new ebInterfaceVersion
                        {
                            Version = Models.EbIVersion.V4P2,
                            VersionType = typeof(Schema.ebInterface4p2.InvoiceType),
                            IsSaveSupported = true
                        }
                },
                {"http://www.ebinterface.at/schema/4p3/",
                    new ebInterfaceVersion
                        {
                            Version = Models.EbIVersion.V4P3,
                            VersionType = typeof(Schema.ebInterface4p3.InvoiceType),
                            IsSaveSupported = true
                        }
                },
                {"http://www.ebinterface.at/schema/5p0/",
                    new ebInterfaceVersion
                        {
                            Version = Models.EbIVersion.V5P0,
                            VersionType = typeof(Schema.ebInterface5p0.InvoiceType),
                            IsSaveSupported = true
                        }
                },

            };


        public static List<string> GetVersionsWithSaveSupported()
        {
            var list = from x in InvoiceTypes where x.Value.IsSaveSupported == true select x.Value.Version.ToString();

            //   List<string> ebiList = new List<string>() { ebIVersion.V4P2.ToString(), ebIVersion.V4P1.ToString() };
            return list.ToList();
            ;
        }

        public const string VatIdDefault = "00000000";
        public static IInvoiceModel CreateInvoice()
        {
            IInvoiceModel invoice = new Models.InvoiceModel();
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

        public static IInvoiceModel LoadXmlVm(string xmlInvoice)
        {
            ebInterfaceVersion version = GetVersion(xmlInvoice);
            var inv = Deserialize(xmlInvoice, version.VersionType);
            return inv;
        }

        /// <summary>
        /// Validiert den Xml String gemäß ebInterface Standard und lädt das ebInterface Object aus derm Xml String
        /// </summary>
        /// <param name="xmlInvoice">ebInterface Xml String</param>
        /// <returns>Eine neue <see cref="Models.InvoiceModel"/> Instanz
        /// </returns>        
        public static Models.IInvoiceModel LoadXml(string xmlInvoice)
        {
            ebInterfaceVersion version = GetVersion(xmlInvoice);
            var inv = Deserialize(xmlInvoice, version.VersionType);
            return inv;
        }

        private static Models.IInvoiceModel Deserialize(string xmlInvoice, Type currentType)
        {
            StringReader stringReader = new StringReader(xmlInvoice);
            XmlSerializer serializer = new XmlSerializer(currentType);
            var invLoaded = serializer.Deserialize(XmlReader.Create(stringReader));
            var inv = MapInvoice.MapToModel(invLoaded);
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
        /// Ein neue <see cref="Models.InvoiceModel" /> Instanz
        /// </returns>
        public static IInvoiceModel Load(string fileName)
        {
            string xmlInvoice = File.ReadAllText(fileName);
            IInvoiceModel inv = LoadXml(xmlInvoice);
            return inv;
        }

        /// <summary>
        /// Validiert den Xml String gemäß ebInterface Standard und lädt das ebInterface Object aus derm Xml String
        /// </summary>
        /// <param name="file">Stream der die ebInterface Xml Rechnung enthält</param>
        /// <returns>Ein neue <see cref="Models.InvoiceModel"/> Instanz
        /// </returns>        
        public static IInvoiceModel Load(StreamReader file)
        {
            string xmlInvoice = file.ReadToEnd();
            return LoadXml(xmlInvoice);
        }

        /// <summary>
        /// Validiert den Xml String gemäß ebInterface Standard und lädt das ebInterface Object aus derm Xml String
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// Ein neue <see cref="Models.InvoiceModel" /> Instanz
        /// </returns>
        public static Models.IInvoiceModel LoadTemplate(string fileName)
        {
            string text = File.ReadAllText(fileName);
            // string xmlInvoice = Models.InvoiceModel.RemoveVorlageText(text);
            var xmlClean = RemoveEmptyNodes(text);
            Check4ebInterface(xmlClean);
            Models.IInvoiceModel inv = LoadXmlVm(xmlClean);
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
