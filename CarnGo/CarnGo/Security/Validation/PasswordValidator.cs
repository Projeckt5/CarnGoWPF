using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace CarnGo.Security
{
    public class PasswordValidator : IValidator
    {
        private readonly SecureString _password;

        public PasswordValidator(SecureString password)
        {
            _password = password;
        }
        public List<string> ValidationErrorMessages { get; } = new List<string>();
        public bool Validate()
        {
            ValidationErrorMessages.Clear();
            bool result = true;
            if (string.IsNullOrWhiteSpace(_password.ConvertToString()))
            {
                ValidationErrorMessages.Add("Password can't be empty");
                return false;
            }
            if (_password.Length < 6)
            {
                ValidationErrorMessages.Add("Password must be longer than 6 characters");
                result = false;
            }
            if (_password.ConvertToString().Any(char.IsDigit) == false)
            {
                ValidationErrorMessages.Add("The password must contain a number");
                result = false;
            }
            if (_password.ConvertToString().Any(char.IsLetter) == false)
            {
                ValidationErrorMessages.Add("The password must contain a character");
                result = false;
            }

            return result;
        }
    }
}