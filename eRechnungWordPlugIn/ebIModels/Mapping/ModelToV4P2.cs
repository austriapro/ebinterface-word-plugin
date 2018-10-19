using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ebIModels.Schema;
using TARGET = ebIModels.Schema.ebInterface4p2;
using ExtensionMethods;
using Model = ebIModels.Models;
using ebIModels.Schema.ebInterface4p2;
using SettingsManager;

namespace ebIModels.Mapping.V4p2
{
    public static partial class MapInvoice
    {
        /// <summary>
        /// Mapped InvoiceType Model auf ebInterface4p1
        /// </summary>
        /// <param name="source">Invoice Model</param>
        /// <returns>ebInterface 4p1 InvoiceType</returns>
        internal static TARGET.InvoiceType MapModelToV4p2(Model.IInvoiceModel source)
        {
            TARGET.InvoiceType invoice = new TARGET.InvoiceType(); // new V4P1.InvoiceType();

            #region Rechnungskopf
            invoice.InvoiceSubtype = source.InvoiceSubtype;
            invoice.InvoiceNumber = source.InvoiceNumber;
            invoice.InvoiceDate = source.InvoiceDate;
            invoice.GeneratingSystem = source.GeneratingSystem;
            invoice.DocumentType = source.DocumentType.ConvertEnum<DocumentTypeType>();
            invoice.DocumentTitle = source.DocumentTitle;
            invoice.InvoiceCurrency = source.InvoiceCurrency.ToString(); //.ConvertEnum<CurrencyType>();

            invoice.Language = source.Language.ToString();// .ConvertEnum<LanguageType>();


            invoice.Comment = source.Comment;
            #endregion
            #region Cancelled Original Document
            invoice.CancelledOriginalDocument = null;
            if (source.CancelledOriginalDocument != null)
            {
                invoice.CancelledOriginalDocument = new TARGET.CancelledOriginalDocumentType()
                {
                    Comment = source.CancelledOriginalDocument.Comment,
                    DocumentType = source.CancelledOriginalDocument.DocumentType.ConvertEnum<TARGET.DocumentTypeType>(),
                    DocumentTypeSpecified = source.CancelledOriginalDocument.DocumentTypeSpecified,
                    InvoiceDate = source.CancelledOriginalDocument.InvoiceDate,
                    InvoiceNumber = source.CancelledOriginalDocument.InvoiceNumber

                };
            }
            #endregion

            #region Related Document
            if (source.RelatedDocument.Any())
            {
                invoice.RelatedDocument = new TARGET.RelatedDocumentType[]
                {
                    new TARGET.RelatedDocumentType()
                    {
                    Comment = source.RelatedDocument[0].Comment,
                    DocumentType = source.RelatedDocument[0].DocumentType.ConvertEnum<TARGET.DocumentTypeType>(),
                    DocumentTypeSpecified = source.RelatedDocument[0].DocumentTypeSpecified,
                    InvoiceDate = source.RelatedDocument[0].InvoiceDate,
                    InvoiceDateSpecified = source.RelatedDocument[0].InvoiceDateSpecified,
                    InvoiceNumber = source.RelatedDocument[0].InvoiceNumber

                    },
                };
            }
            #endregion

            #region Delivery
            if (source.Delivery.Item is Model.PeriodType delType)
            {
                if ((delType.ToDate != null) && (delType.ToDate != DateTime.MinValue))
                {
                    var deliveryType = new TARGET.PeriodType
                    {
                        FromDate = delType.FromDate,
                        ToDate = delType.ToDate
                    };
                    invoice.Delivery.Item = deliveryType;
                }
                else
                {
                    invoice.Delivery.Item = delType.FromDate;
                }
            }
            else
            {
                invoice.Delivery.Item = source.Delivery.Item;
            }
            #endregion

            #region Biller
            if (source.Biller != null)
            {
                invoice.Biller = new TARGET.BillerType
                {
                    VATIdentificationNumber = source.Biller.VATIdentificationNumber,
                    InvoiceRecipientsBillerID = source.Biller.InvoiceRecipientsBillerID,
                    Address = GetAddress(source.Biller.Address, source.Biller.Contact),
                    FurtherIdentification = GetFurtherIdentification(source.Biller.FurtherIdentification)
                };
            }
            #endregion

            #region InvoiceReceipient
            if (source.InvoiceRecipient != null)
            {
                invoice.InvoiceRecipient = new TARGET.InvoiceRecipientType
                {
                    BillersInvoiceRecipientID = source.InvoiceRecipient.BillersInvoiceRecipientID,
                    VATIdentificationNumber = source.InvoiceRecipient.VATIdentificationNumber,
                    Address = GetAddress(source.InvoiceRecipient.Address, source.InvoiceRecipient.Contact),
                    OrderReference = new OrderReferenceType()
                };
                invoice.InvoiceRecipient.OrderReference.OrderID = source.InvoiceRecipient.OrderReference.OrderID;
                if (source.InvoiceRecipient.OrderReference.ReferenceDateSpecified)
                {
                    invoice.InvoiceRecipient.OrderReference.ReferenceDate = source.InvoiceRecipient.OrderReference.ReferenceDate;
                    invoice.InvoiceRecipient.OrderReference.ReferenceDateSpecified = source.InvoiceRecipient.OrderReference.ReferenceDateSpecified;
                }
                invoice.InvoiceRecipient.AccountingArea = source.InvoiceRecipient.AccountingArea;
                invoice.InvoiceRecipient.SubOrganizationID = source.InvoiceRecipient.SubOrganizationID;
                invoice.InvoiceRecipient.FurtherIdentification = GetFurtherIdentification(source.InvoiceRecipient.FurtherIdentification);
            }
            #endregion

            #region Details
            invoice.Details = new DetailsType
            {
                HeaderDescription = source.Details.HeaderDescription,
                FooterDescription = source.Details.FooterDescription
            };

            // inv4P1.Details.ItemList = new List<InvV4p1.ItemListType>();
            var detailsItemList = new List<ItemListType>();
            // inv4P1.Details.ItemList.Clear();

            // InvV4p1.ItemListType item = new InvV4p1.ItemListType();

            foreach (Model.ItemListType srcItemList in source.Details.ItemList)
            {
                ItemListType itemList = new ItemListType();

                var itemListLineItem = new List<ListLineItemType>();
                foreach (Model.ListLineItemType srcLineItem in srcItemList.ListLineItem)
                {
                    ListLineItemType lineItem = new ListLineItemType
                    {
                        PositionNumber = srcLineItem.PositionNumber,
                        Description = srcLineItem.Description.ToArray(),
                        AdditionalInformation = null,

                        ArticleNumber = GetArtikelList(srcLineItem.ArticleNumber),

                        // Menge
                        Quantity = new UnitType()
                    };
                    lineItem.Quantity.Unit = srcLineItem.Quantity.Unit;
                    lineItem.Quantity.Value = srcLineItem.Quantity.Value;

                    // Einzelpreis
                    lineItem.UnitPrice = new UnitPriceType()
                    {
                        Value = srcLineItem.UnitPrice.Value
                    };

                    // Auftragsreferenz
                    if (!string.IsNullOrEmpty(srcLineItem.InvoiceRecipientsOrderReference.OrderPositionNumber) ||
                        source.InvoiceRecipient.BestellPositionErforderlich)   // Orderposition angegeben oder erforderlich
                    {
                        lineItem.InvoiceRecipientsOrderReference.OrderID = source.InvoiceRecipient.OrderReference.OrderID;
                        lineItem.InvoiceRecipientsOrderReference.OrderPositionNumber =
                            srcLineItem.InvoiceRecipientsOrderReference.OrderPositionNumber;
                    }

                    // Rabatte / Zuschläge
                    lineItem.DiscountFlagSpecified = true;
                    lineItem.DiscountFlag = false;
                    lineItem.ReductionAndSurchargeListLineItemDetails = null;

                    if (srcLineItem.ReductionAndSurchargeListLineItemDetails != null)
                    {
                        lineItem.ReductionAndSurchargeListLineItemDetails = GetReductionDetails(srcLineItem.ReductionAndSurchargeListLineItemDetails, out bool discoutFlag);

                        lineItem.DiscountFlag = discoutFlag;
                        lineItem.DiscountFlagSpecified = true;
                    }
                    lineItem.Description = srcLineItem.Description.ToArray();
                    lineItem.ReCalcLineItemAmount();
                    // Steuer
                    lineItem.Item = MapVatItemType(srcLineItem.TaxItem);

                    itemListLineItem.Add(lineItem);
                }
                itemList.ListLineItem = itemListLineItem.ToArray();
                detailsItemList.Add(itemList);
            }
            invoice.Details.ItemList = detailsItemList.ToArray();
            #endregion

            #region Global ReductionANdSurcharge
            invoice.ReductionAndSurchargeDetails = new ReductionAndSurchargeDetailsType
            {
                Items = null,
                ItemsElementName = new ItemsChoiceType1[]
                {
                    ItemsChoiceType1.Reduction
                }
            };

            #endregion

            #region Tax
            var taxVATList = new List<VATItemType>();
            foreach (var vatItem in source.Tax.TaxItem)
            {
                VATItemType vatItemNeu = new VATItemType()
                {
                    Amount = vatItem.TaxAmount.FixedFraction(2),
                    TaxedAmount = vatItem.TaxableAmount.FixedFraction(2),
                };

                vatItemNeu.Item = MapVatItemType(vatItem);
                taxVATList.Add(vatItemNeu);
            }
            invoice.Tax.VAT = taxVATList.ToArray();
            #endregion

            #region Amount
            invoice.TotalGrossAmount = source.TotalGrossAmount;
            invoice.PaymentMethod.Comment = source.PaymentMethod.Comment;
            invoice.PayableAmount = source.PayableAmount;
            #endregion

            #region PaymentMethod
            invoice.PaymentMethod.Comment = source.PaymentMethod.Comment;
            if (source.PaymentMethod.Item.GetType() == typeof(Model.UniversalBankTransactionType))
            {
                Model.UniversalBankTransactionType txType = source.PaymentMethod.Item as Model.UniversalBankTransactionType;

                invoice.PaymentMethod = new PaymentMethodType
                {
                    Item = new UniversalBankTransactionType()
                };
                ((UniversalBankTransactionType)invoice.PaymentMethod.Item).BeneficiaryAccount = new AccountType[]
                {
                    new AccountType()
                    {
                        BIC = txType.BeneficiaryAccount.First().BIC,
                        BankName = txType.BeneficiaryAccount.First().BankName,
                        IBAN = txType.BeneficiaryAccount.First().IBAN,
                        BankAccountOwner = txType.BeneficiaryAccount.First().BankAccountOwner
                    }
                };
            }
            #endregion

            #region PaymentCoditions
            if (source.PaymentConditions.DueDate > DateTime.MinValue)
            {
                invoice.PaymentConditions.DueDate = source.PaymentConditions.DueDate;
                invoice.PaymentConditions.DueDateSpecified = true;
            }
            if (source.PaymentConditions.Discount != null)
            {
                // inv4P1.PaymentConditions.Discount.Clear();
                var discountList = new List<DiscountType>();
                foreach (Model.DiscountType srcDiscount in source.PaymentConditions.Discount)
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
                    discountList.Add(discount);
                }
                invoice.PaymentConditions.Discount = discountList.ToArray();
            }
            #endregion

