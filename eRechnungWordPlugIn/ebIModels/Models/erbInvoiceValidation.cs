using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using ebIModels.Models;
using ExtensionMethods;
using ebIModels.Schema;

namespace ebIModels.Models
{
    /// <summary>
    /// Enthält die Datenprüfungen für eRechnung an doe öffentl. Verwaltung
    /// </summary>
    /// <seealso cref="ebIModels.Models.InvoiceModel" />
    /// <seealso cref="ebIModels.Models.IInvoiceModel" />
    public partial class InvoiceModel 
    {
        /// <summary>
        /// Prüft ob Rechnung erb.gv.at. konform ist:
        /// </summary>
        /// <returns>Eine <see cref="EbInterfaceResult"/> Instanz</returns>
        /// <remarks>
        /// Folgende Prüfungen werden durchgeführt        
        /// <list type="bullet">
        ///  <item>Es werden nur Rechnungen  oder Gutschriften akzeptiert <see cref="DocumentType"/></item>
        ///  <item>eine Rechnung darf sich nur auf eine Bestellung beziehen</item>
        ///  <item>Bestellposition muss angegeben sein</item>
        ///  <item>CancelledOriginalDocument darf nicht angegeben sein</item>
        ///  <item>OrderID muss angegeben sein 
        /// <para>Regeln:
        /// <list type="bullet">
        /// <item>3-stellig alphanumerisch oder</item>
        /// <item>3-stellig alphanumerisch ":" Irgendwas (Gesamtlänge maximal 35 Stellen)</item>
        /// <item>10-stellig numerisch</item>
        /// </list>
        /// </para>
        /// </item>
        ///  <item>Biller E-Mail verpflichtend</item>
        ///  <item>Max. 999 Rechnungszeilen</item>
        ///  <item>Skonto &gt; 0 und &lt; 100</item>
        ///  <item>Max. 2 Discount Elemente</item>
        ///  <item>Nur ein Benificiary Account</item>
        ///  <item>IBAN und BIC</item>
        ///  <item>Kein NoPayment</item>
        ///  <item>OrderId in ListLineItem darf nicht leer sein</item>
        ///  <item>GLN Optional</item>
        ///  <item>Invoice/Biller/InvoiceRecipientsBillerID 10 Stell. alphanumerisch</item>
        /// </list>
        /// </remarks>
        public EbInterfaceResult IsValidErbInvoice()
        {
            EbInterfaceResult result = new EbInterfaceResult();
            Schema.ebInterface5p0.InvoiceType invoice = Mapping.MapInvoice.MapModelToV5p0(this);
            
            if (result.ResultType != ResultType.IsValid)
            {
                return result;
            }
            if ((DocumentType != DocumentTypeType.Invoice) && (DocumentType != DocumentTypeType.CreditMemo))
            {
                result.ResultMessages.Add(new ResultMessage()
                {
                    Field = "DocumentType",
                    Severity = MessageType.Error,
                    Message = "Das Feld muss entweder DocumentTypeType.Invoice oder DocumentTypeType.CreditMemo enthalten"
                });
            }

            // A04 OrderID muss angegeben sein 
            if ((InvoiceRecipient.OrderReference == null) || (InvoiceRecipient.OrderReference.OrderID == null))
            {
                result.ResultMessages.Add(new ResultMessage()
                {
                    Field = "InvoiceRecipient.OrderReference.OrderID",
                    Severity = MessageType.Error,
                    Message = "Das Feld InvoiceRecipient.OrderReference.OrderID darf nicht leer sein"
                });
                result.ResultType = ResultType.ErbValidationIssue;
                return result;
            }
            if (string.IsNullOrWhiteSpace(InvoiceRecipient.OrderReference.OrderID))
            {
                result.ResultMessages.Add(new ResultMessage()
                {
                    Field = "InvoiceRecipient.OrderReference.OrderID",
                    Severity = MessageType.Error,
                    Message = "Das Feld InvoiceRecipient.OrderReference.OrderID darf nicht leer sein"
                });
            }

            //Regeln:
            // 3-stellig alphanumerisch oder
            // 3-stellig alphanumerisch „:“ Irgendwas (Gesamtlänge maximal 35 Stellen)
            // 10-stellig numerisch

            string msg = null;
            msg = IsValidOrderIdBund(InvoiceRecipient.OrderReference.OrderID, out OrderIdTypeType orderIdType);
            if (msg != null)
            {
                result.ResultMessages.Add(new ResultMessage()
                {
                    Field = "InvoiceRecipient.OrderReference.OrderID",
                    Severity = MessageType.Error,
                    Message = msg
                });
            }

            // A01 eine Rechnung = eine Bestellung
            // A02 Bestellposition muss angegeben sein
            // A12 OrderId in ListLineItem <> leer

            int n = 0;
            foreach (ItemListType itemListType in Details.ItemList)
            {
                int i = 0;
                foreach (ListLineItemType lineItem in itemListType.ListLineItem)
                {
                    if (orderIdType != OrderIdTypeType.EKGR) // Bei Einkäufergruppe muss keine Positionsnummer angegeben werden
                    {
                    
                        if ((lineItem.InvoiceRecipientsOrderReference == null) ||
                            (lineItem.InvoiceRecipientsOrderReference.OrderPositionNumber == null) ||
                            (string.IsNullOrWhiteSpace(lineItem.InvoiceRecipientsOrderReference.OrderPositionNumber)))
                        {
                            result.ResultMessages.Add(new ResultMessage()
                            {
                                Field =
                                    string.Format(
                                        "Details.Itemlist[{0}].ListLineItem[{1}].InvoiceRecipientsOrderReference.OrderPositionNumber",
                                        n, i),
                                Severity = MessageType.Error,
                                Message = string.Format("Das Feld darf nicht leer sein.")

                            });
                        }

                    }
                    else
                    {
                        if (InvoiceRecipient.OrderReference.OrderID != lineItem.InvoiceRecipientsOrderReference.OrderID)
                        {
                            result.ResultMessages.Add(new ResultMessage()
                            {
                                Field =
                                    string.Format(
                                    "Details.Itemlist[{0}].ListLineItem[{1}].InvoiceRecipientsOrderReference.OrderID", n, i),
                                Message =
                                    string.Format(
                                        "Das Feld muss gleich InvoiceRecipient.OrderReference.OrderID sein und darf nicht leer sein und soll mit InvoiceRecipient.OrderReference.OrderID übereinstimmen.",
                                        i)
                            });

                        }
                    }
                    i = i + 1;
                }
            }


            // A03 CancelledOriginalDocument darf nicht angegeben sein
            //if (!string.IsNullOrWhiteSpace(CancelledOriginalDocument))
            //{
            //    result.ResultMessages.Add(new ResultMessage()
            //    {
            //        Field = "CancelledOriginalDocument",
            //        Severity = MessageType.High,
            //        Message = "Das Feld muss leer sein"
            //    });

            //}


            // A05 Biller E-Mail verpflichtend
            if (Biller.Address.Email.Count()<1)
            {
                result.ResultMessages.Add(new ResultMessage()
                {
                    Field = "Biller.Address.Email",
                    Severity = MessageType.Error,
                    Message = "Feld darf nicht leer sein und muss eine gültige E-Mail Adresse enthalten"
                });
            }
            // A06 Max. 999 Rechnungszeilen
            if (Details.ItemList[0].ListLineItem.Count() > 999)
            {
                result.ResultMessages.Add(new ResultMessage()
                {
                    Field = "Details.ItemList[0].ListLineItem.Count()",
                    Severity = MessageType.Error,
                    Message = "Es dürfen max. 999 Rechnungszeilen angegeben werden."
                });
            }
            // A08 Max. 2 Discount Elemente
            if (PaymentConditions.Discount != null)
            {
                if (PaymentConditions.Discount.Count() > 2)
                {
                    result.ResultMessages.Add(new ResultMessage()
                    {
                        Field = "PaymentConditions.Discount.Count())",
                        Severity = MessageType.Error,
                        Message = "Es dürfen max. 2 Discount Items angegeben werden."
                    });

                }

                // A07 Skonto >0 und < 100
                int i = 0;
                foreach (var discountType in PaymentConditions.Discount)
                {
                    if (!((discountType.Percentage > 0) && (discountType.Percentage < 100)))
                    {
                        result.ResultMessages.Add(new ResultMessage()
                        {
                            Field = string.Format(" PaymentConditions.Discount[{0}].Percentage", i),
                            Severity = MessageType.Error,
                            Message = "Der Wert muss > 0 und < 100 sein."
                        });
                    }
                    i = i + 1;
                }
            }
            // A09 Nur ein Benificiary Account
            //if (PaymentMethod is UniversalBankTransactionType)
            //{
            //    var benficiaryAccount = PaymentMethod as UniversalBankTransactionType;
            //    if (benficiaryAccount.BeneficiaryAccount.Count() > 1)
            //    {
            //        result.ResultMessages.Add(new ResultMessage()
            //        {
            //            Field = "PaymentMethod.BeneficiaryAccount.Count()",
            //            Severity = MessageType.High,
            //            Message = "Es darf nur ein BeneficiaryAccount angegeben werden"
            //        });
            //    }
            //    // A10 IBAN und BIC
            //    if (string.IsNullOrWhiteSpace(benficiaryAccount.BeneficiaryAccount[0].IBAN))
            //    {
            //        result.ResultMessages.Add(new ResultMessage()
            //        {
            //            Field = "PaymentMethod.BeneficiaryAccount.IBAN",
            //            Severity = MessageType.High,
            //            Message = "Das Feld darf nicht leer sein"
            //        });

            //    }
            //    if (string.IsNullOrWhiteSpace(benficiaryAccount.BeneficiaryAccount[0].BIC))
            //    {
            //        result.ResultMessages.Add(new ResultMessage()
            //        {
            //            Field = "PaymentMethod.BeneficiaryAccount.BIC",
            //            Severity = MessageType.High,
            //            Message = "Das Feld darf nicht leer sein"
            //        });

            //    }
            //}

            // A11 Kein NoPayment
            //if (PaymentMethod is NoPaymentType)
            //{
            //    result.ResultMessages.Add(new ResultMessage()
            //    {
            //        Field = "PaymentMethod",
            //        Severity = MessageType.High,
            //        Message = "Der Type darf nicht NoPaymentType sein"
            //    });

            //}

            // A14 GLN Optional
            if (Biller.InvoiceRecipientsBillerID != null)
            {
                // A14 Invoice/Biller/InvoiceRecipientsBillerID 10 Stell. alpha
                if (!((Biller.InvoiceRecipientsBillerID.Length <= 10) && (Biller.InvoiceRecipientsBillerID.Length >= 1)))
                {
                    result.ResultMessages.Add(new ResultMessage()
                    {
                        Field = "Biller.InvoiceRecipientsBillerID",
                        Severity = MessageType.Error,
                        Message = "Das Feld muss max. 10 Stell. lang sein"
                    });

                }
                else
                {
                    if (!Biller.InvoiceRecipientsBillerID.IsAlphaNum())
                    {
                        result.ResultMessages.Add(new ResultMessage()
                        {
                            Field = "Biller.InvoiceRecipientsBillerID",
                            Severity = MessageType.Error,
                            Message = "Das Feld darf nur  alphanumerische Zeichen enthalten."

                        });
                    }
                }

            }
            else
            {
                result.ResultMessages.Add(new ResultMessage()
                {
                    Field = "Biller.InvoiceRecipientsBillerID",
                    Severity = MessageType.Error,
                    Message = "Das Feld darf nicht null sein."

                });

            }
            if (result.ResultMessages.Count > 0)
            {
                result.ResultType = ResultType.ErbValidationIssue;
            }
            else
            {
                result.ResultType = ResultType.IsValid;
            }
            return result;
        }

