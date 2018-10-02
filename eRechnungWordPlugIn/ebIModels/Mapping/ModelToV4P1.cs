using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ebIModels.Schema;
using V4P1 = ebIModels.Schema.ebInterface4p1;
using ExtensionMethods;
using VM = ebIModels.Models;

namespace ebIModels.Mapping.V4p1
{
    public static partial class MapInvoice
    {
        /// <summary>
        /// Mapped InvoiceType Model auf ebInterface4p1
        /// </summary>
        /// <param name="source">Invoice Model</param>
        /// <returns>ebInterface 4p1 InvoiceType</returns>
        internal static V4P1.InvoiceType MapModelToV4p1(VM.IInvoiceModel source)
        {
            V4P1.InvoiceType inv4P1 = new V4P1.InvoiceType(); // new V4P1.InvoiceType();

            #region Rechnungskopf
            inv4P1.InvoiceSubtype = source.InvoiceSubtype;
            inv4P1.InvoiceNumber = source.InvoiceNumber;
            inv4P1.InvoiceDate = source.InvoiceDate;
            inv4P1.GeneratingSystem = source.GeneratingSystem;
            inv4P1.DocumentType = source.DocumentType.ConvertEnum<V4P1.DocumentTypeType>();
            inv4P1.DocumentTitle = source.DocumentTitle;
            inv4P1.InvoiceCurrency = source.InvoiceCurrency.ConvertEnum<V4P1.CurrencyType>();
            inv4P1.Language = source.Language.ConvertEnum<V4P1.LanguageType>();
            inv4P1.Comment = source.Comment;
            inv4P1.CancelledOriginalDocument = null;
            if (source.CancelledOriginalDocument != null)
            {
                inv4P1.CancelledOriginalDocument = new V4P1.CancelledOriginalDocumentType()
                {
                    Comment = source.CancelledOriginalDocument.Comment,
                    DocumentType = source.CancelledOriginalDocument.DocumentType.ConvertEnum<V4P1.DocumentTypeType>(),
                    DocumentTypeSpecified = source.CancelledOriginalDocument.DocumentTypeSpecified,
                    InvoiceDate = source.CancelledOriginalDocument.InvoiceDate,
                    InvoiceNumber = source.CancelledOriginalDocument.InvoiceNumber

                };
            }
            #endregion

            #region Related Document
            if (source.RelatedDocument.Any())
            {
                inv4P1.RelatedDocument = new V4P1.RelatedDocumentType[]
                {
                    new V4P1.RelatedDocumentType()
                    {
                    Comment = source.RelatedDocument[0].Comment,
                    DocumentType = source.RelatedDocument[0].DocumentType.ConvertEnum<V4P1.DocumentTypeType>(),
                    DocumentTypeSpecified = source.RelatedDocument[0].DocumentTypeSpecified,
                    InvoiceDate = source.RelatedDocument[0].InvoiceDate,
                    InvoiceDateSpecified = source.RelatedDocument[0].InvoiceDateSpecified,
                    InvoiceNumber = source.RelatedDocument[0].InvoiceNumber
                        
                    }, 
                };
            }
            #endregion

            #region Delivery
            if (source.Delivery.Item is VM.PeriodType)
            {
                var delType = (VM.PeriodType)source.Delivery.Item;
                if (delType.ToDate != null)
                {
                    var deliveryType = new V4P1.PeriodType();
                    deliveryType.FromDate = delType.FromDate;
                    deliveryType.ToDate = delType.ToDate;
                    inv4P1.Delivery.Item = deliveryType;
                }
                else
                {
                    inv4P1.Delivery.Item = delType.FromDate;
                }
            }
            else
            {
                inv4P1.Delivery.Item = source.Delivery.Item;
            }
            #endregion

            #region Biller
            if (source.Biller != null)
            {
                inv4P1.Biller = new V4P1.BillerType();
                inv4P1.Biller.VATIdentificationNumber = source.Biller.VATIdentificationNumber;
                inv4P1.Biller.InvoiceRecipientsBillerID = source.Biller.InvoiceRecipientsBillerID;
                inv4P1.Biller.Address = GetAddress(source.Biller.Address);
                inv4P1.Biller.FurtherIdentification = GetFurtherIdentification(source.Biller.FurtherIdentification);
            }
            #endregion

            #region InvoiceReceipient
            if (source.InvoiceRecipient != null)
            {
                inv4P1.InvoiceRecipient = new V4P1.InvoiceRecipientType();
                inv4P1.InvoiceRecipient.BillersInvoiceRecipientID = source.InvoiceRecipient.BillersInvoiceRecipientID;
                inv4P1.InvoiceRecipient.VATIdentificationNumber = source.InvoiceRecipient.VATIdentificationNumber;
                inv4P1.InvoiceRecipient.Address = GetAddress(source.InvoiceRecipient.Address);
                inv4P1.InvoiceRecipient.OrderReference = new V4P1.OrderReferenceType();
                inv4P1.InvoiceRecipient.OrderReference.OrderID = source.InvoiceRecipient.OrderReference.OrderID;
                inv4P1.InvoiceRecipient.OrderReference.ReferenceDateSpecified = source.InvoiceRecipient.OrderReference.ReferenceDateSpecified;
                inv4P1.InvoiceRecipient.OrderReference.ReferenceDate = source.InvoiceRecipient.OrderReference.ReferenceDate;
                inv4P1.InvoiceRecipient.AccountingArea = source.InvoiceRecipient.AccountingArea;
                inv4P1.InvoiceRecipient.SubOrganizationID = source.InvoiceRecipient.SubOrganizationID;
                inv4P1.InvoiceRecipient.FurtherIdentification = GetFurtherIdentification(source.InvoiceRecipient.FurtherIdentification);
            }
            #endregion

            #region Details
            inv4P1.Details = new V4P1.DetailsType();
            inv4P1.Details.HeaderDescription = source.Details.HeaderDescription;
            inv4P1.Details.FooterDescription = source.Details.FooterDescription;

            // inv4P1.Details.ItemList = new List<InvV4p1.ItemListType>();
            var detailsItemList = new List<V4P1.ItemListType>();
            // inv4P1.Details.ItemList.Clear();

            // InvV4p1.ItemListType item = new InvV4p1.ItemListType();

            foreach (VM.ItemListType srcItemList in source.Details.ItemList)
            {
                V4P1.ItemListType itemList = new V4P1.ItemListType();

                var itemListLineItem = new List<V4P1.ListLineItemType>();
                foreach (VM.ListLineItemType srcLineItem in srcItemList.ListLineItem)
                {
                    V4P1.ListLineItemType lineItem = new V4P1.ListLineItemType();

                    lineItem.PositionNumber = srcLineItem.PositionNumber;
                    lineItem.Description = srcLineItem.Description.ToArray();
                    lineItem.AdditionalInformation = null;

                    lineItem.ArticleNumber = GetArtikelList(srcLineItem.ArticleNumber);

                    // Menge
                    lineItem.Quantity = new V4P1.UnitType();
                    lineItem.Quantity.Unit = srcLineItem.Quantity.Unit;
                    lineItem.Quantity.Value = srcLineItem.Quantity.Value;

                    // Einzelpreis
                    lineItem.UnitPrice = new V4P1.UnitPriceType()
                    {
                        Value = srcLineItem.UnitPrice.Value
                    };

                    // Steuer
                    // lineItem.Item = srcLineItem.Item;
                    lineItem.Item = MapVatItemType(srcLineItem.Item);
                    // Auftragsreferenz
                    if (!string.IsNullOrEmpty(srcLineItem.InvoiceRecipientsOrderReference.OrderPositionNumber) ||
                        source.InvoiceRecipient.BestellPositionErforderlich)   // Orderposition angegeben oder erforderlich
                    {
                        lineItem.InvoiceRecipientsOrderReference.OrderID = source.InvoiceRecipient.OrderReference.OrderID;
                    lineItem.InvoiceRecipientsOrderReference.OrderPositionNumber =
                        srcLineItem.InvoiceRecipientsOrderReference.OrderPositionNumber;
                    }

                    // Rabatte / Zuschläge
                    lineItem.DiscountFlagSpecified = false;
                    if (srcLineItem.ReductionAndSurchargeListLineItemDetails != null)
                    {
                        lineItem.ReductionAndSurchargeListLineItemDetails = GetReductionDetails(srcLineItem.ReductionAndSurchargeListLineItemDetails);

                        // Kein DIscount Flag, da das im Word PlugIn sowieso nicht unterstützt ist.
                        //lineItem.DiscountFlag = srcLineItem.DiscountFlag;
                        //lineItem.DiscountFlagSpecified = srcLineItem.DiscountFlagSpecified;
                    }
                    lineItem.Description = srcLineItem.Description.ToArray();
                    lineItem.LineItemAmount = srcLineItem.LineItemAmount;
                    itemListLineItem.Add(lineItem);
                }
                itemList.ListLineItem = itemListLineItem.ToArray();
                detailsItemList.Add(itemList);
            }
            inv4P1.Details.ItemList = detailsItemList.ToArray();
            if (source.Details.BelowTheLineItem != null)
            {
                if (source.Details.BelowTheLineItem.Count > 0)
                {
                    List<V4P1.BelowTheLineItemType> belowItems = new List<V4P1.BelowTheLineItemType>();
                    foreach (VM.BelowTheLineItemType item in source.Details.BelowTheLineItem)
                    {
                        
                        if (!string.IsNullOrEmpty(item.Description))
                        {
                            belowItems.Add(new V4P1.BelowTheLineItemType()
                    {
                        Description = item.Description,
                        LineItemAmount = item.LineItemAmount
                    });
                            
                        }
                    }
                    if (belowItems.Any())
                    {
                        inv4P1.Details.BelowTheLineItem = belowItems.ToArray();
                    }
                }
            }
            #endregion

            #region Tax
            var taxVATList = new List<V4P1.VATItemType>();
            foreach (var vatItem in source.Tax.VAT)
            {
                V4P1.VATItemType vatItemNeu = new V4P1.VATItemType()
                {
                    Amount = vatItem.Amount.GetValueOrDefault(),
                    TaxedAmount = vatItem.TaxedAmount.GetValueOrDefault(),
                };

                vatItemNeu.Item = MapVatItemType(vatItem.Item);
                taxVATList.Add(vatItemNeu);
            }
            inv4P1.Tax.VAT = taxVATList.ToArray();
            #endregion

            #region Global ReductionANdSurcharge
            inv4P1.ReductionAndSurchargeDetails = new V4P1.ReductionAndSurchargeDetailsType();
            inv4P1.ReductionAndSurchargeDetails.Items = null;
            inv4P1.ReductionAndSurchargeDetails.ItemsElementName = new V4P1.ItemsChoiceType1[]
            {
                V4P1.ItemsChoiceType1.Reduction 
            };

            #endregion

            #region Amount
            inv4P1.TotalGrossAmount = source.TotalGrossAmount;
            inv4P1.PaymentMethod.Comment = source.PaymentMethod.Comment;
            inv4P1.PayableAmount = source.PayableAmount;
            #endregion

            #region PaymentMethod
            inv4P1.PaymentMethod.Comment = source.PaymentMethod.Comment;
            if (source.PaymentMethod.Item.GetType() == typeof(VM.UniversalBankTransactionType))
            {
                VM.UniversalBankTransactionType txType = source.PaymentMethod.Item as VM.UniversalBankTransactionType;

                inv4P1.PaymentMethod = new V4P1.PaymentMethodType();
                inv4P1.PaymentMethod.Item = new V4P1.UniversalBankTransactionType();
                ((V4P1.UniversalBankTransactionType)inv4P1.PaymentMethod.Item).BeneficiaryAccount = new V4P1.AccountType[]
                {
                    new V4P1.AccountType()
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
            inv4P1.PaymentConditions.DueDate = source.PaymentConditions.DueDate;
            if (source.PaymentConditions.Discount != null)
            {
                // inv4P1.PaymentConditions.Discount.Clear();
                var discountList = new List<V4P1.DiscountType>();
                foreach (VM.DiscountType srcDiscount in source.PaymentConditions.Discount)
                {
                    V4P1.DiscountType discount = new V4P1.DiscountType()
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
                inv4P1.PaymentConditions.Discount = discountList.ToArray();
            }
            #endregion

            // Fixe Einträge
            #region PresentationDetails
            inv4P1.PresentationDetails = new V4P1.PresentationDetailsType()
            {
                URL = "http://www.austriapro.at",
                SuppressZero = true,
                SuppressZeroSpecified = true
            };
            #endregion
            return inv4P1;
        }

        private static V4P1.ReductionAndSurchargeListLineItemDetailsType GetReductionDetails(VM.ReductionAndSurchargeListLineItemDetailsType srcRed)
        {
            if (srcRed == null)
                return null;
            V4P1.ReductionAndSurchargeListLineItemDetailsType lineRed = new V4P1.ReductionAndSurchargeListLineItemDetailsType();
            lineRed.Items = new object[srcRed.Items.Count];
            int i = 0;

            foreach (var item1 in srcRed.Items)
            {
                if (item1 is VM.ReductionAndSurchargeBaseType)
                {
                    VM.ReductionAndSurchargeBaseType item = item1 as VM.ReductionAndSurchargeBaseType;
                    V4P1.ReductionAndSurchargeBaseType redBase = new V4P1.ReductionAndSurchargeBaseType();
                    redBase.Amount = item.Amount;
                    redBase.AmountSpecified = item.AmountSpecified;
                    redBase.BaseAmount = item.BaseAmount;
                    redBase.Comment = item.Comment;
                    redBase.Percentage = item.Percentage;
                    redBase.PercentageSpecified = item.PercentageSpecified;
                    lineRed.Items[i] = redBase;
                    i++;
                }
            }

            lineRed.ItemsElementName = new V4P1.ItemsChoiceType[srcRed.ItemsElementName.Count()];
            i = 0;
            foreach (
                VM.ItemsChoiceType choiceType in
                    srcRed.ItemsElementName)
            {
                lineRed.ItemsElementName[i] =
                    (choiceType.ConvertEnum<V4P1.ItemsChoiceType>());
                i++;
            }
            return lineRed;
        }

        private static object MapVatItemType(object vatItem)
        {
            if (vatItem==null)
            {
                return null;
            }
            if (vatItem is VM.TaxExemptionType)
            {
                var taxexNew = new V4P1.TaxExemptionType();
                var taxex = vatItem as VM.TaxExemptionType;
                taxexNew.TaxExemptionCode = taxex.TaxExemptionCode;
                taxexNew.Value = taxex.Value;
                return taxexNew;
            }
            else
            {
                var taxexNew = new V4P1.VATRateType();
                var taxex = vatItem as VM.VATRateType;
                taxexNew.TaxCode = taxex.TaxCode;
                taxexNew.Value = taxex.Value;
                return taxexNew;
            }

        }

        private static V4P1.AddressType GetAddress(VM.AddressType address)
        {

            if (address == null)
            {
                return null;
            }
            V4P1.AddressType addrNew = new V4P1.AddressType();
            addrNew.Name = address.Name;
            addrNew.Contact = address.Contact;    
            addrNew.Email = address.Email;
            addrNew.Phone = address.Phone;
            addrNew.Salutation = address.Salutation;
            addrNew.Street = address.Street;
            addrNew.Country = GetCountry(address.Country);
            addrNew.ZIP = address.ZIP;
            addrNew.Town = address.Town;
            addrNew.AddressIdentifier = GetAddressIdentifier(address.AddressIdentifier);
            return addrNew;
        }

        private static V4P1.CountryType GetCountry(VM.CountryType countryType)
        {
            if (countryType == null)
                return null;
            V4P1.CountryType cty = new V4P1.CountryType();
            if (countryType.Text == null)
            {
                cty.CountryCode = V4P1.CountryCodeType.AT;
                cty.CountryCodeSpecified = true;
                cty.Text = new string[] { "Österreich" };

            }
            else
            {
                cty.CountryCode = countryType.CountryCode.ConvertEnum<V4P1.CountryCodeType>();
                cty.CountryCodeSpecified = true; // This is always true!
                cty.Text = countryType.Text.ToArray();
            }
            return cty;
        }

        private static V4P1.AddressIdentifierType[] GetAddressIdentifier(List<VM.AddressIdentifierType> adrIn)
        {
            if (adrIn == null)
            {
                return null;

            }


            List<V4P1.AddressIdentifierType> adIdList = new List<V4P1.AddressIdentifierType>();

            foreach (VM.AddressIdentifierType addrId in adrIn)
            {
                V4P1.AddressIdentifierType adId = new V4P1.AddressIdentifierType();
                adId.Value = addrId.Value;
                adId.AddressIdentifierType1 =
                    addrId.AddressIdentifierType1.ConvertEnum<V4P1.AddressIdentifierTypeType>();
                adId.AddressIdentifierType1Specified = addrId.AddressIdentifierType1Specified;
                adIdList.Add(adId);
            }
            return adIdList.ToArray();
        }

        private static V4P1.ArticleNumberType[] GetArtikelList(List<VM.ArticleNumberType> srcArticle)
        {
            List<V4P1.ArticleNumberType> artNrList = new List<V4P1.ArticleNumberType>();
            foreach (VM.ArticleNumberType articleNumberType in srcArticle)
            {
                V4P1.ArticleNumberType art = new V4P1.ArticleNumberType();
                art.Text = articleNumberType.Text.ToArray();
                art.ArticleNumberType1Specified = articleNumberType.ArticleNumberType1Specified;
                art.ArticleNumberType1 = articleNumberType.ArticleNumberType1.ConvertEnum<V4P1.ArticleNumberTypeType>();
                artNrList.Add(art);

            }
            return artNrList.ToArray();
        }

        private static V4P1.FurtherIdentificationType[] GetFurtherIdentification(List<VM.FurtherIdentificationType> furtherID)
        {
            List<V4P1.FurtherIdentificationType> fIdList = new List<V4P1.FurtherIdentificationType>();
            if (furtherID == null)
            {
                return fIdList.ToArray();
            }

            List<string> supportedIds = Enum.GetNames(typeof(VM.FurtherIdentificationType.SupportedIds)).ToList();
            foreach (VM.FurtherIdentificationType item in furtherID)
            {
                if (supportedIds.Contains(item.IdentificationType))
                {
                    V4P1.FurtherIdentificationType fId = new V4P1.FurtherIdentificationType()
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
