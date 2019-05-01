using System.Collections.Generic;
using System.Linq;
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
        private readonly IEventAggregator _eventAggregator;
        private readonly IApplication _application;
        private int _numUnreadNotifications;
        #endregion
        #region Default Constructor
        public HeaderBarViewModel(IEventAggregator eventAggregator, IApplication application)
        {
            _eventAggregator = eventAggregator;
            _application = application;
            _eventAggregator.GetEvent<NotificationMessageUpdateEvent>().Subscribe(UpdateUnreadNotifications);
            NumUnreadNotifications = _application.CurrentUser?.MessageModels.Count(msg => msg.MessageRead == false) ?? 0;
        }
        #endregion
        #region Public Properties

        public UserModel UserModel => _application.CurrentUser;

        public string SearchKeyWord { get; set; }

        public int NumUnreadNotifications
        {
            get => _numUnreadNotifications;
            set
            {
                if (_numUnreadNotifications == value)
                    return;
                _numUnreadNotifications = value;
                OnPropertyChanged(nameof(NumUnreadNotifications));
                OnPropertyChanged(nameof(UnreadNotifications));
            }
        }

        public bool UnreadNotifications => NumUnreadNotifications > 0;

        #endregion
        #region Public Commands

        public ICommand NavigateHomeCommand => new DelegateCommand(() =>_application.GoToPage(ApplicationPage.StartPage));

        public ICommand NotificationCommand => new DelegateCommand(ShowNotification);


        public ICommand NavigateUserCommand => new DelegateCommand(() => _application.GoToPage(ApplicationPage.EditUserPage));

        public ICommand SearchCommand => new DelegateCommand(Search);


        public ICommand LogoutCommand => new DelegateCommand(Logout);

        public ICommand NavigateSearchPageCommand => new DelegateCommand(()=> _application.GoToPage(ApplicationPage.SearchPage));

        #endregion
        #region Command Helpers

        public void UpdateUnreadNotifications(List<MessageModel> messageModels)
        {
            NumUnreadNotifications = messageModels.Count(msg => msg.MessageRead == false);
        }

        private void Logout()
        {
            _application.LogUserOut();
        }

        private void Search()
        {
            _application.GoToPage(ApplicationPage.SearchPage);
            _eventAggregator.GetEvent<SearchEvent>().Publish(SearchKeyWord);
        }

        private void ShowNotification()
        {
            NumUnreadNotifications = 0;
        }
        #endregion
    }
}