        private enum OrderIdTypeType
        {
            Unknown = 0,
            OrderId,
            EKGR
        }

        /// <summary>
        /// Determines whether [is valid order identifier bund] [the specified order identifier].
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="orderIdType">Returns the orderId type</param>
        /// <returns></returns>
        private static string IsValidOrderIdBund(string orderId, out OrderIdTypeType orderIdType)
        {
            /*
             * Regeln:
             * 3-stellig alphanumerisch oder
             * 3-stellig alphanumerisch ":" Irgendwas (Gesamtlänge maximal 35 Stellen)
             * 10-stellig numerisch
             */
            orderIdType = OrderIdTypeType.Unknown;
            if (string.IsNullOrEmpty(orderId))
            {
                return "Auftragsreferenz fehlt.";
            }
            int tLen = orderId.Length;

            switch (tLen)
            {
                case 3:
                    if (!orderId.IsAlphaNum())
                    {
                        return "Auftragsreferenz nicht drei-stellig alpha-numerisch.";
                    }
                    break;
                case 10:
                    if (!orderId.IsNumeric())
                    {
                        return "Auftragsreferenz nicht 10-stellig numerisch.";
                    }
                    orderIdType = OrderIdTypeType.OrderId;
                    break;
                default:
                    if (!orderId.Contains(":"))
                    {
                        return "Auftragsreferenz ungültig.";
                    }
                    if (tLen > 35)
                    {
                        return "Auftragsreferenz enthält mehr als 35 Zeichen.";
                    }
                    string x1 = orderId.Substring(3, 1);
                    if (x1 != ":")
                    {
                        return "Austragsreferenz enthält keinen Doppelpunkt an der 4. Stelle.";
                    }
                    string[] xSplt = orderId.Split(':');
                    if (xSplt[1].Length < 1)
                    {
                        return "Auftragsreferenz enthält keine Zeichen nach dem Doppelpunkt.";
                    }
                    string x2 = orderId.Substring(0, 3);
                    if (!x2.IsAlphaNum())
                    {
                        return "In Auftragsreferenz sind die ersten drei Stellen nicht alpha-numerisch.";
                    }
                    orderIdType = OrderIdTypeType.EKGR;
                    break;
            }

            return null;

        }

