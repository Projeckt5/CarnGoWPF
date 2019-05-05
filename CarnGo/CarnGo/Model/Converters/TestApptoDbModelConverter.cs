using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarnGo.Database.Models;
using CarnGo.Security;

namespace CarnGo
{
    public class TestApptoDbModelConverter : IAppToDbModelConverter
    {
        private User _user;
        private List<Message> _messages;
        private Message _message;
        public TestApptoDbModelConverter()
        {
            _user = new User()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Adress = "TestAddress",
                Email = "Test@Test.com",
                AuthorizationString = Guid.Parse("TestGuid123"),
                MessagesWithUsers = new List<MessagesWithUsers>(),
                Cars = new List<CarProfile>(),
                Password = "P4ssw0rd".ConvertToSecureString(),
                UserType = 1
            };

            _message = new Message()
            {
                TheMessage = "TestMsg",
                Lessor = "TestLessor",
                Renter = "TestRenter",
                MsgType = 1,
                HaveBeenSeen = false,
                Confirmation = false,
                MessagesWithUsers = new List<MessagesWithUsers>()
                {
                        new MessagesWithUsers()
                        {
                            Message =_message,
                            User = _user,
                        }
                }
            };
            _messages = new List<Message>()
            {
                _message
            };
        }

        public User Convert(UserModel appUser)
        {
            return _user;
        }

        public List<Message> Convert(List<MessageModel> appMessages)
        {
            return _messages;
        }
    }
}
