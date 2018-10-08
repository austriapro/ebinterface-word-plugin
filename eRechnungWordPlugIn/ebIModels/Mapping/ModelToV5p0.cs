using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Schema.ebInterface5p0;
using ExtensionMethods;
using Model = ebIModels.Models;
using TARGET = ebIModels.Schema.ebInterface5p0;

namespace ebIModels.Mapping.V5p0
{
    public static partial class MapInvoice
    {
        /// <summary>
        /// Maps ebInterface 5p0 InvoiceType to internal InvoiceType Model
        /// </summary>
        /// <param name="source">ebInterface 5p0 InvoiceType</param>
        /// <returns></returns>
        internal static TARGET.InvoiceType MapModelToV5p0(Model.IInvoiceModel source)
        {
            TARGET.InvoiceType invoice = new TARGET.InvoiceType();
            mappingErrors = new List<MappingError>();

            #region Rechnungskopf

            invoice.InvoiceSubtype = source.InvoiceSubtype;
            // GeneratingSystem xs:string
            invoice.GeneratingSystem = source.GeneratingSystem;
            // DocumentType DocumentTypeType

            // InvoiceCurrency CurrencyType
            invoice.InvoiceCurrency = source.InvoiceCurrency.ToString();

            // ManualProcessing xs:boolean
            invoice.ManualProcessing = source.ManualProcessing;

            // DocumentTitle xs:string
            invoice.DocumentTitle = source.DocumentTitle;

            // Language LanguageType
            invoice.Language = source.Language.ToString();

            // IsDuplicate xs:boolean
            invoice.IsDuplicate = source.IsDuplicate;

            // InvoiceNumber IDType
            invoice.InvoiceNumber = source.InvoiceNumber;

            // InvoiceDate xs:date
            invoice.InvoiceDate = source.InvoiceDate;
            #endregion

            #region Cancelled Original Document
            // CancelledOriginalDocument [0..1] CancelledOriginalDocumentType

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
            // RelatedDocument [0..*] RelatedDocumentType

            if (!invoice.RelatedDocument.Any())
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
                invoice.RelatedDocument = relDocs.ToArray();
            }
            #endregion

            #region Additional Information
            // AdditionalInformation [0..*] AdditionalInformationType
            if (source.AdditionalInformation.Any())
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
                invoice.AdditionalInformation = additionalInformations.ToArray();
            }
            #endregion

