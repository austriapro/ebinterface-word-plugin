using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;
using ebIModels.Models;
using SRC = ebIModels.Schema.ebInterface4p0;
using ebIModels.Schema;
using SettingsManager;

namespace ebIModels.Mapping.V4p0
{
    public static partial class MapInvoice
    {
        /// <summary>
        /// Mapped ebInterface4p1 InvoiceType auf InvoiceType Model
        /// </summary>
        /// <param name="source">ebInterface4p1 InvoiceType</param>
        /// <returns>InvoiceType Model</returns>
        internal static IInvoiceModel MapV4P0ToVm(SRC.InvoiceType source)
        {
            IInvoiceModel invoice = InvoiceFactory.CreateInvoice();
            #region Rechnungskopf
            invoice.InvoiceNumber = source.InvoiceNumber;
            invoice.InvoiceDate = source.InvoiceDate;
            invoice.GeneratingSystem = source.GeneratingSystem;
            invoice.DocumentTitle = source.DocumentTitle;
            invoice.DocumentType = source.DocumentType.ConvertEnum<DocumentTypeType>();
            invoice.InvoiceCurrency = source.InvoiceCurrency.ConvertEnum<CurrencyType>(); // source.InvoiceCurrency.ConvertEnum<CurrencyType>();
            invoice.Language = source.Language.ConvertEnum<LanguageType>();

            if (source.CancelledOriginalDocument == null)
            {
                invoice.CancelledOriginalDocument = null;
            }
            else
            {
                invoice.CancelledOriginalDocument = new CancelledOriginalDocumentType()
                {
                    DocumentTypeSpecified = false,

                    InvoiceNumber = source.CancelledOriginalDocument
                };
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
            if (source.InvoiceRecipient.OrderReference != null)
            {
                invoice.InvoiceRecipient.OrderReference.OrderID = source.InvoiceRecipient.OrderReference.OrderID;
                invoice.InvoiceRecipient.OrderReference.ReferenceDateSpecified = source.InvoiceRecipient.OrderReference.ReferenceDateSpecified;
                invoice.InvoiceRecipient.OrderReference.ReferenceDate = source.InvoiceRecipient.OrderReference.ReferenceDate;

            }
            invoice.InvoiceRecipient.FurtherIdentification = null;
            invoice.InvoiceRecipient.SubOrganizationID = source.InvoiceRecipient.SubOrganizationID;
            invoice.InvoiceRecipient.AccountingArea = source.InvoiceRecipient.AccountingArea;
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
                            Value = srcLineItem.UnitPrice
                        };

                        // Auftragsreferenz
                        lineItem.InvoiceRecipientsOrderReference.OrderID =
                            srcLineItem?.InvoiceRecipientsOrderReference?.OrderID;
                        lineItem.InvoiceRecipientsOrderReference.OrderPositionNumber =
                            srcLineItem?.InvoiceRecipientsOrderReference?.OrderPositionNumber;

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

                        // lineItem.LineItemAmount = srcLineItem.LineItemAmount;
                        lineItem.ReCalcLineItemAmount();
                        // Steuer
                        lineItem.TaxItem = MapVatItemType2Vm(srcLineItem.TaxRate, lineItem.LineItemAmount);
                        item.ListLineItem.Add(lineItem);
                    }
                    invoice.Details.ItemList.Add(item);
                }

            #endregion

            #region Tax
            invoice.Tax.TaxItem.Clear();
            if (source.Tax.VAT.Items.Any())
            {
                foreach (var item in source.Tax.VAT.Items)
                {
                    if (item is SRC.ItemType)
                    {

                        SRC.ItemType vatItem = item as SRC.ItemType;

                        TaxItemType taxItem = new TaxItemType()
                        {
                            TaxPercent = new TaxPercentType()
                            {
                                TaxCategoryCode = PlugInSettings.Default.GetValueFromPercent(vatItem.TaxRate.Value).Code,
                                Value = vatItem.TaxRate.Value
                            },
                            TaxAmountSpecified = false
                        };
                        invoice.Tax.TaxItem.Add(taxItem);
                        break;
                    }
                    else
                    {

                        TaxItemType taxItemVat = new TaxItemType()
                        {
                            TaxPercent = new TaxPercentType()
                            {
                                Value = 0,
                                TaxCategoryCode = PlugInSettings.VStBefreitCode
                            },
                            TaxAmount = 0,
                            TaxableAmount = 0,
                            TaxAmountSpecified = false,
                            Comment = (string)item
                        };
                        invoice.Tax.TaxItem.Add(taxItemVat);
                    }
                }
            }

