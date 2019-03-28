using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using Microsoft.Win32;
using Prism.Commands;

namespace CarnGo
{
    public class HeaderBarViewModel : BaseViewModel
    {
        #region Private Fields

        private bool _showNotifications;

        #endregion
        #region Public Properties

        public static HeaderBarViewModel Instance => new HeaderBarViewModel();
        public string SearchKeyWord { get; set; }

        public bool ShowNotifications
        {
            get => _showNotifications;
            set
            {
                if(_showNotifications == value)
                    return;
                _showNotifications = value;
                OnPropertyChanged(nameof(ShowNotifications));
            }
        }
        #endregion
        #region Public Commands

        public ICommand NavigateHomeCommand => new DelegateCommand(() =>
                                                   ViewModelLocator.ApplicationViewModel
                                                       .GoToPage(ApplicationPage.StartPage));

        public ICommand NotificationCommand => new DelegateCommand(ShowNotification);


        public ICommand NavigateUserCommand => new DelegateCommand(() =>
                                                   ViewModelLocator.ApplicationViewModel
                                                       .GoToPage(ApplicationPage.EditUserPage));

        public ICommand SearchCommand => new DelegateCommand(Search);


        public ICommand LogoutCommand => new DelegateCommand(Logout);

        public ICommand FindCarCommand => new DelegateCommand(()=>
                                                 ViewModelLocator.ApplicationViewModel
                                                     .GoToPage(ApplicationPage.SearchPage));

        #endregion
        #region Command Helpers

        private void Logout()
        {
            //TODO: Log the user out
            ViewModelLocator.ApplicationViewModel
                .GoToPage(ApplicationPage.LoginPage);
        }

        private void Search()
        {
            ViewModelLocator.ApplicationViewModel
                .GoToPage(ApplicationPage.SearchPage);
            //TODO: Make the search with the search string
        }

        private void ShowNotification()
        {
            ShowNotifications ^= true;
            //TODO: Create notification view and bind its visibility to this
        }
        #endregion
    }
}