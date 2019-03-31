using System;
using System.Collections.Generic;
using System.Windows.Documents;
using Prism.Events;

namespace CarnGo
{
    public class CarProfileDataEvent : PubSubEvent<CarProfileModel> { }
    
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

            _searchResultItems = new List<SearchResultItemViewModel>
            {
                new SearchResultItemViewModel
                { 
                    Name = "CLA 250",
                    Brand = "Mercedes",
                    Age = 2010,
                    Regnr = "CA86304",
                    Location = "Aarhus",
                    Seats = 2,
                    Price = 400,
                    StartLeaseTime = DateTime.Today,
                    EndLeaseTime = DateTime.Today,
                    Owner = jensJensen
                },
                new SearchResultItemViewModel
                {
                    Name = "CLA 250",
                    Brand = "Mercedes",
                    Age = 2010,
                    Regnr = "CA86304",
                    Location = "Aarhus",
                    Seats = 2,
                    Price = 400,
                    StartLeaseTime = DateTime.Today,
                    EndLeaseTime = DateTime.Today,
                    Owner = jensJensen
                },
                new SearchResultItemViewModel
                {
                    Name = "Model S",
                    Brand = "Tesla",
                    Age = 2018,
                    Regnr = "CA86304",
                    Location = "Aarhus",
                    Seats = 5,
                    Price = 600,
                    StartLeaseTime = DateTime.Today,
                    EndLeaseTime = DateTime.Today,
                    Owner = jensJensen
                },
                new SearchResultItemViewModel
                {
                    Name = "Fortwo",
                    Brand = "Smart",
                    Age = 2016,
                    Regnr = "CA86304",
                    Location = "Padborg",
                    Seats = 2,
                    Price = 150,
                    StartLeaseTime = DateTime.Today,
                    EndLeaseTime = DateTime.Today,
                    Owner = jensJensen
                },
                new SearchResultItemViewModel
                {
                    Name = "CLA 250",
                    Brand = "Mercedes",
                    Age = 2010,
                    Regnr = "CA86304",
                    Location = "Aarhus",
                    Seats = 2,
                    Price = 400,
                    StartLeaseTime = DateTime.Today,
                    EndLeaseTime = DateTime.Today,
                    Owner = jensJensen
                },
                new SearchResultItemViewModel
                {
                    Name = "CLA 250",
                    Brand = "Mercedes",
                    Age = 2010,
                    Regnr = "CA86304",
                    Location = "Aarhus",
                    Seats = 2,
                    Price = 400,
                    StartLeaseTime = DateTime.Today,
                    EndLeaseTime = DateTime.Today,
                    Owner = jensJensen
                },
                new SearchResultItemViewModel
                {
                    Name = "Berlingo",
                    Brand = "Citroen",
                    Age = 2010,
                    Regnr = "CA86304",
                    Location = "Aarhus",
                    Seats = 5,
                    Price = 200,
                    StartLeaseTime = DateTime.Today,
                    EndLeaseTime = DateTime.Today,
                    Owner = jensJensen
                },
                new SearchResultItemViewModel
                {
                    Name = "A6",
                    Brand = "Audi",
                    Age = 2008,
                    Regnr = "CA86304",
                    Location = "Odense",
                    Seats = 5,
                    Price = 200,
                    StartLeaseTime = DateTime.Today,
                    EndLeaseTime = DateTime.Today,
                    Owner = jensJensen
                },
            };
        }

        #endregion


        #region Fields

        private List<SearchResultItemViewModel> _searchResultItems;
        
        #endregion

        #region Properties

        public List<SearchResultItemViewModel> SearchResultItems
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