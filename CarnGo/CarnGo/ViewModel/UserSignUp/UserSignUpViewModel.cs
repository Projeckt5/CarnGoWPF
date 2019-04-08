using CarnGo.Security;
using Prism.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarnGo
{
    public class UserSignUpViewModel : BaseViewModelWithValidation
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
                ValidatePasswordMatch();
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
        private async Task RegisterUser()
        {
            IsRegistering = true;
            AllErrors.Clear();
            ValidateAll();
            if (HasErrors)
            {
                List<string> allErrorsList = new List<string>();
                foreach (var error in ErrorsDictionary)
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

        public void ValidateAll()
        {
            ValidateEmail();
            ValidatePassword();
            ValidatePasswordMatch();
        }


        public void ValidateEmail()
        {
            Validate(nameof(Email),Email, _emailValidator);
        }
        public void ValidatePassword()
        {
            Validate(nameof(PasswordSecureString),PasswordSecureString,_passwordValidator);
        }
        
        public void ValidatePasswordMatch()
        {
            var passwords = new List<SecureString>() {PasswordSecureString, PasswordValidateSecureString};
            Validate(nameof(PasswordValidateSecureString),passwords, _passwordMatchValidator);
        }

        #endregion

    }
}
