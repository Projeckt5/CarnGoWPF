using System;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace CarnGo
{
    public class TestDatabaseQuerier: IQueryDatabase
    {
        public async Task RegisterUser(string email, SecureString password)
        {
            await Task.Delay(2000);
        }

        public async Task<UserModel> GetUserTask(string email, SecureString password)
        {
            await Task.Delay(2000);
            
            return new UserModel("Test", "Test", "Test@Test.Com", "TestAddress", UserType.OrdinaryUser);
        }
    }
}