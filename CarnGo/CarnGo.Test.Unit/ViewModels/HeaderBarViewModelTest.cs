using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using Prism.Events;
using System.Collections.Generic;
using CarnGo.Database;
using CarnGo.Database.Models;
using CarnGo.Model.ThreadTimer;
using NSubstitute.ExceptionExtensions;

namespace CarnGo.Test.Unit.ViewModels
{
    [TestFixture]
    public class HeaderBarViewModelTest
    {
        private HeaderBarViewModel _uut;
        private IEventAggregator _fakeEventAggregator;
        private IApplication _fakeApplication;
        private IQueryDatabase _fakeDatabaseQuery;
        private SearchEvent _fakeSearchEvent;
        private NotificationMessagesUpdateEvent _fakeUpdateEvent;
        private UserUpdateEvent _fakeUserUpdateEvent;
        private DatabasePollingLoop _fakeDatabasePollingLoopEvent;

        [SetUp]
        public void TestSetup()
        {
            _fakeEventAggregator = Substitute.For<IEventAggregator>();
            _fakeApplication = Substitute.For<IApplication>();
            _fakeSearchEvent = Substitute.For<SearchEvent>();
            _fakeDatabaseQuery = Substitute.For<IQueryDatabase>();
            _fakeUserUpdateEvent = Substitute.For<UserUpdateEvent>();
            _fakeUpdateEvent = Substitute.For<NotificationMessagesUpdateEvent>();
            _fakeDatabasePollingLoopEvent = Substitute.For<DatabasePollingLoop>();
            _fakeEventAggregator.GetEvent<SearchEvent>().Returns(_fakeSearchEvent);
            _fakeEventAggregator.GetEvent<NotificationMessagesUpdateEvent>().Returns(_fakeUpdateEvent);
            _fakeEventAggregator.GetEvent<UserUpdateEvent>().Returns(_fakeUserUpdateEvent);
            _fakeEventAggregator.GetEvent<DatabasePollingLoop>().Returns(_fakeDatabasePollingLoopEvent);
            _uut = new HeaderBarViewModel(_fakeEventAggregator,_fakeApplication, _fakeDatabaseQuery);

            _fakeApplication.CurrentUser.Returns(TestModelFactory.CreateUserModel());

            _fakeDatabaseQuery.GetUserMessagesTask(Arg.Any<UserModel>(), Arg.Any<int>(), Arg.Any<int>()).Returns(
                new List<MessageModel>()
                {
                    TestModelFactory.CreateMessageModel("TestMsg",MessageType.LessorMessage)
                });
            _fakeDatabaseQuery.GetUserTask(Arg.Any<UserModel>()).Returns(TestModelFactory.CreateUserModel());
        }

        #region Search

        [Test]
        public void Search_SearchInvokesEventAggregator_EventAggregatorGetsRightEventArgs()
        {
            _uut.SearchKeyWord = "Test";

            _uut.SearchCommand.Execute(null);

            _fakeEventAggregator.GetEvent<SearchEvent>().Received().Publish(Arg.Is<string>(str => str.Contains("Test")));
        }


        [Test]
        public void Search_SearchInvokesApplication_PageChanged()
        {
            _uut.SearchKeyWord = "Test";

            _uut.SearchCommand.Execute(null);

            _fakeApplication.Received().GoToPage(Arg.Is<ApplicationPage>(page => page == ApplicationPage.SearchPage));
        } 
        #endregion
        #region Logout

        [Test]
        public void Logout_UserLogout_ApplicationReceivedLogOut()
        {

            _uut.LogoutCommand.Execute(null);

            _fakeApplication.Received().LogUserOut();
        } 
        #endregion

        #region Notification


        [Test]
        public void Notification_ShowNotifications_UnreadNotificationsFalse()
        {

            _uut.NotificationCommand.Execute(null);

            Assert.That(_uut.UnreadNotifications, Is.False);
        }

        [Test]
        public void Notification_ShowNotifications_NotificationsUpdatedAsRead()
        {

            _uut.NotificationCommand.Execute(null);

            _fakeDatabaseQuery.Received()
                .UpdateUserMessagesTask(Arg.Any<List<MessageModel>>());
        }

        [Test]
        public void Notification_ShowNotifications_NotificationsReceivedFromDb()
        {

            _uut.NotificationCommand.Execute(null);

            _fakeDatabaseQuery.Received().GetUserMessagesTask(Arg.Any<UserModel>(), Arg.Any<int>(), Arg.Any<int>());
        }


