﻿using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace CarnGo.Security
{
    public class PasswordValidator : IValidator<SecureString>
    {
        public List<string> ValidationErrorMessages { get; } = new List<string>();
        public bool Validate(SecureString secureString)
        {
            ValidationErrorMessages.Clear();
            bool result = true;
            if (string.IsNullOrWhiteSpace(secureString.ConvertToString()))
            {
                ValidationErrorMessages.Add("Password can't be empty");
                return false;
            }
            if (secureString.Length < 6)
            {
                ValidationErrorMessages.Add("Password must be longer than 6 characters");
                result = false;
            }
            if (secureString.ConvertToString().Any(char.IsDigit) == false)
            {
                ValidationErrorMessages.Add("The password must contain a number");
                result = false;
            }
            if (secureString.ConvertToString().Any(char.IsLetter) == false)
            {
                ValidationErrorMessages.Add("The password must contain a character");
                result = false;
            }

            return result;
        }
    }
}