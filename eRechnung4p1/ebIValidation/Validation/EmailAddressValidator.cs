using System;
using ExtensionMethods;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace ebIValidation
{
    class EmailAddressValidator : Validator<string>
    {
        internal bool CanBeEmpty =true;
        public EmailAddressValidator(string messageTemplate, string tag) : base(messageTemplate, tag)
        {

        }

        public EmailAddressValidator(bool canBeEmpty,string messageTemplate, string tag)
            : base(messageTemplate, tag)
        {
            CanBeEmpty = canBeEmpty;
        }

        protected override string DefaultMessageTemplate
        {
            get { throw new NotImplementedException(); }
        }

        protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            if (CanBeEmpty && string.IsNullOrEmpty(objectToValidate))
            {
                return;
            }
            if (!objectToValidate.IsValidEmail())
            {
                LogValidationResult(validationResults,MessageTemplate,currentTarget,key);
            }
        }
    }
}