            // Fixe Einträge
            #region PresentationDetails
            invoice.PresentationDetails = new PresentationDetailsType()
            {
                URL = "http://www.austriapro.at",
                SuppressZero = true,
                SuppressZeroSpecified = true
            };
            #endregion
            return invoice;
        }

        private static ReductionAndSurchargeListLineItemDetailsType GetReductionDetails(Model.ReductionAndSurchargeListLineItemDetailsType srcRed, out bool discountFlag)
        {
            discountFlag = false;
            if (srcRed == null)
            {
                
                return null;
            }
            
            ReductionAndSurchargeListLineItemDetailsType lineRed = new ReductionAndSurchargeListLineItemDetailsType
            {
                Items = new object[srcRed.Items.Count]
            };
            int i = 0;
            if (lineRed.Items.Count()>0)
            {
                discountFlag = true;
            }
            foreach (var item1 in srcRed.Items)
            {
                if (item1 is Model.ReductionAndSurchargeBaseType)
                {
                    Model.ReductionAndSurchargeBaseType item = item1 as Model.ReductionAndSurchargeBaseType;
                    ReductionAndSurchargeBaseType redBase = new ReductionAndSurchargeBaseType
                    {
                        Amount = item.Amount,
                        AmountSpecified = item.AmountSpecified,
                        BaseAmount = item.BaseAmount,
                        Comment = item.Comment,
                        Percentage = item.Percentage,
                        PercentageSpecified = item.PercentageSpecified
                    };
                    lineRed.Items[i] = redBase;
                    i++;
                }
            }

            lineRed.ItemsElementName = new ItemsChoiceType[srcRed.ItemsElementName.Count()];
            i = 0;
            foreach (
                Model.ItemsChoiceType choiceType in
                    srcRed.ItemsElementName)
            {
                lineRed.ItemsElementName[i] =
                    (choiceType.ConvertEnum<ItemsChoiceType>());
                i++;
            }
            return lineRed;
        }

