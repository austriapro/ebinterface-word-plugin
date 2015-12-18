using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;
using ebIModels.Models;
using V4P1 = ebIModels.Schema.ebInterface4p1;

namespace ebIModels.Mapping
{
    public static class MappingService4p1ToVm
    {
        /// <summary>
        /// Mapped ebInterface4p1 InvoiceType auf InvoiceType Model
        /// </summary>
        /// <param name="source">ebInterface4p1 InvoiceType</param>
        /// <returns>InvoiceType Model</returns>
        public static IInvoiceType MapV4P1ToVm(V4P1.InvoiceType source)
        {
            InvoiceType Invoice = new InvoiceType();
            #region Rechnungskopf
            Invoice.InvoiceNumber = source.InvoiceNumber;
            Invoice.InvoiceDate = source.InvoiceDate;
            Invoice.GeneratingSystem = source.GeneratingSystem;
            Invoice.DocumentType = source.DocumentType.ConvertEnum<DocumentTypeType>();
            Invoice.InvoiceCurrency = source.InvoiceCurrency.ConvertEnum<CurrencyType>();
            Invoice.Language = source.Language.ConvertEnum<LanguageType>();
            Invoice.Comment = source.Comment;
            if (source.CancelledOriginalDocument == null)
            {
                Invoice.CancelledOriginalDocument = null;
            }
            else
            {
                Invoice.CancelledOriginalDocument = new CancelledOriginalDocumentType()
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
                Invoice.RelatedDocument = new List<RelatedDocumentType>();
                foreach (V4P1.RelatedDocumentType relDoc in source.RelatedDocument)
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
                    Invoice.RelatedDocument.Add(newRel);
                }
            }

            #endregion

            #region Delivery
            if (source.Delivery != null)
            {
                if (source.Delivery.Item is V4P1.PeriodType)
                {
                    var deliveryType = new PeriodType();
                    deliveryType.FromDate = ((V4P1.PeriodType)source.Delivery.Item).FromDate;
                    deliveryType.ToDate = ((V4P1.PeriodType)source.Delivery.Item).ToDate;
                    Invoice.Delivery.Item = deliveryType;
                }
                else
                {
                    // Invoice.Delivery.Item = source.Delivery.Item;
                    var period = new PeriodType();
                    if (source.Delivery.Item != null)
                    {
                        period.FromDate = (DateTime)source.Delivery.Item;
                    }
                    Invoice.Delivery.Item = period;    // für das Model immer eine Lieferperiode, damit von/bis leichter abgebildet werden kann
                }
            }
            #endregion

            #region Biller
            Invoice.Biller.VATIdentificationNumber = source.Biller.VATIdentificationNumber;
            Invoice.Biller.InvoiceRecipientsBillerID = source.Biller.InvoiceRecipientsBillerID;
            Invoice.Biller.Address.Name = source.Biller.Address.Name;
            Invoice.Biller.Address.Street = source.Biller.Address.Street;
            Invoice.Biller.Address.Town = source.Biller.Address.Town;
            Invoice.Biller.Address.ZIP = source.Biller.Address.ZIP;
            Invoice.Biller.Address.Email = source.Biller.Address.Email;
            Invoice.Biller.Address.Country.CountryCode =
                source.Biller.Address.Country.CountryCode.ConvertEnum<CountryCodeType>();
            Invoice.Biller.Address.Country.CountryCodeSpecified = source.Biller.Address.Country.CountryCodeSpecified;
            if (source.Biller.Address.Country.Text != null)
            {
                Invoice.Biller.Address.Country.Text =
                    new List<string>(source.Biller.Address.Country.Text.ToList());
            }
            List<string> list = new List<string>();
            if (source.Biller.Address.Country.Text != null)
            {
                foreach (string s in source.Biller.Address.Country.Text)
                    list.Add(s);
                Invoice.Biller.Address.Country.Text = list;
            }
            Invoice.Biller.Address.AddressIdentifier = GetAddressIdentifier(source.Biller.Address.AddressIdentifier);
            Invoice.Biller.FurtherIdentification = GetFurtherIdentification(source.Biller.FurtherIdentification);

            #endregion

