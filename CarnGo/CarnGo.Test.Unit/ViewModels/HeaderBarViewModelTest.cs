using NSubstitute;
using NUnit.Framework;
using Prism.Events;

namespace CarnGo.Test.Unit.ViewModels
{
    [TestFixture]
    public class HeaderBarViewModelTest
    {
        private HeaderBarViewModel _uut;
        private IEventAggregator _fakeEventAggregator;
        private SearchEvent _fakeSearchEvent;

        [SetUp]
        public void TestSetup()
        {
            _fakeEventAggregator = Substitute.For<IEventAggregator>();
            _fakeSearchEvent = Substitute.For<SearchEvent>();
            _uut = new HeaderBarViewModel(_fakeEventAggregator);
        }

        [Test]
        public void Search_SearchInvokesEventAggregator_EventAggregatorGetsRightEventArgs()
        {
            _uut.SearchKeyWord = "Test";
            _fakeEventAggregator.GetEvent<SearchEvent>().Returns(_fakeSearchEvent);
            
            _uut.SearchCommand.Execute(null);

            _fakeEventAggregator.GetEvent<SearchEvent>().Received().Publish(Arg.Is<string>(str=>str.Contains("Test")));
        }

        [Test]
        public void Logout_UserLogout_UserIsNull()
        {
            ViewModelLocator.ApplicationViewModel.CurrentUser = new UserModel("fake","fake","fake@fake.com","fake",UserType.OrdinaryUser);

            _uut.LogoutCommand.Execute(null);

            Assert.That(_uut.UserModel, Is.Null);
        }


        [Test]
        public void Notification_ShowNotifications_UnreadNotificationsFalse()
        {
            _uut.NumUnreadNotifications = 10;

            _uut.NotificationCommand.Execute(null);

            Assert.That(_uut.UnreadNotifications, Is.False);
        }

        [Test]
        public void Notification_ShowNotifications_NumUnreadNotifications0()
        {
            _uut.NumUnreadNotifications = 10;

            _uut.NotificationCommand.Execute(null);

            Assert.That(_uut.NumUnreadNotifications, Is.EqualTo(0));
        }


        [Test]
        public void Notification_NumUnreadNotificationSetTwice_PropertyChangedInvokedOnce()
        {
            int invoked = 0;
            _uut.PropertyChanged += (sender, args) => ++invoked;

            _uut.NumUnreadNotifications = 10;
            _uut.NumUnreadNotifications = 10;

            //Equal to 2 because it invokes both NumUnreadNotifications and UnreadNotifications
            Assert.That(invoked, Is.EqualTo(2));
        }
    }
}