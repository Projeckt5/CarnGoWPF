using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;

namespace CarnGo
{
    public class SearchDesignModel : SearchViewModel
    {
        public static SearchDesignModel Instance => 
            new SearchDesignModel(IoCContainer.Resolve<IEventAggregator>(), IoCContainer.Resolve<IApplication>(), 
                IoCContainer.Resolve<ISearchViewModelHelper>(), IoCContainer.Resolve<ISearchQueries>());

        #region Constructor

        public SearchDesignModel(IEventAggregator eventAggregator, IApplication application, ISearchViewModelHelper helper, ISearchQueries dbContext) 
            : base(eventAggregator, application, helper, dbContext)
        {
            UserModel jensJensen = new UserModel
            {
                FirstName = "Jens",
                LastName = "Jensen",
                Address = "Finlandsgade 1",
                Email = "hmm@gmail.com",
                UserType = UserType.Lessor
            };

            SearchResultItems = new ObservableCollection<SearchResultItemViewModel>
            {
                new SearchResultItemViewModel(IoCContainer.Resolve<IEventAggregator>(), IoCContainer.Resolve<IApplication>())
                {
                        Model = "CLA 250",
                        Brand = "Mercedes",
                        Location = "Aarhus",
                        RegNr = "AF75903",
                        Seats = 2,
                        Price = 400,
                        StartLeaseTime = new DateTime(2019, 07, 07),
                        EndLeaseTime = new DateTime(2019, 08, 07),
                        Owner = jensJensen,
                },
                new SearchResultItemViewModel(IoCContainer.Resolve<IEventAggregator>(), IoCContainer.Resolve<IApplication>())
                {
                        Model = "CLA 250",
                        Brand = "Mercedes",
                        Location = "Aarhus",
                        RegNr = "AF75903",
                        Seats = 2,
                        Price = 400,
                        StartLeaseTime = new DateTime(2019, 12, 01),
                        EndLeaseTime = new DateTime(2019, 12, 15),
                        Owner = jensJensen
                },
                new SearchResultItemViewModel(IoCContainer.Resolve<IEventAggregator>(), IoCContainer.Resolve<IApplication>())
                {
                        Model = "Model S",
                        Brand = "Tesla",
                        Location = "Aarhus",
                        RegNr = "AF75903",
                        Seats = 5,
                        Price = 600,
                        StartLeaseTime = new DateTime(2019, 09, 01),
                        EndLeaseTime = new DateTime(2019, 09, 15),
                        Owner = jensJensen
                },
                new SearchResultItemViewModel(IoCContainer.Resolve<IEventAggregator>(), IoCContainer.Resolve<IApplication>())
                {
                        Model = "Fortwo",
                        Brand = "Smart",
                        Location = "Padborg",
                        RegNr = "AF75903",
                        Seats = 2,
                        Price = 150,
                        StartLeaseTime = new DateTime(2019, 10, 01),
                        EndLeaseTime = new DateTime(2019, 10, 30),
                        Owner = jensJensen
                },
                new SearchResultItemViewModel(IoCContainer.Resolve<IEventAggregator>(), IoCContainer.Resolve<IApplication>())
                {
                        Model = "CLA 250",
                        Brand = "Mercedes",
                        Location = "Aarhus",
                        RegNr = "AF75903",
                        Seats = 2,
                        Price = 400,
                        StartLeaseTime = new DateTime(2019, 06, 05),
                        EndLeaseTime = new DateTime(2019, 06, 20),
                        Owner = jensJensen
                },
                new SearchResultItemViewModel(IoCContainer.Resolve<IEventAggregator>(), IoCContainer.Resolve<IApplication>())
                {
                        Model = "CLA 250",
                        Brand = "Mercedes",
                        Location = "Aarhus",
                        RegNr = "AF75903",
                        Seats = 2,
                        Price = 400,
                        StartLeaseTime = new DateTime(2019, 04, 25),
                        EndLeaseTime = new DateTime(2019, 5, 17),
                        Owner = jensJensen
                },
                new SearchResultItemViewModel(IoCContainer.Resolve<IEventAggregator>(), IoCContainer.Resolve<IApplication>())
                {
                        Model = "Berlingo",
                        Brand = "Citroen",
                        Location = "Aarhus",
                        RegNr = "AF75903",
                        Seats = 5,
                        Price = 200,
                        StartLeaseTime = new DateTime(2019, 05, 01),
                        EndLeaseTime = new DateTime(2019, 05, 30),
                        Owner = jensJensen
                },
                new SearchResultItemViewModel(IoCContainer.Resolve<IEventAggregator>(), IoCContainer.Resolve<IApplication>())
                {
                        Model = "A6",
                        Brand = "Audi",
                        Location = "Odense",
                        RegNr = "AF75903",
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
