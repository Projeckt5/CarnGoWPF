﻿using System.Security;
using System.Threading.Tasks;

namespace CarnGo
{
    public interface IQueryDatabase
    {
        Task RegisterUserTask(string email, SecureString password);
        Task<UserModel> GetUserTask(string email, SecureString password);
    }
}