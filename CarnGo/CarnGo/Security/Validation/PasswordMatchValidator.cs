using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace CarnGo.Security
{
    public class PasswordMatchValidator : IValidator<List<SecureString>>
    {
        public List<string> ValidationErrorMessages { get; } = new List<string>();
        public bool Validate(List<SecureString> passwords)
        {
            ValidationErrorMessages.Clear();
            //Check if all passwords are the same
            if (passwords.All(x => x.ConvertToString() == passwords.First().ConvertToString()))
            {
                return true;
            }
            ValidationErrorMessages.Add("Passwords are not identical");
            return false;
        }
    }
}