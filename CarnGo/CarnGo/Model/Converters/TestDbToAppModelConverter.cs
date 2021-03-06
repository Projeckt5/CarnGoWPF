﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using CarnGo.Database.Models;

namespace CarnGo
{
    public class TestDbToAppModelConverter : IDbToAppModelConverter
    {
        public UserModel Convert(User dbUser)
        {
            return new UserModel()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "TestAddress",
                Email = "Test@Test.com",
                AuthenticationString = dbUser.AuthenticationString,
                MessageModels = new List<MessageModel>(),
                UserType = UserType.Lessor
            };
        }

        public CarProfileModel Convert(CarProfile dbCarProfile)
        {
            var owner = Convert(dbCarProfile.Owner);
            return new CarProfileModel(owner,
                dbCarProfile.Model,
                dbCarProfile.Brand,
                dbCarProfile.Age,
                dbCarProfile.RegNr,
                dbCarProfile.Location,
                dbCarProfile.Seats,
                dbCarProfile.DaysThatIsRented[0].Date,
                dbCarProfile.DaysThatIsRented[1].Date,
                dbCarProfile.Price);
        }


        public List<MessageModel> Convert(List<Message> dbMessages)
        {
            var returnList = new List<MessageModel>();
            foreach (var dbMessage in dbMessages)
            {

                var lessor = dbMessage.MessagesWithUsers.Select(mwu => mwu.User).Single(u => u.Email == dbMessage.LessorEmail);
                var lessorUserModel = Convert(lessor);
                var renter = dbMessage.MessagesWithUsers.Select(mwu => mwu.User).Single(u => u.Email == dbMessage.RenterEmail);
                var renterUserModel = Convert(renter);
                var car = Convert(dbMessage.CarProfile);
                switch ((MessageType)dbMessage.MsgType)
                {
                    case MessageType.LessorMessage:
                        returnList.Add(new MessageFromLessorModel(renterUserModel, lessorUserModel, car,dbMessage.TheMessage,(MsgStatus)dbMessage.ConfirmationStatus));
                        break;
                    case MessageType.RenterMessage:
                        returnList.Add(new MessageFromRenterModel(renterUserModel,lessorUserModel,car,dbMessage.TheMessage));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }   
            }

            return returnList;
        }

        public List<CarProfileModel> Convert(List<CarProfile> carProfiles)
        {
            throw new NotImplementedException();
        }

        public List<PossibleToRentDayModel> Convert(List<PossibleToRentDay> possibleToRentDays)
        {
            throw new NotImplementedException();
        }

        public List<DayThatIsRentedModel> Convert(List<DayThatIsRented> dayThatIsRented)
        {
            throw new NotImplementedException();
        }

        public BitmapImage Convert(byte[] image)
        {
            throw new NotImplementedException();
        }
    }
}