            #endregion

            #region Amount
            invoice.TotalGrossAmount = source.TotalGrossAmount;
            invoice.PayableAmount = source.TotalGrossAmount;
            #endregion

            #region PaymentMethod
            invoice.PaymentMethod.Comment = source.PaymentMethod.Comment;
            {
                SRC.UniversalBankTransactionType txType = source.PaymentMethod as SRC.UniversalBankTransactionType;
                invoice.PaymentMethod = new PaymentMethodType
                {
                    Item = new UniversalBankTransactionType()
                };
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
                Name = address.Contact,
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
                Phone = address.Phone,
                POBox = address.POBox,
                Email = address.Email,
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
            if (countryType.Text != null)
            {
                cty.CountryCode = countryType.CountryCode.ToString();
                cty.Value = CountryCodes.GetFromCode(cty.CountryCode.ToString()).Country;
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

        private static TaxItemType MapVatItemType2Vm(object vatItem, decimal taxableAmount)
        {
            if (vatItem == null)
                return null;
            if (vatItem.GetType() != typeof(SRC.TaxRateType))
            {
                string taxExemption = (string)vatItem;
                TaxItemType taxItem = new TaxItemType()
                {
                    TaxableAmount = taxableAmount,
                    TaxPercent = new TaxPercentType()
                    {
                        TaxCategoryCode = PlugInSettings.VStBefreitCode,
                        Value = 0
                    },
                    Comment = taxExemption
                };
                return taxItem;
            }
            SRC.TaxRateType vATRate = (SRC.TaxRateType)vatItem;
            TaxItemType taxItemVat = new TaxItemType()
            {
                TaxableAmount = taxableAmount,
                TaxPercent = new TaxPercentType()
                {
                    Value = vATRate.Value,
                    TaxCategoryCode = PlugInSettings.Default.GetValueFromPercent(vATRate.Value).Code
                },
            };
            return taxItemVat;
        }

        private static List<ArticleNumberType> GetArtikelList(SRC.ArticleNumberType[] srcArticle)
        {
            List<ArticleNumberType> artNrList = new List<ArticleNumberType>();
            if (srcArticle == null)
            {
                return artNrList;
            }
            foreach (SRC.ArticleNumberType articleNumberType in srcArticle)
            {
                ArticleNumberType art = new ArticleNumberType
                {
                    Value = articleNumberType.Text[0],
                    ArticleNumberType1Specified = articleNumberType.ArticleNumberType1Specified,
                    ArticleNumberType1 = articleNumberType.ArticleNumberType1.ConvertEnum<ArticleNumberTypeType>()
                };
                artNrList.Add(art);

            }
            return artNrList;
        }
        private static List<AddressIdentifierType> GetAddressIdentifier(SRC.AddressIdentifierType addressIdentifier)
        {
            if (addressIdentifier == null)
            {
                return null;

            }

            List<AddressIdentifierType> adrOutList = new List<AddressIdentifierType>();
            AddressIdentifierType adId = new AddressIdentifierType();

            if (addressIdentifier.AddressIdentifierType1Specified)
            {
                adId.AddressIdentifierType1 = addressIdentifier.AddressIdentifierType1.ConvertEnum<AddressIdentifierTypeType>();
                adId.Value = addressIdentifier.Text[0];

            }
            adId.Value = addressIdentifier.Text[0];
            adrOutList.Add(adId);

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
