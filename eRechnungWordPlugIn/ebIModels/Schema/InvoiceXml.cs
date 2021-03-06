﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using ebIModels.Models;
using ebIModels.Services;
using ExtensionMethods;

namespace ebIModels.Schema
{

    /// <summary>
    /// Hilfsklasse für die Xml Konvertierung
    /// </summary>
    public partial class InvoiceXml // Type : Schema.InvoiceBase /*: object */ 
    {
    //    private const string VorlageString =
    //"Das ist eine Vorlage für das ebinterface Word PlugIn und kann nicht als eRechnung eingebracht werden.";
    //    const string EbInvoiceNumber = "InvoiceNumber";
    //    internal const string SchemaPath = "ebIModels.Schema.";


    //    /// <summary>
    //    /// Erzeugt ein XmlDocument aus dem ebInterface Objekt 
    //    /// </summary>
    //    /// <returns>Das <see cref="XmlDocument"/> der Objekt Instanz</returns>
    //    public static XmlDocument ToXmlDocument(object invoice)
    //    {

    //        Assembly assembly = Assembly.GetExecutingAssembly();
    //        FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
    //        string fversion = fvi.FileVersion;
    //        IInvoiceBase invBase = (IInvoiceBase)invoice;
    //        string genSystem = invBase.InvoiceSubtype.DocTypeNew + GetTfsString();// String.Format("DotNetApi für ebInterface V{0}", fversion);

    //        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
    //        // ns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");

    //        foreach (var ebISchema in invBase.CurrentSchemas)
    //        {
    //            ns.Add(ebISchema.Prefix, ebISchema.Url);
    //        }
    //        XmlDocument xDoc = new XmlDocument();
    //        // XmlSerializer serializer = new XmlSerializer(this.GetType(), null, Type.EmptyTypes, new XmlRootAttribute("InvoiceType"), "http://schemas.microsoft.com/2003/10/serialization/");
    //        XmlSerializer serializer = new XmlSerializer(invoice.GetType());

    //        using (StringWriterUtf8 stringWriter = new StringWriterUtf8())
    //        {
    //            XmlWriterSettings settings = new XmlWriterSettings
    //            {
    //                Encoding = Encoding.UTF8,
    //                Indent = true,
    //                IndentChars = "\t",
    //                NewLineChars = Environment.NewLine,
    //                //ConformanceLevel = ConformanceLevel.Document
    //            };

    //            using (XmlWriter writer = XmlWriter.Create(stringWriter, settings))
    //            {
    //                serializer.Serialize(writer, invoice, ns);
    //                string xmlString = stringWriter.ToString();
    //                xDoc.LoadXml(xmlString);
    //            }
    //        }
    //        //XmlAttribute attr = xDoc.CreateAttribute("xsi", "schemaLocation", "");
    //        //attr.Value = "http://www.ebinterface.at/schema/4p0/ http://www.ebinterface.at/schema/4p0/Invoice.xsd";

    //        XmlAttributeCollection attrColl = xDoc.DocumentElement.Attributes;
    //        attrColl.Remove(attrColl["eb:GeneratingSystem"]);
    //        var nspUriSel = from si in invBase.CurrentSchemas where si.Prefix == "eb" select si;
    //        string NspUri = nspUriSel.FirstOrDefault().Url;
    //        XmlAttribute genSys = xDoc.CreateAttribute("eb", "GeneratingSystem", NspUri);
    //        genSys.Value = genSystem;
    //        xDoc.DocumentElement.Attributes.Append(genSys);

    //        return RemoveEmptyNodes(xDoc);
    //    }
    //    private static string GetTfsString()
    //    {
    //        string ret;
    //        var vInfo = new ProductInfo();
    //        ret = string.Format(" V{0} ({1}, {2:G})", vInfo.VersionInfo.ChangeSetId, vInfo.VersionInfo.BuildName, vInfo.VersionInfo.CompileTime);
    //        return ret;
    //    }
    //    private static XmlDocument RemoveEmptyNodes(XmlDocument source)
    //    {
    //        var doc = source.ToXDocument();
    //        doc.Descendants()
    //            .Where(e => e.IsEmpty || String.IsNullOrWhiteSpace(e.Value))
    //            .Remove();

