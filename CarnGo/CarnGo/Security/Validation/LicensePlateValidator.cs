using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarnGo.Security.Validation
{
    class LicensePlateValidator : IValidator<string>
    {
        public List<string> ValidationErrorMessages { get; }
        public bool Validate(string toValidate)
        {
            bool validated = true;
            var numbersAndLettersPattern = @"[a-zA-Z0-9]";
            ValidationErrorMessages.Clear();

            var numbersAndLetters = Regex.Match(toValidate, numbersAndLettersPattern);

            if (!numbersAndLetters.Success)
            {
                ValidationErrorMessages.Add("The license plate can only contain numbers and letters");
                validated = false;
            }

            if (toValidate.Length < 2)
            {
                ValidationErrorMessages.Add("The license plate must be at least 2 characters");
                validated = false;
            }

            if (toValidate.Length > 8)
            {
                ValidationErrorMessages.Add("The license plate can be no more than 8 characters");
                validated = false;
            }

            return validated;
        }
    }
}
