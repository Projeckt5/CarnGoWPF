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
    public class SearchResultItemViewModelTest
    {
        private SearchResultItemViewModel _uut;
        private IEventAggregator _eventAggregator;
        private IApplication _application;
        private CarProfileDataEvent _carProfileDataEvent;

        [SetUp]
        public void SetUp()
        {
            _eventAggregator = Substitute.For<IEventAggregator>();
            _application = Substitute.For<IApplication>();
            _carProfileDataEvent = Substitute.For<CarProfileDataEvent>();
            _eventAggregator.GetEvent<CarProfileDataEvent>().Returns(_carProfileDataEvent);
            _uut = new SearchResultItemViewModel(_eventAggregator, _application);
        }

        [Test]
        public void SendRequest_SendRequestInvokesEventAggregator_EventAggregatorGetsRightEventArgs()
        {
            _uut.RegNr = "AF75903";

            _uut.SendRequestCommand.Execute(null);

            _eventAggregator.GetEvent<CarProfileDataEvent>().Received().Publish(Arg.Is<string>(arg => arg.Equals("AF75903")));
        }

        [Test]
        public void SendRequest_SendRequestInvokesApplication_PageChangedToSendRequest()
        {
            _uut.SendRequestCommand.Execute(null);

            _application.Received().GoToPage(Arg.Is<ApplicationPage>(page => page == ApplicationPage.SendRequestPage));
        }
    }
}