            #region InvoiceRecipient
            Invoice.InvoiceRecipient.BillersInvoiceRecipientID = source.InvoiceRecipient.BillersInvoiceRecipientID;
            Invoice.InvoiceRecipient.VATIdentificationNumber = source.InvoiceRecipient.VATIdentificationNumber;
            Invoice.InvoiceRecipient.Address.Name = source.InvoiceRecipient.Address.Name;
            Invoice.InvoiceRecipient.Address.Contact = source.InvoiceRecipient.Address.Contact;
            Invoice.InvoiceRecipient.Address.Email = source.InvoiceRecipient.Address.Email;
            Invoice.InvoiceRecipient.Address.Salutation = source.InvoiceRecipient.Address.Salutation;
            Invoice.InvoiceRecipient.Address.Street = source.InvoiceRecipient.Address.Street;
            Invoice.InvoiceRecipient.Address.Country.CountryCode =
                source.InvoiceRecipient.Address.Country.CountryCode.ConvertEnum<CountryCodeType>();
            if (source.InvoiceRecipient.Address.Country.Text != null)
            {
                Invoice.InvoiceRecipient.Address.Country.Text =
                    new List<string>(source.InvoiceRecipient.Address.Country.Text.ToList());
            }
            Invoice.InvoiceRecipient.Address.AddressIdentifier = GetAddressIdentifier(source.InvoiceRecipient.Address.AddressIdentifier);
            Invoice.InvoiceRecipient.Address.ZIP = source.InvoiceRecipient.Address.ZIP;
            Invoice.InvoiceRecipient.Address.Town = source.InvoiceRecipient.Address.Town;

            Invoice.InvoiceRecipient.OrderReference.OrderID = source.InvoiceRecipient.OrderReference.OrderID;
            Invoice.InvoiceRecipient.FurtherIdentification = GetFurtherIdentification(source.InvoiceRecipient.FurtherIdentification);
            #endregion

            #region Details
            Invoice.Details.HeaderDescription = source.Details.HeaderDescription;
            Invoice.Details.FooterDescription = source.Details.FooterDescription;

            Invoice.Details.ItemList = new List<ItemListType>();

            if (source.Details.ItemList != null)
                foreach (V4P1.ItemListType srcItemList in source.Details.ItemList)
                {
                    ItemListType item = new ItemListType();
                    item.ListLineItem = new List<ListLineItemType>();
                    foreach (V4P1.ListLineItemType srcLineItem in srcItemList.ListLineItem)
                    {
                        ListLineItemType lineItem = new ListLineItemType();
                        lineItem.AdditionalInformation = null;
                        lineItem.PositionNumber = srcLineItem.PositionNumber;
                        lineItem.Description = new List<string>();
                        if (srcLineItem.Description != null)
                        {
                            lineItem.Description = srcLineItem.Description.ToList();
                        }

                        lineItem.ArticleNumber = GetArtikelList(srcLineItem.ArticleNumber);

                        // Menge
                        lineItem.Quantity = new UnitType();
                        lineItem.Quantity.Unit = srcLineItem.Quantity.Unit;
                        lineItem.Quantity.Value = srcLineItem.Quantity.Value;

                        // Einzelpreis
                        lineItem.UnitPrice = new UnitPriceType()
                        {
                            Value = srcLineItem.UnitPrice.Value
                        };

                        // Steuer
                        lineItem.Item = MapVatItemType2Vm(srcLineItem.Item);
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
                        lineItem.DiscountFlag = srcLineItem.DiscountFlag;
                        lineItem.DiscountFlagSpecified = srcLineItem.DiscountFlagSpecified;

                        lineItem.LineItemAmount = srcLineItem.LineItemAmount;
                        item.ListLineItem.Add(lineItem);
                    }
                    Invoice.Details.ItemList.Add(item);
                }

            if (source.Details.BelowTheLineItem != null)
            {
                if (source.Details.BelowTheLineItem.Length > 0)
                {
                    List<BelowTheLineItemType> belowItems = new List<BelowTheLineItemType>();
                    foreach (V4P1.BelowTheLineItemType item in source.Details.BelowTheLineItem)
                    {
                        belowItems.Add(new BelowTheLineItemType()
                        {
                            Description = item.Description,
                            LineItemAmount = item.LineItemAmount
                        });
                    }
                    Invoice.Details.BelowTheLineItem.AddRange(belowItems);
                }
            } 
            #endregion

            #region Tax
            Invoice.Tax.VAT.Clear();

            Invoice.Tax.VAT = new List<VATItemType>();
            if (source.Tax.VAT != null)
                foreach (var vatItem in source.Tax.VAT)
                {

                    VATItemType vatItemNeu = new VATItemType()
                    {
                        Amount = vatItem.Amount,
                        TaxedAmount = vatItem.TaxedAmount,
                        Item = MapVatItemType2Vm(vatItem.Item)
                    };
                    Invoice.Tax.VAT.Add(vatItemNeu);
                }
            #endregion

            #region Amount
            Invoice.TotalGrossAmount = source.TotalGrossAmount;
            Invoice.PayableAmount = source.PayableAmount;
            #endregion

            #region PaymentMethod
            Invoice.PaymentMethod.Comment = source.PaymentMethod.Comment;

