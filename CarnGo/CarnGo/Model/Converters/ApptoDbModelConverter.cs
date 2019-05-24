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
                FirstName = appUser.FirstName ?? "",
                Address = appUser.Address ?? "",
                LastName = appUser.LastName ?? "",
                Cars = new List<CarProfile>(),
                MessagesWithUsers = new List<MessagesWithUsers>(),
                AuthenticationString = appUser.AuthenticationString,
                UserType = (int)appUser.UserType,
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
                CarPicture = Convert(car.CarPicture),
                Owner = Convert(car.Owner),
                Age = car.Age,
                Brand = car.Brand ?? "",
                CarDescription = car.CarDescription ?? "",
                CarEquipment = carEquip,
                Price = car.Price,
                RentalPrice = car.RentalPrice,
                FuelType = car.FuelType ?? "",
                Location = car.Location ?? "",
                Seats = car.Seats,
                EndLeaseTime = car.EndLeaseTime,
                StartLeaseTime = car.StartLeaseTime,
                Model = car.Model ?? "",
                RegNr = car.RegNr,
                OwnerEmail = car.Owner?.Email ?? ""
            };
            return dbCarModel;
        }

        public List<PossibleToRentDay> Convert(List<PossibleToRentDayModel> carPossibleToRentDays)
        {
            var possibleToRentDays = new List<PossibleToRentDay>();
            foreach (var day in carPossibleToRentDays)
            {
                if (day == null)
                {
                    possibleToRentDays.Add(default);
                    continue;
                }
                possibleToRentDays.Add(Convert(day));
            }
            return possibleToRentDays;
        }

        public PossibleToRentDay Convert(PossibleToRentDayModel carPossibleToRentDays)
        {
            return new PossibleToRentDay()
            {
                Id = carPossibleToRentDays.Id,
                Date = carPossibleToRentDays.Date
            };
        }

        public List<DayThatIsRented> Convert(List<DayThatIsRentedModel> carDayThatIsRented)
        {
            var daysThatIsRented = new List<DayThatIsRented>();
            foreach (var day in carDayThatIsRented)
            {
                if (day == null)
                {
                    daysThatIsRented.Add(default);
                    continue;
                }
                daysThatIsRented.Add(Convert(day));
            }
            return daysThatIsRented;
        }

        public DayThatIsRented Convert(DayThatIsRentedModel carDayThatIsRented)
        {
            return new DayThatIsRented()
            {
                Renter = Convert(carDayThatIsRented.Renter),
                Date = carDayThatIsRented.Date,
                Id = carDayThatIsRented.Id
            };
        }

        public Database.Models.CarEquipment Convert(CarEquipment carEquipment)
        {
            if (carEquipment == null)
            {
                return new Database.Models.CarEquipment()
                {
                    Audioplayer = false,
                    Childseat = false,
                    Smoking = false,
                    GPS = false,
                };
            }
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
                var returnMsg = new Message()
                {
                    CarProfile = Convert(msg?.RentCar),
                    CarProfileRegNr = msg?.RentCar?.RegNr ?? "",
                    ConfirmationStatus = (int) (msg?.ConfirmationStatus ?? 0),
                    HaveBeenSeen = msg?.MessageRead ?? false,
                    LessorEmail = msg?.Lessor.Email ?? "",
                    RenterEmail = msg?.Renter.Email ?? "",
                    SenderEmail = msg?.Sender.Email ?? "",
                    ReceiverEmail = msg?.Receiver.Email??"",
                    MsgType = (int)(msg?.MsgType ?? 0),
                    TheMessage = msg?.Message ?? "",
                    MessageID = msg?.Id ?? -1,
                    CreatedDate = msg?.TimeStamp ?? DateTime.Now
                };
                return returnMsg;
            }
            else
            {
                var msg = appMessage as MessageFromRenterModel;
                var returnMsg = new Message()
                {
                    CarProfile = Convert(msg?.RentCar),
                    CarProfileRegNr = msg?.RentCar?.RegNr ?? "",
                    ConfirmationStatus = (int)(msg?.ConfirmationStatus ?? 0),
                    HaveBeenSeen = msg?.MessageRead ?? false,
                    RenterEmail = msg?.Renter.Email ?? "",
                    LessorEmail = msg?.Lessor.Email ?? "",
                    SenderEmail = msg?.Sender.Email ?? "",
                    ReceiverEmail = msg?.Receiver.Email ?? "",
                    MsgType = (int)(msg?.MsgType ?? 0),
                    TheMessage = msg?.Message ?? "",
                    MessageID = msg?.Id ?? -1,
                    CreatedDate = msg?.TimeStamp ?? DateTime.Now
                };
                return returnMsg;
            }
        }

        public byte[] Convert(BitmapImage image)
        {
            if (image == null)
                return default;
            byte[] converted;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                converted = ms.ToArray();
            }
            return converted;
        }
    }
}
