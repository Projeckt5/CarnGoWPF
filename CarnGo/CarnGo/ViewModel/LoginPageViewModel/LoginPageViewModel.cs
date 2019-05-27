using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CarnGo.Security;
using Prism.Commands;
using CarnGo.Database;
using CarnGo.Database.Models;
using Prism.Events;

namespace CarnGo
{
    public class LoginPageViewModel : BaseViewModelWithValidation
    {
        

        #region Private Field
        private readonly IValidator<string> _emailValidator = new EmailValidator();
        private readonly IValidator<SecureString> _passwordValidator = new PasswordValidator();
        private string _email;
        private SecureString _password;
        private bool _IsLogin;
        private ObservableCollection<string> _allErrors = new ObservableCollection<string>();
        private readonly IApplication _application;
        private bool _rememberUser;
        private readonly IQueryDatabase _databaseAccess;

        #endregion

        #region Constructor
        public LoginPageViewModel(
            IValidator<string> emailValidator,
            IValidator<SecureString> passwordValidator,
            IQueryDatabase databaseAccess,
            IApplication application)
        {
            _application = application;
            _emailValidator = emailValidator;
            _passwordValidator = passwordValidator;
            _databaseAccess = databaseAccess;
            _application = application;
        }
        #endregion

        #region Public Properties

        public bool IsLogin
        {
            get => _IsLogin;
            set
            {
                if (_IsLogin == value)
                    return;
                _IsLogin = value;
                ValidateEmail();
                OnPropertyChanged(nameof(IsLogin));
            }
        }
        public bool RememberUser
        {
            get => _rememberUser;
            set
            {
                if (_rememberUser == value)
                    return;
                _rememberUser = value;
                OnPropertyChanged(nameof(RememberUser));
            }
        }

        public string Email
        {
            get => _email;
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
            get => _password;
            set
            {
                if (_password == value)
                    return;
                _password = value;
                ValidatePassword();
                OnPropertyChanged(nameof(PasswordSecureString));
            }
        }

        public ObservableCollection<string> AllErrors
        {
            get => _allErrors;
            set
            {
                if (_allErrors == value)
                    return;
                _allErrors = value;
                OnPropertyChanged(nameof(AllErrors));
            }
        }

        #endregion


        #region Public Commands

        public ICommand LoginCommand => new DelegateCommand(async () => await Login());
        public ICommand NavigateUserSignupCommand => new DelegateCommand(NavigateUserSignup);
        public ICommand NavigateStartPageCommand => new DelegateCommand(NavigateStartPage);


        #endregion


        #region Command Helpers
        public async Task Login()
        {
            if (IsLogin)
                return;

            IsLogin = true;

            try
            {
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
                    return;
                }

                await _application.LogUserIn(Email, PasswordSecureString);
                NavigateStartPage();
            }
            catch (AuthenticationFailedException e)
            {
                AllErrors.Add(e.Message);
            }
            finally
            {
                IsLogin = false;
            }

           
        }

        private void NavigateStartPage()
        {
            _application.GoToPage(ApplicationPage.StartPage);
        }

        public void NavigateUserSignup()
        {
            _application.GoToPage(ApplicationPage.UserSignUpPage);
        }
        #endregion
        #region Error Handling

        private void ValidateAll()
        {
            ValidateEmail();
            ValidatePassword();
        }

        private void ValidateEmail()
        {
            Validate(nameof(Email), Email, _emailValidator);
        }
        private void ValidatePassword()
        {
            Validate(nameof(PasswordSecureString), PasswordSecureString, _passwordValidator);
        }

        #endregion

    }

}


