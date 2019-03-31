using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Windows.Input;
using CarnGo.Security;
using Prism.Commands;

namespace CarnGo
{
    public class UserSignUpViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        #region Private Fields

        private string _email;
        private SecureString _password;
        private SecureString _passwordValidate;
        private ObservableCollection<string> _allErrors = new ObservableCollection<string>();
        #endregion
        #region Default Constructor

        public UserSignUpViewModel()
        {
        }
        #endregion
        #region Public Properties
        public string Email
        {
            get=>_email;
            set
            {
                if (_email == value)
                    return;
                _email = value;
                ValidateEmail(nameof(Email),Email);
                OnPropertyChanged(nameof(Email));
            }
        }

        public SecureString PasswordSecureString
        {
            get=>_password;
            set
            {
                if (_password == value)
                    return;
                _password = value;
                ValidatePassword(nameof(PasswordSecureString),PasswordSecureString);
                OnPropertyChanged(nameof(PasswordSecureString));
            }
        }
        public SecureString PasswordValidateSecureString
        {
            get => _passwordValidate;
            set
            {
                if (_passwordValidate == value)
                    return;
                _passwordValidate = value;
                ValidatePasswordMatch(nameof(PasswordValidateSecureString), PasswordValidateSecureString, PasswordSecureString);
                OnPropertyChanged(nameof(PasswordValidateSecureString));
            }
        }

        public ObservableCollection<string> AllErrors
        {
            get=> _allErrors;
            set
            {
                if(_allErrors == value)
                    return;
                _allErrors = value;
                OnPropertyChanged(nameof(AllErrors));
            }
        }
        #endregion
        #region Public Commands

        public ICommand RegisterCommand => new DelegateCommand(RegisterUser);


        #endregion
        #region Command Helpers
        private void RegisterUser()
        {
            AllErrors.Clear();
            ValidateAll();
            if (HasErrors)
            {
                List<string> allErrorsList = new List<string>();
                foreach (var error in _errorsDictionary)
                {
                    allErrorsList.AddRange(error.Value);
                }
                AllErrors = new ObservableCollection<string>(allErrorsList);
                return;
            }
        }
        #endregion
        #region Error Handling

        public Dictionary<string, List<string>> _errorsDictionary { get; }= new Dictionary<string, List<string>>();
        public IEnumerable GetErrors(string propertyName)
        {
            _errorsDictionary.TryGetValue(propertyName, out var errorsForProperty);
            return errorsForProperty;
        }

        public bool HasErrors => _errorsDictionary.Values.FirstOrDefault(err => err.Count > 0) != null;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void ValidateAll()
        {
            ValidateEmail(nameof(Email),Email);
            ValidatePassword(nameof(PasswordSecureString),PasswordSecureString);
            ValidatePasswordMatch(nameof(PasswordValidateSecureString),PasswordValidateSecureString,PasswordSecureString);
        }

        private void ValidateEmail(string propertyName, string email)
        {
            List<string> emailErrors = (GetErrors(propertyName) as List<string>);
            if (emailErrors == null)
            {
                emailErrors = new List<string>();
            }
            else
            {
                emailErrors.Clear();
            }

            try
            {
                System.Net.Mail.MailAddress mailAddress = new System.Net.Mail.MailAddress(email);
            }
            catch
            {
                emailErrors.Add("Email is not a valid email");
            }
            _errorsDictionary[propertyName] = emailErrors;
            if (emailErrors.Count > 0)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }

        }
        private void ValidatePassword(string propertyName, SecureString secureString)
        {
            List<string> passwordErrors = (GetErrors(propertyName) as List<string>);
            if (passwordErrors == null)
            {
                passwordErrors = new List<string>();
            }
            else
            {
                passwordErrors.Clear();
            }
            
            if(string.IsNullOrWhiteSpace(secureString.ConvertToString()))
            {
                passwordErrors.Add("Password can't be empty");
            }
            else
            {
                if (secureString.Length < 6)
                {
                    passwordErrors.Add("Password must be longer than 6 characters");
                }
                if (secureString.ConvertToString().Any(char.IsDigit) == false)
                {
                    passwordErrors.Add("The password must contain a number");
                }
                if (secureString.ConvertToString().Any(char.IsLetter) == false)
                {
                    passwordErrors.Add("The password must contain a character");
                }
            }

            _errorsDictionary[propertyName] = passwordErrors;
            if (passwordErrors.Count > 0)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }

        }

        private void ValidatePasswordMatch(string passwordValidationPropertyName, SecureString passwordValidation, SecureString password)
        {
            List<string> passwordConfirmationErrors = (GetErrors(passwordValidationPropertyName) as List<string>);
            if (passwordConfirmationErrors == null)
            {
                passwordConfirmationErrors = new List<string>();
            }
            else
            {
                passwordConfirmationErrors.Clear();
            }
            if (password.ConvertToString() != passwordValidation.ConvertToString())
            {
                passwordConfirmationErrors.Add("Passwords don't match");
            }
            _errorsDictionary[passwordValidationPropertyName] = passwordConfirmationErrors;
            if (passwordConfirmationErrors.Count > 0)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(passwordValidationPropertyName));
            }
        }
        #endregion
    }
}