    //        var outDoc = doc.ToXmlDocument();

    //        return outDoc;
    //    }

    //    private string GetEncodingFromXmlFile(string filename)
    //    {
    //        XmlDocument xDoc = new XmlDocument();
    //        xDoc.Load(filename);
    //        string encString = GetEncodingString(xDoc);
    //        return encString;
    //    }

    //    private string GetEncodingString(XmlDocument xDoc)
    //    {
    //        string encString = "UTF-8";
    //        if (xDoc.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
    //        {
    //            XmlDeclaration xmlDecl = (XmlDeclaration)xDoc.FirstChild;
    //            encString = xmlDecl.Encoding;
    //        }
    //        return encString;
    //    }

    //    /// <summary>
    //    /// Gets the XML schemas.
    //    /// </summary>
    //    /// <returns></returns>
    //    internal static XmlSchemaSet GetXmlSchemas(XmlDocument xDoc)
    //    {
    //        var xSchema = new XmlSchemaSet();
    //        ebInterfaceVersion ebiVersion = InvoiceFactory.GetVersion(xDoc);
    //        Assembly Asm = Assembly.GetExecutingAssembly();
    //        List<EbISchema> schemas = null;

    //        switch (ebiVersion.Version)
    //        {
    //            case Models.EbIVersion.V4P1:
    //                schemas = ebInterface4p1.InvoiceType._schemaInfo;
    //                break;
    //            case Models.EbIVersion.V4P2:
    //                schemas = ebInterface4p2.InvoiceType._schemaInfo;
    //                break;
    //            case Models.EbIVersion.V4P3:
    //                schemas = ebInterface4p3.InvoiceType._schemaInfo;
    //                break;
    //            case Models.EbIVersion.V5P0:
    //                schemas = ebInterface5p0.InvoiceType._schemaInfo;
    //                break;
    //            default:
    //                throw new NotSupportedException("ebInterface Version nicht unterstützt.");
    //        }

    //        foreach (var schema in schemas)
    //        {
    //            string res = SchemaPath + schema.CacheName;
    //            using (var stream = Asm.GetManifestResourceStream(res))
    //            {
    //                if (schema.UseInSchema)
    //                {
    //                    XmlReader rdr = XmlReader.Create(stream);
    //                    xSchema.Add(schema.Url, rdr);
    //                }
    //            }
    //        }

    //        return xSchema;
    //    }

    //    /// <summary>
    //    /// Validiert das angegebene XmlDocument auf gegen das ebInterface Schema
    //    /// </summary>
    //    /// <param name="xDoc">The x document.</param>
    //    /// <returns>Eine <see cref="ebInterfaceResult"/> Instanz</returns>
    //    public ebInterfaceResult IsValidInvoice(XmlDocument xDoc)
    //    {
    //        XmlValidator xmlValidator = new XmlValidator();
    //        XmlValidator.XmlValidatorResult xmlResult;
    //        xmlValidator.SchemaSet = GetXmlSchemas(xDoc);

    //        xmlResult = xmlValidator.ValidateXmlDocument(xDoc, ValidationType.Schema, null);
    //        ebInterfaceResult ebIResult = new ebInterfaceResult();
    //        if (xmlResult == XmlValidator.XmlValidatorResult.Valid)
    //        {
    //            ebIResult.ResultType = ResultType.IsValid;
    //        }
    //        else
    //        {
    //            ebIResult.ResultType = ResultType.XmlValidationIssue;
    //            foreach (var errMsg in xmlValidator.Warnings)
    //            {
    //                ebIResult.ResultMessages.Add(new ResultMessage()
    //                {
    //                    Field = errMsg.Field,
    //                    Message = errMsg.Message,
    //                    Severity = MessageType.Warning
    //                });
    //            }
    //            foreach (var errMsg in xmlValidator.Errors)
    //            {
    //                ebIResult.ResultMessages.Add(new ResultMessage()
    //                {
    //                    Field = errMsg.Field,
    //                    Message = errMsg.Message,
    //                    Severity = MessageType.Error
    //                });
    //            }
    //        }
    //        return ebIResult;
    //    }

