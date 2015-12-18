using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace ebIViewModels.Validation
{
    public class DecimalFractionValidatorAttribute : ValidatorAttribute
    {
        private int _decimalFractions;
        public int DecimalFractions { get { return _decimalFractions; } }

        public DecimalFractionValidatorAttribute(int decimalFractions)
        {
            this._decimalFractions = decimalFractions;
        }

        protected override Validator DoCreateValidator(Type targetType)
        {
            return new DecimalFractionValidator(this.DecimalFractions, this.MessageTemplate, this.Tag);
        }
    }
}