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
using Database;
using Database.Models;

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
        #endregion

        #region Constructor
        public LoginPageViewModel()
        {

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
        public ICommand RegisterUserCommand => new DelegateCommand(() => ViewModelLocator.ApplicationViewModel.GoToPage(ApplicationPage.UserSignUpPage));



        #endregion


        #region Command Helpers
        //TODO make async and add loading flag
        private async Task Login()
        {
            IsLogin = true;
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
            //var repo=new CarnGoReposetory();
           
            await Task.Delay(2000);
            IsLogin = false;
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


