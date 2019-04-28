using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
using Prism.Events;
using Prism.Mvvm;

namespace CarnGo.Test.Unit.ViewModels
{
    [TestFixture]
    public class SearchViewModelTest
    {
        private SearchViewModel _uut;
        private IEventAggregator _eventAggregator;
        private DateTime _today;
        private SearchEvent _searchEvent;

        [SetUp]
        public void SetUp()
        {
            _eventAggregator = new EventAggregator();
            //_eventAggregator = Substitute.For<IEventAggregator>();
            _uut = new SearchViewModel(_eventAggregator);
            _today = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            _searchEvent = new SearchEvent();

            UserModel jensJensen = new UserModel
            {
                Firstname = "Jens",
                Lastname = "Jensen",
                Address = "Finlandsgade 1",
                Email = "hmm@gmail.com",
                UserType = UserType.Lessor
            };

            _uut.SearchResultItems = new ObservableCollection<SearchResultItemViewModel>()
            {
                new SearchResultItemViewModel
                {
                    Model = "CLA 250",
                    Brand = "Mercedes",
                    Location = "Aarhus",
                    Seats = 2,
                    Price = 400,
                    StartLeaseTime = new DateTime(2019, 07, 07),
                    EndLeaseTime = new DateTime(2019, 08, 07),
                    Owner = jensJensen,
                },
                new SearchResultItemViewModel
                {
                    Model = "Model S",
                    Brand = "Tesla",
                    Location = "Odense",
                    Seats = 5,
                    Price = 600,
                    StartLeaseTime = new DateTime(2019, 09, 01),
                    EndLeaseTime = new DateTime(2019, 09, 15),
                    Owner = jensJensen
                },
                new SearchResultItemViewModel
                {
                    Model = "Berlingo",
                    Brand = "Citroen",
                    Location = "Copenhagen",
                    Seats = 5,
                    Price = 200,
                    StartLeaseTime = new DateTime(2019, 05, 01),
                    EndLeaseTime = new DateTime(2019, 05, 30),
                    Owner = jensJensen
                }
            };
        }

        [TestCase("1")]
        [TestCase("60")]
        public void IsValid_ValidatingSeatsTextInput_ReturnsTrue(string seats)
        {
            _uut.SeatsText = seats;
            _uut.DateFrom = _today;
            _uut.DateTo = _today;
            Assert.That(_uut.IsValid, Is.EqualTo(true));
        }

        [TestCase("0")]
        [TestCase("-1")]
        [TestCase("tester")]
        public void IsValid_ValidatingSeatsTextInput_ReturnsFalse(string seats)
        {
            _uut.SeatsText = seats;
            _uut.DateFrom = _today;
            _uut.DateTo = _today;

            Assert.That(_uut.IsValid, Is.EqualTo(false));
        }

        [TestCase(0, 0)]
        [TestCase(1, 2)]
        [TestCase(365, 410)]
        public void IsValid_ValidatingDatesInput_ReturnsTrue(int addToDateFrom, int addToDateTo)
        {
            _uut.SeatsText = "1";
            _uut.DateFrom = _today.AddDays(addToDateFrom);
            _uut.DateTo = _today.AddDays(addToDateTo);

            Assert.That(_uut.IsValid, Is.EqualTo(true));
        }

        [TestCase(0, -1)]
        [TestCase(-1, 0)]
        [TestCase(-1, -1)]
        [TestCase(2, 1)]
        public void IsValid_ValidatingDatesInput_ReturnsFalse(int addToDateFrom, int addToDateTo)
        {
            _uut.SeatsText = "1";
            _uut.DateFrom = _today.AddDays(addToDateFrom);
            _uut.DateTo = _today.AddDays(addToDateTo);

            Assert.That(_uut.IsValid, Is.EqualTo(false));
        }

        [Test]
        public void GetValidationError_ValidProperties_ErrorCollectionIsEmpty()
        {
            _uut.SeatsText = "1";
            _uut.DateFrom = _today;
            _uut.DateTo = _today;

            _uut.GetValidationError("SeatsText");
            _uut.GetValidationError("DateFrom");
            _uut.GetValidationError("DateTo");

            CollectionAssert.IsEmpty(_uut.Errors.Values);
        }

