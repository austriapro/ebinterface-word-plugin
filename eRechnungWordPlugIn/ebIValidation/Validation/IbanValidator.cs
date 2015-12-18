using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExtensionMethods;

namespace ebIValidation
{
    public class IbanValidator : Validator<string>
    {
        public IbanValidator(string messageTemplate, string tag)
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
           // bool rc = false;
            var status = objectToValidate.IsIbanValid(true);

            if (!status.IsValid)
            {
                LogValidationResult(validationResults, MessageTemplate + " " + status.Message, currentTarget, key);
            }
        }

    }
}
