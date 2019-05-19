using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Drawing;
using System.Windows.Navigation;
using CarnGo.Database.Models;
using NSubstitute;
using Prism.Events;
using CarnGo;



namespace CarnGo.Test.Unit.ViewModels.SendRequestViewModel
{
    [TestFixture]
    public class SendRequestUnitTest
    {
        
        private CarnGo.SendRequestViewModel _uut;
        private CarProfileModel _uutCarModel;
        private IEventAggregator _event;
        private IApplication _application;
        private IQueueDatabaseForSendRequestViewModel _dbContext;
        private ISendRequestViewModelHelperFunction _helper;
        private CarProfileDataEvent _dataEvent;
        private CarProfile _car;
        private DateTime _startDate;

        [SetUp]
        public void Setup()
        {
            var user = new User();
            var possibleDates = new List<PossibleToRentDay>();
            var alreadyRentedDates = new List<DayThatIsRented>();
            for (var date = _startDate; date <= _startDate.AddMonths(1); date = date.AddDays(1))
            {
                possibleDates.Add(new PossibleToRentDay() { CarProfile = _car, Date = date });
                if (date <= _startDate.AddDays(6))
                    alreadyRentedDates.Add(new DayThatIsRented() { User = user, CarProfile = _car, Date = date });
            }

            _uutCarModel = new CarProfileModel
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
                Owner = new UserModel()
                {
                    
                    Email = "martinx1995x@hotmail.com"
                },
                RegNr = "1107959",
                RentalPrice = 5000,
                FuelType = "Premium91",
                Seats = 5,
                Price = 200000,
                Location = "Århus",
                StartLeaseTime = new DateTime(2019, 4, 25),
                EndLeaseTime = new DateTime(2019, 5, 25),
                PossibleToRentDays = possibleDates,
                DayThatIsRented = alreadyRentedDates

            };
            _car=new CarProfile()
            {
                Model = "Mustang",
                Brand = "Ford",
                Age = 2010,
                CarDescription = "Bilen har kun været brugt 5 gange i løbet af de 10 år jeg har eget den, så den er så god som ny.",
                User = new User{Address="Århus",
                    Email ="martinx1995x@hotmail.com",
                    FirstName = "Martin",
                    LastName = "Jespersen",

                },
                RegNr = "1107959",
                RentalPrice = 5000,
                FuelType = "Premium91",
                Seats = 5,
                Price = 200000,
                Location = "Århus",
                PossibleToRentDays = possibleDates,
                DaysThatIsRented = alreadyRentedDates
                //StartLeaseTime = new DateTime(2019, 4, 25), mangler i database
                //EndLeaseTime = new DateTime(2019, 5, 25) mangler i database
            };
            _event = Substitute.For<IEventAggregator>();
            _application = Substitute.For<IApplication>();
            _dbContext = Substitute.For<IQueueDatabaseForSendRequestViewModel>();
            _helper = Substitute.For<ISendRequestViewModelHelperFunction>();
            _dataEvent = Substitute.For<CarProfileDataEvent>();
            _event.GetEvent<CarProfileDataEvent>().Returns(_dataEvent);
            //EventAggregator events=new EventAggregator();
            _uut = new CarnGo.SendRequestViewModel(_event, _application,_dbContext,_helper);
            _dbContext.GetCarProfileForSendRequestView(Arg.Any<string>()).Returns(_car);
            // events.GetEvent<CarProfileDataEvent>().Publish("1107959");
            _application.CurrentUser.Returns(_uutCarModel.Owner);
            _uut.SearchCarProfileEvent("1107959");
        }


