using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace ebIValidation
{
    public class EmailAddressValidatorAttribute : ValidatorAttribute
    {
        public EmailAddressValidatorAttribute()
        {
            _canBeEmpty = false;
        }

        public EmailAddressValidatorAttribute(bool canBeEmpty)
        {
            _canBeEmpty = canBeEmpty;
        }
        private bool _canBeEmpty;
        public bool CanBeEmpty { get { return _canBeEmpty; } }
        protected override Validator DoCreateValidator(System.Type targetType)
        {
            return new EmailAddressValidator(this.CanBeEmpty,this.MessageTemplate, this.Tag);
        }
    }
}