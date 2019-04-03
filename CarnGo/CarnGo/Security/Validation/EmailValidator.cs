using System;
using System.Collections.Generic;

namespace CarnGo.Security
{
    public class EmailValidator:  IValidator
    {
        private readonly string _email;
        public EmailValidator(string email)
        {
            _email = email;
        }
        public List<string> ValidationErrorMessages { get; } = new List<string>();

        public bool Validate()
        {
            ValidationErrorMessages.Clear();
            try
            {
                System.Net.Mail.MailAddress mailAddress = new System.Net.Mail.MailAddress(_email);
                return true;
            }
            catch
            {
                ValidationErrorMessages.Add("Email was not a valid email");
                return false;
            }
        }
    }

}