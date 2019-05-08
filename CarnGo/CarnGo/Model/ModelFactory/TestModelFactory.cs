using System.Collections.Generic;

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
            return new MessageModel()
            {
                Id = 1,
                Message = msg,
                MessageRead = false,
                MsgType = type
            };
        }

        public static UserModel CreateUserModel(UserType type)
        {
            return new UserModel("TestFirstName", "TestLastName",
                "Test@Test.com", "TestAddress", type);
        }
    }
}