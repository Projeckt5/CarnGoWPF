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

        #endregion

        #region Constructor
        public LoginPageViewModel(IApplication application)
        {
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
        public ICommand RegisterUserCommand => new DelegateCommand(() => _application.GoToPage(ApplicationPage.UserSignUpPage));
        public ICommand LoginPageLoadedCommand => new DelegateCommand(() =>
        {
            if (Properties.Settings.Default.Username != string.Empty)
            {
                RememberUser = Properties.Settings.Default.RememberMe;
                Email = Properties.Settings.Default.Username;
            }
        });


        #endregion


        #region Command Helpers
        private async Task Login()
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
                if (RememberUser)
                {
                    RememberUserLocally(PasswordSecureString);
                }
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

        private void RememberUserLocally(SecureString passwordSecureString)
        {
            Properties.Settings.Default.RememberMe = RememberUser;
            Properties.Settings.Default.Username = Email;
            Properties.Settings.Default.Save();
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

        #region DatabaseFunction

        public void CreateUserModel()
        {
          //  var repo=new CarnGoReposetory();
            //var usermodel=GetUserModel(Email); database
            //var usermodel=new UserModel(); skal fjernes
            //if(PasswordSecureString!=usermodel.Password)
            //{
               // fejlbesked
               //return
            //}
            //convertering fra database til usermodel
            //var currentUser = new UserModel();
            //currentUser.Email = usermodel.Email;
            //currentUser.Firstname = usermodel.FirstName;
            //currentUser.Lastname = usermodel.LastName;
            //currentUser.Address = usermodel.Address;
            //currentUser.UserType = usermodel.UserType;

        }

        #endregion
    }

}


