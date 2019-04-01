using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using CarnGo.Security;
using Prism.Commands;

namespace CarnGo
{
    public class UserSignUpViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        #region Private Fields
        private readonly IValidator<string> _emailValidator = new EmailValidator();
        private readonly IValidator<SecureString> _passwordValidator = new PasswordValidator();
        private readonly IValidator<List<SecureString>> _passwordMatchValidator = new PasswordMatchValidator();
        private string _email;
        private SecureString _password;
        private SecureString _passwordValidate;
        private ObservableCollection<string> _allErrors = new ObservableCollection<string>();
        private bool _isRegistering;
        #endregion
        #region Default Constructor

        public UserSignUpViewModel()
        {
        }
        #endregion
        #region Public Properties

        public bool IsRegistering
        {
            get=> _isRegistering;
            set
            {
                if (_isRegistering == value)
                    return;
                _isRegistering = value;
                OnPropertyChanged(nameof(IsRegistering));
            }
        }
        public string Email
        {
            get=>_email;
            set
            {
                if (_email == value)
                    return;
                _email = value;
                ValidateEmail();
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
                ValidatePassword();
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
                ValidatePasswordMatch();
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

        public ICommand RegisterCommand => new DelegateCommand(async ()=> await RegisterUser());
        public ICommand NavigateLoginCommand => new DelegateCommand(()=> ViewModelLocator.ApplicationViewModel.GoToPage(ApplicationPage.LoginPage));


        #endregion
        #region Command Helpers
        //TODO make async and add loading flag
        private async Task RegisterUser()
        {
            IsRegistering = true;
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
            }
            //TODO: AWAIT REGISTERING THE USER ON THE DB
            await Task.Delay(2000);
            IsRegistering = false;
        }
        #endregion
        #region Error Handling

        private readonly Dictionary<string, List<string>> _errorsDictionary = new Dictionary<string, List<string>>();

        public IEnumerable GetErrors(string propertyName)
        {
            _errorsDictionary.TryGetValue(propertyName, out var errorsForProperty);
            return errorsForProperty;
        }

        public bool HasErrors => _errorsDictionary.Values.FirstOrDefault(err => err.Count > 0) != null;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void ValidateAll()
        {
            ValidateEmail();
            ValidatePassword();
            ValidatePasswordMatch();
        }

        private void ValidateEmail()
        {
            const string emailPropertyName = nameof(Email);
            List<string> emailErrors = (GetErrors(emailPropertyName) as List<string>);
            if (emailErrors == null)
            {
                emailErrors = new List<string>();
            }
            else
            {
                emailErrors.Clear();
            }
            
            if(_emailValidator.Validate(Email) == false)
            {
                emailErrors.AddRange(_emailValidator.ValidationErrorMessages);
            }

            _errorsDictionary[emailPropertyName] = emailErrors;
            if (emailErrors.Count > 0)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(emailPropertyName));
            }

        }
        private void ValidatePassword()
        {
            const string propertyName = nameof(PasswordSecureString);
            List<string> passwordErrors = (GetErrors(propertyName) as List<string>);
            if (passwordErrors == null)
            {
                passwordErrors = new List<string>();
            }
            else
            {
                passwordErrors.Clear();
            }

            if (_passwordValidator.Validate(PasswordSecureString) == false)
            {
                passwordErrors.AddRange(_passwordValidator.ValidationErrorMessages);
            }

            _errorsDictionary[propertyName] = passwordErrors;
            if (passwordErrors.Count > 0)
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }

        }

        private void ValidatePasswordMatch()
        {
            const string passwordValidationPropertyName = nameof(PasswordValidateSecureString);
            List<string> passwordConfirmationErrors = (GetErrors(passwordValidationPropertyName) as List<string>);
            if (passwordConfirmationErrors == null)
            {
                passwordConfirmationErrors = new List<string>();
            }
            else
            {
                passwordConfirmationErrors.Clear();
            }

            if (_passwordMatchValidator.Validate(new List<SecureString>()
                    {PasswordSecureString, PasswordValidateSecureString}) == false)
            {
                passwordConfirmationErrors.AddRange(_passwordMatchValidator.ValidationErrorMessages);
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