    //    /// <summary>
    //    /// Validiert das Objekt gegen das ebInterface Schema
    //    /// </summary>       
    //    /// <returns>Eine <see cref="ebInterfaceResult"/> Instanz</returns>
    //    public ebInterfaceResult IsValidInvoice()
    //    {
    //        XmlDocument xDoc = ToXmlDocument();

    //        ebInterfaceResult result = IsValidInvoice(xDoc);
    //        return result;
    //    }

    //    /// <summary>
    //    /// Prüft die Rechnung auf Gültigkeit gemäß ebInterface Standard [is valid invoice].
    //    /// </summary>
    //    /// <param name="xmlInvoice">ebInterface Xml String</param>
    //    /// <returns>Eine <see cref="ebInterfaceResult"/> Instanz</returns>
    //    public static ebInterfaceResult IsValidInvoice(string xmlInvoice)
    //    {
    //        XmlDocument xDoc = new XmlDocument();
    //        xDoc.LoadXml(xmlInvoice);
    //        ebInterfaceResult result = IsValidInvoice(xDoc);
    //        return result;
    //    }

    //    /// <summary>
    //    /// Speichert die ebInterface Rechnung unter dem angegebenen Dateinamen
    //    /// </summary>
    //    /// <param name="file">Pfad und Dateiname in den die Xml Rechnung geschrieben wird</param>
    //    public virtual ebInterfaceResult Save(string file)
    //    {
    //        XmlDocument xmlDocument = ToXmlDocument();
    //        ebInterfaceResult ebiResult = IsValidInvoice(xmlDocument);
    //        if (ebiResult.ResultType == ResultType.IsValid)
    //        {
    //            string xmlstr = WriteXML(xmlDocument);
    //            File.WriteAllText(file, xmlstr);
    //        }
    //        return ebiResult;
    //    }

    //    public virtual void SaveTemplate(string file)
    //    {
    //        XmlDocument xmlDocument = ToXmlDocument();
    //        XDocument xDoc = xmlDocument.ToXDocument();
    //        xDoc.Root.AddFirst(new XElement("Vorlage", VorlageString));

    //        string xmlstr = WriteXML(xDoc.ToXmlDocument());
    //        string xmlTeil = xmlstr.Replace(EbInvoiceNumber, VorlageString);

    //        File.WriteAllText(file, xmlstr);
    //    }

    //    public static string RemoveVorlageText(string text)
    //    {
    //        return text.Replace(VorlageString, EbInvoiceNumber);
    //    }

    //    private string WriteXML(XmlDocument xdoc)
    //    {

    //        var xwsetting = new XmlWriterSettings
    //        {
    //            Indent = true,
    //            NewLineChars = Environment.NewLine,
    //            Encoding = Encoding.UTF8
    //        };
    //        StringWriterUtf8 sWrt = new StringWriterUtf8();
    //        XmlWriter myXMLWriter = XmlWriter.Create(sWrt, xwsetting);
    //        xdoc.Save(myXMLWriter);
    //        myXMLWriter.Close();
    //        // myXMLWriter.Flush();
    //        string xmlStr = sWrt.ToString();
    //        return xmlStr;
    //    }
    //}

    ///// <summary>
    ///// Auflistung der Komponenten die Messages zurückliefern
    ///// </summary>
    //public enum ResultType
    //{
    //    /// <summary>
    //    /// Der Vorgang war erfolgreich
    //    /// </summary>
    //    IsValid = 0,
    //    /// <summary>
    //    /// Die Xml Validierung lieferte Fehler oder Warnungen
    //    /// </summary>
    //    XmlValidationIssue,

    //    /// <summary>
    //    /// Die Prüfung der erb Spezifika lieferte Fehler. <seealso cref="ebInterface4p0.InvoiceType.IsValidErbInvoice()">Hier finden Sie eine Beschreibung der Prüfungen</seealso>
    //    /// </summary>
    //    ErbValidationIssue,
    //    /// <summary>
    //    /// Der Upload zu Erb.gv.at lieferte Fehler zurück
    //    /// </summary>
    //    UploadToErbIssue,
    }
}
