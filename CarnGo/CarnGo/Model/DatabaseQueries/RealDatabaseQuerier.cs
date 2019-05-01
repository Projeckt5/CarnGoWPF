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
    }
}