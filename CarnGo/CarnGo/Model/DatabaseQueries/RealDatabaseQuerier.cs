using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;

namespace CarnGo
{
    public class RealDatabaseQuerier : IQueryDatabase
    {
        public Task RegisterUserTask(string email, SecureString password)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserModel> GetUserTask(string email, SecureString password)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<MessageModel>> GetUserMessages(UserModel user)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateUserMessages(UserModel user, List<MessageModel> messages)
        {
            throw new System.NotImplementedException();
        }
    }
}