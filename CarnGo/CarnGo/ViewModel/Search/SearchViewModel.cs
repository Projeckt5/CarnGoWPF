using Prism.Events;

namespace CarnGo
{
    public class CarProfileDataEvent : PubSubEvent<object> { }
    
    public class SearchViewModel
    {
        IEventAggregator _eventAggregator;

        public SearchViewModel(IEventAggregator ea)
        {
            _eventAggregator = ea;

            //var hej = new CarProfileDataEvent();
            //_eventAggregator.GetEvent<CarProfileDataEvent>().Publish(hej);
        }
    }
}