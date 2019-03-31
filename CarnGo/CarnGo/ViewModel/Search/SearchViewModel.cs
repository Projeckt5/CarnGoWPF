using System;
using System.Collections.Generic;
using System.Windows.Documents;
using Prism.Events;

namespace CarnGo
{
    public class CarProfileDataEvent : PubSubEvent<object> { }
    
    public class SearchViewModel : CarProfileModel
    {
        public static SearchViewModel Instance => new SearchViewModel();

        #region Fields
        private List<CarProfileModel> _searchResultItems;

        #endregion

        #region Properties
        public List<CarProfileModel> SearchResultItems
        {
            get { return _searchResultItems; }
            set
            {
                _searchResultItems = value;
                OnPropertyChanged(nameof(SearchResultItems));
            }
        }



        #endregion

        //IEventAggregator _eventAggregator;

        //public SearchViewModel(IEventAggregator ea)
        //{
        //    _eventAggregator = ea;

        //    var hej = new CarProfileDataEvent();
        //    _eventAggregator.GetEvent<CarProfileDataEvent>().Publish(hej);
        //}
    }
}