        [Test]
        public void Notification_ShowNotifications_NotificationsSendToPopUp()
        {
            _fakeDatabaseQuery.GetUserTask(Arg.Any<UserModel>()).Returns(TestModelFactory.CreateUserModel());

            _uut.NotificationCommand.Execute(null);

            _fakeEventAggregator.GetEvent<NotificationMessagesUpdateEvent>()
                .Received().Publish(Arg.Is<List<MessageModel>>(noti => noti == _uut.UserModel.MessageModels));
        }



        [Test]
        public void Notification_ShowNotifications_NotificationsAreRead()
        {

            _uut.NotificationCommand.Execute(null);

            Assert.That(_uut.UserModel.MessageModels.TrueForAll(m => m.MessageRead));
        }



        [Test]
        public void Notification_ShowNotifications_NotificationsAreUpdatedAsReadInDb()
        {

            _uut.NotificationCommand.Execute(null);

            _fakeDatabaseQuery.Received().UpdateUserMessagesTask(
                Arg.Is<List<MessageModel>>(msgList => msgList.TrueForAll(msg => msg.MessageRead)));
        }


        [Test]
        public void Notification_ShowNotifications_EventAggregatorPublishedWithNewModels()
        {

            _uut.NotificationCommand.Execute(null);

            _fakeEventAggregator.GetEvent<NotificationMessagesUpdateEvent>().Received()
                .Publish(Arg.Is<List<MessageModel>>(msgList => 
                    msgList.TrueForAll(msg => msg.Message == "TestMsg" && msg.MessageRead)));
        }

        [Test]
        public void Notification_ShowNotificationsThrowsAuthorizationException_ApplicationReceivesLogout()
        {
            _fakeDatabaseQuery.GetUserMessagesTask(Arg.Any<UserModel>(), Arg.Any<int>(), Arg.Any<int>()).Throws<AuthorizationFailedException>();

            _uut.NotificationCommand.Execute(null);

            _fakeApplication.Received().LogUserOut();
        }


        [Test]
        public void Notification_LoadsWhileGettingNotifications_IsQueryingDatabaseFalse()
        {
            _uut.NotificationCommand.Execute(null);

            Assert.That(_uut.IsQueryingDatabase, Is.False);
        }


        [Test]
        public void Notification_IsQuerying_NoQueriesMade()
        {
            _uut.IsQueryingDatabase = true;

            _uut.NotificationCommand.Execute(null);

            _fakeApplication.DidNotReceive().LogUserOut();
            _fakeDatabaseQuery.DidNotReceive().GetUserMessagesTask(Arg.Any<UserModel>(), Arg.Any<int>(), Arg.Any<int>());
            _fakeEventAggregator.GetEvent<NotificationMessagesUpdateEvent>().DidNotReceive().Publish(Arg.Any<List<MessageModel>>());
        }

        #endregion

        #region Properties

        [Test]
        public void UpdateUser_UserFirstNameUpdated_FirstNameCorrect()
        {
            var firstName = "TestFirstName";
            var lastName = "TestLastName";
            _uut.UserModel = TestModelFactory.CreateUserModel(firstName, lastName);

            Assert.That(_uut.FirstName, Is.EqualTo(firstName));
        }

        [Test]
        public void UpdateUser_UserUpdateNumUnreadNotification_NumCorrect()
        {
            _uut.UserModel = TestModelFactory.CreateUserModel();

            Assert.That(_uut.NumUnreadNotifications, Is.GreaterThanOrEqualTo(0));
        }


        [Test]
        public void UpdateUser_UserUpdateUnreadNotification_UnreadNotificationsTrue()
        {
            _uut.UserModel = TestModelFactory.CreateUserModel();

            Assert.That(_uut.UnreadNotifications, Is.True);
        }


        [TestCase(UserType.Lessor, true)]
        [TestCase(UserType.NonUser, false)]
        [TestCase(UserType.OrdinaryUser, false)]
        public void UpdateUser_ManageCarsVisibleIfUserIsRentor_FirstNameCorrect(UserType type, bool expectedResult)
        {
            _uut.UserModel = TestModelFactory.CreateUserModel(type);

            Assert.That(_uut.ManageCarsVisible, Is.EqualTo(expectedResult));
        }
        #endregion
    }
}