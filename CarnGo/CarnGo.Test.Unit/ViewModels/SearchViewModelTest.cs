using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Data;

namespace CarnGo.Test.Unit.ViewModels
{
    [TestFixture]
    public class SearchViewModelTest
    {
        #region Fields

        private SearchViewModel _uut;
        private IEventAggregator _eventAggregator;
        private IApplication _application;
        private DateTime _today;
        private SearchEvent _searchEvent;
        private object _car1;
        private object _car2;
        private object _car3;

        #endregion

        #region Setup

        [SetUp]
        public void SetUp()
        {
            _eventAggregator = Substitute.For<IEventAggregator>();
            _application = Substitute.For<IApplication>();
            _searchEvent = new SearchEvent();
            _eventAggregator.GetEvent<SearchEvent>().Returns(_searchEvent);
            _uut = new SearchViewModel(_eventAggregator, _application);
            _today = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            _uut.SearchResultItems = new ObservableCollection<SearchResultItemViewModel>();

            UserModel jensJensen = new UserModel
            {
                Firstname = "Jens",
                Lastname = "Jensen",
                Address = "Finlandsgade 1",
                Email = "hmm@gmail.com",
                UserType = UserType.Lessor
            };
            _car1 = new SearchResultItemViewModel(_eventAggregator, _application)
            {
                Model = "CLA 250",
                Brand = "Mercedes",
                Location = "Aarhus",
                RegNr = "AF75903",
                Seats = 2,
                Price = 400,
                StartLeaseTime = _today,
                EndLeaseTime = _today.AddMonths(1),
                Owner = jensJensen,
            };
            _car2 = new SearchResultItemViewModel(_eventAggregator, _application)
            {
                Model = "Model S",
                Brand = "Tesla",
                Location = "Odense",
                RegNr = "AF75903",
                Seats = 5,
                Price = 600,
                StartLeaseTime = _today.AddMonths(1),
                EndLeaseTime = _today.AddMonths(2),
                Owner = jensJensen
            };
            _car3 = new SearchResultItemViewModel(_eventAggregator, _application)
            {
                Model = "Berlingo",
                Brand = "Citroen",
                Location = "Copenhagen",
                RegNr = "AF75903",
                Seats = 5,
                Price = 200,
                StartLeaseTime = _today.AddMonths(2),
                EndLeaseTime = _today.AddMonths(3),
                Owner = jensJensen
            };
            _uut.SearchResultItems.Add((SearchResultItemViewModel) _car1);
            _uut.SearchResultItems.Add((SearchResultItemViewModel) _car2);
            _uut.SearchResultItems.Add((SearchResultItemViewModel) _car3);
        }

        #endregion

        #region Search and Filtering

        [Test]
        public void SearchCommand_FilterByLocation_Car1PassesFilter()
        {
            CollectionView cv = new CollectionView(_uut.SearchResultItems);
            _uut.LocationText = "Aarhus";
            _uut.SearchCommand.Execute(null);
            cv.Filter = _uut.Filtering;

            Assert.IsTrue(cv.PassesFilter(_car1));
        }

        [Test]
        public void SearchCommand_FilterByLocation_Car2And3DoesNotPassFilter()
        {
            CollectionView cv = new CollectionView(_uut.SearchResultItems);
            _uut.LocationText = "Aarhus";
            _uut.SearchCommand.Execute(null);
            cv.Filter = _uut.Filtering;

            Assert.IsFalse(cv.PassesFilter(_car2));
            Assert.IsFalse(cv.PassesFilter(_car3));
        }

        [Test]
        public void SearchCommand_FilterByBrand_Car2PassesFilter()
        {
            CollectionView cv = new CollectionView(_uut.SearchResultItems);
            _uut.BrandText = "tesla";
            _uut.SearchCommand.Execute(null);
            cv.Filter = _uut.Filtering;

            Assert.IsTrue(cv.PassesFilter(_car2));
        }

        [Test]
        public void SearchCommand_FilterByBrand_Car1And3DoesNotPassFilter()
        {
            CollectionView cv = new CollectionView(_uut.SearchResultItems);
            _uut.BrandText = "tesla";
            _uut.SearchCommand.Execute(null);
            cv.Filter = _uut.Filtering;

            Assert.IsFalse(cv.PassesFilter(_car1));
            Assert.IsFalse(cv.PassesFilter(_car3));
        }

        [Test]
        public void SearchCommand_FilterBySeats_Car2And3PassesFilter()
        {
            CollectionView cv = new CollectionView(_uut.SearchResultItems);
            _uut.SeatsText = "5";
            _uut.SearchCommand.Execute(null);
            cv.Filter = _uut.Filtering;

            Assert.IsTrue(cv.PassesFilter(_car2));
            Assert.IsTrue(cv.PassesFilter(_car3));
        }

        [Test]
        public void SearchCommand_FilterBySeats_Car1DoesNotPassFilter()
        {
            CollectionView cv = new CollectionView(_uut.SearchResultItems);
            _uut.SeatsText = "5";
            _uut.SearchCommand.Execute(null);
            cv.Filter = _uut.Filtering;

            Assert.IsFalse(cv.PassesFilter(_car1));
        }

        [Test]
        public void SearchCommand_FilterByDates_Car1PassesFilter()
        {
            CollectionView cv = new CollectionView(_uut.SearchResultItems);
            _uut.DateFrom = _today.AddDays(15);
            _uut.DateTo = _today.AddDays(20);
            _uut.SearchCommand.Execute(null);
            cv.Filter = _uut.Filtering;

            Assert.IsTrue(cv.PassesFilter(_car1));
        }

