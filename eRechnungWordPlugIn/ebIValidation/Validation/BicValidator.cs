using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace ebIValidation
{
    public class BicValidator : Validator<string>
    {
        public BicValidator(string messageTemplate, string tag)
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
            bool rc = false;
            var reg = new Regex("([a-zA-Z]{4}[a-zA-Z]{2}[a-zA-Z0-9]{2}([a-zA-Z0-9]{3})?)"); // "[0-9A-Za-z]{8}([0-9A-Za-z]{3})?"  Pattern aus xsd
            rc = reg.IsMatch(objectToValidate);
            if (!rc)
            {
                LogValidationResult(validationResults, MessageTemplate, currentTarget, key);
            }
        }

    }
}
