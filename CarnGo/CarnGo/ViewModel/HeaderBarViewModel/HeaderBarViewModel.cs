using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IQueryDatabase _databaseQuery;
        private bool _isQueryingDatabase;

        #endregion
        #region Default Constructor
        public HeaderBarViewModel(IEventAggregator eventAggregator, IApplication application, IQueryDatabase databaseQuery)
        {
            _eventAggregator = eventAggregator;
            _application = application;
            _databaseQuery = databaseQuery;
        }
        #endregion
        #region Public Properties

        public UserModel UserModel => _application.CurrentUser;

        public string SearchKeyWord { get; set; }

        public int NumUnreadNotifications => UserModel.MessageModels.Count(msg => msg.MessageRead == false);

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

        public ICommand NavigateSearchPageCommand => new DelegateCommand(()=> _application.GoToPage(ApplicationPage.SearchPage));

        #endregion
        #region Command Helpers

        private void Logout()
        {
            _application.LogUserOut();
        }

        private void Search()
        {
            _eventAggregator.GetEvent<SearchEvent>().Publish(SearchKeyWord);
            _application.GoToPage(ApplicationPage.SearchPage);
        }

        private async Task ShowNotification()
        {

            IsQueryingDatabase = true;
            try
            {

                UserModel.MessageModels = await _databaseQuery.GetUserMessages(UserModel);
                UserModel.MessageModels.ForEach(msg => msg.MessageRead = true);
                OnPropertyChanged(nameof(UnreadNotifications));
                _eventAggregator.GetEvent<NotificationMessageUpdateEvent>().Publish(UserModel.MessageModels);
                await _databaseQuery.UpdateUserMessages(UserModel, UserModel.MessageModels);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
            finally
            {
                IsQueryingDatabase = false;
            }
        }


        #endregion
    }
}