using Prism.Events;

namespace CarnGo
{
    public class CarProfileDataEvent : PubSubEvent<CarProfileModel> { }
    
    public class SearchViewModel
    {
        

        public SearchViewModel()
        {
            

            //var hej = new CarProfileDataEvent();
            //_eventAggregator.GetEvent<CarProfileDataEvent>().Publish(hej);
        }
    }
}