        private static object MapVatItemType(Model.TaxItemType vatItem)
        {

            if (vatItem.TaxPercent.TaxCategoryCode == PlugInSettings.VStBefreitCode)
            {
                var taxexNew = new TaxExemptionType
                {
                    Value = vatItem.Comment
                };
                return taxexNew;
            }
            else
            {
                var taxexNew = new VATRateType
                {
                    Value = vatItem.TaxPercent.Value
                };
                return taxexNew;
            }

        }

        private static AddressType GetAddress(Model.AddressType address, Model.ContactType contact)
        {

            if (address == null)
            {
                return null;
            }
            AddressType addrNew = new AddressType
            {
                Name = address.Name,
                Contact = contact?.Name,
                Phone = address.Phone,
                POBox = address.POBox,
                Email = address.Email,
                Salutation = contact?.Salutation,
                Street = address.Street,
                Country = GetCountry(address.Country),
                ZIP = address.ZIP,
                Town = address.Town,
                AddressIdentifier = GetAddressIdentifier(address.AddressIdentifier)
            };
            return addrNew;
        }

        private static CountryType GetCountry(Model.CountryType countryType)
        {
            if (countryType == null)
                return null;
            CountryType cty = new CountryType();
            if (string.IsNullOrEmpty(countryType.Value))
            {
                cty.CountryCode = Model.CountryCodeType.AT.ToString();
                cty.Value = "Österreich";                 
            }
            else
            {
                cty.CountryCode = countryType.CountryCode.ToString(); //.ConvertEnum<CountryCodeType>();
                cty.Value = Model.CountryCodes.GetFromCode(cty.CountryCode).Country;                
            }
            return cty;
        }

