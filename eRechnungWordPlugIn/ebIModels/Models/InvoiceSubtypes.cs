﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ebIModels.Models
{
    public class InvoiceSubtypes
    {

        private const string VorlageBundOld = "XML Vorlage für ebInterface 4.0 Bund";
        private const string VorlageWordOld = "XML Vorlage für ebInterface 4.0 Word";

        private const string PlugInBundOld = "ebInterface 4.0 Word PlugIn für den Bund";
        private const string PlugInWordOld = "ebInterface 4.0 Beta 4 Word PlugIn";

        private const string PlugInBund4p1a = "ebInterface Word PlugIn an den Bund V4p1";
        private const string PlugInBund4p1 = "ebInterface Word PlugIn an die öffentl. Verwaltung V4p1";
        private const string PlugInWord4p1 = "ebInterface Word PlugIn an die Wirtschaft V4p1";

        private const string PlugInBund4p2 = "ebInterface Word PlugIn an die öffentl. Verwaltung V4p2";
        private const string PlugInWord4p2 = "ebInterface Word PlugIn an die Wirtschaft V4p2";

        private const string PlugInBund4p3 = "ebInterface Word PlugIn an die öffentl. Verwaltung V4p3";
        private const string PlugInWord4p3 = "ebInterface Word PlugIn an die Wirtschaft V4p3";

        private const string PlugInBund5p0 = "ebInterface Word PlugIn an die öffentl. Verwaltung V5p0";
        private const string PlugInWord5p0 = "ebInterface Word PlugIn an die Wirtschaft V5p0"; 


        public enum ValidationRuleSet
        {
            Industries = 0,
            Government,
            Invalid
        }

        private readonly static List<InvoiceSubtype> Variants = new List<InvoiceSubtype>()
        {
            new InvoiceSubtype()
            {
               FriendlyName = "Wirtschaft",
               VariantOption = InvoiceSubtypes.ValidationRuleSet.Industries,
               FileName = "DocumentTypeStandard.xml",
               DocTypeAlt = PlugInWordOld,
               DocTypeVorlageAlt = VorlageWordOld,
               DocTypeNew = PlugInWord5p0
            },

            new InvoiceSubtype()
            {
               FriendlyName = "Wirtschaft",
               VariantOption = InvoiceSubtypes.ValidationRuleSet.Industries,
               FileName = "DocumentTypeStandard.xml",
               DocTypeAlt = PlugInWord4p1,
               DocTypeVorlageAlt = VorlageWordOld,
               DocTypeNew = PlugInWord5p0

            },

            new InvoiceSubtype()
            {
               FriendlyName = "Wirtschaft",
               VariantOption = InvoiceSubtypes.ValidationRuleSet.Industries,
               FileName = "DocumentTypeStandard.xml",
               DocTypeAlt = PlugInWord4p2,
               DocTypeVorlageAlt = VorlageWordOld,
               DocTypeNew = PlugInWord5p0

            },

            new InvoiceSubtype()
            {
               FriendlyName = "Wirtschaft",
               VariantOption = InvoiceSubtypes.ValidationRuleSet.Industries,
               FileName = "DocumentTypeStandard.xml",
               DocTypeAlt = PlugInWord4p3,
               DocTypeVorlageAlt = VorlageWordOld,
               DocTypeNew = PlugInWord5p0

            },

            new InvoiceSubtype()
            {
                FriendlyName = "öffentl. Verwaltung",
                VariantOption = InvoiceSubtypes.ValidationRuleSet.Government,
                FileName = "DocumentTypeBund.xml",
                DocTypeAlt = PlugInBundOld,
                DocTypeVorlageAlt = VorlageBundOld,
                DocTypeNew = PlugInBund5p0
            },

            new InvoiceSubtype()
            {
                FriendlyName = "öffentl. Verwaltung",
                VariantOption = InvoiceSubtypes.ValidationRuleSet.Government,
                FileName = "DocumentTypeBund.xml",
                DocTypeAlt = PlugInBund4p1a,
                DocTypeVorlageAlt = VorlageBundOld,
                DocTypeNew = PlugInBund5p0
            },

            new InvoiceSubtype()
            {
                FriendlyName = "öffentl. Verwaltung",
                VariantOption = InvoiceSubtypes.ValidationRuleSet.Government,
                FileName = "DocumentTypeBund.xml",
                DocTypeAlt = PlugInBund4p1,
                DocTypeVorlageAlt = VorlageBundOld,
                DocTypeNew = PlugInBund5p0
            },

            new InvoiceSubtype()
            {
                FriendlyName = "öffentl. Verwaltung",
                VariantOption = InvoiceSubtypes.ValidationRuleSet.Government,
                FileName = "DocumentTypeBund.xml",
                DocTypeAlt = PlugInBund4p2,
                DocTypeVorlageAlt = VorlageBundOld,
                DocTypeNew = PlugInBund5p0
            },

            new InvoiceSubtype()
            {
                FriendlyName = "öffentl. Verwaltung",
                VariantOption = InvoiceSubtypes.ValidationRuleSet.Government,
                FileName = "DocumentTypeBund.xml",
                DocTypeAlt = PlugInBund4p3,
                DocTypeVorlageAlt = VorlageBundOld,
                DocTypeNew = PlugInBund5p0
            },
            //new InvoiceSubtype()
            //{
            //    FriendlyName = "Invalid",
            //    VariantOption = ValidationRuleSet.Invalid
            //}
        };
        public static InvoiceSubtype AktuelleVersionIndustries { get; private set; }
        public static InvoiceSubtype AktuelleVersionBund { get; private set; }
        public static List<InvoiceSubtype> GetList()
        {
            return Variants;
        }

        public static InvoiceSubtypes.ValidationRuleSet GetVariant(string friedlyName)
        {

            var res = from v in Variants where v.FriendlyName == friedlyName select v;
            if (!res.Any())
            {
                throw new ArgumentException("FriendlyName not found");
            }
            var invoiceVariant = res.FirstOrDefault();
            InvoiceSubtype it = new InvoiceSubtype()
            {
                DocTypeAlt = invoiceVariant.DocTypeAlt,
                DocTypeNew = invoiceVariant.DocTypeNew,
                DocTypeVorlageAlt = invoiceVariant.DocTypeVorlageAlt,
                FileName = invoiceVariant.FileName,
                FriendlyName = invoiceVariant.FriendlyName,
                VariantOption = invoiceVariant.VariantOption
            };

            return it.VariantOption;
        }

        public static InvoiceSubtype GetSubtype(InvoiceSubtypes.ValidationRuleSet tgt)
        {
            InvoiceSubtype defaultSubtype = Variants[0];
            // if (tgt == ValidationRuleSet.Invalid) return defaultSubtype;
            var res = Variants.Find(v => v.VariantOption == tgt);
            if (res == null)
            {
                // throw new ArgumentException("Target not found");
                return defaultSubtype;
            }
            var invoiceVariant = res;
            InvoiceSubtype it = new InvoiceSubtype()
            {
                DocTypeAlt = invoiceVariant.DocTypeAlt,
                DocTypeNew = invoiceVariant.DocTypeNew,
                DocTypeVorlageAlt = invoiceVariant.DocTypeVorlageAlt,
                FileName = invoiceVariant.FileName,
                FriendlyName = invoiceVariant.FriendlyName,
                VariantOption = invoiceVariant.VariantOption
            };

            return it;
        }

        public static InvoiceSubtype GetVariantFromGeneratingSystem(string docType)
        {
            if (docType != null)
            {
                foreach (InvoiceSubtype invoiceVariant in Variants)
                {
                    if (docType.StartsWith(invoiceVariant.DocTypeAlt) ||
                        docType.StartsWith(invoiceVariant.DocTypeVorlageAlt) ||
                        docType.StartsWith(invoiceVariant.DocTypeNew))
                    {
                        InvoiceSubtype it = new InvoiceSubtype()
                        {
                            DocTypeAlt = invoiceVariant.DocTypeAlt,
                            DocTypeNew = invoiceVariant.DocTypeNew,
                            DocTypeVorlageAlt = invoiceVariant.DocTypeVorlageAlt,
                            FileName = invoiceVariant.FileName,
                            FriendlyName = invoiceVariant.FriendlyName,
                            VariantOption = invoiceVariant.VariantOption
                        };

                        return it;
                    }
                }
            }
            return new InvoiceSubtype()
            {
                FriendlyName = "Invalid",
                VariantOption = InvoiceSubtypes.ValidationRuleSet.Invalid
            };
        }

    }

    public class InvoiceSubtype
    {
        public InvoiceSubtypes.ValidationRuleSet VariantOption { get; set; }
        public string FriendlyName { get; set; }
        public string FileName { get; set; }
        public string DocTypeAlt { get; set; }
        public string DocTypeVorlageAlt { get; set; }
        public string DocTypeNew { get; set; }
    }
}
