using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;
using ebIModels.Models;
using SRC = ebIModels.Schema.ebInterface4p2;
using ebIModels.Schema;
using SettingsManager;

namespace ebIModels.Mapping.V4p2
{
    public static partial class MapInvoice
    {
        /// <summary>
        /// Mapped ebInterface4p2 InvoiceType auf InvoiceType Model
        /// </summary>
        /// <param name="source">ebInterface4p2 InvoiceType</param>
        /// <returns>InvoiceType Model</returns>
        internal static IInvoiceModel MapV4P2ToVm(SRC.InvoiceType source)
        {
            IInvoiceModel invoice = InvoiceFactory.CreateInvoice(); 
            #region Rechnungskopf
            invoice.InvoiceNumber = source.InvoiceNumber;
            invoice.InvoiceDate = source.InvoiceDate;
            invoice.GeneratingSystem = source.GeneratingSystem;
            invoice.DocumentTitle = source.DocumentTitle;
            invoice.DocumentType = source.DocumentType.ConvertEnum<DocumentTypeType>();
            invoice.InvoiceCurrency = source.InvoiceCurrency.ToEnum(ModelConstants.CurrencyCodeFixed); // source.InvoiceCurrency.ConvertEnum<CurrencyType>();
            if (!string.IsNullOrEmpty(source.Language))
            {
                invoice.Language = source.Language.ToEnum(ModelConstants.LanguangeCodeFixed);  //source.Language.ConvertEnum<LanguageType>();
                // invoice.LanguageSpecified = true;
            }
            else
            {
                // invoice.LanguageSpecified = false;
                invoice.Language = ModelConstants.LanguangeCodeFixed;
            }
            invoice.Comment = source.Comment;
            if (source.CancelledOriginalDocument == null)
            {
                invoice.CancelledOriginalDocument = null;
            }
            else
            {
                invoice.CancelledOriginalDocument = new CancelledOriginalDocumentType()
                {
                    Comment = source.CancelledOriginalDocument.Comment,
                    DocumentType = source.CancelledOriginalDocument.DocumentType.ConvertEnum<DocumentTypeType>(),
                    DocumentTypeSpecified = source.CancelledOriginalDocument.DocumentTypeSpecified,
                    InvoiceDate = source.CancelledOriginalDocument.InvoiceDate,
                    InvoiceNumber = source.CancelledOriginalDocument.InvoiceNumber
                };
            }
            #endregion
            #region Releated Document
            if (source.RelatedDocument != null && source.RelatedDocument.Any())
            {
                invoice.RelatedDocument = new List<RelatedDocumentType>();
                foreach (SRC.RelatedDocumentType relDoc in source.RelatedDocument)
                {
                    var newRel = new RelatedDocumentType()
                    {
                        Comment = relDoc.Comment,
                        DocumentTypeSpecified = relDoc.DocumentTypeSpecified,
                        InvoiceDateSpecified = relDoc.InvoiceDateSpecified,
                        InvoiceNumber = relDoc.InvoiceNumber
                    };
                    if (relDoc.InvoiceDateSpecified)
                    {
                        newRel.InvoiceDate = relDoc.InvoiceDate;
                    }
                    if (relDoc.DocumentTypeSpecified)
                    {
                        newRel.DocumentType = relDoc.DocumentType.ConvertEnum<DocumentTypeType>();
                    }
                    invoice.RelatedDocument.Add(newRel);
                }
            }

            #endregion

            #region Delivery
            if (source.Delivery != null)
            {
                if (source.Delivery.Item is SRC.PeriodType)
                {
                    var deliveryType = new PeriodType
                    {
                        FromDate = ((SRC.PeriodType)source.Delivery.Item).FromDate,
                        ToDate = ((SRC.PeriodType)source.Delivery.Item).ToDate
                    };
                    invoice.Delivery.Item = deliveryType;
                }
                else
                {
                    // Invoice.Delivery.Item = source.Delivery.Item;
                    var period = new PeriodType();
                    if (source.Delivery.Item != null)
                    {
                        period.FromDate = (DateTime)source.Delivery.Item;
                    }
                    invoice.Delivery.Item = period;    // für das Model immer eine Lieferperiode, damit von/bis leichter abgebildet werden kann
                }
            }
            #endregion

            #region Biller
            invoice.Biller.VATIdentificationNumber = source.Biller.VATIdentificationNumber;
            invoice.Biller.InvoiceRecipientsBillerID = source.Biller.InvoiceRecipientsBillerID;
            invoice.Biller.Address = GetAddress(source.Biller.Address);
            invoice.Biller.Contact = GetContact(source.Biller.Address);
            invoice.Biller.FurtherIdentification = GetFurtherIdentification(source.Biller.FurtherIdentification);

            #endregion

            #region InvoiceRecipient
            invoice.InvoiceRecipient.BillersInvoiceRecipientID = source.InvoiceRecipient.BillersInvoiceRecipientID;
            invoice.InvoiceRecipient.VATIdentificationNumber = source.InvoiceRecipient.VATIdentificationNumber;
            invoice.InvoiceRecipient.Address = GetAddress(source.InvoiceRecipient.Address);
            invoice.InvoiceRecipient.Contact = GetContact(source.InvoiceRecipient.Address);
            invoice.InvoiceRecipient.OrderReference.OrderID = source.InvoiceRecipient.OrderReference.OrderID;
            invoice.InvoiceRecipient.OrderReference.ReferenceDateSpecified = source.InvoiceRecipient.OrderReference.ReferenceDateSpecified;
            invoice.InvoiceRecipient.OrderReference.ReferenceDate = source.InvoiceRecipient.OrderReference.ReferenceDate;
            invoice.InvoiceRecipient.FurtherIdentification = GetFurtherIdentification(source.InvoiceRecipient.FurtherIdentification);
            #endregion

            #region Details
            invoice.Details.HeaderDescription = source.Details.HeaderDescription;
            invoice.Details.FooterDescription = source.Details.FooterDescription;

            invoice.Details.ItemList = new List<ItemListType>();

            if (source.Details.ItemList != null)
                foreach (SRC.ItemListType srcItemList in source.Details.ItemList)
                {
                    ItemListType item = new ItemListType
                    {
                        ListLineItem = new List<ListLineItemType>()
                    };
                    foreach (SRC.ListLineItemType srcLineItem in srcItemList.ListLineItem)
                    {
                        ListLineItemType lineItem = new ListLineItemType
                        {
                            AdditionalInformation = null,
                            PositionNumber = srcLineItem.PositionNumber,
                            Description = new List<string>()
                        };
                        if (srcLineItem.Description != null)
                        {
                            lineItem.Description = srcLineItem.Description.ToList();
                        }

                        lineItem.ArticleNumber = GetArtikelList(srcLineItem.ArticleNumber);

                        // Menge
                        lineItem.Quantity = new UnitType
                        {
                            Unit = srcLineItem.Quantity.Unit,
                            Value = srcLineItem.Quantity.Value
                        };

                        // Einzelpreis
                        lineItem.UnitPrice = new UnitPriceType()
                        {
                            Value = srcLineItem.UnitPrice.Value
                        };

                        // Steuer
                        lineItem.TaxItem = MapVatItemType2Vm(srcLineItem.Item, lineItem.LineItemAmount);
                        // Auftragsreferenz
                        lineItem.InvoiceRecipientsOrderReference.OrderID =
                            srcLineItem.InvoiceRecipientsOrderReference.OrderID;
                        lineItem.InvoiceRecipientsOrderReference.OrderPositionNumber =
                            srcLineItem.InvoiceRecipientsOrderReference.OrderPositionNumber;


                        // Rabatte / Zuschläge
                        if (srcLineItem.ReductionAndSurchargeListLineItemDetails != null)
                        {
                            lineItem.ReductionAndSurchargeListLineItemDetails = GetReductionDetails(srcLineItem.ReductionAndSurchargeListLineItemDetails);
                        }
                        lineItem.Description = new List<string>();
                        if (srcLineItem.Description != null)
                        {
                            lineItem.Description = srcLineItem.Description.ToList();
                        }

                        lineItem.LineItemAmount = srcLineItem.LineItemAmount;
                        item.ListLineItem.Add(lineItem);
                    }
                    invoice.Details.ItemList.Add(item);
                }

            if (source.Details.BelowTheLineItem != null)
            {
            //    if (source.Details.BelowTheLineItem.Length > 0)
            //    {
            //        List<BelowTheLineItemType> belowItems = new List<BelowTheLineItemType>();
            //        foreach (SRC.BelowTheLineItemType item in source.Details.BelowTheLineItem)
            //        {
            //            belowItems.Add(new BelowTheLineItemType()
            //            {
            //                Description = item.Description,
            //                LineItemAmount = item.LineItemAmount
            //            });
            //        }
            //        Invoice.Details.BelowTheLineItem.AddRange(belowItems);
            //    }
                Mapping.MapInvoice.mappingErrors.Add(new MappingError(source.Details.BelowTheLineItem.GetType(), "BelowTheLineItem nicht konvertiert."));
            }
            #endregion

            #region Tax
            invoice.Tax.TaxItem.Clear();
            if (source.Tax.VAT.Any())
                {
                foreach (var item in source.Tax.VAT)
                {
                    if (item.Item.GetType() == typeof(SRC.TaxExemptionType))
                    {
                        SRC.TaxExemptionType taxExemption = (SRC.TaxExemptionType)item.Item;
                        TaxItemType taxItem = new TaxItemType()
                        {
                            TaxPercent = new TaxPercentType()
                    {
                                TaxCategoryCode = PlugInSettings.VStBefreitCode,
                                Value = 0
                            },
                            TaxableAmount = item.TaxedAmount,
                            Comment = taxExemption.Value
                    };
                        invoice.Tax.TaxItem.Add(taxItem);
                        if (source.Tax.VAT.Count() > 1)
                        {
                            Mapping.MapInvoice.mappingErrors.Add(new MappingError(source.Tax.VAT.GetType(), "Tax.Vat kann neben TaxExemption kein weiteres Element enthalten"));
                }
                        break;
                    }
                    SRC.VATRateType vATRate = (SRC.VATRateType)item.Item;
                    TaxItemType taxItemVat = new TaxItemType()
                    {
                        TaxPercent = new TaxPercentType()
                        {
                            Value = vATRate.Value,
                            TaxCategoryCode = PlugInSettings.Default.GetValueFromPercent(vATRate.Value).Code
                        },
                        TaxAmount = item.Amount,
                        TaxableAmount = item.TaxedAmount,
                    };
                    invoice.Tax.TaxItem.Add(taxItemVat);
                }
            }

            #endregion

            #region Amount
            invoice.TotalGrossAmount = source.TotalGrossAmount;
            invoice.PayableAmount = source.PayableAmount;
            #endregion

            #region PaymentMethod
            invoice.PaymentMethod.Comment = source.PaymentMethod.Comment;

            if (source.PaymentMethod.Item != null && source.PaymentMethod.Item.GetType() == typeof(SRC.UniversalBankTransactionType))
            {
                SRC.UniversalBankTransactionType txType = source.PaymentMethod.Item as SRC.UniversalBankTransactionType;
                invoice.PaymentMethod = new PaymentMethodType
                {
                    Item = new UniversalBankTransactionType()
                };
                ((UniversalBankTransactionType)invoice.PaymentMethod.Item).BeneficiaryAccount = new List<AccountType>()
                {
                    new AccountType()
                    {
                        BIC = txType.BeneficiaryAccount[0].BIC,
                        BankName = txType.BeneficiaryAccount[0].BankName,
                        IBAN = txType.BeneficiaryAccount[0].IBAN,
                        BankAccountOwner = txType.BeneficiaryAccount[0].BankAccountOwner
                    },
                };
            }

            #endregion

            #region PaymentConditions
            invoice.PaymentConditions.DueDate = source.PaymentConditions.DueDate;
            if (source.PaymentConditions.Discount != null)
            {
                invoice.PaymentConditions.Discount.Clear();
                foreach (SRC.DiscountType srcDiscount in source.PaymentConditions.Discount)
                {
                    DiscountType discount = new DiscountType()
                    {
                        Amount = srcDiscount.Amount,
                        AmountSpecified = srcDiscount.AmountSpecified,
                        BaseAmount = srcDiscount.BaseAmount,
                        BaseAmountSpecified = srcDiscount.BaseAmountSpecified,
                        PaymentDate = srcDiscount.PaymentDate,
                        Percentage = srcDiscount.Percentage,
                        PercentageSpecified = srcDiscount.PercentageSpecified
                    };
                    invoice.PaymentConditions.Discount.Add(discount);
                }
            }

            #endregion
            return invoice;
        }

