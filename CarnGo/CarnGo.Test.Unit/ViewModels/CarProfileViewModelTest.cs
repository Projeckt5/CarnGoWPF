using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Prism.Events;

namespace CarnGo.Test.Unit.ViewModels
{
    [TestFixture]
    public class CarProfileViewModelTest
    {
        private CarProfileViewModel _uut;
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

            _fakeApplication.CurrentUser.Returns(new UserModel("Edward", "Brunton", "edward.brunton@me.com", "Bernhard Jensens Boulevard 95, 10.3", UserType.OrdinaryUser));

            _uut = new CarProfileViewModel(_fakeApplication, new CarProfileModel(_fakeApplication.CurrentUser, "R8", "Audi", 2017, "1337133", "Unknown", 0, DateTime.Today, DateTime.Today, 0));
        }

        [Test]
        public void CarProfile_()
        {

        }



    }
}
