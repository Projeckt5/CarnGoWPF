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
            if (dbUser == null)
                return default;
            return new UserModel()
            {
                Firstname = dbUser.FirstName ?? "",
                Lastname = dbUser.LastName ?? "",
                Address = dbUser.Address ?? "",
                Email = dbUser.Email ?? "",
                AuthorizationString = dbUser.AuthorizationString,
                MessageModels = new List<MessageModel>(),
                UserType = (UserType)dbUser.UserType
            };
        }

        public CarProfileModel Convert(CarProfile dbCarProfile)
        {
            if (dbCarProfile == null)
                return default;
            var owner = Convert(dbCarProfile.Owner);
            return new CarProfileModel(owner,
                dbCarProfile.Model ?? "",
                dbCarProfile.Brand ?? "",
                dbCarProfile.Age,
                dbCarProfile.RegNr ?? "",
                dbCarProfile.Location ?? "",
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
                if (dbMessage == null)
                {
                    returnList.Add(default);
                    continue;
                }
                var lessor = dbMessage.MessagesWithUsers.Select(mwu => mwu.User).Single(u => u.Email == dbMessage.LessorEmail);
                var lessorUserModel = Convert(lessor);
                var renter = dbMessage.MessagesWithUsers.Select(mwu => mwu.User).Single(u => u.Email == dbMessage.RenterEmail);
                var renterUserModel = Convert(renter);
                var car = Convert(dbMessage.CarProfile);
                switch ((MessageType)dbMessage.MsgType)
                {
                    case MessageType.LessorMessage:
                        returnList.Add(new MessageFromLessorModel(renterUserModel, lessorUserModel, car,dbMessage.TheMessage,(MsgStatus) dbMessage.ConfirmationStatus)
                        {
                            Sender = lessorUserModel.Email == dbMessage.SenderEmail ? lessorUserModel : renterUserModel,
                            Receiver = lessorUserModel.Email == dbMessage.ReceiverEmail ? lessorUserModel : renterUserModel,
                            ConfirmationStatus = (MsgStatus) dbMessage.ConfirmationStatus,
                            Id = dbMessage.MessageID,
                            MsgType = (MessageType)dbMessage.MsgType
                        });
                        break;
                    case MessageType.RenterMessage:
                        returnList.Add(new MessageFromRenterModel(renterUserModel,lessorUserModel,car,dbMessage.TheMessage)
                        {
                            Sender = lessorUserModel.Email == dbMessage.SenderEmail ? lessorUserModel : renterUserModel,
                            Receiver = lessorUserModel.Email == dbMessage.ReceiverEmail ? lessorUserModel : renterUserModel,
                            Id = dbMessage.MessageID,
                            MsgType = (MessageType)dbMessage.MsgType,
                            ConfirmationStatus = (MsgStatus)dbMessage.ConfirmationStatus
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

            foreach (var car in carProfiles)
            {
                if (car == null)
                {
                    carModels.Add(default);
                    continue;
                }
                var newModel = Convert(car);
                carModels.Add(newModel);
            }
            return carModels;
        }
    }
}