        private static ContactType GetContact(SRC.AddressType address)
        {
            ContactType contact = new ContactType()
            {
                Email = new List<string>() { address.Email },
                Name = address.Name,
                Phone = new List<string>() { address.Phone },
                Salutation = address.Salutation
            };
            return contact;
        }

        private static AddressType GetAddress(SRC.AddressType address)
        {

            if (address == null)
            {
                return null;
            }
            AddressType addrNew = new AddressType
            {
                Name = address.Name,
                //addrNew.Contact = address.Contact;
                Phone = address.Phone ,
                POBox = address.POBox,
                Email = address.Email ,
                //addrNew.Salutation = address.Salutation;
                Street = address.Street,
                Country = GetCountry(address.Country),
                ZIP = address.ZIP,
                Town = address.Town,
                AddressIdentifier = GetAddressIdentifier(address.AddressIdentifier)
            };
            return addrNew;
        }
        private static CountryType GetCountry(SRC.CountryType countryType)
        {
            if (countryType == null)
                return null;
            CountryType cty = new CountryType(CountryCodeType.AT);
            if (countryType.Value != null)
            {
                cty.CountryCode = countryType.CountryCode.ToEnum(CountryCodeType.AT).ToString(); //.ConvertEnum<V4P3.CountryCodeType>();
                //cty.CountryCodeSpecified = true; // This is always true!
                //cty.Text = countryType.Text.ToArray();
                if (!string.IsNullOrEmpty(countryType.Value))
                {
                    cty.Value = countryType.Value;
                }
                else
                {
                    cty.Value = CountryCodes.GetFromCode(cty.CountryCode.ToString()).Country;
                }

            }
            return cty;
        }
        private static ReductionAndSurchargeListLineItemDetailsType GetReductionDetails(SRC.ReductionAndSurchargeListLineItemDetailsType srcRed)
        {
            if (srcRed.Items == null)
                return null;
            ReductionAndSurchargeListLineItemDetailsType lineRed = new ReductionAndSurchargeListLineItemDetailsType();
            //lineRed.Items = new object[srcRed.Items.Length];

            foreach (var item1 in srcRed.Items)
            {
                if (item1 is SRC.ReductionAndSurchargeBaseType)
                {
                    SRC.ReductionAndSurchargeBaseType item = item1 as SRC.ReductionAndSurchargeBaseType;
                    ReductionAndSurchargeBaseType redBase = new ReductionAndSurchargeBaseType
                    {
                        Amount = item.Amount,
                        AmountSpecified = item.AmountSpecified,
                        BaseAmount = item.BaseAmount,
                        Comment = item.Comment,
                        Percentage = item.Percentage,
                        PercentageSpecified = item.PercentageSpecified
                    };
                    lineRed.Items.Add(redBase);
                }
            }
            lineRed.ItemsElementName = new List<ItemsChoiceType>();
            foreach (
                SRC.ItemsChoiceType choiceType in
                    srcRed.ItemsElementName)
            {
                lineRed.ItemsElementName.Add(choiceType.ConvertEnum<ItemsChoiceType>());
            }
            return lineRed;
        }

