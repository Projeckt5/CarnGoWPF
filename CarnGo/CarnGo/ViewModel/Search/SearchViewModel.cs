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

        #region Constructor

        public SearchViewModel()
        {
            UserModel jensJensen = new UserModel
            {
                Firstname = "Jens",
                Lastname = "Jensen",
                Address = "Finlandsgade 1",
                Email = "hmm@gmail.com",
                UserType = UserType.Lessor
            };

            _searchResultItems = new List<CarProfileModel>()
            {
                new CarProfileModel()
                { 
                    Name = "CLA 250",
                    Brand = "Mercedes",
                    Age = 5,
                    Regnr = "CA86304",
                    Location = "Aarhus",
                    Seats = 2,
                    Price = 400,
                    StartLeaseTime = DateTime.Today,
                    EndLeaseTime = DateTime.Today,
                    Owner = jensJensen
                }
            };
        }

        #endregion


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