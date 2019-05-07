using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using CarnGo.Database.Models;

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

            var User1 = new UserModel("asd", "asd", "asd@hotmail.com", "asd", UserType.Lessor);
            var User2 = new UserModel("Marcus", "Gasberg", "xXxGitMazterxXx@hotmail.com", "Gellerup", UserType.OrdinaryUser);
            var Car = new CarProfileModel(User2, "X-360", "BMW", 1989, "1234567", "Aarhus", 2, DateTime.Today, DateTime.Today, 1);

            return new UserModel("Test", "Test", "Test@Test.Com", "TestAddress", UserType.OrdinaryUser)
            {
                MessageModels =  new List<MessageModel>()
                {
                    new MessageFromLessorModel(User2, User1, Car, "Du kommer bare :)", true),
                    new MessageFromLessorModel(User2, User1, Car, "Det kan du godt glemme makker! Det kan du godt glemme makker! Det kan du godt glemme makker!", false),
                    new MessageFromRenterModel(User2, User1, Car, "Må jeg godt låne din flotte bil?"),
                }
            };
        }

        public async Task<List<MessageModel>> GetUserMessagesTask(UserModel user)
        {
            await Task.Delay(2000);

            var User1 = new UserModel("TestGetUserMessages1", "TestGetUserMessages1", "TestGetUserMessages1@hotmail.com", "TestGetUserMessages1", UserType.Lessor);
            var User2 = new UserModel("TestGetUserMessages2", "TestGetUserMessages2", "TestGetUserMessages2@hotmail.com", "TestGetUserMessages2", UserType.OrdinaryUser);
            var Car = new CarProfileModel(User2, "X-360", "BMW", 1989, "1234567", "Aarhus", 2, DateTime.Today, DateTime.Today, 1);
            //Find user messages
            user.MessageModels.Add(new MessageFromRenterModel(User2, User1, Car, "Må jeg godt låne din flotte bil?"));
            return user.MessageModels;
        }

        public async Task UpdateUserMessagesTask(UserModel user, List<MessageModel> messages)
        {
            await Task.Delay(2000);
        }

        public Task UpdateUser(UserModel user)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetUserTask(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}