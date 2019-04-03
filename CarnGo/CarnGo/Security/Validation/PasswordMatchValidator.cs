using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace CarnGo.Security
{
    public class PasswordMatchValidator : IValidator
    {
        private readonly SecureString _password;
        private readonly SecureString _passwordToValidateAgainst;

        public PasswordMatchValidator(SecureString password, SecureString passwordToValidateAgainst)
        {
            _password = password;
            _passwordToValidateAgainst = passwordToValidateAgainst;
        }
        public List<string> ValidationErrorMessages { get; } = new List<string>();
        public bool Validate()
        {
            ValidationErrorMessages.Clear();
            //Check if all passwords are the same
            if (_password.ConvertToString() == _passwordToValidateAgainst.ConvertToString())
            {
                return true;
            }
            ValidationErrorMessages.Add("Passwords are not identical");
            return false;
        }
    }
}