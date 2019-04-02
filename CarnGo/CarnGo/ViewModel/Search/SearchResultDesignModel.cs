using System;
using System.Collections.ObjectModel;

namespace CarnGo
{
    public class SearchResultDesignModel : SearchResultViewModel
    {
        public static SearchResultDesignModel Instance => new SearchResultDesignModel();

        #region Constructor

        public SearchResultDesignModel()
        {
            UserModel jensJensen = new UserModel
            {
                Firstname = "Jens",
                Lastname = "Jensen",
                Address = "Finlandsgade 1",
                Email = "hmm@gmail.com",
                UserType = UserType.Lessor
            };

            SearchResultItems = new ObservableCollection<SearchResultItemViewModel>
            {
                new SearchResultItemViewModel
                {
                    Model = "CLA 250",
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
                    Model = "CLA 250",
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
                    Model = "Model S",
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
                    Model = "Fortwo",
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
                    Model = "CLA 250",
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
                    Model = "CLA 250",
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
                    Model = "Berlingo",
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
                    Model = "A6",
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
    }
}
