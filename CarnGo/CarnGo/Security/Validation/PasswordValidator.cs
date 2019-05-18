using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace CarnGo.Security
{
    public class PasswordValidator : IValidator<SecureString>
    {
        public List<string> ValidationErrorMessages { get; } = new List<string>();

        public bool Validate(SecureString password)
        {
            ValidationErrorMessages.Clear();
            bool result = true;
            if (ValidateEmptyPassword(password) == false)
                return false;

            result &= ValidatePasswordLength(password);
            result &= ValidateHasNumber(password);
            result &= ValidateHasCharacter(password);

            return result;
        }

        private bool ValidateEmptyPassword(SecureString password)
        {
            if (!string.IsNullOrWhiteSpace(password.ConvertToString()))
                return true;
            ValidationErrorMessages.Add("Password can't be empty");
            return false;

        }

        private bool ValidatePasswordLength(SecureString password)
        {
            if (password.Length >= 6)
                return true;
            ValidationErrorMessages.Add("Password must be longer than 6 characters");
            return false;
        }

        private bool ValidateHasNumber(SecureString password)
        {
            if (password.ConvertToString().Any(char.IsDigit))
                return true;
            ValidationErrorMessages.Add("The password must contain a number");
            return false;
        }

        private bool ValidateHasCharacter(SecureString password)
        {
            if (password.ConvertToString().Any(char.IsLetter))
                return true;
            ValidationErrorMessages.Add("The password must contain a character");
            return false;

        }
    }
}