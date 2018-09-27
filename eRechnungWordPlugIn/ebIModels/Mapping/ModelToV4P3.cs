using System;
using System.Collections.Generic;
using System.Linq;
using ebIModels.Schema.ebInterface4p3;
using ExtensionMethods;
using VM = ebIModels.Models;
using ebIModels.Schema;
using V4P3 = ebIModels.Schema.ebInterface4p3;

namespace ebIModels.Mapping
{
    public static class MappingServiceVmTo4p3 
    {
        /// <summary>
        /// Mapped InvoiceType Model auf ebInterface4p1
        /// </summary>
        /// <param name="source">Invoice Model</param>
        /// <returns>ebInterface 4p1 InvoiceType</returns>
        public static V4P3.InvoiceType MapModelToV4p3(VM.IInvoiceModel source)
        {
            V4P3.InvoiceType invoice = new V4P3.InvoiceType();

            #region Rechnungskopf
            invoice.InvoiceSubtype = source.InvoiceSubtype;
            invoice.InvoiceNumber = source.InvoiceNumber;
            invoice.InvoiceDate = source.InvoiceDate;
            invoice.GeneratingSystem = source.GeneratingSystem;
            invoice.DocumentType = source.DocumentType.ConvertEnum<DocumentTypeType>();
            invoice.DocumentTitle = source.DocumentTitle;
            invoice.InvoiceCurrency = source.InvoiceCurrency.ToString(); //.ConvertEnum<CurrencyType>();
            if (source.LanguageSpecified)
            {
                invoice.Language = source.Language.ToString();// .ConvertEnum<LanguageType>();

            }
            else
            {
                invoice.Language = null;
            }

            invoice.Comment = source.Comment;
            invoice.CancelledOriginalDocument = null;
            if (source.CancelledOriginalDocument != null)
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

            #region Related Document
            if (source.RelatedDocument.Any())
            {
                invoice.RelatedDocument = new RelatedDocumentType[]
                {
                    new RelatedDocumentType()
                    {
                    Comment = source.RelatedDocument[0].Comment,
                    DocumentType = source.RelatedDocument[0].DocumentType.ConvertEnum<DocumentTypeType>(),
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
                    var deliveryType = new PeriodType();
                    deliveryType.FromDate = delType.FromDate ?? new DateTime();
                    deliveryType.ToDate = delType.ToDate ?? new DateTime();
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
                invoice.Biller = new BillerType();
                invoice.Biller.VATIdentificationNumber = source.Biller.VATIdentificationNumber;
                invoice.Biller.InvoiceRecipientsBillerID = source.Biller.InvoiceRecipientsBillerID;
                invoice.Biller.Address = GetAddress(source.Biller.Address);
                invoice.Biller.FurtherIdentification = GetFurtherIdentification(source.Biller.FurtherIdentification);
            }
            #endregion

            #region InvoiceReceipient
            if (source.InvoiceRecipient != null)
            {
                invoice.InvoiceRecipient = new InvoiceRecipientType();
                invoice.InvoiceRecipient.BillersInvoiceRecipientID = source.InvoiceRecipient.BillersInvoiceRecipientID;
                invoice.InvoiceRecipient.VATIdentificationNumber = source.InvoiceRecipient.VATIdentificationNumber;
                invoice.InvoiceRecipient.Address = GetAddress(source.InvoiceRecipient.Address);
                invoice.InvoiceRecipient.OrderReference = new OrderReferenceType();
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
            invoice.Details = new DetailsType();
            invoice.Details.HeaderDescription = source.Details.HeaderDescription;
            invoice.Details.FooterDescription = source.Details.FooterDescription;

            // inv4P1.Details.ItemList = new List<InvV4p1.ItemListType>();
            var detailsItemList = new List<ItemListType>();
            // inv4P1.Details.ItemList.Clear();

            // InvV4p1.ItemListType item = new InvV4p1.ItemListType();

            foreach (VM.ItemListType srcItemList in source.Details.ItemList)
            {
                ItemListType itemList = new ItemListType();

                var itemListLineItem = new List<ListLineItemType>();
                foreach (VM.ListLineItemType srcLineItem in srcItemList.ListLineItem)
                {
                    ListLineItemType lineItem = new ListLineItemType();

                    lineItem.PositionNumber = srcLineItem.PositionNumber;
                    lineItem.Description = srcLineItem.Description.ToArray();
                    lineItem.AdditionalInformation = null;

                    lineItem.ArticleNumber = GetArtikelList(srcLineItem.ArticleNumber);

                    // Menge
                    lineItem.Quantity = new UnitType();
                    lineItem.Quantity.Unit = srcLineItem.Quantity.Unit;
                    lineItem.Quantity.Value = srcLineItem.Quantity.Value.GetValueOrDefault();

                    // Einzelpreis
                    lineItem.UnitPrice = new UnitPriceType()
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
            invoice.Details.ItemList = detailsItemList.ToArray();
            if (source.Details.BelowTheLineItem != null)
            {
                if (source.Details.BelowTheLineItem.Count > 0)
                {
                    List<BelowTheLineItemType> belowItems = new List<BelowTheLineItemType>();
                    foreach (VM.BelowTheLineItemType item in source.Details.BelowTheLineItem)
                    {

                        if (!string.IsNullOrEmpty(item.Description))
                        {
                            belowItems.Add(new BelowTheLineItemType()
                            {
                                Description = item.Description,
                                LineItemAmount = item.LineItemAmount ?? 0
                            });

                        }
                    }
                    if (belowItems.Any())
                    {
                        invoice.Details.BelowTheLineItem = belowItems.ToArray();
                    }
                }
            }
            #endregion

            #region Tax
            var taxVATList = new List<VATItemType>();
            foreach (var vatItem in source.Tax.VAT)
            {
                VATItemType vatItemNeu = new VATItemType()
                {
                    Amount = vatItem.Amount.GetValueOrDefault(),
                    TaxedAmount = vatItem.TaxedAmount.GetValueOrDefault(),
                };

                vatItemNeu.Item = MapVatItemType(vatItem.Item);
                taxVATList.Add(vatItemNeu);
            }
            invoice.Tax.VAT = taxVATList.ToArray();
            #endregion

            #region Global ReductionANdSurcharge
            invoice.ReductionAndSurchargeDetails = new ReductionAndSurchargeDetailsType();
            invoice.ReductionAndSurchargeDetails.Items = null;
            invoice.ReductionAndSurchargeDetails.ItemsElementName = new ItemsChoiceType1[]
            {
                ItemsChoiceType1.Reduction
            };

            #endregion

            #region Amount
            invoice.TotalGrossAmount = source.TotalGrossAmount.GetValueOrDefault();
            invoice.PaymentMethod.Comment = source.PaymentMethod.Comment;
            invoice.PayableAmount = source.PayableAmount.GetValueOrDefault();
            #endregion

            #region PaymentMethod
            invoice.PaymentMethod.Comment = source.PaymentMethod.Comment;
            if (source.PaymentMethod.Item.GetType() == typeof(VM.UniversalBankTransactionType))
            {
                VM.UniversalBankTransactionType txType = source.PaymentMethod.Item as VM.UniversalBankTransactionType;

                invoice.PaymentMethod = new PaymentMethodType();
                invoice.PaymentMethod.Item = new UniversalBankTransactionType();
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
                foreach (VM.DiscountType srcDiscount in source.PaymentConditions.Discount)
                {
                    DiscountType discount = new DiscountType()
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

        private static ReductionAndSurchargeListLineItemDetailsType GetReductionDetails(VM.ReductionAndSurchargeListLineItemDetailsType srcRed)
        {
            if (srcRed == null)
                return null;
            ReductionAndSurchargeListLineItemDetailsType lineRed = new ReductionAndSurchargeListLineItemDetailsType();
            lineRed.Items = new object[srcRed.Items.Count];
            int i = 0;

            foreach (var item1 in srcRed.Items)
            {
                if (item1 is VM.ReductionAndSurchargeBaseType)
                {
                    VM.ReductionAndSurchargeBaseType item = item1 as VM.ReductionAndSurchargeBaseType;
                    ReductionAndSurchargeBaseType redBase = new ReductionAndSurchargeBaseType();
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

            lineRed.ItemsElementName = new ItemsChoiceType[srcRed.ItemsElementName.Count()];
            i = 0;
            foreach (
                VM.ItemsChoiceType choiceType in
                    srcRed.ItemsElementName)
            {
                lineRed.ItemsElementName[i] =
                    (choiceType.ConvertEnum<ItemsChoiceType>());
                i++;
            }
            return lineRed;
        }

        private static object MapVatItemType(object vatItem)
        {
            if (vatItem is VM.TaxExemptionType)
            {
                var taxexNew = new TaxExemptionType();
                var taxex = vatItem as VM.TaxExemptionType;
                taxexNew.TaxExemptionCode = taxex.TaxExemptionCode;
                taxexNew.Value = taxex.Value;
                return taxexNew;
            }
            else
            {
                var taxexNew = new VATRateType();
                var taxex = vatItem as VM.VATRateType;
                taxexNew.TaxCode = taxex.TaxCode;
                taxexNew.Value = taxex.Value ?? 0;
                return taxexNew;
            }

        }

        private static AddressType GetAddress(VM.AddressType address)
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

        private static CountryType GetCountry(VM.CountryType countryType)
        {
            if (countryType == null)
                return null;
            CountryType cty = new CountryType();
            if (countryType.Text == null)
            {
                cty.CountryCode = VM.CountryCodeType.AT.ToString();
                //cty.CountryCodeSpecified = true;
                //cty.Text = new string[] { "Österreich" };
                cty.Value = "Österreich";
            }
            else
            {
                cty.CountryCode = countryType.CountryCode.ToString(); //.ConvertEnum<CountryCodeType>();
                                                                      //cty.CountryCodeSpecified = true; // This is always true!
                                                                      //cty.Text = countryType.Text.ToArray();
                if (countryType.Text.Any())
                {
                    cty.Value = countryType.Text[0];
                }
                else
                {
                    cty.Value = VM.CountryCodes.GetFromCode(cty.CountryCode).Country;
                }

            }
            return cty;
        }

        private static AddressIdentifierType[] GetAddressIdentifier(List<VM.AddressIdentifierType> adrIn)
        {
            if (adrIn == null)
            {
                return null;

            }


            List<AddressIdentifierType> adIdList = new List<AddressIdentifierType>();

            foreach (VM.AddressIdentifierType addrId in adrIn)
            {
                AddressIdentifierType adId = new AddressIdentifierType();
                adId.Value = addrId.Value;
                adId.AddressIdentifierType1 =
                    addrId.AddressIdentifierType1.ConvertEnum<AddressIdentifierTypeType>();
                adId.AddressIdentifierType1Specified = addrId.AddressIdentifierType1Specified;
                adIdList.Add(adId);
            }
            return adIdList.ToArray();
        }

        private static ArticleNumberType[] GetArtikelList(List<VM.ArticleNumberType> srcArticle)
        {
            List<ArticleNumberType> artNrList = new List<ArticleNumberType>();
            foreach (VM.ArticleNumberType articleNumberType in srcArticle)
            {
                ArticleNumberType art = new ArticleNumberType();
                art.Value = articleNumberType.Text[0];
                art.ArticleNumberType1Specified = articleNumberType.ArticleNumberType1Specified;
                art.ArticleNumberType1 = articleNumberType.ArticleNumberType1.ConvertEnum<ArticleNumberTypeType>();
                artNrList.Add(art);

            }
            return artNrList.ToArray();
        }

        private static FurtherIdentificationType[] GetFurtherIdentification(List<VM.FurtherIdentificationType> furtherID)
        {
            List<FurtherIdentificationType> fIdList = new List<FurtherIdentificationType>();
            if (furtherID == null)
            {
                return fIdList.ToArray();
            }

            List<string> supportedIds = Enum.GetNames(typeof(VM.FurtherIdentificationType.SupportedIds)).ToList();
            foreach (VM.FurtherIdentificationType item in furtherID)
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
