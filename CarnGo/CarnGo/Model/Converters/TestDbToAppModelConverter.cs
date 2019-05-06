using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarnGo.Database.Models;

namespace CarnGo
{
    public class TestDbToAppModelConverter : IDbToAppModelConverter
    {
        public UserModel Convert(User dbUser)
        {
            return new UserModel()
            {
                Firstname = "TestFirstName",
                Lastname = "TestLastName",
                Address = "TestAddress",
                Email = "Test@Test.com",
                AuthorizationString = Guid.Parse("TestGuid123"),
                MessageModels = new List<MessageModel>(),
                UserType = UserType.Lessor
            };
        }

        public List<MessageModel> Convert(List<Message> dbMessages)
        {
            return new List<MessageModel>()
            {
                new MessageModel()
                {
                    Message = "TestMessage",
                    MessageRead = false,
                    MsgType = MessageType.LessorMessage
                }
            };
        }
    }
}