        private static AddressIdentifierType[] GetAddressIdentifier(List<Model.AddressIdentifierType> adrIn)
        {
            if (adrIn == null)
            {
                return null;

            }


            List<AddressIdentifierType> adIdList = new List<AddressIdentifierType>();

            foreach (Model.AddressIdentifierType addrId in adrIn)
            {
                AddressIdentifierType adId = new AddressIdentifierType
                {
                    Value = addrId.Value,
                    AddressIdentifierType1 =
                    addrId.AddressIdentifierType1.ConvertEnum<AddressIdentifierTypeType>(), //.ConvertEnum<AddressIdentifierTypeType>();
                    AddressIdentifierType1Specified = true
                };
                adIdList.Add(adId);
            }
            return adIdList.ToArray();
        }

        private static ArticleNumberType[] GetArtikelList(List<Model.ArticleNumberType> srcArticle)
        {
            List<ArticleNumberType> artNrList = new List<ArticleNumberType>();
            foreach (Model.ArticleNumberType articleNumberType in srcArticle)
            {
                ArticleNumberType art = new ArticleNumberType
                {
                    Value = articleNumberType.Value,
                    ArticleNumberType1Specified = articleNumberType.ArticleNumberType1Specified,
                    ArticleNumberType1 = articleNumberType.ArticleNumberType1.ConvertEnum<ArticleNumberTypeType>()
                };
                artNrList.Add(art);

            }
            return artNrList.ToArray();
        }

        private static FurtherIdentificationType[] GetFurtherIdentification(List<Model.FurtherIdentificationType> furtherID)
        {
            List<FurtherIdentificationType> fIdList = new List<FurtherIdentificationType>();
            if (furtherID == null)
            {
                return fIdList.ToArray();
            }

            List<string> supportedIds = Enum.GetNames(typeof(Model.FurtherIdentificationType.SupportedIds)).ToList();
            foreach (Model.FurtherIdentificationType item in furtherID)
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
            return fIdList.ToArray();
        }

    }
}