        /// <summary>
        /// Determines whether [is valid erb invoice] [the specified erb invoice].
        /// </summary>
        /// <param name="erbInvoice">The erb invoice.</param>
        /// <returns>Eine <see cref="EbInterfaceResult"/> Instanz</returns>
        public static EbInterfaceResult IsValidErbInvoice(string erbInvoice)
        {
            var inv = (InvoiceModel)InvoiceFactory.LoadXml(erbInvoice);
            EbInterfaceResult result = inv.IsValidErbInvoice();
            return result;
        }

        /// <summary>
        /// Determines whether [is valid erb invoice] [the specified erb invoice].
        /// </summary>
        /// <param name="erbInvoice">The erb invoice.</param>
        /// <returns>Eine <see cref="EbInterfaceResult"/> Instanz</returns>
        public static EbInterfaceResult IsValidErbInvoice(XmlDocument erbInvoice)
        {

            EbInterfaceResult result = IsValidErbInvoice(erbInvoice.InnerXml);
            return result;
        }

        //internal static List<ebISchema> _schemaInfo = new List<ebISchema>()
        //    {
        //        // new ebISchema(){Prefix = "xsi",Url="http://www.w3.org/2001/XMLSchema-instance",CacheName = "xml.xsd",UseInSchema = false},
        //        new ebISchema(){Prefix = "eb",Url="http://www.ebinterface.at/schema/4p1/",CacheName = "ebInterface4p1.Invoice.xsd",UseInSchema = true},
        //        // new ebISchema(){Prefix = "dsig",Url="http://www.w3.org/2000/09/xmldsig#",CacheName = "xmldsig-core-schema.xsd",UseInSchema = true},
        //        new ebISchema(){Prefix = "ext",Url="http://www.ebinterface.at/schema/4p1/extensions/ext",CacheName = "ebInterface4p1.ebInterfaceExtension.xsd",UseInSchema = true},
        //        new ebISchema(){Prefix = "sv",Url="http://www.ebinterface.at/schema/4p1/extensions/sv",CacheName = "ebInterface4p1.ebInterfaceExtension_SV.xsd",UseInSchema = true}
        //    };


    }
}
