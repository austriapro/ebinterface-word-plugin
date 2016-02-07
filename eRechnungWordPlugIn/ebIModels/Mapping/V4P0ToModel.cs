using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ebIModels.Models;
using ExtensionMethods;
using V4P0 = ebIModels.Schema.ebInterface4p0;


namespace ebIModels.Mapping
{
    public static class MappingService4p0ToVm
    {

        public static IInvoiceType MapV4p0ToModel(V4P0.InvoiceType source)
        {
            InvoiceType invoice = new InvoiceType();
            try
            {
                #region Rechnungskopf
                invoice.InvoiceNumber = source.InvoiceNumber;
                invoice.InvoiceDate = source.InvoiceDate;
                invoice.GeneratingSystem = source.GeneratingSystem;
                invoice.DocumentType = source.DocumentType.ConvertEnum<DocumentTypeType>();
                invoice.DocumentTitle = source.DocumentTitle;
                invoice.InvoiceCurrency = source.InvoiceCurrency.ConvertEnum<CurrencyType>();
                invoice.Language = source.Language.ConvertEnum<LanguageType>();
                invoice.LanguageSpecified = source.LanguageSpecified;
                invoice.IsDuplicateSpecified = false;
                invoice.CancelledOriginalDocument = null;
                #endregion

                #region Delivery
                if (source.Delivery.Item is V4P0.PeriodType)
                {
                    var deliveryType = new PeriodType();
                    deliveryType.FromDate = ((V4P0.PeriodType)source.Delivery.Item).FromDate;
                    deliveryType.ToDate = ((V4P0.PeriodType)source.Delivery.Item).ToDate;
                    invoice.Delivery.Item = deliveryType;
                }
                else
                {
                    var period = new PeriodType();
                    period.FromDate = (DateTime)source.Delivery.Item;
                    invoice.Delivery.Item = period;
                }

                #endregion

                #region Biller
                if (source.Biller != null)
                {
                    invoice.Biller.VATIdentificationNumber = source.Biller.VATIdentificationNumber;
                    invoice.Biller.InvoiceRecipientsBillerID = source.Biller.InvoiceRecipientsBillerID;
                    invoice.Biller.Address = GetAddress(source.Biller.Address);
                    invoice.Biller.FurtherIdentification = GetFurtherIdentification(source.Biller.FurtherIdentification);
                }

                #endregion

                #region Receipient
                if (source.InvoiceRecipient != null)
                {
                    invoice.InvoiceRecipient.BillersInvoiceRecipientID = source.InvoiceRecipient.BillersInvoiceRecipientID;
                    invoice.InvoiceRecipient.VATIdentificationNumber = source.InvoiceRecipient.VATIdentificationNumber;
                    invoice.InvoiceRecipient.Address = GetAddress(source.InvoiceRecipient.Address);

                    if (source.InvoiceRecipient.OrderReference != null)
                    {
                        invoice.InvoiceRecipient.OrderReference.OrderID = source.InvoiceRecipient.OrderReference.OrderID;
                        invoice.InvoiceRecipient.OrderReference.ReferenceDateSpecified = source.InvoiceRecipient.OrderReference.ReferenceDateSpecified;
                        invoice.InvoiceRecipient.OrderReference.ReferenceDate = source.InvoiceRecipient.OrderReference.ReferenceDate;
                    }
                }

                #endregion

                #region Details
                if (source.Details != null)
                {
                    invoice.Details.HeaderDescription = source.Details.HeaderDescription;
                    invoice.Details.FooterDescription = source.Details.FooterDescription;

                    var itList = new List<ItemListType>();
                    if (source.Details.ItemList != null)
                    {
                        foreach (Schema.ebInterface4p0.ItemListType srcItemList in source.Details.ItemList)
                        {
                            ItemListType item = new ItemListType();

                            item.HeaderDescription = null;

                            var detailsList = GetListLineItems(srcItemList);
                            item.ListLineItem = detailsList;
                            itList.Add(item);
                        }
                    }
                    invoice.Details.ItemList = itList;
                }

                #endregion

                #region Tax
                invoice.Tax = new TaxType();
                //invoice.Tax.VAT 
                var vatItemList = new List<VATItemType>();
                if ((source.Tax != null) && (source.Tax.VAT != null) && (source.Tax.VAT.Items != null))
                {
                foreach (var item in source.Tax.VAT.Items)
                {
                    if (item is V4P0.ItemType)
                    {
                        V4P0.ItemType srcVat = item as V4P0.ItemType;
                        VATRateType vat = new VATRateType()
                        {
                            TaxCode = srcVat.TaxRate.TaxCode,
                            Value = srcVat.TaxRate.Value
                        };
                        VATItemType vatItem = new VATItemType()
                        {
                            Amount = srcVat.Amount,
                            TaxedAmount = srcVat.TaxedAmount,
                            Item = vat
                        };
                        vatItemList.Add(vatItem);
                    }
                    else
                    {
                        TaxExemptionType taxExemption = new TaxExemptionType()
                        {
                            Value = item as string
                        };
                        VATItemType vatItem = new VATItemType()
                        {
                            Item = taxExemption
                        };
                        vatItemList.Add(vatItem);

                    }
                }
                } 
                invoice.Tax.VAT = vatItemList;//.ToArray();

                #endregion

                #region Amount
                invoice.TotalGrossAmount = source.TotalGrossAmount;
                invoice.PayableAmount = source.TotalGrossAmount;

                #endregion

                #region PaymentMethod
                if (source.PaymentMethod.GetType() == typeof(V4P0.UniversalBankTransactionType))
                {
                    V4P0.UniversalBankTransactionType txType = source.PaymentMethod as V4P0.UniversalBankTransactionType;
                    invoice.PaymentMethod = new PaymentMethodType();
                    invoice.PaymentMethod.Item = new UniversalBankTransactionType();
                    ((UniversalBankTransactionType)invoice.PaymentMethod.Item).BeneficiaryAccount = new List<AccountType>()
                {
                    new AccountType()
                    {
                        BIC = txType.BeneficiaryAccount.First().BIC,
                        BankName = txType.BeneficiaryAccount.First().BankName,
                        IBAN = txType.BeneficiaryAccount.First().IBAN,
                        BankAccountOwner = txType.BeneficiaryAccount.First().BankAccountOwner

                    },
                };
                }

                #endregion

                #region Paymentconditions
                invoice.PaymentConditions = new PaymentConditionsType()
                {
                    DueDate = source.PaymentConditions.DueDate,
                };
                if (source.PaymentConditions.Discount != null)
                {
                    // invoice.PaymentConditions.Discount = new List<DiscountType>();
                    var discountList = new List<DiscountType>();
                    foreach (V4P0.DiscountType srcDiscount in source.PaymentConditions.Discount)
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
                    invoice.PaymentConditions.Discount = discountList;
                }
                #endregion
            }
            catch (Exception ex)
            {
                // What to do????
                var ex1 = new FormatException("Fehler beim Erstellen der Invoice", ex);
                throw ex1;
            }
            
            return invoice;
        }

