using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarnGo.Database.Models;

namespace CarnGo
{
    public class DbToAppModelConverter : IDbToAppModelConverter
    {
        public UserModel Convert(User dbUser)
        {
            return new UserModel()
            {
                Firstname = dbUser.FirstName,
                Lastname = dbUser.LastName,
                Address = dbUser.Address,
                Email = dbUser.Email,
                AuthorizationString = dbUser.AuthorizationString,
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
                dbCarProfile.StartLeaseTime,
                dbCarProfile.EndLeaseTime,
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
                        returnList.Add(new MessageFromLessorModel(renterUserModel, lessorUserModel, car,dbMessage.TheMessage,dbMessage.Confirmation)
                        {
                            StatusConfirmation = dbMessage.Confirmation,
                            Id = dbMessage.MessageID,
                            MsgType = (MessageType)dbMessage.MsgType
                        });
                        break;
                    case MessageType.RenterMessage:
                        returnList.Add(new MessageFromRenterModel(renterUserModel,lessorUserModel,car,dbMessage.TheMessage)
                        {
                            Id = dbMessage.MessageID,
                            MsgType = (MessageType)dbMessage.MsgType
                        }); ;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }   
            }

            return returnList;
        }
        public List<CarProfileModel> Convert(List<CarProfile> carProfiles)
        {
            var carModels = new List<CarProfileModel>();

            foreach (var cars in carProfiles)
            {
                var newModel = new CarProfileModel();
                var newOwner = new UserModel()
                {
                    Address = cars.User.Address,
                    Email = cars.User.Email,
                    Firstname = cars.User.FirstName,
                    Lastname = cars.User.LastName,

                };
                newModel.DayThatIsRented = cars.DaysThatIsRented;
                newModel.PossibleToRentDays = cars.PossibleToRentDays;
                newModel.Brand = cars.Brand;
                newModel.CarDescription = cars.CarDescription;
                newModel.FuelType = cars.FuelType;
                newModel.Location = cars.Location;
                newModel.Model = cars.Model;
                newModel.Price = cars.Price;
                newModel.Age = cars.Age;
                newModel.RentalPrice = cars.RentalPrice;
                newModel.Seats = cars.Seats;

                newModel.Owner = newOwner;

                carModels.Add(newModel);
            }


            return carModels;
        }
    }
}
