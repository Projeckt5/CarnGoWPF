using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnGo.Security.Validation
{
    public class CarAgeValidator : IValidator<int>
    {

        public CarAgeValidator()
        {
            ValidationErrorMessages = new List<string>();
        }
        public List<string> ValidationErrorMessages { get; }
        public bool Validate(int toValidate)
        {
            ValidationErrorMessages.Clear();
            var validation = (toValidate > 0);

            if (!validation)
                ValidationErrorMessages.Add("Must be a positive value");

            return validation;
        }
    }
}
