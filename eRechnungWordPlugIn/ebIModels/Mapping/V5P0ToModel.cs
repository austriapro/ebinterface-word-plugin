using ebIModels.Models;
using SRC = ebIModels.Schema.ebInterface5p0;
using System;
using System.Collections.Generic;
using System.Linq;
using Model = ebIModels.Models;
using ExtensionMethods;

namespace ebIModels.Mapping.V5p0
{
    public static partial class MapInvoice
    {

        /// <summary>
        /// Maps ebInterface 5p0 InvoiceType to internal InvoiceType Model
        /// </summary>
        /// <param name="source">ebInterface 5p0 InvoiceType</param>
        /// <returns></returns>
        internal static IInvoiceModel MapV5p0ToVm(SRC.InvoiceType source)
        {
            Model.InvoiceModel invoice = new Model.InvoiceModel();

            #region Rechnungskopf

            invoice.InvoiceSubtype = source.InvoiceSubtype;
            // GeneratingSystem xs:string
            invoice.GeneratingSystem = source.GeneratingSystem;
            // DocumentType DocumentTypeType
            invoice.DocumentType = source.DocumentType.ConvertEnum<Model.DocumentTypeType>();

            // InvoiceCurrency CurrencyType
            invoice.InvoiceCurrency = source.InvoiceCurrency.ToEnum(Model.CurrencyType.EUR);

            // ManualProcessing xs:boolean
            invoice.ManualProcessing = source.ManualProcessing;

            // DocumentTitle xs:string
            invoice.DocumentTitle = source.DocumentTitle;

            // Language LanguageType
            invoice.Language = source.Language.ToEnum(LanguageType.ger);

            // IsDuplicate xs:boolean
            invoice.IsDuplicate = source.IsDuplicate;

            // InvoiceNumber IDType
            invoice.InvoiceNumber = source.InvoiceNumber;

            // InvoiceDate xs:date
            invoice.InvoiceDate = source.InvoiceDate;
            #endregion

            #region Cancelled Original Document
            // CancelledOriginalDocument [0..1] CancelledOriginalDocumentType

            invoice.CancelledOriginalDocument = null;
            if (source.CancelledOriginalDocument != null)
            {
                invoice.CancelledOriginalDocument = new Model.CancelledOriginalDocumentType()
                {
                    Comment = source.CancelledOriginalDocument.Comment,
                    DocumentType = source.CancelledOriginalDocument.DocumentType.ConvertEnum<Model.DocumentTypeType>(),
                    DocumentTypeSpecified = source.CancelledOriginalDocument.DocumentTypeSpecified,
                    InvoiceDate = source.CancelledOriginalDocument.InvoiceDate,
                    InvoiceNumber = source.CancelledOriginalDocument.InvoiceNumber
                };
            }
            #endregion

            #region Related Document
            // RelatedDocument [0..*] RelatedDocumentType

            if (source.RelatedDocument != null)
            {
                List<RelatedDocumentType> relDocs = new List<RelatedDocumentType>();
                foreach (var rDoc in invoice.RelatedDocument)
                {
                    RelatedDocumentType related = new RelatedDocumentType()
                    {
                        Comment = rDoc.Comment,
                        DocumentType = rDoc.DocumentType.ConvertEnum<DocumentTypeType>(),
                        DocumentTypeSpecified = rDoc.DocumentTypeSpecified,
                        InvoiceDate = rDoc.InvoiceDate,
                        InvoiceDateSpecified = rDoc.InvoiceDateSpecified,
                        InvoiceNumber = rDoc.InvoiceNumber

                    };
                    relDocs.Add(related);
                }
                invoice.RelatedDocument = relDocs;
            }
            #endregion

            #region Additional Information
            // AdditionalInformation [0..*] AdditionalInformationType
            if (source.AdditionalInformation != null)
            {
                List<AdditionalInformationType> additionalInformations = new List<AdditionalInformationType>();
                foreach (var addInfo in source.AdditionalInformation)
                {
                    AdditionalInformationType addInfoNew = new AdditionalInformationType()
                    {
                        Key = addInfo.Key,
                        Value = addInfo.Value
                    };
                    additionalInformations.Add(addInfoNew);
                }
                invoice.AdditionalInformation = additionalInformations;
            }
            #endregion

            #region Delivery
            // Delivery [0..1] DeliveryType
            // Delivery Address & Contact are not supported
            if (source.Delivery != null)
            {
                if (source.Delivery.Item is SRC.PeriodType delType)
                {
                    if ((delType.ToDate != null) && (delType.ToDate != DateTime.MinValue))
                    {
                        var deliveryType = new PeriodType
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
                    // ein einzelnes Datum
                    invoice.Delivery.Item = source.Delivery.Item;
                }

            }
            #endregion

            #region Biller
            // Biller BillerType            
            if (source.Biller != null)
            {
                invoice.Biller = new Model.BillerType
                {
                    VATIdentificationNumber = source.Biller.VATIdentificationNumber,
                    InvoiceRecipientsBillerID = source.Biller.InvoiceRecipientsBillerID,
                    Address = GetAddress(source.Biller.Address, source.Biller.Contact),
                    FurtherIdentification = GetFurtherIdentification(source.Biller.FurtherIdentification)

                };
                invoice.Biller.Contact = GetContact(source.Biller.Contact);
            }
            #endregion
            #region Invoice Recipient
            // InvoiceRecipient InvoiceRecipientType            
            if (source.InvoiceRecipient != null)
            {
                invoice.InvoiceRecipient = new Model.InvoiceRecipientType
                {
                    BillersInvoiceRecipientID = source.InvoiceRecipient.BillersInvoiceRecipientID,
                    VATIdentificationNumber = source.InvoiceRecipient.VATIdentificationNumber,
                    Address = GetAddress(source.InvoiceRecipient.Address, source.InvoiceRecipient.Contact),
                    OrderReference = new OrderReferenceType()

                };
                invoice.InvoiceRecipient.Contact = GetContact(source.InvoiceRecipient.Contact);
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

            #region Ordering Party
            // OrderingParty [0..1] OrderingPartyType not supported

            #endregion

            #region Details
            // Details DetailsType

            invoice.Details = new DetailsType
            {
                HeaderDescription = source.Details.HeaderDescription,
                FooterDescription = source.Details.FooterDescription
            };

            var detailsItemList = new List<ItemListType>();

            if (source.Details?.ItemList != null)
            {
                foreach (SRC.ItemListType srcItemList in source.Details.ItemList)
                {
                    ItemListType itemList = new ItemListType();

                    var itemListLineItem = new List<ListLineItemType>();
                    foreach (SRC.ListLineItemType srcLineItem in srcItemList.ListLineItem)
                    {
                        ListLineItemType lineItem = new ListLineItemType
                        {
                            PositionNumber = srcLineItem.PositionNumber,
                            Description = srcLineItem.Description?.ToList(),
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

                        if (!string.IsNullOrEmpty(srcLineItem.InvoiceRecipientsOrderReference.OrderPositionNumber))
                        {
                            lineItem.InvoiceRecipientsOrderReference.OrderID = source.InvoiceRecipient.OrderReference.OrderID;
                            lineItem.InvoiceRecipientsOrderReference.OrderPositionNumber =
                                srcLineItem.InvoiceRecipientsOrderReference.OrderPositionNumber;
                        }

                        // Rabatte / Zuschläge                    
                        if (srcLineItem.ReductionAndSurchargeListLineItemDetails != null)
                        {
                            lineItem.ReductionAndSurchargeListLineItemDetails = GetReductionDetails(srcLineItem.ReductionAndSurchargeListLineItemDetails);
                        }
                        lineItem.Description = srcLineItem.Description?.ToList();
                        lineItem.LineItemAmount = srcLineItem.LineItemAmount;
                        //lineItem.ReCalcLineItemAmount();
                        // Steuer
                        lineItem.TaxItem = MapTaxItemType(srcLineItem.TaxItem);

                        itemListLineItem.Add(lineItem);
                    }
                    itemList.ListLineItem = itemListLineItem;
                    detailsItemList.Add(itemList);
                }

            }
            else
            {
                detailsItemList = null;
            }
            invoice.Details.ItemList = detailsItemList;
            #endregion

            #region Global Reduction and Surcharge
            // ReductionAndSurchargeDetails [0..1] ReductionAndSurchargeDetailsType
            // not supported
            // invoice.ReductionAndSurchargeDetails = MapReductionAndSurchargeDetails(source.ReductionAndSurchargeDetails);
            // 
            #endregion

            #region Tax
            // Erzeuge Tax TaxType
            invoice.CalculateTotals();
            #endregion

            #region Amount
            // TotalGrossAmount Decimal2Type
            invoice.TotalGrossAmount = source.TotalGrossAmount;

            // PrepaidAmount [0..1] Decimal2Type
            invoice.PrepaidAmount = source.PrepaidAmount;

            // RoundingAmount [0..1] Decimal2Type

            // PayableAmount Decimal2Type
            invoice.PayableAmount = source.PayableAmount;
            #endregion

            // PaymentMethod [0..1] PaymentMethodType
            invoice.PaymentMethod = MapPaymentMethod(source.PaymentMethod);

            // PaymentConditions [0..1] PaymentConditionsType
            invoice.PaymentConditions = MapPaymentConditions(source.PaymentConditions);

            // Comment [0..1] xs:string
            invoice.Comment = source.Comment;
            return invoice;
        }

        /// <summary>
        /// Mappping for Contact 
        /// </summary>
        /// <param name="contactSource"></param>
        /// <returns></returns>
        private static Model.ContactType GetContact(SRC.ContactType contactSource)
        {
            if (contactSource == null)
            {
                return new ContactType()
                {
                    Email = new List<string>(),
                    Name = "",
                    Phone = new List<string>(),
                    Salutation = ""
                };
            };

            ContactType contact = new ContactType
            {
                Email = contactSource.Email?.ToList(),
                Name = contactSource.Name,
                Phone = contactSource.Phone?.ToList(),
                Salutation = contactSource.Salutation
            };
            return contact;
        }

        /// <summary>
        /// Mapping für ReductionAndSurchargeListLineItemDetailsType
        /// </summary>
        /// <param name="srcRed"></param>
        /// <returns></returns>
        private static Model.ReductionAndSurchargeListLineItemDetailsType GetReductionDetails(SRC.ReductionAndSurchargeListLineItemDetailsType srcRed)
        {
            if (srcRed == null)
                return null;
            if (srcRed.Items == null)
            {
                return null;
            }
            ReductionAndSurchargeListLineItemDetailsType lineRed = new ReductionAndSurchargeListLineItemDetailsType
            {
                Items = new List<object>()
            };

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
            foreach (SRC.ItemsChoiceType choiceType in srcRed.ItemsElementName)
            {
                lineRed.ItemsElementName.Add(choiceType.ConvertEnum<ItemsChoiceType>());
            }
            return lineRed;
        }

        private static Model.TaxItemType MapTaxItemType(SRC.TaxItemType taxItem)
        {
            TaxItemType tax = new TaxItemType()
            {
                TaxableAmount = taxItem.TaxableAmount.FixedFraction(2),
                Comment = taxItem.Comment,
                TaxAmountSpecified = true,
                TaxAmount = (taxItem.TaxableAmount * taxItem.TaxPercent.Value / 100).FixedFraction(2),
                TaxPercent = new TaxPercentType()
                {
                    TaxCategoryCode = taxItem.TaxPercent.TaxCategoryCode,
                    Value = taxItem.TaxPercent.Value
                }
            };
            return tax;
        }

        private static List<Model.ArticleNumberType> GetArtikelList(SRC.ArticleNumberType[] articleNumbers)
        {
            List<ArticleNumberType> artNrList = new List<ArticleNumberType>();
            if (articleNumbers == null)
            {
                return artNrList;
            }
            for (int i = 0; i < articleNumbers.Count(); i++)
            {
                SRC.ArticleNumberType articleNumberType = articleNumbers[i];
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

        private static List<Model.FurtherIdentificationType> GetFurtherIdentification(SRC.FurtherIdentificationType[] furtherID)
        {
            List<FurtherIdentificationType> fIdList = new List<FurtherIdentificationType>();
            if (furtherID == null)
            {
                return fIdList;
            }

            List<string> supportedIds = Enum.GetNames(typeof(Model.FurtherIdentificationType.SupportedIds)).ToList();
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

        private static Model.AddressType GetAddress(SRC.AddressType address, SRC.ContactType contact)
        {
            if (address == null)
            {
                return null;
            }
            AddressType addrNew = new AddressType
            {
                Name = address.Name,
                POBox = address.POBox,
                Street = address.Street,
                Country = GetCountry(address.Country),
                ZIP = address.ZIP,
                Town = address.Town,
                AddressIdentifier = GetAddressIdentifier(address.AddressIdentifier),
                Email = address.Email?[0],
                Phone = address.Phone?[0],
            };
            return addrNew;
        }

        private static List<Model.AddressIdentifierType> GetAddressIdentifier(SRC.AddressIdentifierType[] adrIn)
        {
            if (adrIn == null)
            {
                return null;

            }

            List<AddressIdentifierType> adIdList = new List<AddressIdentifierType>();

            foreach (SRC.AddressIdentifierType addrId in adrIn)
            {
                AddressIdentifierType adId = new AddressIdentifierType
                {
                    Value = addrId.Value,
                    AddressIdentifierType1 = addrId.AddressIdentifierType1.ToEnum(AddressIdentifierTypeType.GLN)
                };
                adIdList.Add(adId);
            }
            return adIdList;
        }

        private static Model.CountryType GetCountry(SRC.CountryType countryType)
        {
            if (countryType == null)
                return null;
            CountryType cty = new CountryType(CountryCodeType.AT);
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

        #region PaymentConditions 
        /// <summary>
        /// Maps the PaymentConditions from Model to ebInterface V5p0
        /// </summary>
        /// <param name="paymentConditions">Payment Conditions from Model</param>
        /// <returns></returns>
        private static Model.PaymentConditionsType MapPaymentConditions(SRC.PaymentConditionsType paymentConditions)
        {

            if (paymentConditions == null)
            {
                return null;
            }
            PaymentConditionsType payment = new PaymentConditionsType
            {
                Comment = paymentConditions.Comment
            };


            if (paymentConditions.DueDate > DateTime.MinValue)
            {
                payment.DueDate = paymentConditions.DueDate;
                payment.DueDateSpecified = true;
            }
            else
            {
                payment.DueDateSpecified = false;
            }
            if (paymentConditions.Discount != null)
            {
                // inv4P1.PaymentConditions.Discount.Clear();
                var discountList = new List<DiscountType>();
                foreach (SRC.DiscountType srcDiscount in paymentConditions.Discount)
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
                payment.Discount = discountList;
            }
            return payment;
        }
        #endregion

        #region PaymentMethod
        /// <summary>
        /// Maps the payment method.
        /// </summary>
        /// <param name="paymentMethod">The payment method.</param>
        /// <returns></returns>
        private static Model.PaymentMethodType MapPaymentMethod(SRC.PaymentMethodType paymentMethod)
        {
            if (paymentMethod == null)
            {
                return new PaymentMethodType();
            }
            PaymentMethodType paymethod = new PaymentMethodType
            {
                Comment = paymentMethod.Comment
            };
            if (paymentMethod.Item?.GetType() == typeof(SRC.UniversalBankTransactionType))
            {
                SRC.UniversalBankTransactionType txType = paymentMethod.Item as SRC.UniversalBankTransactionType;

                paymethod.Item = new UniversalBankTransactionType();
                ((UniversalBankTransactionType)paymethod.Item).BeneficiaryAccount = new List<AccountType>()
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
            return paymethod;
        }
        #endregion

        /// <summary>
        /// Maps the tax from Model to ebInterface V5p0
        /// Caution: OtherTax Element not suppored.
        /// </summary>
        /// <param name="SourceTax">The source tax.</param>
        /// <returns></returns>
        private static Model.TaxType MapTax(SRC.TaxType SourceTax)
        {
            TaxType tax = new TaxType();
            List<TaxItemType> taxItems = new List<TaxItemType>();
            if (SourceTax.TaxItem == null)
            {
                return null;
            }
            foreach (var taxItemModel in SourceTax.TaxItem)
            {
                TaxItemType taxItem = new TaxItemType()
                {
                    TaxableAmount = taxItemModel.TaxableAmount,
                    TaxAmount = taxItemModel.TaxAmount, //(taxItemModel.TaxableAmount * taxItemModel.TaxPercent.Value / 100).FixedFraction(2),
                    TaxAmountSpecified = true,
                    TaxPercent = new TaxPercentType()
                    {
                        TaxCategoryCode = taxItemModel.TaxPercent.TaxCategoryCode,
                        Value = taxItemModel.TaxPercent.Value
                    },

                };
                taxItems.Add(taxItem);
            }
            tax.TaxItem = taxItems;
            return tax;
        }


    }
}
