using System;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;
using CarnGo.Security;
using Prism.Events;
using Unity;

namespace CarnGo
{

    public class NotificationMessageUpdateEvent: PubSubEvent<List<MessageModel>>{ }

    public class ApplicationViewModel : BaseViewModel, IApplication
    {
        private readonly IQueryDatabase _queryDatabase;
        private ApplicationPage _applicationPage;
        
        private UserModel _currentUser;

        public ApplicationViewModel(IQueryDatabase queryDatabase)
        {
            _applicationPage = ApplicationPage.StartPage;
            _queryDatabase = queryDatabase;
            _currentUser = new UserModel("Test", "Test", "Test@Test.Com", "TestAddress", UserType.OrdinaryUser)
            {
                MessageModels = new List<MessageModel>()
                {
                    new MessageModel()
                    {
                        Message = "Test", MessageRead = false, MsgType = MessageType.LessorMessage
                    }
                }
            };
        }

        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPage CurrentPage
        {
            get => _applicationPage;
            private set
            {
                if (_applicationPage == value)
                    return;
                _applicationPage = value;
                OnPropertyChanged(nameof(CurrentPage));
                IoCContainer.Resolve<MainWindowViewModel>().HeaderBarVisibility = CurrentPage == ApplicationPage.LoginPage ? "Hidden" : "Visible";
            }
        }

        public bool ShowHeaderBar => CurrentPage != ApplicationPage.LoginPage;

        /// <summary>
        /// The current user logged into the application
        /// </summary>
        public UserModel CurrentUser
        {
            get => _currentUser;
            private set
            {
                if (_currentUser == value)
                    return;
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        /// <summary>
        /// Navigate to the specified page
        /// </summary>
        /// <param name="page">The page to go to</param>
        public void GoToPage(ApplicationPage page)
        {
            CurrentPage = page;
        }

        public async Task LogUserIn(string email, SecureString password)
        {
            try
            {
                CurrentUser = await _queryDatabase.GetUserTask(email, password);
                GoToPage(ApplicationPage.StartPage);
            }
            catch (UserNotFoundException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void LogUserOut()
        {
            CurrentUser = null;
            GoToPage(ApplicationPage.LoginPage);
        }
    }
}