        [Test]
        public void SearchCommand_FilterByDates_Car2And3DoesNotPassFilter()
        {
            CollectionView cv = new CollectionView(_uut.SearchResultItems);
            _uut.DateFrom = _today.AddDays(15);
            _uut.DateTo = _today.AddDays(20);
            _uut.SearchCommand.Execute(null);
            cv.Filter = _uut.Filtering;

            Assert.IsFalse(cv.PassesFilter(_car2));
            Assert.IsFalse(cv.PassesFilter(_car3));
        }

        [Test]
        public void SearchCommand_FilterByLocationAndSeats_Car1PassesFilter()
        {
            CollectionView cv = new CollectionView(_uut.SearchResultItems);
            _uut.LocationText = "aarhus";
            _uut.SeatsText = "2";
            _uut.SearchCommand.Execute(null);
            cv.Filter = _uut.Filtering;

            Assert.IsTrue(cv.PassesFilter(_car1));
        }

        [Test]
        public void SearchCommand_FilterByLocationAndSeats_Car2And3DoesNotPassFilter()
        {
            CollectionView cv = new CollectionView(_uut.SearchResultItems);
            _uut.LocationText = "aarhus";
            _uut.SeatsText = "2";
            _uut.SearchCommand.Execute(null);
            cv.Filter = _uut.Filtering;

            Assert.IsFalse(cv.PassesFilter(_car2));
            Assert.IsFalse(cv.PassesFilter(_car3));
        }

        [Test]
        public void SearchCommand_FilterByAllCriteria_Car2PassesFilter()
        {
            CollectionView cv = new CollectionView(_uut.SearchResultItems);
            _uut.BrandText = "Tesla";
            _uut.LocationText = "Odense";
            _uut.SeatsText = "5";
            _uut.DateFrom = _today.AddMonths(1);
            _uut.DateTo = _today.AddMonths(2);
            _uut.SearchCommand.Execute(null);
            cv.Filter = _uut.Filtering;

            Assert.IsTrue(cv.PassesFilter(_car2));
        }

        [Test]
        public void SearchCommand_FilterByAllCriteria_Car1And3DoesNotPassFilter()
        {
            CollectionView cv = new CollectionView(_uut.SearchResultItems);
            _uut.BrandText = "Tesla";
            _uut.LocationText = "Odense";
            _uut.SeatsText = "5";
            _uut.DateFrom = _today.AddMonths(1);
            _uut.DateTo = _today.AddMonths(2);
            _uut.SearchCommand.Execute(null);
            cv.Filter = _uut.Filtering;

            Assert.IsFalse(cv.PassesFilter(_car1));
            Assert.IsFalse(cv.PassesFilter(_car3));
        }

        [Test]
        public void SearchCommand_FilterByLocation_NoCarsPassFilter()
        {
            CollectionView cv = new CollectionView(_uut.SearchResultItems);
            _uut.LocationText = "Amsterdam";
            _uut.SearchCommand.Execute(null);
            cv.Filter = _uut.Filtering;

            Assert.IsFalse(cv.PassesFilter(_car1));
            Assert.IsFalse(cv.PassesFilter(_car2));
            Assert.IsFalse(cv.PassesFilter(_car3));
        }

        [Test]
        public void SearchCommand_NoSearchCriteria_AllCarsPassFilter()
        {
            CollectionView cv = new CollectionView(_uut.SearchResultItems);
            _uut.SearchCommand.Execute(null);
            cv.Filter = _uut.Filtering;

            Assert.IsTrue(cv.PassesFilter(_car1));
            Assert.IsTrue(cv.PassesFilter(_car2));
            Assert.IsTrue(cv.PassesFilter(_car3));
        }

        [Test]
        public void SearchCommand_AllButSingleCriteriaMatchCar1_Car1DoesNotPassFilter()
        {
            CollectionView cv = new CollectionView(_uut.SearchResultItems);
            _uut.LocationText = "Aarhus";
            _uut.BrandText = "Mercedes";
            _uut.SeatsText = "2";
            _uut.DateFrom = _today.AddDays(1);
            _uut.DateTo = _today.AddMonths(2);
            _uut.SearchCommand.Execute(null);
            cv.Filter = _uut.Filtering;

            Assert.IsFalse(cv.PassesFilter(_car1));
        }

        [Test]
        public void ClearSearchCommand_FilterByBrandAndSeats_AllCarsPassFilter()
        {
            CollectionView cv = new CollectionView(_uut.SearchResultItems);
            _uut.BrandText = "Mercedes";
            _uut.SeatsText = "2";
            _uut.SearchCommand.Execute(null);
            _uut.ClearSearchCommand.Execute(null);
            cv.Filter = _uut.Filtering;

            Assert.IsTrue(cv.PassesFilter(_car1));
            Assert.IsTrue(cv.PassesFilter(_car2));
            Assert.IsTrue(cv.PassesFilter(_car3));
        }

        #endregion

        #region Validation and ErrorHandling

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

        #endregion

        #region EventAggregator

        [Test]
        public void EventAggregatorSubscription_SearchEventPublished_LocationTextEqualsEventArg()
        {
            string searchKeyWord = "Aarhus";

            _eventAggregator
                .GetEvent<SearchEvent>()
                .Publish(searchKeyWord);

            Assert.AreEqual(searchKeyWord, _uut.LocationText);
        }

        #endregion
    }
}