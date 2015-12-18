using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace ebIViewModels.Validation
{
   public class GLNValidatorAttribute : ValidatorAttribute
    {
       protected override Validator DoCreateValidator(Type targetType)
       {
           return new GLNValidator(this.MessageTemplate, this.Tag);
       }
    }
}
