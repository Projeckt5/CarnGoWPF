﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarnGo.Database.Models;
using NUnit.Framework;

namespace CarnGo.Test.Unit.ViewModels.SendRequestViewModel
{
    [TestFixture]
    public class SendRequestViewModelHelperFunctions
    {
        private ISendRequestViewModelHelperFunction _helper;
        private CarProfile _car;
        private DateTime _startDate;
        private List<PossibleToRentDay> _possibleDates;
        private List<DayThatIsRented> _alreadyRentedDates;
        public SendRequestViewModelHelperFunctions()
        {
            
        }

        [SetUp]
        public void Setup()
        {
            var user = new User();
            _possibleDates = new List<PossibleToRentDay>();
            _alreadyRentedDates = new List<DayThatIsRented>();
            for (var date = _startDate; date <= _startDate.AddMonths(1); date = date.AddDays(1))
            {
                _possibleDates.Add(new PossibleToRentDay() { CarProfile = _car, Date = date });
                if (date <= _startDate.AddDays(6))
                    _alreadyRentedDates.Add(new DayThatIsRented() { User = user, CarProfile = _car, Date = date });
            }
            _helper =new SendRequestViewModelHelperFunction();
            _car = new CarProfile();
            
            _startDate = new DateTime(2019, 1, 1);
            
            _car.DaysThatIsRented = new List<DayThatIsRented>(_alreadyRentedDates);
            _car.PossibleToRentDays = new List<PossibleToRentDay>(_possibleDates);
            
        }



        
        [Test]
        public void ConfirmRentingDates_InPossibbleRangeAndOutOfButCloseToAlreadyRentedRange_returnTrue()
        {
            DateTime from = _startDate.AddDays(7);
            DateTime to=from.AddDays(7);
            var mes = "";
            bool assert = _helper.ConfirmRentingDates(_car,to,from,ref mes);
            Assert.True(assert);
        }

        [Test]
        public void ConfirmRentingDates_OutofButCloseTopossibleRange_returnFalse()
        {
            DateTime from = _startDate.AddDays(23);
            DateTime to = _startDate.AddMonths(1).AddDays(1);
            var mes = "";
            bool assert = _helper.ConfirmRentingDates(_car,to,from,ref mes);
            Assert.False(assert);
        }

        [Test]
        public void ConfirmRentingDates_InPossibbleRangeAndOutOfAlreadyRentedRangeCloseToBeingOutOfPossibleRange_returnTrue()
        {
            DateTime from = _startDate.AddDays(23);
            DateTime to = _startDate.AddMonths(1);
            var mes = "";
            bool assert = _helper.ConfirmRentingDates(_car,to,from,ref mes);
            Assert.True(assert);
        }

        [Test]
        public void
            ConfirmRentingDates_InPossibbleRangeAndInAlreadyRentedRangeCloseToBeingOutOfAlreadyRentedRange_returnTrue()
        {
            DateTime from = _startDate.AddDays(6);
            DateTime to = _startDate.AddMonths(14);
            var mes = "";
            bool assert = _helper.ConfirmRentingDates(_car,to,from,ref mes);
            Assert.False(assert);
        }

        [Test]
        public void
            ConfirmRentingDates_InPossibbleRangeAndInAlreadyRentedRangeCloseToBeingOutOfAlreadyRentedRange_WriteErrorMessage()
        {
            DateTime from = _startDate.AddDays(6);
            DateTime to = _startDate.AddDays(14);
            var mes = "";
            bool assert = _helper.ConfirmRentingDates(_car,to,from,ref mes);
            Assert.That(mes, Is.EqualTo("*Another lessor has rented this car in the specified period"));
        }

        [Test]
        public void ConfirmRentingDates_OutofButCloseTopossibleRange_WriteErrorMessage()
        {
            DateTime from = _startDate.AddDays(23);
            DateTime to = _startDate.AddMonths(1).AddDays(1);
            var mes = "";
            bool assert = _helper.ConfirmRentingDates(_car,to,from,ref mes);
            Assert.That(mes, Is.EqualTo("*It is not possible to rent the car in the specified period"));
        }
        

        [Test]
        public void CreateDayThatIsRentedList_FromStartDateToAWeekAfter_CorrectList()
        {
            var checkList=new List<DayThatIsRented>();
            for (var date = _startDate; date <= _startDate.AddDays(6); date = date.AddDays(1))
            {
                checkList.Add(new DayThatIsRented() { CarProfile = _car, Date = date });
             
            }
            DateTime from = _startDate;
            DateTime to = from.AddDays(6);
            var list = _helper.CreateDayThatIsRentedList(from, to, _car);
            Assert.True(((list[0].Date==checkList[0].Date)&&(list[6].Date==checkList[6].Date)&&
                        (list[0].CarProfile == checkList[0].CarProfile) && (list[6].CarProfile == checkList[6].CarProfile)
                        &&(list.Count==checkList.Count)));
        }

        //[Test]
        //public void CreateMessageToLessor_MessageIsHey()
        //{
        //    var mes = "hey";
        //    var user=new User();
        //    var message = _helper.CreateMessageToLessor(mes, _car, user);
        //    Assert.True((message.Confirmation==false)&&(message.HaveBeenSeen==false)
        //                &&(message.TheMessage==mes));//mangler at asserte på noget hvis den skal testes helt igennem
        //}

    }
}
