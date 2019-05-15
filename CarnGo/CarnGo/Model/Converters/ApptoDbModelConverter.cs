using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using CarnGo.Database.Models;
using CarnGo.Security;

namespace CarnGo
{
    public class ApptoDbModelConverter : IAppToDbModelConverter
    {

        public Database.Models.User Convert(UserModel appUser)
        {
            if (appUser == null)
                return default;
            var user = new User
            {
                Email = appUser.Email ?? "",
                FirstName = appUser.Firstname ?? "",
                Address = appUser.Address ?? "",
                LastName = appUser.Lastname ?? "",
                Cars = new List<CarProfile>(),
                MessagesWithUsers = new List<MessagesWithUsers>(),
                AuthorizationString = appUser.AuthorizationString,
                UserType = (int)appUser.UserType,
                UserPhoto = Convert(appUser.UserImage)
            };
            return user;
        }

        public Database.Models.CarProfile Convert(CarProfileModel car)
        {
            if (car == null)
                return default;
            var carEquip = Convert(car.CarEquipment);
            var dbCarModel = new Database.Models.CarProfile()
            {
                Owner = Convert(car.Owner),
                Age = car.Age,
                Brand = car.Brand ?? "",
                CarDescription = car.CarDescription ?? "",
                CarEquipment = carEquip,
                DaysThatIsRented = car.DayThatIsRented,
                Price = car.Price,
                RentalPrice = car.RentalPrice,
                FuelType = car.FuelType ?? "",
                Location = car.Location ?? "",
                Seats = car.Seats,
                EndLeaseTime = car.EndLeaseTime,
                StartLeaseTime = car.StartLeaseTime,
                CarPicture = Convert(car.CarPicture)
            };
            return dbCarModel;
        }

        private Database.Models.CarEquipment Convert(CarEquipment carEquipment)
        {
            if (carEquipment == null)
                return default;
            var dbCarEquipment = new Database.Models.CarEquipment()
            {
                Audioplayer = carEquipment.AudioPlayer,
                Childseat = carEquipment.ChildSeat,
                Smoking = carEquipment.Smoking,
                GPS = carEquipment.Gps,
            };
            return dbCarEquipment;
        }

        public List<Database.Models.Message> Convert(List<MessageModel> appMessages)
        {
            var messages = new List<Message>();
            foreach (var messageModel in appMessages)
            {
                if (messageModel == null)
                {
                    messages.Add(default);
                    continue;
                }
                messages.Add(Convert(messageModel));
            }
            return messages;
        }

        public Message Convert(MessageModel appMessage)
        {
            if (appMessage == null)
                return default;
            if (appMessage.MsgType == MessageType.LessorMessage)
            {
                var msg = appMessage as MessageFromLessorModel;
                return new Message()
                {
                    CarProfile = Convert(msg.RentCar),
                    CarProfileRegNr = msg.RentCar.RegNr ?? "",
                    Confirmation = msg.StatusConfirmation,
                    HaveBeenSeen = msg.MessageRead,
                    LessorEmail = msg.Lessor.Email ?? "",
                    RenterEmail = msg.Renter.Email ?? "",
                    SenderEmail = msg.Sender.Email ?? "",
                    MsgType = (int)msg.MsgType,
                    TheMessage = msg.Message ?? "",
                    MessageID = msg.Id,
                };
            }
            else
            {
                var msg = appMessage as MessageFromRenterModel;
                return new Message()
                {
                    CarProfile = Convert(msg.RentCar),
                    CarProfileRegNr = msg.RentCar.RegNr ?? "",
                    HaveBeenSeen = msg.MessageRead,
                    RenterEmail = msg.Renter.Email ?? "",
                    LessorEmail = msg.Lessor.Email ?? "",
                    MsgType = (int)msg.MsgType,
                    TheMessage = msg.Message ?? "",
                    MessageID = msg.Id
                };
            }
        }

        private string Convert(BitmapImage image)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return System.Text.Encoding.ASCII.GetString(data);

        }
    }
}
