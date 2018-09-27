using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ebIModels.Schema;
using V4P2 = ebIModels.Schema.ebInterface4p2;
using ExtensionMethods;
using VM = ebIModels.Models;

namespace ebIModels.Mapping
{
    public static class MappingServiceVmTo4p2
    {
        /// <summary>
        /// Mapped InvoiceType Model auf ebInterface4p1
        /// </summary>
        /// <param name="source">Invoice Model</param>
        /// <returns>ebInterface 4p1 InvoiceType</returns>
        public static V4P2.InvoiceType MapModelToV4p2(VM.IInvoiceModel source)
        {
            V4P2.InvoiceType inv4P2 = InvoiceFactory.CreateInvoice(InvoiceModel.ebIVersion.V4P2) as V4P2.InvoiceType; // new V4P1.InvoiceType();

            #region Rechnungskopf
            inv4P2.InvoiceSubtype = source.InvoiceSubtype;
            inv4P2.InvoiceNumber = source.InvoiceNumber;
            inv4P2.InvoiceDate = source.InvoiceDate;
            inv4P2.GeneratingSystem = source.GeneratingSystem;
            inv4P2.DocumentType = source.DocumentType.ConvertEnum<V4P2.DocumentTypeType>();
            inv4P2.DocumentTitle = source.DocumentTitle;
            inv4P2.InvoiceCurrency = source.InvoiceCurrency.ToString(); //.ConvertEnum<V4P2.CurrencyType>();
            if (source.LanguageSpecified)
            {
                inv4P2.Language = source.Language.ToString();// .ConvertEnum<V4P2.LanguageType>();

            }
            else
            {
                inv4P2.Language = null;
            }

            inv4P2.Comment = source.Comment;
            inv4P2.CancelledOriginalDocument = null;
            if (source.CancelledOriginalDocument != null)
            {
                inv4P2.CancelledOriginalDocument = new V4P2.CancelledOriginalDocumentType()
                {
                    Comment = source.CancelledOriginalDocument.Comment,
                    DocumentType = source.CancelledOriginalDocument.DocumentType.ConvertEnum<V4P2.DocumentTypeType>(),
                    DocumentTypeSpecified = source.CancelledOriginalDocument.DocumentTypeSpecified,
                    InvoiceDate = source.CancelledOriginalDocument.InvoiceDate,
                    InvoiceNumber = source.CancelledOriginalDocument.InvoiceNumber

                };
            }
            #endregion

            #region Related Document
            if (source.RelatedDocument.Any())
            {
                inv4P2.RelatedDocument = new V4P2.RelatedDocumentType[]
                {
                    new V4P2.RelatedDocumentType()
                    {
                    Comment = source.RelatedDocument[0].Comment,
                    DocumentType = source.RelatedDocument[0].DocumentType.ConvertEnum<V4P2.DocumentTypeType>(),
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
                    var deliveryType = new V4P2.PeriodType();
                    deliveryType.FromDate = delType.FromDate ?? new DateTime();
                    deliveryType.ToDate = delType.ToDate ?? new DateTime();
                    inv4P2.Delivery.Item = deliveryType;
                }
                else
                {
                    inv4P2.Delivery.Item = delType.FromDate;
                }
            }
            else
            {
                inv4P2.Delivery.Item = source.Delivery.Item;
            }
            #endregion

            #region Biller
            if (source.Biller != null)
            {
                inv4P2.Biller = new V4P2.BillerType();
                inv4P2.Biller.VATIdentificationNumber = source.Biller.VATIdentificationNumber;
                inv4P2.Biller.InvoiceRecipientsBillerID = source.Biller.InvoiceRecipientsBillerID;
                inv4P2.Biller.Address = GetAddress(source.Biller.Address);
                inv4P2.Biller.FurtherIdentification = GetFurtherIdentification(source.Biller.FurtherIdentification);
            }
            #endregion

            #region InvoiceReceipient
            if (source.InvoiceRecipient != null)
            {
                inv4P2.InvoiceRecipient = new V4P2.InvoiceRecipientType();
                inv4P2.InvoiceRecipient.BillersInvoiceRecipientID = source.InvoiceRecipient.BillersInvoiceRecipientID;
                inv4P2.InvoiceRecipient.VATIdentificationNumber = source.InvoiceRecipient.VATIdentificationNumber;
                inv4P2.InvoiceRecipient.Address = GetAddress(source.InvoiceRecipient.Address);
                inv4P2.InvoiceRecipient.OrderReference = new V4P2.OrderReferenceType();
                inv4P2.InvoiceRecipient.OrderReference.OrderID = source.InvoiceRecipient.OrderReference.OrderID;
                if (source.InvoiceRecipient.OrderReference.ReferenceDateSpecified)
                {
                    inv4P2.InvoiceRecipient.OrderReference.ReferenceDate = source.InvoiceRecipient.OrderReference.ReferenceDate;
                    inv4P2.InvoiceRecipient.OrderReference.ReferenceDateSpecified = source.InvoiceRecipient.OrderReference.ReferenceDateSpecified;
                }                
                inv4P2.InvoiceRecipient.AccountingArea = source.InvoiceRecipient.AccountingArea;
                inv4P2.InvoiceRecipient.SubOrganizationID = source.InvoiceRecipient.SubOrganizationID;
                inv4P2.InvoiceRecipient.FurtherIdentification = GetFurtherIdentification(source.InvoiceRecipient.FurtherIdentification);
            }
            #endregion

            #region Details
            inv4P2.Details = new V4P2.DetailsType();
            inv4P2.Details.HeaderDescription = source.Details.HeaderDescription;
            inv4P2.Details.FooterDescription = source.Details.FooterDescription;

            // inv4P1.Details.ItemList = new List<InvV4p1.ItemListType>();
            var detailsItemList = new List<V4P2.ItemListType>();
            // inv4P1.Details.ItemList.Clear();

            // InvV4p1.ItemListType item = new InvV4p1.ItemListType();

            foreach (VM.ItemListType srcItemList in source.Details.ItemList)
            {
                V4P2.ItemListType itemList = new V4P2.ItemListType();

                var itemListLineItem = new List<V4P2.ListLineItemType>();
                foreach (VM.ListLineItemType srcLineItem in srcItemList.ListLineItem)
                {
                    V4P2.ListLineItemType lineItem = new V4P2.ListLineItemType();

                    lineItem.PositionNumber = srcLineItem.PositionNumber;
                    lineItem.Description = srcLineItem.Description.ToArray();
                    lineItem.AdditionalInformation = null;

                    lineItem.ArticleNumber = GetArtikelList(srcLineItem.ArticleNumber);

                    // Menge
                    lineItem.Quantity = new V4P2.UnitType();
                    lineItem.Quantity.Unit = srcLineItem.Quantity.Unit;
                    lineItem.Quantity.Value = srcLineItem.Quantity.Value.GetValueOrDefault();

                    // Einzelpreis
                    lineItem.UnitPrice = new V4P2.UnitPriceType()
                    {
                        Value = srcLineItem.UnitPrice.Value.GetValueOrDefault()
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
                    lineItem.LineItemAmount = srcLineItem.LineItemAmount.GetValueOrDefault();
                    itemListLineItem.Add(lineItem);
                }
                itemList.ListLineItem = itemListLineItem.ToArray();
                detailsItemList.Add(itemList);
            }
            inv4P2.Details.ItemList = detailsItemList.ToArray();
            if (source.Details.BelowTheLineItem != null)
            {
                if (source.Details.BelowTheLineItem.Count > 0)
                {
                    List<V4P2.BelowTheLineItemType> belowItems = new List<V4P2.BelowTheLineItemType>();
                    foreach (VM.BelowTheLineItemType item in source.Details.BelowTheLineItem)
                    {
                        
                        if (!string.IsNullOrEmpty(item.Description))
                        {
                        belowItems.Add(new V4P2.BelowTheLineItemType()
                        {
                            Description = item.Description,
                            LineItemAmount = item.LineItemAmount ?? 0
                        });
                            
                        }
                    }
                    if (belowItems.Any())
                    {
                    inv4P2.Details.BelowTheLineItem = belowItems.ToArray();
                    }
                }
            }
            #endregion

            #region Tax
            var taxVATList = new List<V4P2.VATItemType>();
            foreach (var vatItem in source.Tax.VAT)
            {
                V4P2.VATItemType vatItemNeu = new V4P2.VATItemType()
                {
                    Amount = vatItem.Amount.GetValueOrDefault(),
                    TaxedAmount = vatItem.TaxedAmount.GetValueOrDefault(),
                };

                vatItemNeu.Item = MapVatItemType(vatItem.Item);
                taxVATList.Add(vatItemNeu);
            }
            inv4P2.Tax.VAT = taxVATList.ToArray();
            #endregion

            #region Global ReductionANdSurcharge
            inv4P2.ReductionAndSurchargeDetails = new V4P2.ReductionAndSurchargeDetailsType();
            inv4P2.ReductionAndSurchargeDetails.Items = null;
            inv4P2.ReductionAndSurchargeDetails.ItemsElementName = new V4P2.ItemsChoiceType1[]
            {
                V4P2.ItemsChoiceType1.Reduction 
            };

            #endregion

            #region Amount
            inv4P2.TotalGrossAmount = source.TotalGrossAmount.GetValueOrDefault();
            inv4P2.PaymentMethod.Comment = source.PaymentMethod.Comment;
            inv4P2.PayableAmount = source.PayableAmount.GetValueOrDefault();
            #endregion

            #region PaymentMethod
            inv4P2.PaymentMethod.Comment = source.PaymentMethod.Comment;
            if (source.PaymentMethod.Item.GetType() == typeof(VM.UniversalBankTransactionType))
            {
                VM.UniversalBankTransactionType txType = source.PaymentMethod.Item as VM.UniversalBankTransactionType;

                inv4P2.PaymentMethod = new V4P2.PaymentMethodType();
                inv4P2.PaymentMethod.Item = new V4P2.UniversalBankTransactionType();
                ((V4P2.UniversalBankTransactionType)inv4P2.PaymentMethod.Item).BeneficiaryAccount = new V4P2.AccountType[]
                {
                    new V4P2.AccountType()
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
                inv4P2.PaymentConditions.DueDate = source.PaymentConditions.DueDate;
                inv4P2.PaymentConditions.DueDateSpecified = true;
            }            
            if (source.PaymentConditions.Discount != null)
            {
                // inv4P1.PaymentConditions.Discount.Clear();
                var discountList = new List<V4P2.DiscountType>();
                foreach (VM.DiscountType srcDiscount in source.PaymentConditions.Discount)
                {
                    V4P2.DiscountType discount = new V4P2.DiscountType()
                    {
                        Amount = srcDiscount.Amount.GetValueOrDefault(),
                        AmountSpecified = srcDiscount.AmountSpecified,
                        BaseAmount = srcDiscount.BaseAmount.GetValueOrDefault(),
                        BaseAmountSpecified = srcDiscount.BaseAmountSpecified,
                        PaymentDate = srcDiscount.PaymentDate,
                        Percentage = srcDiscount.Percentage.GetValueOrDefault(),
                        PercentageSpecified = srcDiscount.PercentageSpecified
                    };
                    discountList.Add(discount);
                }
                inv4P2.PaymentConditions.Discount = discountList.ToArray();
            }
            #endregion

            // Fixe Einträge
            #region PresentationDetails
            inv4P2.PresentationDetails = new V4P2.PresentationDetailsType()
            {
                URL = "http://www.austriapro.at",
                SuppressZero = true,
                SuppressZeroSpecified = true
            };
            #endregion
            return inv4P2;
        }

        private static V4P2.ReductionAndSurchargeListLineItemDetailsType GetReductionDetails(VM.ReductionAndSurchargeListLineItemDetailsType srcRed)
        {
            if (srcRed == null)
                return null;
            V4P2.ReductionAndSurchargeListLineItemDetailsType lineRed = new V4P2.ReductionAndSurchargeListLineItemDetailsType();
            lineRed.Items = new object[srcRed.Items.Count];
            int i = 0;

            foreach (var item1 in srcRed.Items)
            {
                if (item1 is VM.ReductionAndSurchargeBaseType)
                {
                    VM.ReductionAndSurchargeBaseType item = item1 as VM.ReductionAndSurchargeBaseType;
                    V4P2.ReductionAndSurchargeBaseType redBase = new V4P2.ReductionAndSurchargeBaseType();
                    redBase.Amount = item.Amount ?? 0;
                    redBase.AmountSpecified = item.AmountSpecified;
                    redBase.BaseAmount = item.BaseAmount ?? 0;
                    redBase.Comment = item.Comment;
                    redBase.Percentage = item.Percentage ?? 0;
                    redBase.PercentageSpecified = item.PercentageSpecified;
                    lineRed.Items[i] = redBase;
                    i++;
                }
            }

            lineRed.ItemsElementName = new V4P2.ItemsChoiceType[srcRed.ItemsElementName.Count()];
            i = 0;
            foreach (
                VM.ItemsChoiceType choiceType in
                    srcRed.ItemsElementName)
            {
                lineRed.ItemsElementName[i] =
                    (choiceType.ConvertEnum<V4P2.ItemsChoiceType>());
                i++;
            }
            return lineRed;
        }

        private static object MapVatItemType(object vatItem)
        {
            if (vatItem is VM.TaxExemptionType)
            {
                var taxexNew = new V4P2.TaxExemptionType();
                var taxex = vatItem as VM.TaxExemptionType;
                taxexNew.TaxExemptionCode = taxex.TaxExemptionCode;
                taxexNew.Value = taxex.Value;
                return taxexNew;
            }
            else
            {
                var taxexNew = new V4P2.VATRateType();
                var taxex = vatItem as VM.VATRateType;
                taxexNew.TaxCode = taxex.TaxCode;
                taxexNew.Value = taxex.Value ?? 0;
                return taxexNew;
            }

        }

        private static V4P2.AddressType GetAddress(VM.AddressType address)
        {

            if (address == null)
            {
                return null;
            }
            V4P2.AddressType addrNew = new V4P2.AddressType();
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

        private static V4P2.CountryType GetCountry(VM.CountryType countryType)
        {
            if (countryType == null)
                return null;
            V4P2.CountryType cty = new V4P2.CountryType();
            if (countryType.Text == null)
            {
                cty.CountryCode = VM.CountryCodeType.AT.ToString();
                //cty.CountryCodeSpecified = true;
                //cty.Text = new string[] { "Österreich" };
                cty.Value = "Österreich";
            }
            else
            {
                cty.CountryCode = countryType.CountryCode.ToString(); //.ConvertEnum<V4P2.CountryCodeType>();
                //cty.CountryCodeSpecified = true; // This is always true!
                //cty.Text = countryType.Text.ToArray();
                if (countryType.Text.Any())
                {
                    cty.Value = countryType.Text[0];
                } else
                {
                    cty.Value = VM.CountryCodes.GetFromCode(cty.CountryCode).Country;
                }

            }
            return cty;
        }

        private static V4P2.AddressIdentifierType[] GetAddressIdentifier(List<VM.AddressIdentifierType> adrIn)
        {
            if (adrIn == null)
            {
                return null;

            }


            List<V4P2.AddressIdentifierType> adIdList = new List<V4P2.AddressIdentifierType>();

            foreach (VM.AddressIdentifierType addrId in adrIn)
            {
                V4P2.AddressIdentifierType adId = new V4P2.AddressIdentifierType();
                adId.Value = addrId.Value;
                adId.AddressIdentifierType1 =
                    addrId.AddressIdentifierType1.ConvertEnum<V4P2.AddressIdentifierTypeType>();
                adId.AddressIdentifierType1Specified = addrId.AddressIdentifierType1Specified;
                adIdList.Add(adId);
            }
            return adIdList.ToArray();
        }

        private static V4P2.ArticleNumberType[] GetArtikelList(List<VM.ArticleNumberType> srcArticle)
        {
            List<V4P2.ArticleNumberType> artNrList = new List<V4P2.ArticleNumberType>();
            foreach (VM.ArticleNumberType articleNumberType in srcArticle)
            {
                V4P2.ArticleNumberType art = new V4P2.ArticleNumberType();
                art.Value = articleNumberType.Text[0];
                art.ArticleNumberType1Specified = articleNumberType.ArticleNumberType1Specified;
                art.ArticleNumberType1 = articleNumberType.ArticleNumberType1.ConvertEnum<V4P2.ArticleNumberTypeType>();
                artNrList.Add(art);

            }
            return artNrList.ToArray();
        }

        private static V4P2.FurtherIdentificationType[] GetFurtherIdentification(List<VM.FurtherIdentificationType> furtherID)
        {
            List<V4P2.FurtherIdentificationType> fIdList = new List<V4P2.FurtherIdentificationType>();
            if (furtherID == null)
            {
                return fIdList.ToArray();
            }

            List<string> supportedIds = Enum.GetNames(typeof(VM.FurtherIdentificationType.SupportedIds)).ToList();
            foreach (VM.FurtherIdentificationType item in furtherID)
            {
                if (supportedIds.Contains(item.IdentificationType))
                {
                    V4P2.FurtherIdentificationType fId = new V4P2.FurtherIdentificationType()
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
