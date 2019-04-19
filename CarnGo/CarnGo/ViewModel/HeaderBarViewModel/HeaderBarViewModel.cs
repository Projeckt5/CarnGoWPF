using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;

namespace CarnGo
{
    public class SearchEvent : PubSubEvent<string> { }
    public class HeaderBarViewModel : BaseViewModel
    {
        #region Private Fields
        private int _numUnreadNotifications = 0;
        private bool _unreadNotifications = false;
        #endregion
        #region Default Constructor
        public HeaderBarViewModel()
        {
        }
        #endregion
        #region Public Properties

        public UserModel UserModel => ViewModelLocator.ApplicationViewModel.CurrentUser;
        public string SearchKeyWord { get; set; }

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
            ViewModelLocator.ApplicationViewModel.CurrentUser = null;
            ViewModelLocator.ApplicationViewModel
                .GoToPage(ApplicationPage.LoginPage);
        }

        private void Search()
        {
            ViewModelLocator.ApplicationViewModel
                .GoToPage(ApplicationPage.SearchPage);
            IoCContainer.Resolve<IEventAggregator>().GetEvent<SearchEvent>().Publish(SearchKeyWord);
        }

        private void ShowNotification()
        {
            NumUnreadNotifications = 0;
        }
        #endregion
    }
}