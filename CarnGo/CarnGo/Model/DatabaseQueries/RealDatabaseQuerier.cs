using System;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;
using CarnGo.Database;
using CarnGo.Database.Models;

namespace CarnGo
{
    public class RealDatabaseQuerier : IQueryDatabase
    {
        private readonly AppDbContext _dbContext;
        private readonly IDbToAppModelConverter _dbToAppModelConverter;
        private readonly IAppToDbModelConverter _appToDbModelConverter;


        public RealDatabaseQuerier(AppDbContext dbContext, 
            IDbToAppModelConverter dbToAppModelConverter,
            IAppToDbModelConverter appToDbModelConverter)
        {
            _dbContext = dbContext;
            _dbToAppModelConverter = dbToAppModelConverter;
            _appToDbModelConverter = appToDbModelConverter;
        }
        public async Task RegisterUserTask(string email, SecureString password)
        {
            var user = new User
            {
                Email = email,
                Password = password
            };
            await _dbContext.AddUser(user);
        }

        public async Task<UserModel> GetUserTask(string email, SecureString password)
        {
            var user = await _dbContext.GetUser(email, password);
            var userModel = _dbToAppModelConverter.Convert(user);
            return userModel;
        }

        public async Task<List<MessageModel>> GetUserMessages(UserModel user)
        {
            var dbUser = await _dbContext.GetUser(user.Email, user.AuthorizationString);
            var dbMessages = await _dbContext.GetMessages(dbUser);
            var messages = _dbToAppModelConverter.Convert(dbMessages);
            return messages;
        }

        public async Task UpdateUserMessages(UserModel user, List<MessageModel> messages)
        {
            var messagesAsDbMessages = _appToDbModelConverter.Convert(messages);
            foreach (var dbMessage in messagesAsDbMessages)
            {
                await _dbContext.UpdateMessage(dbMessage);
            }
        }
    }
}