            if (source.PaymentMethod.Item != null && source.PaymentMethod.Item.GetType() == typeof(V4P1.UniversalBankTransactionType))
            {
                V4P1.UniversalBankTransactionType txType = source.PaymentMethod.Item as V4P1.UniversalBankTransactionType;
                Invoice.PaymentMethod = new PaymentMethodType();
                Invoice.PaymentMethod.Item = new UniversalBankTransactionType();
                ((UniversalBankTransactionType)Invoice.PaymentMethod.Item).BeneficiaryAccount = new List<AccountType>()
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
            Invoice.PaymentConditions.DueDate = source.PaymentConditions.DueDate;
            if (source.PaymentConditions.Discount != null)
            {
                Invoice.PaymentConditions.Discount.Clear();
                foreach (V4P1.DiscountType srcDiscount in source.PaymentConditions.Discount)
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
                    Invoice.PaymentConditions.Discount.Add(discount);
                }
            }

            #endregion
            return Invoice;
        }
        private static ReductionAndSurchargeListLineItemDetailsType GetReductionDetails(V4P1.ReductionAndSurchargeListLineItemDetailsType srcRed)
        {
            if (srcRed.Items == null)
                return null;
            ReductionAndSurchargeListLineItemDetailsType lineRed = new ReductionAndSurchargeListLineItemDetailsType();
            //lineRed.Items = new object[srcRed.Items.Length];

            foreach (var item1 in srcRed.Items)
            {
                if (item1 is V4P1.ReductionAndSurchargeBaseType)
                {
                    V4P1.ReductionAndSurchargeBaseType item = item1 as V4P1.ReductionAndSurchargeBaseType;
                    ReductionAndSurchargeBaseType redBase = new ReductionAndSurchargeBaseType();
                    redBase.Amount = item.Amount;
                    redBase.AmountSpecified = item.AmountSpecified;
                    redBase.BaseAmount = item.BaseAmount;
                    redBase.Comment = item.Comment;
                    redBase.Percentage = item.Percentage;
                    redBase.PercentageSpecified = item.PercentageSpecified;
                    lineRed.Items.Add(redBase);
                }
            }
            lineRed.ItemsElementName = new List<ItemsChoiceType>();
            foreach (
                V4P1.ItemsChoiceType choiceType in
                    srcRed.ItemsElementName)
            {
                lineRed.ItemsElementName.Add(choiceType.ConvertEnum<ItemsChoiceType>());
            }
            return lineRed;
        }

        private static object MapVatItemType2Vm(object vatItem)
        {
            if (vatItem == null)
                return null;
            if (vatItem is V4P1.TaxExemptionType)
            {
                var taxexNew = new TaxExemptionType();
                var taxex = vatItem as V4P1.TaxExemptionType;
                taxexNew.TaxExemptionCode = taxex.TaxExemptionCode;
                taxexNew.Value = taxex.Value;
                return taxexNew;
            }
            else
            {
                var taxexNew = new VATRateType();
                var taxex = vatItem as V4P1.VATRateType;
                taxexNew.TaxCode = taxex.TaxCode;
                taxexNew.Value = taxex.Value;
                return taxexNew;
            }

        }

        private static List<ArticleNumberType> GetArtikelList(V4P1.ArticleNumberType[] srcArticle)
        {
            List<ArticleNumberType> artNrList = new List<ArticleNumberType>();
            foreach (V4P1.ArticleNumberType articleNumberType in srcArticle)
            {
                ArticleNumberType art = new ArticleNumberType();
                art.Text = new List<string>(articleNumberType.Text.ToList());
                art.ArticleNumberType1Specified = articleNumberType.ArticleNumberType1Specified;
                art.ArticleNumberType1 = articleNumberType.ArticleNumberType1.ConvertEnum<ArticleNumberTypeType>();
                artNrList.Add(art);

            }
            return artNrList;
        }
        private static List<AddressIdentifierType> GetAddressIdentifier(V4P1.AddressIdentifierType[] adrIn)
        {
            if (adrIn == null)
            {
                return null;

            }

            List<AddressIdentifierType> adrOutList = new List<AddressIdentifierType>();
            foreach (V4P1.AddressIdentifierType item in adrIn)
            {
                AddressIdentifierType adId = new AddressIdentifierType();

                if (item.AddressIdentifierType1Specified)
                {
                    adId.AddressIdentifierType1 =
                (AddressIdentifierTypeType)Enum.Parse(typeof(AddressIdentifierTypeType),
                    item.AddressIdentifierType1.ToString());
                    adId.AddressIdentifierType1Specified = true;

                } 
                adId.Value = item.Value;
                adrOutList.Add(adId);
            }
            return adrOutList;
        }
        private static List<FurtherIdentificationType> GetFurtherIdentification(V4P1.FurtherIdentificationType[] furtherID)
        {
            List<FurtherIdentificationType> fIdList = new List<FurtherIdentificationType>();
            if (furtherID == null)
            {
                return fIdList;
            }

            List<string> supportedIds = Enum.GetNames(typeof(FurtherIdentificationType.SupportedIds)).ToList();
            foreach (V4P1.FurtherIdentificationType item in furtherID)
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
