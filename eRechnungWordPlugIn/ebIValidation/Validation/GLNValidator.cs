﻿using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace ebIValidation
{
    public class GLNValidator : Validator<string>
    {
        //  bool _isBund = false;
        public GLNValidator(string messageTemplate, string tag)
            : base(messageTemplate, tag)
        {
        }

        protected override string DefaultMessageTemplate
        {
            get { throw new NotImplementedException(); }
        }

        protected override void DoValidate(string objectToValidate, object currentTarget, string key,
            ValidationResults validationResults)
        {
            if (string.IsNullOrEmpty(objectToValidate))
            {
                return;
            }
            bool isBund = (Tag == "GLN-Bund");
            var err = IsValidGln(objectToValidate, isBund);
            if (err != GLN_ErrorCode.OK)
            {
                LogValidationResult(validationResults, MessageTemplate + " " + GlnErrorToString(err), currentTarget, key);
            }
        }

        public enum GLN_ErrorCode
        {
            OK = 0,
            NichtNumerisch,
            Längefalsch,
            LängefalschBund,
            Prüfziffer

        }

        public static GLN_ErrorCode IsValidGln(string gln, bool isBund)
        {
            // Muss numerisch sein
            if (!gln.IsNumeric())
            {
                return GLN_ErrorCode.NichtNumerisch;
            }
            // muss eine der fixen Längen haben
            int[] validLen = { 8, 12, 13, 14, 17, 18 };
            if (isBund)
            {
                if (gln.Length != 13)
                {
                    return GLN_ErrorCode.LängefalschBund;
                }
            }
            else
            {
                if (!validLen.Contains(gln.Length))
                {
                    return GLN_ErrorCode.Längefalsch;
                }
            }
            // Prüfziffer muss stimmen = letzte stelle
            int checkSum = 0;
            int mult = 3;
            for (int i = (gln.Length - 2); i >= 0; i--)
            {
                int iNum = int.Parse(gln.Substring(i, 1));
                iNum = iNum * mult;
                checkSum = checkSum + iNum;
                mult = 4 - mult;
            }
            int iPrz = int.Parse(gln.Substring(gln.Length - 1, 1));
            checkSum = (10 - (checkSum % 10)) % 10;
            if (iPrz == checkSum)
            {
                return GLN_ErrorCode.OK;
            }
            return GLN_ErrorCode.Prüfziffer;
        }

        public static string GlnErrorToString(GLN_ErrorCode cde)
        {
            Dictionary<GLN_ErrorCode, string> msgDict = new Dictionary<GLN_ErrorCode, string>()
                {
                    {GLN_ErrorCode.OK, "OK"},
                    {GLN_ErrorCode.NichtNumerisch, "GLN nicht numerisch"},
                    {GLN_ErrorCode.Längefalsch, "GLN hat ein ungültige Anzahl von Zeichen. Es sind 8, 12, 13, 14, 17, 18 Zeichen erlaubt"},
                    {GLN_ErrorCode.LängefalschBund, "GLN kann leer sein oder muss genau 13 Zeichen enthalten."},
                    {GLN_ErrorCode.Prüfziffer, "GLN Prüfziffer ist falsch"}
                    
                };
            return msgDict[cde];
        }
    }

}

