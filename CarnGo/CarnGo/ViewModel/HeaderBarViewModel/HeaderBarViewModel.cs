using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using CarnGo.Database;
using CarnGo.Database.Models;
using CarnGo.Model.ThreadTimer;
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
        private readonly IQueryDatabase _databaseQuery;
        private bool _isQueryingDatabase;
        private UserModel _currentUser;
        private int _amountNotificationsToLoad = 10;
        
        

        #endregion
        #region Default Constructor
        public HeaderBarViewModel(IEventAggregator eventAggregator, IApplication application, IQueryDatabase databaseQuery)
        {
            _eventAggregator = eventAggregator;
            _application = application;
            _databaseQuery = databaseQuery;
            _currentUser = new UserModel();
            _eventAggregator.GetEvent<UserUpdateEvent>().Subscribe(user => UserModel = user);
        }
        #endregion
        #region Public Properties

        public UserModel UserModel
        {
            get => _currentUser;
            set
            {
                if (_currentUser == value)
                    return;
                _currentUser = value;
                OnPropertyChanged(nameof(UserModel));
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(ManageCarsVisible));
                OnPropertyChanged(nameof(NumUnreadNotifications));
                OnPropertyChanged(nameof(UnreadNotifications));
            }
        }

        public string FirstName => UserModel?.FirstName.Length > 20 ? UserModel?.FirstName.Substring(0,15) + "..." : UserModel?.FirstName;
        public bool ManageCarsVisible => UserModel?.UserType == UserType.Lessor;

        public string SearchKeyWord { get; set; }

        public int NumUnreadNotifications => UserModel?.MessageModels.Count(msg => msg.MessageRead == false) ?? 0;

        public bool UnreadNotifications => NumUnreadNotifications > 0;

        public bool IsQueryingDatabase
        {
            get=>_isQueryingDatabase;
            set
            {
                if (_isQueryingDatabase == value)
                    return;
                _isQueryingDatabase = value;
                OnPropertyChanged(nameof(IsQueryingDatabase));
            }
        }

        #endregion
        #region Public Commands

        public ICommand NavigateHomeCommand => new DelegateCommand(() =>_application.GoToPage(ApplicationPage.StartPage));

        public ICommand NotificationCommand => new DelegateCommand(async () => await ShowNotification());


        public ICommand NavigateUserCommand => new DelegateCommand(() => _application.GoToPage(ApplicationPage.EditUserPage));

        public ICommand SearchCommand => new DelegateCommand(Search);


        public ICommand LogoutCommand => new DelegateCommand(Logout);

        public ICommand NavigateSearchPageCommand => new DelegateCommand(NavigateSearchPage);

        public ICommand ManageCarCommand => new DelegateCommand(()=>NavigateToCarProfile());

        #endregion
        #region Command Helpers

        private void Logout()
        {
            _application.LogUserOut();
        }

        private void Search()
        {
            _application.GoToPage(ApplicationPage.SearchPage);
            _eventAggregator.GetEvent<SearchEvent>().Publish(SearchKeyWord);
        }

        private void NavigateToCarProfile()
        {
            _application.GoToPage(ApplicationPage.CarLeasePage);
            _eventAggregator.GetEvent<CarLeaseViewModel.GetCarEvent>().Publish();
        }

        private void NavigateSearchPage()
        {
            _application.GoToPage(ApplicationPage.SearchPage);
            _eventAggregator.GetEvent<InitializeSearchResultItemsEvent>().Publish();
        }

        public async Task ShowNotification()
        {
            if(IsQueryingDatabase)
                return;

            try
            {
                IsQueryingDatabase = true;
                var notifications = await _databaseQuery.GetUserMessagesTask(_application.CurrentUser,
                    _amountNotificationsToLoad + UserModel.MessageModels.Count);
                //If user logs out while getting messages do nothing
                if (UserModel == null)
                    return;
                notifications.ForEach(msg => msg.MessageRead = true);
                UpdateNotifications(notifications);
                _eventAggregator.GetEvent<NotificationMessagesUpdateEvent>().Publish(UserModel.MessageModels);
                await _databaseQuery.UpdateUserMessagesTask(UserModel?.MessageModels);
            }
            catch (AuthenticationFailedException e)
            {
                Logout();
            }
            finally
            {
                IsQueryingDatabase = false;
            }
        }

        private void UpdateNotifications(List<MessageModel> notifications)
        {
            notifications.RemoveAll(n => n.Sender.Email == UserModel.Email);
            UserModel.MessageModels.AddRange(notifications);
            UserModel.MessageModels = UserModel?.MessageModels.OrderByDescending(m => m.TimeStamp).ToList();
            OnPropertyChanged(nameof(UnreadNotifications));
            OnPropertyChanged(nameof(NumUnreadNotifications));
        }

        #endregion

        
    }
}