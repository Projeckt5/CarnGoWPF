using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using Prism.Events;

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

        [SetUp]
        public void TestSetup()
        {
            _fakeEventAggregator = Substitute.For<IEventAggregator>();
            _fakeApplication = Substitute.For<IApplication>();
            _fakeSearchEvent = Substitute.For<SearchEvent>();
            _fakeUpdateEvent = Substitute.For<NotificationMessageUpdateEvent>();
            _fakeEventAggregator.GetEvent<SearchEvent>().Returns(_fakeSearchEvent);
            _fakeEventAggregator.GetEvent<NotificationMessageUpdateEvent>().Returns(_fakeUpdateEvent);
            _uut = new HeaderBarViewModel(_fakeEventAggregator,_fakeApplication);
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
        public void Logout_UserLogout_UserIsNull()
        {

            _uut.LogoutCommand.Execute(null);

            _fakeApplication.Received().LogUserOut();
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