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
                Address = "TestAddress",
                Email = "Test@Test.com",
                AuthorizationString = Guid.NewGuid(),
                MessagesWithUsers = new List<MessagesWithUsers>(),
                Cars = new List<CarProfile>(),
                Password = "P4ssw0rd",
                UserType = 1
            };

            _message = new Message()
            {
                TheMessage = "TestMsg",
                LessorEmail = "TestLessor",
                RenterEmail = "TestRenter",
                MsgType = 1,
                HaveBeenSeen = false,
                ConfirmationStatus = (int)MsgStatus.Unhandled,
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
            _user.AuthorizationString = appUser.AuthorizationString;
            return _user;
        }

        public List<Message> Convert(List<MessageModel> appMessages)
        {
            return _messages;
        }

        public CarProfile Convert(CarProfileModel carProfile)
        {
            throw new NotImplementedException();
        }

        public Message Convert(MessageModel appMessage)
        {
            throw new NotImplementedException();
        }

        public List<PossibleToRentDay> Convert(List<PossibleToRentDayModel> carPossibleToRentDays)
        {
            throw new NotImplementedException();
        }

        public List<DayThatIsRented> Convert(List<DayThatIsRentedModel> carDayThatIsRented)
        {
            throw new NotImplementedException();
        }
    }
}
