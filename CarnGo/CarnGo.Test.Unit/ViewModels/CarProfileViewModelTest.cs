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
        private IQueryDatabase _queryDatabase;
        private SearchEvent _fakeSearchEvent;
        private NotificationMessagesUpdateEvent _fakeUpdateEvent;

        [SetUp]
        public void TestSetup()
        {
            _fakeEventAggregator = Substitute.For<IEventAggregator>();
            _fakeApplication = Substitute.For<IApplication>();
            _queryDatabase = Substitute.For<IQueryDatabase>();
            _fakeSearchEvent = Substitute.For<SearchEvent>();
            _fakeUpdateEvent = Substitute.For<NotificationMessagesUpdateEvent>();
            _fakeEventAggregator.GetEvent<SearchEvent>().Returns(_fakeSearchEvent);
            _fakeEventAggregator.GetEvent<NotificationMessagesUpdateEvent>().Returns(_fakeUpdateEvent);

            _fakeApplication.CurrentUser.Returns(new UserModel("Edward", "Brunton", "edward.brunton@me.com", "Bernhard Jensens Boulevard 95, 10.3", UserType.OrdinaryUser));

        }

        [Test]
        public void CarProfile_()
        {

        }



    }
}