        [Test]
        public void RentCarFunction_MessageNotEntered_error()
        {
                       
            //_event.GetEvent<CarProfileDataEvent>().Subscribe().Returns(i);

            _uut.To = new DateTime(DateTime.Today.Year + 2, DateTime.Today.Month, DateTime.Today.Day);
            _uut.From = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, DateTime.Today.Day);
            //var execute = _uut.RentCarCommand;
            _uut.RentCarCommand.Execute(null);
            Assert.That(_uut.ErrorText,Is.EqualTo("*Information was not entered correctly"));

        }

        [Test]
        public void RentCarFunction_ToDateEnteredBeforeCurrentDate_error()
        {
            _uut.To= new DateTime(DateTime.Today.Year -1, DateTime.Today.Month, DateTime.Today.Day);
            _uut.From = new DateTime(DateTime.Today.Year +1, DateTime.Today.Month, DateTime.Today.Day);
            _uut.Message = "Unittest";
            _uut.RentCarCommand.Execute(null);
            Assert.That(_uut.ErrorText, Is.EqualTo("*Information was not entered correctly"));
        }

        [Test]
        public void RentCarFunction_FromDateEnteredBeforeCurrentDate_error()
        {
            _uut.To = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, DateTime.Today.Day);
            _uut.From = new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, DateTime.Today.Day);
            _uut.Message = "Unittest";
            _uut.RentCarCommand.Execute(null);
            Assert.That(_uut.ErrorText, Is.EqualTo("*Information was not entered correctly"));
        }

        [Test]
        public void RentCarFunction_ToDateBeforeFromDate_error()
        {
            _uut.To = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, DateTime.Today.Day);
            _uut.From = new DateTime(DateTime.Today.Year +3 , DateTime.Today.Month, DateTime.Today.Day);
            _uut.Message = "Unittest";
            _uut.RentCarCommand.Execute(null);
            Assert.That(_uut.ErrorText, Is.EqualTo("*Information was not entered correctly"));
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
        public void RentCarFunction_CreateDayThatIsRentedIsCalled_WithCorrectArguments()
        {
            _uut.To = new DateTime(DateTime.Today.Year + 2, DateTime.Today.Month, DateTime.Today.Day);
            _uut.From = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, DateTime.Today.Day);
            _uut.Message = "Unittest";
            _helper.ConfirmRentingDates(Arg.Is(_car), Arg.Is(_uut.To), Arg.Is(_uut.From), ref Arg.Any<string>()).Returns(true);
            
            _uut.RentCarCommand.Execute(null);
            _helper.Received(1).CreateDayThatIsRentedList(Arg.Is(_uut.From), Arg.Is(_uut.To), Arg.Is(_car));
            
        }

        [Test]
        public void RentCarFunction_AddDayThatIsRentedIsCalled_WithCorrectListArgument()
        {
            _uut.To = new DateTime(DateTime.Today.Year + 2, DateTime.Today.Month, DateTime.Today.Day);
            _uut.From = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, DateTime.Today.Day);
            _uut.Message = "Unittest";
            _helper.ConfirmRentingDates(Arg.Is(_car), Arg.Is(_uut.To), Arg.Is(_uut.From), ref Arg.Any<string>()).Returns(true);
            var list = new List<DayThatIsRented>();
            _helper.CreateDayThatIsRentedList(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<CarProfile>())
                .Returns(list);

            _uut.RentCarCommand.Execute(null);
           
            _dbContext.Received(1).AddDayThatIsRentedList(Arg.Is(list));
        }

        [Test]
        public void RentCarFunction_GetUserIsCalled_WithCorrectArgument()
        {
            _uut.To = new DateTime(DateTime.Today.Year + 2, DateTime.Today.Month, DateTime.Today.Day);
            _uut.From = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, DateTime.Today.Day);
            _uut.Message = "Unittest";
            _helper.ConfirmRentingDates(Arg.Is(_car), Arg.Is(_uut.To), Arg.Is(_uut.From), ref Arg.Any<string>()).Returns(true);
            var list = new List<DayThatIsRented>();
            _helper.CreateDayThatIsRentedList(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<CarProfile>())
                .Returns(list);
            _uut.RentCarCommand.Execute(null);
            
            _dbContext.Received(1).GetUser(Arg.Is("martinx1995x@hotmail.com"));
        }

        [Test]
        public void RentCarFunction_AddMessageToLessorIsCalled_WithCorrectArgument()
        {
            _uut.To = new DateTime(DateTime.Today.Year + 2, DateTime.Today.Month, DateTime.Today.Day);
            _uut.From = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, DateTime.Today.Day);
            _uut.Message = "Unittest";
            _helper.ConfirmRentingDates(Arg.Is(_car), Arg.Is(_uut.To), Arg.Is(_uut.From), ref Arg.Any<string>()).Returns(true);
            var list = new List<DayThatIsRented>();
            _helper.CreateDayThatIsRentedList(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<CarProfile>())
                .Returns(list);
            var user = new User();
            _dbContext.GetUser(Arg.Any<string>()).Returns(user);

            _uut.RentCarCommand.Execute(null);


            _dbContext.Received(1).AddMessageToLessor(_uut.Message, _car, user);
        }

       // [Test]
       //public void RentCarFunction_AddMessageToLessorIsCalled_WithCorrectArguments()
       // {
       //     _uut.To = new DateTime(DateTime.Today.Year + 2, DateTime.Today.Month, DateTime.Today.Day);
       //     _uut.From = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, DateTime.Today.Day);
       //     _uut.Message = "Unittest";
       //     _helper.ConfirmRentingDates(Arg.Is(_car), Arg.Is(_uut.To), Arg.Is(_uut.From), ref Arg.Any<string>()).Returns(true);
       //     var list = new List<DayThatIsRented>();
       //     _helper.CreateDayThatIsRentedList(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<CarProfile>())
       //         .Returns(list);
       //     var user = new User();
       //     _dbContext.GetUser(Arg.Any<string>()).Returns(user);
       //     var mes = new Message();
       //     _helper.CreateMessageToLessor(Arg.Is("Unittest"), Arg.Is(_car), Arg.Is(user)).Returns(mes);

       //     _uut.RentCarCommand.Execute(null);
           
            
       //     _dbContext.Received(1).AddMessageToLessor(Arg.Is(mes));
       // }
       


    }
}
