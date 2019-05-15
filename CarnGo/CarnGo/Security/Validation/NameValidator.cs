using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarnGo.Security.Validation
{
    public  class NameValidator : IValidator<string>
    {
        public List<string> ValidationErrorMessages { get; }
        public bool Validate(string toValidate)
        {
            ValidationErrorMessages.Clear();

            var pattern = @"[a - zA - Z0 - 9]";

            var validation = Regex.Match(toValidate, pattern);

            if (!validation.Success)
                ValidationErrorMessages.Add("Names can only be letters");

            return validation.Success;
        }
    }
}
