using System;
using System.Collections.Generic;

namespace CarnGo.Security
{
    public class EmailValidator:  IValidator<string>
    {
        public List<string> ValidationErrorMessages { get; } = new List<string>();

        public bool Validate(string email)
        {
            ValidationErrorMessages.Clear();
            return ValidateEmail(email);
        }

        private bool ValidateEmail(string email)
        {
            try
            {
                System.Net.Mail.MailAddress mailAddress = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                ValidationErrorMessages.Add("Email was not a valid password");
                return false;
            }
        }
    }

}