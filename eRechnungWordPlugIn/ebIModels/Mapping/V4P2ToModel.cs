using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;
using ebIModels.Models;
using V4P2 = ebIModels.Schema.ebInterface4p2;

namespace ebIModels.Mapping
{
    public static class MappingService4p2ToVm
    {
        /// <summary>
        /// Mapped ebInterface4p2 InvoiceType auf InvoiceType Model
        /// </summary>
        /// <param name="source">ebInterface4p2 InvoiceType</param>
        /// <returns>InvoiceType Model</returns>
        public static IInvoiceType MapV4P2ToVm(V4P2.InvoiceType source)
        {
            InvoiceType Invoice = new InvoiceType();
            #region Rechnungskopf
            Invoice.InvoiceNumber = source.InvoiceNumber;
            Invoice.InvoiceDate = source.InvoiceDate;
            Invoice.GeneratingSystem = source.GeneratingSystem;
            Invoice.DocumentTitle = source.DocumentTitle;
            Invoice.DocumentType = source.DocumentType.ConvertEnum<DocumentTypeType>();
            Invoice.InvoiceCurrency = source.InvoiceCurrency.ToEnum(CurrencyType.EUR); // source.InvoiceCurrency.ConvertEnum<CurrencyType>();
            if (!string.IsNullOrEmpty(source.Language))
            {
                Invoice.Language = source.Language.ToEnum(LanguageType.ger);  //source.Language.ConvertEnum<LanguageType>();
                Invoice.LanguageSpecified = true;
            }
            else
            {
                Invoice.LanguageSpecified = false;
                Invoice.Language = LanguageType.ger;
            }
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
                foreach (V4P2.RelatedDocumentType relDoc in source.RelatedDocument)
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
                if (source.Delivery.Item is V4P2.PeriodType)
                {
                    var deliveryType = new PeriodType();
                    deliveryType.FromDate = ((V4P2.PeriodType)source.Delivery.Item).FromDate;
                    deliveryType.ToDate = ((V4P2.PeriodType)source.Delivery.Item).ToDate;
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
            Invoice.Biller.Address = GetAddress(source.Biller.Address);
            Invoice.Biller.FurtherIdentification = GetFurtherIdentification(source.Biller.FurtherIdentification);

            #endregion

            #region InvoiceRecipient
            Invoice.InvoiceRecipient.BillersInvoiceRecipientID = source.InvoiceRecipient.BillersInvoiceRecipientID;
            Invoice.InvoiceRecipient.VATIdentificationNumber = source.InvoiceRecipient.VATIdentificationNumber;
            Invoice.InvoiceRecipient.Address = GetAddress(source.InvoiceRecipient.Address);

            Invoice.InvoiceRecipient.OrderReference.OrderID = source.InvoiceRecipient.OrderReference.OrderID;
            Invoice.InvoiceRecipient.FurtherIdentification = GetFurtherIdentification(source.InvoiceRecipient.FurtherIdentification);
            #endregion

            #region Details
            Invoice.Details.HeaderDescription = source.Details.HeaderDescription;
            Invoice.Details.FooterDescription = source.Details.FooterDescription;

            Invoice.Details.ItemList = new List<ItemListType>();

            if (source.Details.ItemList != null)
                foreach (V4P2.ItemListType srcItemList in source.Details.ItemList)
                {
                    ItemListType item = new ItemListType();
                    item.ListLineItem = new List<ListLineItemType>();
                    foreach (V4P2.ListLineItemType srcLineItem in srcItemList.ListLineItem)
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
                    foreach (V4P2.BelowTheLineItemType item in source.Details.BelowTheLineItem)
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

            if (source.PaymentMethod.Item != null && source.PaymentMethod.Item.GetType() == typeof(V4P2.UniversalBankTransactionType))
            {
                V4P2.UniversalBankTransactionType txType = source.PaymentMethod.Item as V4P2.UniversalBankTransactionType;
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
                foreach (V4P2.DiscountType srcDiscount in source.PaymentConditions.Discount)
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
        private static AddressType GetAddress(V4P2.AddressType address)
        {

            if (address == null)
            {
                return null;
            }
            AddressType addrNew = new AddressType();
            addrNew.Name = address.Name;
            addrNew.Contact = address.Contact;
            addrNew.Phone = address.Phone;
            addrNew.POBox = address.POBox;
            addrNew.Email = address.Email;
            addrNew.Salutation = address.Salutation;
            addrNew.Street = address.Street;
            addrNew.Country = GetCountry(address.Country);
            addrNew.ZIP = address.ZIP;
            addrNew.Town = address.Town;
            addrNew.AddressIdentifier = GetAddressIdentifier(address.AddressIdentifier);
            return addrNew;
        }
        private static CountryType GetCountry(V4P2.CountryType countryType)
        {
            if (countryType == null)
                return null;
            CountryType cty = new CountryType();
            if (countryType.Value == null)
            {
                cty.CountryCode = CountryCodeType.AT;
                //cty.CountryCodeSpecified = true;
                //cty.Text = new string[] { "Österreich" };
                cty.Text = new List<string>(){ "Österreich"};
            }
            else
            {
                cty.CountryCode = countryType.CountryCode.ToEnum(CountryCodeType.AT); //.ConvertEnum<V4P2.CountryCodeType>();
                //cty.CountryCodeSpecified = true; // This is always true!
                //cty.Text = countryType.Text.ToArray();
                if (!string.IsNullOrEmpty(countryType.Value))
                {
                    cty.Text = new List<string>() { countryType.Value };
                }
                else
                {
                    cty.Text = new List<string>() { CountryCodes.GetFromCode(cty.CountryCode.ToString()).Country };
                }

            }
            return cty;
        }
        private static ReductionAndSurchargeListLineItemDetailsType GetReductionDetails(V4P2.ReductionAndSurchargeListLineItemDetailsType srcRed)
        {
            if (srcRed.Items == null)
                return null;
            ReductionAndSurchargeListLineItemDetailsType lineRed = new ReductionAndSurchargeListLineItemDetailsType();
            //lineRed.Items = new object[srcRed.Items.Length];

            foreach (var item1 in srcRed.Items)
            {
                if (item1 is V4P2.ReductionAndSurchargeBaseType)
                {
                    V4P2.ReductionAndSurchargeBaseType item = item1 as V4P2.ReductionAndSurchargeBaseType;
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
                V4P2.ItemsChoiceType choiceType in
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
            if (vatItem is V4P2.TaxExemptionType)
            {
                var taxexNew = new TaxExemptionType();
                var taxex = vatItem as V4P2.TaxExemptionType;
                taxexNew.TaxExemptionCode = taxex.TaxExemptionCode;
                taxexNew.Value = taxex.Value;
                return taxexNew;
            }
            else
            {
                var taxexNew = new VATRateType();
                var taxex = vatItem as V4P2.VATRateType;
                taxexNew.TaxCode = taxex.TaxCode;
                taxexNew.Value = taxex.Value;
                return taxexNew;
            }

        }

        private static List<ArticleNumberType> GetArtikelList(V4P2.ArticleNumberType[] srcArticle)
        {
            List<ArticleNumberType> artNrList = new List<ArticleNumberType>();
            if (srcArticle==null)
            {
                return artNrList;
            }
            foreach (V4P2.ArticleNumberType articleNumberType in srcArticle)
            {
                ArticleNumberType art = new ArticleNumberType();
                art.Text = new List<string>() { articleNumberType.Value };
                art.ArticleNumberType1Specified = articleNumberType.ArticleNumberType1Specified;
                art.ArticleNumberType1 = articleNumberType.ArticleNumberType1.ConvertEnum<ArticleNumberTypeType>();
                artNrList.Add(art);

            }
            return artNrList;
        }
        private static List<AddressIdentifierType> GetAddressIdentifier(V4P2.AddressIdentifierType[] adrIn)
        {
            if (adrIn == null)
            {
                return null;

            }

            List<AddressIdentifierType> adrOutList = new List<AddressIdentifierType>();
            foreach (V4P2.AddressIdentifierType item in adrIn)
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
        private static List<FurtherIdentificationType> GetFurtherIdentification(V4P2.FurtherIdentificationType[] furtherID)
        {
            List<FurtherIdentificationType> fIdList = new List<FurtherIdentificationType>();
            if (furtherID == null)
            {
                return fIdList;
            }

            List<string> supportedIds = Enum.GetNames(typeof(FurtherIdentificationType.SupportedIds)).ToList();
            foreach (V4P2.FurtherIdentificationType item in furtherID)
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