            #region Delivery
            // Delivery [0..1] DeliveryType
            // Delivery Address & Contact are not supported
            if (source.Delivery != null)
            {
                if (source.Delivery.Item is Model.PeriodType delType)
                {
                    if (delType.ToDate != null)
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
                invoice.Biller = new TARGET.BillerType
                {
                    VATIdentificationNumber = source.Biller.VATIdentificationNumber,
                    InvoiceRecipientsBillerID = source.Biller.InvoiceRecipientsBillerID,
                    Address = GetAddress(source.Biller.Address, source.Biller.Contact),
                    FurtherIdentification = GetFurtherIdentification(source.Biller.FurtherIdentification)
                    // ToDO Contact missing
                };
            }
            #endregion
            #region Invoice Recipient
            // InvoiceRecipient InvoiceRecipientType            
            if (source.InvoiceRecipient != null)
            {
                invoice.InvoiceRecipient = new TARGET.InvoiceRecipientType
                {
                    BillersInvoiceRecipientID = source.InvoiceRecipient.BillersInvoiceRecipientID,
                    VATIdentificationNumber = source.InvoiceRecipient.VATIdentificationNumber,
                    Address = GetAddress(source.InvoiceRecipient.Address, source.InvoiceRecipient.Contact),
                    OrderReference = new OrderReferenceType()
                    // ToDO Contact missing
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

                    // Steuer

                    lineItem.TaxItem = MapTaxItemType(srcLineItem.TaxItem);
                    // Auftragsreferenz
                    if (!string.IsNullOrEmpty(srcLineItem.InvoiceRecipientsOrderReference.OrderPositionNumber) ||
                        source.InvoiceRecipient.BestellPositionErforderlich)   // Orderposition angegeben oder erforderlich
                    {
                        lineItem.InvoiceRecipientsOrderReference.OrderID = source.InvoiceRecipient.OrderReference.OrderID;
                        lineItem.InvoiceRecipientsOrderReference.OrderPositionNumber =
                            srcLineItem.InvoiceRecipientsOrderReference.OrderPositionNumber;
                    }

                    // Rabatte / Zuschläge                    
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
            invoice.Details.ItemList = detailsItemList.ToArray();
            #endregion

            #region Global Reduction and Surcharge
            // ReductionAndSurchargeDetails [0..1] ReductionAndSurchargeDetailsType
            // not supported
            // invoice.ReductionAndSurchargeDetails = MapReductionAndSurchargeDetails(source.ReductionAndSurchargeDetails);
            // 
            #endregion

            #region Tax
            // Tax TaxType
            invoice.Tax = MapTax(source.Tax);
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

        private static ReductionAndSurchargeListLineItemDetailsType GetReductionDetails(Model.ReductionAndSurchargeListLineItemDetailsType srcRed)
        {
            if (srcRed == null)
                return null;
            ReductionAndSurchargeListLineItemDetailsType lineRed = new ReductionAndSurchargeListLineItemDetailsType
            {
                Items = new object[srcRed.Items.Count]
            };
            int i = 0;

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


        private static TaxItemType MapTaxItemType(Model.TaxItemType taxItem)
        {
            TaxItemType tax = new TaxItemType()
            {
                Comment = taxItem.Comment,
                TaxableAmount = taxItem.TaxableAmount,
                TaxAmount = taxItem.TaxAmount,
                TaxAmountSpecified = taxItem.TaxAmountSpecified,
                TaxPercent = new TaxPercentType()
                {
                    TaxCategoryCode = taxItem.TaxPercent.TaxCategoryCode,
                    Value = taxItem.TaxPercent.Value
                }
            };
            return tax;
        }

        private static ArticleNumberType[] GetArtikelList(List<Model.ArticleNumberType> articleNumbers)
        {
            List<ArticleNumberType> artNrList = new List<ArticleNumberType>();
            for (int i = 0; i < articleNumbers.Count; i++)
            {
                Model.ArticleNumberType articleNumberType = articleNumbers[i];
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

        private static AddressType GetAddress(Model.AddressType address, Model.ContactType contact)
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
                AddressIdentifier = GetAddressIdentifier(address.AddressIdentifier)
            };
            return addrNew;
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
                    AddressIdentifierType1 = addrId.AddressIdentifierType1.ToString()
                };
                adIdList.Add(adId);
            }
            return adIdList.ToArray();
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

        #region PaymentConditions 
        /// <summary>
        /// Maps the PaymentConditions from Model to ebInterface V5p0
        /// </summary>
        /// <param name="paymentConditions">Payment Conditions from Model</param>
        /// <returns></returns>
        private static PaymentConditionsType MapPaymentConditions(Model.PaymentConditionsType paymentConditions)
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
                foreach (Model.DiscountType srcDiscount in paymentConditions.Discount)
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
                payment.Discount = discountList.ToArray();
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
        private static PaymentMethodType MapPaymentMethod(Model.PaymentMethodType paymentMethod)
        {
            PaymentMethodType paymethod = new PaymentMethodType
            {
                Comment = paymentMethod.Comment
            };
            if (paymentMethod.Item?.GetType() == typeof(Model.UniversalBankTransactionType))
            {
                Model.UniversalBankTransactionType txType = paymentMethod.Item as Model.UniversalBankTransactionType;

                paymethod.Item = new UniversalBankTransactionType();
                ((UniversalBankTransactionType)paymethod.Item).BeneficiaryAccount = new AccountType[]
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
        private static TaxType MapTax(Model.TaxType SourceTax)
        {
            TaxType tax = new TaxType();

            List<TaxItemType> taxItems = new List<TaxItemType>();
            foreach (var taxItemModel in SourceTax.TaxItem)
            {
                TaxItemType taxItem = new TaxItemType()
                {
                    Comment = taxItemModel.Comment,
                    TaxableAmount = taxItemModel.TaxableAmount,
                    TaxAmount = taxItemModel.TaxAmount,
                    TaxAmountSpecified = taxItemModel.TaxAmountSpecified,
                    TaxPercent = new TaxPercentType()
                    {
                        TaxCategoryCode = taxItemModel.TaxPercent.TaxCategoryCode,
                        Value = taxItemModel.TaxPercent.Value
                    }
                };
                taxItems.Add(taxItem);
            }
            tax.TaxItem = taxItems.ToArray();
            return tax;
        }

        private static ReductionAndSurchargeDetailsType MapReductionAndSurchargeDetails(Model.ReductionAndSurchargeDetailsType reductionAndSurchargeDetails)
        {
            throw new NotImplementedException();
        }


    }
}
