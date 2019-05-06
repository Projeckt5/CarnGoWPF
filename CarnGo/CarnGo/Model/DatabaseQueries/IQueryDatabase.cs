using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;

namespace CarnGo
{
    public interface IQueryDatabase
    {
        Task RegisterUserTask(string email, SecureString password);
        Task<UserModel> GetUserTask(string email, SecureString password);
        Task<List<MessageModel>> GetUserMessagesTask(UserModel user);
        Task UpdateUserMessagesTask(UserModel user, List<MessageModel> messages);
    }
}