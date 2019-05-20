using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Authentication;
using System.Threading.Tasks;
using CarnGo.Database;
using CarnGo.Database.Models;
using CarnGo.Security;

namespace CarnGo
{
    public class RealDatabaseQuerier : IQueryDatabase
    {
        private readonly IAppDbContext _dbContext;
        private readonly IDbToAppModelConverter _dbToAppModelConverter;
        private readonly IAppToDbModelConverter _appToDbModelConverter;


        public RealDatabaseQuerier(IAppDbContext dbContext, 
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
                Password = password.ConvertToString()
            };
            await _dbContext.AddUser(user);
        }

        public async Task<UserModel> GetUserTask(string email, SecureString password)
        {
            var user = await _dbContext.GetUser(email, password.ConvertToString());
            var userModel = _dbToAppModelConverter.Convert(user);
            return userModel;
        }

        public async Task<UserModel> GetUserTask(UserModel user)
        {
            var dbUser = await _dbContext.GetUser(user.Email, user.AuthorizationString);
            var returnUser = _dbToAppModelConverter.Convert(dbUser);
            return returnUser;
        }

        public async Task<List<CarProfileModel>> GetCarProfilesTask(UserModel user)
        {
            var dbUser = await _dbContext.GetUser(user.Email, user.AuthorizationString);
            var dbCarProfile = await _dbContext.GetAllCars(dbUser);
            return _dbToAppModelConverter.Convert(dbCarProfile);
        }

        public async Task AddUserMessage(MessageModel message)
        {
            var dbMessage = _appToDbModelConverter.Convert(message);
            await _dbContext.AddMessage(dbMessage);
        }


        public async Task<List<MessageModel>> GetUserMessagesTask(UserModel user,int startIndex, int amount)
        {
            var dbUser = await _dbContext.GetUser(user.Email, user.AuthorizationString);
            var messagesRead = _appToDbModelConverter.Convert(user.MessageModels);
            var dbMessages = await _dbContext.GetMessages(dbUser,messagesRead,amount);
            var messages = _dbToAppModelConverter.Convert(dbMessages);
            return messages;
        }

        public async Task UpdateUserMessagesTask(List<MessageModel> messages)
        {
            var messagesAsDbMessages = _appToDbModelConverter.Convert(messages);
            //TODO: UPDATE ON THE WHOLE COLLECTION INSTEAD
            foreach (var dbMessage in messagesAsDbMessages)
            {
                await _dbContext.UpdateMessage(dbMessage);
            }
        }

        public async Task UpdateCarProfileTask(CarProfileModel carProfile)
        {
            var dbCarProfile = _appToDbModelConverter.Convert(carProfile);
            await _dbContext.UpdateCarProfile(dbCarProfile);
        }

        public async Task UpdateUser(UserModel user)
        {
            var dbUser = _appToDbModelConverter.Convert(user);
            await _dbContext.UpdateUserInformation(dbUser);
        }
    }
}