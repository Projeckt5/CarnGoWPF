using System;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;
using CarnGo.Database.Models;
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

            var User1 = new UserModel("asd", "asd", "asd@hotmail.com", "asd", UserType.Lessor);
            var User2 = new UserModel("Marcus", "Gasberg", "xXxGitMazterxXx@hotmail.com", "Gellerup", UserType.OrdinaryUser);
            var Car = new CarProfileModel(User2, "X-360", "BMW", 1989, "1234567", "Aarhus", 2, DateTime.Today, DateTime.Today, 1);
            CurrentUser = new UserModel("Test", "Test", "Test@Test.Test", "Test", UserType.Lessor)
            {
                MessageModels = new List<MessageModel>()
                {
                    new MessageFromLessorModel(User2, User1, Car, "Du kommer bare :)", true),
                    new MessageFromLessorModel(User2, User1, Car, "Det kan du godt glemme makker! Det kan du godt glemme makker! Det kan du godt glemme makker!", false),
                    new MessageFromRenterModel(User2, User1, Car, "Må jeg godt låne din flotte bil?"),
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
                OnPropertyChanged(nameof(ShowHeaderBar));
            }
        }

        public bool ShowHeaderBar => CurrentPage != ApplicationPage.LoginPage && CurrentPage != ApplicationPage.UserSignUpPage;

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

