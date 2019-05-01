using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Prism.Events;
using System.Drawing;
using CarnGo.Database.Models;
using NSubstitute;


namespace CarnGo.Test.Unit
{
    [TestFixture]
    public class SendRequestUnitTest
    {
        public SendRequestUnitTest()
        {
            _car = new Car();
            var carRenter = new CarRenter();
            _startDate = new DateTime(2019, 1, 1);
            var possibleDates = new List<PossibleToRentDay>();
            var alreadyRentedDates = new List<DayThatIsRented>();
            for (var date = _startDate; date <= _startDate.AddMonths(1); date = date.AddDays(1))
            {
                possibleDates.Add(new PossibleToRentDay() { Car = _car, Date = date });
                if (date <= _startDate.AddDays(6))
                    alreadyRentedDates.Add(new DayThatIsRented() { Car = _car, CarRenter = carRenter, Date = date });
            }
            _car.DaysThatIsRented=new List<DayThatIsRented>(alreadyRentedDates);
            _car.PossibleToRentDays=new List<PossibleToRentDay>(possibleDates);
        }
        private SendRequestViewModel _uut;
        private CarProfileModel _uutCarModel;
        private IEventAggregator _event;
        private IApplication _application;
        private CarProfileDataEvent _dataEvent;
        private Car _car;
        private DateTime _startDate;
        [SetUp]
        public void Setup()
        {
            
           
            _uutCarModel= new CarProfileModel
            {
                
                CarEquipment = new CarEquipment
                {
                    AudioPlayer = true,
                    ChildSeat = false,
                    Gps = true,
                    Smoking = false
                },
                Model = "Mustang",
                Brand = "Ford",
                Age = 2010,
                CarDescription = "Bilen har kun været brugt 5 gange i løbet af de 10 år jeg har eget den, så den er så god som ny.",
               
                RegNr = "1107959",
                RentalPrice = 5000,
                FuelType = "Premium91",
                Seats = 5,
                Price = 200000,
                Location = "Århus",
                StartLeaseTime = new DateTime(2019, 4, 25),
                EndLeaseTime = new DateTime(2019, 5, 25)
            };
            _event = Substitute.For<IEventAggregator>();
            _application = Substitute.For<IApplication>();
            _dataEvent = Substitute.For<CarProfileDataEvent>();
            _event.GetEvent<CarProfileDataEvent>().Returns(_dataEvent);
            _uut = new SendRequestViewModel(_event, _application);
        }


        [Test]
        public void RentCarFunction_MessageNotEntered_error()
        {
            
            
            //_event.GetEvent<CarProfileDataEvent>().Subscribe().Returns(i);

            _uut.To = new DateTime(DateTime.Today.Year + 2, DateTime.Today.Month, DateTime.Today.Day);
            _uut.From = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, DateTime.Today.Day);
            //var execute = _uut.RentCarCommand;
            _uut.RentCarCommand.Execute(null);
            Assert.That(_uut.ErrorText,Is.EqualTo("*Informaton was not entered correctly"));

        }

        [Test]
        public void RentCarFunction_ToDateEnteredBeforeCurrentDate_error()
        {
            _uut.To= new DateTime(DateTime.Today.Year -1, DateTime.Today.Month, DateTime.Today.Day);
            _uut.From = new DateTime(DateTime.Today.Year +1, DateTime.Today.Month, DateTime.Today.Day);
            _uut.Message = "Unittest";
            _uut.RentCarCommand.Execute(null);
            Assert.That(_uut.ErrorText, Is.EqualTo("*Informaton was not entered correctly"));
        }

