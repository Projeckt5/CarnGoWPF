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

        private bool _showNotifications = false;
        private int _numUnreadNotifications = 0;
        private bool _unreadNotifications = false;

        #endregion
        #region Default Constructor
        public HeaderBarViewModel()
        {
        }
        #endregion
        #region Public Properties
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

        public int NumUnreadNotifications
        {
            get => _numUnreadNotifications;
            set
            {
                if (_numUnreadNotifications == value)
                    return;
                _numUnreadNotifications = value;
                UnreadNotifications = _numUnreadNotifications > 0;
                OnPropertyChanged(nameof(NumUnreadNotifications));
            }
        }

        public bool UnreadNotifications
        {
            get=>_unreadNotifications;
            set
            {
                if (_unreadNotifications == value)
                    return;
                _unreadNotifications = value;
                OnPropertyChanged(nameof(UnreadNotifications));
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
            NumUnreadNotifications = 0;
        }
        #endregion
    }
}