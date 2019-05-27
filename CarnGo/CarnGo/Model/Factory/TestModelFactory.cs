using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using CarnGo.Database.Models;

namespace CarnGo
{
    public class TestModelFactory
    {
        public static UserModel CreateUserModel()
        {
            return new UserModel("TestFirstName", "TestLastName",
                "Test@Test.com", "TestAddress", UserType.Lessor)
            {
                MessageModels = new List<MessageModel>()
                {
                    CreateMessageModel("TestMsg",MessageType.LessorMessage)
                }
            };
        }

        public static UserModel CreateUserModel(string firstName, string lastName)
        {
            return new UserModel(firstName, lastName,
                "Test@Test.com", "TestAddress", UserType.Lessor);
        }
        public static UserModel CreateUserModel(string email)
        {
            return new UserModel("TestFirstName", "TestLastName",
                email, "TestAddress", UserType.Lessor);
        }

        public static UserModel CreateUserModel(UserType type)
        {
            return new UserModel("TestFirstName", "TestLastName",
                "Test@Test.com", "TestAddress", type);
        }

        public static MessageModel CreateMessageModel()
        {
            return new MessageModel()
            {
                Id = 1,
                Message = "Test",
                MessageRead = false,
                MsgType = MessageType.LessorMessage
            };
        }

        public static MessageModel CreateMessageModel(string msg, MessageType type)
        {
            var lessor = CreateUserModel("lessor@lessor");
            var renter = CreateUserModel("renter@renter");
            var carProfile = CreateCarProfile();
            switch (type)
            {
                case MessageType.LessorMessage:
                    return new MessageFromLessorModel(renter, lessor, carProfile, msg, MsgStatus.Unhandled)
                    {
                        Sender = lessor,
                        Receiver = renter,
                    };
                case MessageType.RenterMessage:
                    return new MessageFromRenterModel(renter, lessor, carProfile, msg)
                    {
                        Sender = renter,
                        Receiver = lessor
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }


        public static CarProfileModel CreateCarProfile()
        {
            return new CarProfileModel()
            {
                RegNr = "TestRegNr",
                Age = 10,
                Brand = "TestBrand",
                CarDescription = "TestDesc",
                CarEquipment = new CarEquipment()
                {
                    AudioPlayer = true,
                    ChildSeat = true,
                    Gps = true,
                    Smoking = true
                },
                CarPicture = null,
                FuelType = "TestFuel",
                Location = "TestLocation",
                Model = "TestModel"
            };
        }
    }
}