        private static List<ListLineItemType> GetListLineItems(Schema.ebInterface4p0.ItemListType srcItemList)
        {
            var detailsList = new List<ListLineItemType>();
            if (srcItemList.ListLineItem == null)
                return detailsList;
            foreach (V4P0.ListLineItemType srcLineItem in srcItemList.ListLineItem)
            {
                ListLineItemType lineItem = new ListLineItemType();

                lineItem.PositionNumber = srcLineItem.PositionNumber;
                lineItem.Description = srcLineItem.Description.ToList();
                lineItem.AdditionalInformation = null;
                var artNrList = GetArticleNumberList(srcLineItem);
                lineItem.ArticleNumber = artNrList;
                
                // Menge
                lineItem.Quantity = new UnitType();
                lineItem.Quantity.Unit = srcLineItem.Quantity.Unit;
                lineItem.Quantity.Value = srcLineItem.Quantity.Value;


                // Einzelpreis
                lineItem.UnitPrice = new UnitPriceType()
                {
                    Value = srcLineItem.UnitPrice,
                    // BaseQuantity = 1,
                    BaseQuantitySpecified = false
                };

                // Steuer
                if (srcLineItem.TaxRate != null)
                {
                    VATRateType vatRate = new VATRateType()
                    {
                        TaxCode = srcLineItem.TaxRate.TaxCode,
                        Value = srcLineItem.TaxRate.Value
                    };
                    lineItem.Item = vatRate;
                }
                else
                {
                    lineItem.Item = null;
                }

                // Auftragsreferenz
                if (srcLineItem.InvoiceRecipientsOrderReference != null)
                {
                    lineItem.InvoiceRecipientsOrderReference = new OrderReferenceDetailType();
                    if (srcLineItem.InvoiceRecipientsOrderReference != null)
                    {
                    lineItem.InvoiceRecipientsOrderReference = new OrderReferenceDetailType()
                    {
                        OrderID = srcLineItem.InvoiceRecipientsOrderReference.OrderID,
                        OrderPositionNumber = srcLineItem.InvoiceRecipientsOrderReference.OrderPositionNumber
                        
                    };

                    }
                }
                // Rabatte / Zuschläge
                lineItem.ReductionAndSurchargeListLineItemDetails = new ReductionAndSurchargeListLineItemDetailsType();
                if (srcLineItem.ReductionAndSurchargeListLineItemDetails != null)
                {
                    var redItems = new List<object>();
                    lineItem.ReductionAndSurchargeListLineItemDetails.ItemsElementName = new List<ItemsChoiceType>();
                    // new ItemsChoiceType[srcLineItem.ReductionAndSurchargeListLineItemDetails.ItemsElementName.Count()];
                    foreach (var reduction in srcLineItem.ReductionAndSurchargeListLineItemDetails.Items)
                    {
                        ReductionAndSurchargeBaseType red = new ReductionAndSurchargeBaseType()
                        {
                            Amount = reduction.Amount,
                            AmountSpecified = reduction.AmountSpecified,
                            BaseAmount = reduction.BaseAmount,
                            Comment = "",
                            Percentage = reduction.Percentage,
                            PercentageSpecified = reduction.PercentageSpecified
                        };
                        redItems.Add(red);
                    }
                    lineItem.ReductionAndSurchargeListLineItemDetails.Items = redItems;
                    foreach (
                        V4P0.ItemsChoiceType choiceType in
                            srcLineItem.ReductionAndSurchargeListLineItemDetails.ItemsElementName)
                    {
                        ItemsChoiceType chType = new ItemsChoiceType();
                        chType = choiceType.ConvertEnum<ItemsChoiceType>();
                        lineItem.ReductionAndSurchargeListLineItemDetails.ItemsElementName.Add(chType);
                    }
                }
                lineItem.Description = srcLineItem.Description.ToList();
                lineItem.DiscountFlag = srcLineItem.DiscountFlag;
                lineItem.DiscountFlagSpecified = srcLineItem.DiscountFlagSpecified;
                lineItem.LineItemAmount = srcLineItem.LineItemAmount;
                detailsList.Add(lineItem);
            }
            return detailsList;
        }