        private static TaxItemType MapVatItemType2Vm(object vatItem, decimal taxedAmount)
        {
            if (vatItem == null)
                return null;
            if (vatItem.GetType() == typeof(SRC.TaxExemptionType))
            {
                SRC.TaxExemptionType taxExemption = (SRC.TaxExemptionType)vatItem;
                TaxItemType taxItem = new TaxItemType()
                {
                    TaxPercent = new TaxPercentType()
            {
                        TaxCategoryCode = PlugInSettings.VStBefreitCode,
                        Value = 0
                    },
                    TaxableAmount = taxedAmount,
                    Comment = taxExemption.Value
                };
                return taxItem;
            }
            SRC.VATRateType vATRate = (SRC.VATRateType)vatItem;
            TaxItemType taxItemVat = new TaxItemType()
            {
                TaxPercent = new TaxPercentType()
                {
                    Value = vATRate.Value,
                    TaxCategoryCode = PlugInSettings.Default.GetValueFromPercent(vATRate.Value).Code
                },
                TaxAmount = (taxedAmount * vATRate.Value / 100).FixedFraction(2),
                TaxableAmount = taxedAmount,
            };
            return taxItemVat;

        }

        private static List<ArticleNumberType> GetArtikelList(SRC.ArticleNumberType[] srcArticle)
        {
            List<ArticleNumberType> artNrList = new List<ArticleNumberType>();
            if (srcArticle==null)
            {
                return artNrList;
            }
            foreach (SRC.ArticleNumberType articleNumberType in srcArticle)
            {
                ArticleNumberType art = new ArticleNumberType
                {
                    Value = articleNumberType.Value,
                    ArticleNumberType1Specified = articleNumberType.ArticleNumberType1Specified,
                    ArticleNumberType1 = articleNumberType.ArticleNumberType1.ConvertEnum<ArticleNumberTypeType>()
                };
                artNrList.Add(art);

            }
            return artNrList;
        }
        private static List<AddressIdentifierType> GetAddressIdentifier(SRC.AddressIdentifierType[] adrIn)
        {
            if (adrIn == null)
            {
                return null;

            }

            List<AddressIdentifierType> adrOutList = new List<AddressIdentifierType>();
            foreach (SRC.AddressIdentifierType item in adrIn)
            {
                AddressIdentifierType adId = new AddressIdentifierType();

                if (item.AddressIdentifierType1Specified)
                {
                    adId.AddressIdentifierType1 = item.AddressIdentifierType1.ConvertEnum<AddressIdentifierTypeType>();
                    adId.Value = item.Value;
                }
                adId.Value = item.Value;
                adrOutList.Add(adId);
            }
            return adrOutList;
        }
        private static List<FurtherIdentificationType> GetFurtherIdentification(SRC.FurtherIdentificationType[] furtherID)
        {
            List<FurtherIdentificationType> fIdList = new List<FurtherIdentificationType>();
            if (furtherID == null)
            {
                return fIdList;
            }

            List<string> supportedIds = Enum.GetNames(typeof(FurtherIdentificationType.SupportedIds)).ToList();
            foreach (SRC.FurtherIdentificationType item in furtherID)
            {
                if (supportedIds.Contains(item.IdentificationType))
                {
                    FurtherIdentificationType fId = new FurtherIdentificationType()
                    {
                        IdentificationType = item.IdentificationType,
                        Value = item.Value
                    };
                    fIdList.Add(fId);
                }
            }
            return fIdList;
        }
    }
}
