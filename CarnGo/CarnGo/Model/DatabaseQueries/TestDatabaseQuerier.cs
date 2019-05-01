using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace CarnGo
{
    public class TestDatabaseQuerier: IQueryDatabase
    {
        public async Task RegisterUserTask(string email, SecureString password)
        {
            await Task.Delay(2000);
        }

        public async Task<UserModel> GetUserTask(string email, SecureString password)
        {
            await Task.Delay(2000);

            if(string.IsNullOrEmpty(email))
                throw new UserNotFoundException("User wasn't found");
            
            return new UserModel("Test", "Test", "Test@Test.Com", "TestAddress", UserType.OrdinaryUser)
            {
                MessageModels = new List<MessageModel>()
                {
                    new MessageModel()
                    {
                        Message = "Test", MessageRead = false, MsgType = MessageType.LessorMessage
                    }
                }
            };
        }
    }
}