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
    public class CurrentUserSetNull : PubSubEvent { }
    public class SearchEvent : PubSubEvent<string> { }
    public class HeaderBarViewModel : BaseViewModel
    {

        #region Private Fields
        private readonly IEventAggregator _eventAggregator;
        private readonly IApplication _application;
        private readonly IQueryDatabase _databaseQuery;
        private bool _isQueryingDatabase;
        private UserModel _currentUser;
        private int _amountLoadedNotifications = 0;
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
            _eventAggregator.GetEvent<DatabasePollingLoop>().Subscribe(NotificationQueryThread);


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

        public string FirstName => UserModel?.Firstname.Length > 20 ? UserModel?.Firstname.Substring(0,15) + "..." : UserModel?.Firstname;
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

        public ICommand ManageCarCommand => new DelegateCommand(()=>_application.GoToPage(ApplicationPage.RegisterCarProfilePage));

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
                    _amountLoadedNotifications,
                    _amountNotificationsToLoad);
                notifications.RemoveAll(n => n.Sender.Email == UserModel.Email);
                UserModel.MessageModels.AddRange(notifications) ;
                _amountLoadedNotifications += UserModel.MessageModels.Count - _amountLoadedNotifications;
                UserModel.MessageModels.ForEach(msg => msg.MessageRead = true);
                OnPropertyChanged(nameof(UnreadNotifications));
                _eventAggregator.GetEvent<NotificationMessagesUpdateEvent>().Publish(UserModel.MessageModels);
                await _databaseQuery.UpdateUserMessagesTask(UserModel.MessageModels);
            }
            catch (AuthorizationFailedException e)
            {
                Logout();
            }
            finally
            {
                IsQueryingDatabase = false;
            }
        }

        public int i { get; set; } = 0;
            
      
        private async void NotificationQueryThread()
        {
            if (_application.IsLoggedIn && _application.CurrentUser!=null)
            {
              
                if (IsQueryingDatabase)
                    return;

                try
                {
                   i++; //test af push besked
                    if (i == 10)
                    {
                        var db = new AppDbContext();
                        var message = new Message()
                        {
                            CarProfileRegNr = "0420305a-2a44-44b6-8f59-88aa8da96103", ConfirmationStatus = 2, CreatedDate = DateTime.Now,
                            HaveBeenSeen = false,
                            LessorEmail = "car@owner", CarProfile = null, MsgType = 1, ReceiverEmail = "car@owner",RenterEmail = "car@renter",
                            TheMessage = "Tristan man må ikke bande"
                        };
                        await db.AddMessage(message);
                    }

                    var notifications = await _databaseQuery.GetUserMessagesTask(_application.CurrentUser,
                    _amountLoadedNotifications,
                    _amountNotificationsToLoad);


                    if (notifications.Count != 0)
                    {
                        notifications.RemoveAll(n => n.Sender.Email == UserModel.Email);
                        UserModel.MessageModels.AddRange(notifications);
                        _amountLoadedNotifications += UserModel.MessageModels.Count - _amountLoadedNotifications;
                        

                        OnPropertyChanged(nameof(UnreadNotifications));
                        OnPropertyChanged(nameof(NumUnreadNotifications));
                    }

                }
                catch (AuthorizationFailedException e)
                {
                    Logout();
                    _eventAggregator.GetEvent<CurrentUserSetNull>().Publish();
                }
                


            }
            else
            {                
                _eventAggregator.GetEvent<CurrentUserSetNull>().Publish();
            }
        }
        #endregion

        
    }
}