        [Test]
        public void GetValidationError_SeatsTextIs0_CorrectErrorMessage()
        {
            _uut.SeatsText = "0";
            _uut.DateFrom = _today;
            _uut.DateTo = _today;

            _uut.GetValidationError("SeatsText");

            CollectionAssert.Contains(_uut.Errors.Values, "Number of seats must be larger than 0");
        }

        [Test]
        public void GetValidationError_SeatsTextIsString_CorrectErrorMessage()
        {
            _uut.SeatsText = "tester";
            _uut.DateFrom = _today;
            _uut.DateTo = _today;

            _uut.GetValidationError("SeatsText");

            CollectionAssert.Contains(_uut.Errors.Values, "Number of seats must be an integer");
        }

        [Test]
        public void GetValidationError_DateFromIsBeforeToday_CorrectErrorMessage()
        {
            _uut.SeatsText = "1";
            _uut.DateFrom = _today.AddDays(-1);
            _uut.DateTo = _today;

            _uut.GetValidationError("DateFrom");

            CollectionAssert.Contains(_uut.Errors.Values, "Pick-up date must be after the current date");
        }

        [Test]
        public void GetValidationError_DateToIsBeforeToday_CorrectErrorMessage()
        {
            _uut.SeatsText = "1";
            _uut.DateFrom = _today;
            _uut.DateTo = _today.AddDays(-1);

            _uut.GetValidationError("DateTo");

            CollectionAssert.Contains(_uut.Errors.Values, "Drop off date must be after the current date");
        }

        [Test]
        public void GetValidationError_DateToIsBeforeDateFrom_CorrectErrorMessage()
        {
            _uut.SeatsText = "1";
            _uut.DateFrom = _today.AddDays(1);
            _uut.DateTo = _today;

            _uut.GetValidationError("DateTo");

            CollectionAssert.Contains(_uut.Errors.Values, "Drop off date must be after the pick-up date");
        }

        [Test]
        public void GetValidationError_AllValidatedPropertiesInvalid_CorrectErrorMessages()
        {
            _uut.SeatsText = "0";
            _uut.DateFrom = _today.AddDays(-1);
            _uut.DateTo = _today.AddDays(-1);

            _uut.GetValidationError("SeatsText");
            _uut.GetValidationError("DateFrom");
            _uut.GetValidationError("DateTo");

            CollectionAssert.Contains(_uut.Errors.Values, "Drop off date must be after the current date");
            CollectionAssert.Contains(_uut.Errors.Values, "Drop off date must be after the current date");
            CollectionAssert.Contains(_uut.Errors.Values, "Number of seats must be larger than 0");
        }

        [Test]
        public void SearchCommand_NoSearchCriteria_CriteriaCollectionIsEmpty()
        {
            _uut.SearchCommand.Execute(null);

            CollectionAssert.IsEmpty(_uut.Criteria);
        }

        // how to test subscription to headerbar search event?
        //[Test]
        //public void hmm()
        //{
        //    _eventAggregator.GetEvent<SearchEvent>().Returns(_searchEvent);
        //    _searchEvent.Publish("tester");
        //    //_eventAggregator.GetEvent<SearchEvent>().Publish("tester");
        //    Assert.That(_uut.LocationText, Is.EqualTo("tester"));
        //}

        // how to test filter properly?
        //[Test]
        //public void Filter_LocationCriteria_ItemsPassesFilterCorrectly()
        //{
        //    CollectionView Cv = new CollectionView(_uut.SearchResultItems);
        //    _uut.LocationText = "aarhus";
        //    Cv.Filter = _uut.Filtering;
        //    //_uut.SearchCommand.Execute(null);
        //    //_uut.Cv.PassesFilter();

        //    Assert.IsTrue(Cv.PassesFilter(_uut.SearchResultItems.Where(car => car.Location.Contains("aarhus"))));
        //}
    }
}