﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;

namespace CarnGo
{
    public class SearchDesignModel : SearchViewModel
    {
        public static SearchDesignModel Instance => new SearchDesignModel();

        #region Constructor

        public SearchDesignModel()
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
                    StartLeaseTime = new DateTime(2019, 07, 07),
                    EndLeaseTime = new DateTime(2019, 08, 07),
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
                    StartLeaseTime = new DateTime(2019, 12, 01),
                    EndLeaseTime = new DateTime(2019, 12, 15),
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
                    StartLeaseTime = new DateTime(2019, 09, 01),
                    EndLeaseTime = new DateTime(2019, 09, 15),
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
                    StartLeaseTime = new DateTime(2019, 10, 01),
                    EndLeaseTime = new DateTime(2019, 10, 30),
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
                    StartLeaseTime = new DateTime(2019, 06, 05),
                    EndLeaseTime = new DateTime(2019, 06, 20),
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
                    StartLeaseTime = new DateTime(2019, 04, 25),
                    EndLeaseTime = new DateTime(2019, 5, 17),
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
                    StartLeaseTime = new DateTime(2019, 05, 01),
                    EndLeaseTime = new DateTime(2019, 05, 30),
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
                    StartLeaseTime = new DateTime(2019, 05, 03),
                    EndLeaseTime = new DateTime(2019, 05, 17),
                    Owner = jensJensen
                },
            };
        }
        #endregion
    }
}
