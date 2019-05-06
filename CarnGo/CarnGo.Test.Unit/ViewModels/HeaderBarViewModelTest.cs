using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using Prism.Events;
using System.Collections.Generic;

namespace CarnGo.Test.Unit.ViewModels
{
    [TestFixture]
    public class HeaderBarViewModelTest
    {
        private HeaderBarViewModel _uut;
        private IEventAggregator _fakeEventAggregator;
        private IApplication _fakeApplication;
        private SearchEvent _fakeSearchEvent;
        private NotificationMessageUpdateEvent _fakeUpdateEvent;
        private IQueryDatabase _fakeDatabaseQuery;

        [SetUp]
        public void TestSetup()
        {
            _fakeEventAggregator = Substitute.For<IEventAggregator>();
            _fakeApplication = Substitute.For<IApplication>();
            _fakeSearchEvent = Substitute.For<SearchEvent>();
            _fakeDatabaseQuery = Substitute.For<IQueryDatabase>();
            _fakeUpdateEvent = Substitute.For<NotificationMessageUpdateEvent>();
            _fakeEventAggregator.GetEvent<SearchEvent>().Returns(_fakeSearchEvent);
            _fakeEventAggregator.GetEvent<NotificationMessageUpdateEvent>().Returns(_fakeUpdateEvent);
            _uut = new HeaderBarViewModel(_fakeEventAggregator,_fakeApplication, _fakeDatabaseQuery);

            _fakeApplication.CurrentUser.Returns( new UserModel("Test", "Test", "Test@Test.Test", "Test", UserType.Lessor)
            {
                MessageModels = new List<MessageModel>()
                {
                    new MessageModel()
                    {
                        MessageRead = false,
                        Message = "Test",
                        MsgType = MessageType.LessorMessage
                    }
                }
            });

            _fakeDatabaseQuery.GetUserMessagesTask(Arg.Any<UserModel>()).Returns(new List<MessageModel>()
            {
                new MessageModel()
                {
                    MessageRead = false,
                    Message = "Test",
                    MsgType = MessageType.LessorMessage
                }
            });
        }

        [Test]
        public void Search_SearchInvokesEventAggregator_EventAggregatorGetsRightEventArgs()
        {
            _uut.SearchKeyWord = "Test";

            _uut.SearchCommand.Execute(null);

            _fakeEventAggregator.GetEvent<SearchEvent>().Received().Publish(Arg.Is<string>(str=>str.Contains("Test")));
        }


        [Test]
        public void Search_SearchInvokesApplication_PageChanged()
        {
            _uut.SearchKeyWord = "Test";

            _uut.SearchCommand.Execute(null);

            _fakeApplication.Received().GoToPage(Arg.Is<ApplicationPage>(page => page == ApplicationPage.SearchPage));
        }

        [Test]
        public void Logout_UserLogout_ApplicationReceivedLogOut()
        {

            _uut.LogoutCommand.Execute(null);

            _fakeApplication.Received().LogUserOut();
        }


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
                .UpdateUserMessagesTask(Arg.Is<UserModel>(um => um == _fakeApplication.CurrentUser),
                    _fakeApplication.CurrentUser.MessageModels);
        }


        [Test]
        public void Notification_ShowNotifications_NotificationsReceivedFromDb()
        {
            _uut.NotificationCommand.Execute(null);

            _fakeDatabaseQuery.Received().GetUserMessagesTask(Arg.Any<UserModel>());
        }


        [Test]
        public void Notification_ShowNotifications_NotificationsSendToPopUp()
        {
            _uut.NotificationCommand.Execute(null);

            _fakeEventAggregator.GetEvent<NotificationMessageUpdateEvent>()
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
                Arg.Is<UserModel>(user => user.Equals(_uut.UserModel)),
                Arg.Is<List<MessageModel>>(msgList => msgList.TrueForAll(msg => msg.MessageRead)));
        }

        [Test]
        public void Notification_LoadsWhileGettingNotifications_IsQueryingDatabaseFalse()
        {
            _uut.NotificationCommand.Execute(null);

            Assert.That(_uut.IsQueryingDatabase, Is.False);
        }
    }
}