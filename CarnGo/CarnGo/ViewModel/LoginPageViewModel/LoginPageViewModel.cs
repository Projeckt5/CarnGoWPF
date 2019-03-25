using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CarnGo
{
    class LoginPageViewModel : UserModel
    {
        // Made a password here for testing my code.
        public string Password { get; set; }

        public bool ValidateLoginCredentials { get; set; }

        // initialise
        public LoginPageViewModel(string email, string password)
        {
            this.Email = email;
            // this.Password = password;
        }
        // Validate string - bruges ikke endnu
        private bool StringValidator(string input)
        {
            string pattern = "[^a-zA-Z]";

            if (Regex.IsMatch(input, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        //Validate Integer - bruges ikke endnu
        private bool IntegerValidator(string input)
        {
            string pattern = "[^0-9]";
            if (Regex.IsMatch(input, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Clears the user input so there is an empty field everytime.
        private void ClearTexts(string email, string password)
        {
            email = string.Empty;
            password = string.Empty;
        }



        internal bool LoginCommand(string email, string password)
        {
            if (string.IsNullOrEmpty(Email))
            {
                MessageBox.Show("Enter the email");
                return false;
            }
            else
            {
                if (Email != email)
                {
                    MessageBox.Show("Incorrect email");
                    ClearTexts(email, password);
                    return false;
                }
                else
                {
                    if (string.IsNullOrEmpty(password))
                    {
                        MessageBox.Show("Enter password");
                        return false;
                    }
                    else if (Password != password)
                    {
                        MessageBox.Show("Incorrect password");
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
            }
        }


    }
}
