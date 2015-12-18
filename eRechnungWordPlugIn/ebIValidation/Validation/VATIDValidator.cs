using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace ebIValidation
{
    public class VATIDValidator : Validator<string>
    {
        public VATIDValidator(string messageTemplate, string tag, decimal? invoiceTotalValue ) : base(messageTemplate, tag)
        {
        }

        protected override string DefaultMessageTemplate
        {
            get { throw new NotImplementedException(); }
        }

        protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            throw new NotImplementedException();
        }
    }
}