        private static List<ArticleNumberType> GetArticleNumberList(Schema.ebInterface4p0.ListLineItemType srcLineItem)
        {
            var artNrList = new List<ArticleNumberType>();
            if (srcLineItem.ArticleNumber == null)
                return artNrList;
            foreach (
                Schema.ebInterface4p0.ArticleNumberType numberType in
                    srcLineItem.ArticleNumber)
            {
                artNrList.Add(new ArticleNumberType()
                {
                    ArticleNumberType1 =
                        numberType.ArticleNumberType1.ConvertEnum<ArticleNumberTypeType>(),
                    Text = numberType.Text.ToList()
                });
            }
            return artNrList;
        }

        private static AddressType GetAddress(V4P0.AddressType address)
        {

            AddressType addrNew = new AddressType();
            if (address == null)
            {
                return addrNew;
            }
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

        private static CountryType GetCountry(Schema.ebInterface4p0.CountryType countryType)
        {
            if (countryType == null)
                return null;
            CountryType cty = new CountryType();
            cty.CountryCode = countryType.CountryCode.ConvertEnum<CountryCodeType>();
            cty.CountryCodeSpecified = countryType.CountryCodeSpecified;
            cty.Text = countryType.Text.ToList();
            return cty;
        }

        private static List<AddressIdentifierType> GetAddressIdentifier(V4P0.AddressIdentifierType adrIn)
        {
            if (adrIn == null)
            {
                return null;

            }
            if (adrIn.AddressIdentifierType1Specified == false)
            {
                return null;
            }

            List<AddressIdentifierType> adIdList = new List<AddressIdentifierType>();

            foreach (string s in adrIn.Text)
            {

                AddressIdentifierType adId = new AddressIdentifierType();

                adId.AddressIdentifierType1 =
                    (AddressIdentifierTypeType)Enum.Parse(typeof(AddressIdentifierTypeType),
                        adrIn.AddressIdentifierType1.ToString());
                adId.AddressIdentifierType1Specified = true;
                adId.Value = s;
                adIdList.Add(adId);
            }
            return adIdList;
        }

        private static List<FurtherIdentificationType> GetFurtherIdentification(V4P0.FurtherIdentificationType[] furtherID)
        {
            List<FurtherIdentificationType> fIdList = new List<FurtherIdentificationType>();
            if (furtherID == null)
            {
                return fIdList;
            }

            List<string> supportedIds = Enum.GetNames(typeof(FurtherIdentificationType.SupportedIds)).ToList();
            foreach (V4P0.FurtherIdentificationType item in furtherID)
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
