using ExtensionMethods;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace ebIValidation
{
    public class DecimalFractionValidator : Validator<decimal>
    {
        internal int DecimalFractions;
        public DecimalFractionValidator(int decimalFractions, string messageTemplate, string tag) : base(messageTemplate, tag)
        {
            DecimalFractions = decimalFractions;
        }

        protected override string DefaultMessageTemplate
        {
            get { throw new System.NotImplementedException(); }
        }

        protected override void DoValidate(decimal objectToValidate, object currentTarget, string key, ValidationResults validationResults)
        {
            if (!objectToValidate.CheckDigits(DecimalFractions))
            {
                LogValidationResult(validationResults, MessageTemplate, currentTarget, key);
            }

        }
    }
}