        [Test]
        public void RentCarFunction_FromDateEnteredBeforeCurrentDate_error()
        {
            _uut.To = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, DateTime.Today.Day);
            _uut.From = new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, DateTime.Today.Day);
            _uut.Message = "Unittest";
            _uut.RentCarCommand.Execute(null);
            Assert.That(_uut.ErrorText, Is.EqualTo("*Informaton was not entered correctly"));
        }

        [Test]
        public void RentCarFunction_ToDateBeforeFromDate_error()
        {
            _uut.To = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, DateTime.Today.Day);
            _uut.From = new DateTime(DateTime.Today.Year +3 , DateTime.Today.Month, DateTime.Today.Day);
            _uut.Message = "Unittest";
            _uut.RentCarCommand.Execute(null);
            Assert.That(_uut.ErrorText, Is.EqualTo("*Informaton was not entered correctly"));
        }

        [Test]
        public void RentCarFunction_ChangePage_Succes()
        {
            _uut.To = new DateTime(DateTime.Today.Year + 2, DateTime.Today.Month, DateTime.Today.Day);
            _uut.From = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, DateTime.Today.Day);
            _uut.Message = "Unittest";
            _uut.RentCarCommand.Execute(null);
            Assert.That(_uut.ErrorText,Is.EqualTo(""));
        }

        [Test]
        public void EmptyTextBoxCommand_EmptyingString_Succes()
        {
            _uut.EmptyTextBoxCommand.Execute(null);
            Assert.That(_uut.Message,Is.EqualTo(""));
        }

        [Test]
        public void TextBoxLostFocus_NoTextEnteredInTextBox_AutoFill()
        {
            _uut.Message = "";
            _uut.TextBoxLostFocusCommand.Execute(null);
            Assert.That(_uut.Message,Is.EqualTo("Message to lessor"));
        }

        [Test]
        public void TextBoxLostFocus_TextWrittenInTextBox_NoAutoFill()
        {
            _uut.Message = "unittest";
            _uut.TextBoxLostFocusCommand.Execute(null);
            Assert.That(_uut.Message, Is.EqualTo("unittest"));
        }

        [Test]
        public void SendRequestThisProperty_ToIsBeforeCurrentDate_GetErrorString()
        {
            _uut.To = new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, DateTime.Today.Day);
            _uut.From = new DateTime(DateTime.Today.Year + 3, DateTime.Today.Month, DateTime.Today.Day);
            var result = _uut["To"];
            Assert.That(result,Is.EqualTo("The Date entered has to be after the current date or after"));
            Assert.That(_uut.ErrorCollection["To"], Is.EqualTo("The Date entered has to be after the current date or after"));
        }

        [Test]
        public void SendRequestThisProperty_ToIsBeforeFromDate_GetErrorString()
        {
            _uut.To = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, DateTime.Today.Day);
            _uut.From = new DateTime(DateTime.Today.Year + 3, DateTime.Today.Month, DateTime.Today.Day);
            var result = _uut["To"];
            Assert.That(result, Is.EqualTo("Date has to be after the the day the car is rented"));
            Assert.That(_uut.ErrorCollection["To"], Is.EqualTo("Date has to be after the the day the car is rented"));
        }

        [Test]
        public void SendRequestThisProperty_FromDateIsBeforeCurrentDate_GetErrorString()
        {
            _uut.To = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, DateTime.Today.Day);
            _uut.From = new DateTime(DateTime.Today.Year -1, DateTime.Today.Month, DateTime.Today.Day);
            var result = _uut["From"];
            Assert.That(result, Is.EqualTo("The Date entered has to be after the current date or after"));
            Assert.That(_uut.ErrorCollection["From"], Is.EqualTo("The Date entered has to be after the current date or after"));
        }

        [Test]
        public void ConfirmRentingDates_InPossibbleRangeAndOutOfButCloseToAlreadyRentedRange_returnTrue()
        {
            _uut.From = _startDate.AddDays(7);
            _uut.To = _uut.From.AddDays(7);
            bool assert=_uut.ConfirmRentingDates(_car);
            Assert.True(assert);
        }

        [Test]
        public void ConfirmRentingDates_OutofButCloseTopossibleRange_returnFalse()
        {
            _uut.From = _startDate.AddDays(23);
            _uut.To =_startDate.AddMonths(1).AddDays(1);
            bool assert= _uut.ConfirmRentingDates(_car);
            Assert.False(assert);
        }
     
        [Test]
        public void ConfirmRentingDates_InPossibbleRangeAndOutOfAlreadyRentedRangeCloseToBeingOutOfPossibleRange_returnTrue()
        {
            _uut.From = _startDate.AddDays(23);
            _uut.To = _startDate.AddMonths(1);
            bool assert = _uut.ConfirmRentingDates(_car);
            Assert.True(assert);
        }

        [Test]
        public void
            ConfirmRentingDates_InPossibbleRangeAndInAlreadyRentedRangeCloseToBeingOutOfAlreadyRentedRange_returnTrue()
        {
            _uut.From = _startDate.AddDays(6);
            _uut.To = _startDate.AddMonths(14);
            bool assert = _uut.ConfirmRentingDates(_car);
            Assert.False(assert);
        }

        [Test]
        public void
            ConfirmRentingDates_InPossibbleRangeAndInAlreadyRentedRangeCloseToBeingOutOfAlreadyRentedRange_WriteErrorMessage()
        {
            _uut.From = _startDate.AddDays(6);
            _uut.To = _startDate.AddMonths(14);
            bool assert = _uut.ConfirmRentingDates(_car);
            Assert.That(_uut.ErrorText, Is.EqualTo("*Another lessor has rented this car in the specified period"));
        }

        [Test]
        public void ConfirmRentingDates_OutofButCloseTopossibleRange_WriteErrorMessage()
        {
            _uut.From = _startDate.AddDays(23);
            _uut.To = _startDate.AddMonths(1).AddDays(1);
            bool assert = _uut.ConfirmRentingDates(_car);
            Assert.That(_uut.ErrorText, Is.EqualTo("*It is not possible to rent the car in the specified period"));
        }
       
        
    }
}
