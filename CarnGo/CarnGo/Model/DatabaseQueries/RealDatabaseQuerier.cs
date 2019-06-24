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
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IDbToAppModelConverter _dbToAppModelConverter;
        private readonly IAppToDbModelConverter _appToDbModelConverter;


        public RealDatabaseQuerier(IDbContextFactory dbContextFactory,IDbToAppModelConverter dbToAppModelConverter,
            IAppToDbModelConverter appToDbModelConverter)
        {
            _dbContextFactory = dbContextFactory;
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
            using (var db = _dbContextFactory.GetContext())
            {
                await db.AddUser(user);
            }
        }

        public async Task RegisterCarProfileTask(CarProfileModel CarProfile)
        {
            var possibleToRentDays = new List<PossibleToRentDayModel>();
            for (var i = CarProfile.StartLeaseTime; i < CarProfile.EndLeaseTime; i+=TimeSpan.FromDays(1))
            {
                possibleToRentDays.Add(new PossibleToRentDayModel()
                {
                    CarProfile = CarProfile,
                    Date = i,
                });
            }
            var carModel = _appToDbModelConverter.Convert(CarProfile);
            carModel.PossibleToRentDays = _appToDbModelConverter.Convert(possibleToRentDays);
            using (var db = _dbContextFactory.GetContext())
            {
                await db.AddCarProfile(carModel);
            }
        }


        public async Task<UserModel> GetUserTask(string email, SecureString password)
        {
            using (var db = _dbContextFactory.GetContext())
            {
                var user = await db.GetUser(email, password.ConvertToString());
                var userModel = _dbToAppModelConverter.Convert(user);
                return userModel;
            }
        }

        public async Task<UserModel> GetUserTask(UserModel user)
        {
            using (var db = _dbContextFactory.GetContext())
            {

                var dbUser = await db.GetUser(user.Email, user.AuthenticationString);
                var returnUser = _dbToAppModelConverter.Convert(dbUser);
                return returnUser;
            }
        }

        public async Task<List<CarProfileModel>> GetCarProfilesTask(UserModel user)
        {
            using (var db = _dbContextFactory.GetContext())
            {
                var dbUser = await db.GetUser(user.Email, user.AuthenticationString);
                var dbCarProfile = await db.GetAllCars(dbUser);
                return _dbToAppModelConverter.Convert(dbCarProfile);
            }
        }

        public async Task AddUserMessage(MessageModel message)
        {
            var dbMessage = _appToDbModelConverter.Convert(message);
            using (var db = _dbContextFactory.GetContext())
            {
                await db.AddMessage(dbMessage);
            }
        }


        public async Task<List<MessageModel>> GetUserMessagesTask(UserModel user, int amount)
        {
            using (var db = _dbContextFactory.GetContext())
            {
                var dbUser = await db.GetUser(user.Email, user.AuthenticationString);
                var messagesRead = _appToDbModelConverter.Convert(user.MessageModels);
                var dbMessages = await db.GetMessages(dbUser, messagesRead, amount);
                var messages = _dbToAppModelConverter.Convert(dbMessages);
                return messages;
            }
        }

        public async Task UpdateUserMessagesTask(List<MessageModel> messages)
        {
            var messagesAsDbMessages = _appToDbModelConverter.Convert(messages);
            using (var db = _dbContextFactory.GetContext())
            {
                //TODO: UPDATE ON THE WHOLE COLLECTION INSTEAD
                foreach (var dbMessage in messagesAsDbMessages)
                {
                    await db.UpdateMessage(dbMessage);
                }
            }
        }

        public async Task UpdateCarProfileTask(CarProfileModel carProfile)
        {
            using(var db = _dbContextFactory.GetContext())
            {
                var dbCarProfile = _appToDbModelConverter.Convert(carProfile);
                await db.UpdateCarProfile(dbCarProfile);
            }
        }

        public async Task UpdateUser(UserModel user)
        {
            using(var db = _dbContextFactory.GetContext())
            {

                var dbUser = _appToDbModelConverter.Convert(user);
                await db.UpdateUserInformation(dbUser);
            }
        }

        public async Task EraseDaysThatIsRented(MessageModel message)
        {
            using(var db = _dbContextFactory.GetContext())
            {

                var mes = new ApptoDbModelConverter().Convert(message);
                var daysThatIsRented = await db.GetDaysThatIsRentedTask(mes.SenderEmail, mes.CarProfile);
                await db.DeleteDaysThatIsRentedTask(daysThatIsRented);
            }
        }

        public async Task AddMessageToLessor(string mes, CarProfileModel carProfile, UserModel renter)
        {
            using (var db = _dbContextFactory.GetContext())
            {

                var message = new MessageFromRenterModel(renter,carProfile.Owner,carProfile,mes)
                {
                    Sender = renter,
                    Receiver = carProfile.Owner,
                    TimeStamp = DateTime.Now
                };
                var dbMessage = _appToDbModelConverter.Convert(message);

                await db.AddMessage(dbMessage);
            }
        }

        public async Task<List<DayThatIsRentedModel>> GetDaysThatIsRentedTask(CarProfileModel carProfile)
        {
            using (var db = _dbContextFactory.GetContext())
            {

                var dbRentedDays = await db.GetCarProfileRentedDaysTask(carProfile.RegNr);
                var rentedDays = _dbToAppModelConverter.Convert(dbRentedDays);
                return rentedDays;
            }
        }

        public async Task<List<PossibleToRentDayModel>> GetPossibleToRentDayTask(CarProfileModel carProfile)
        {
            using (var db = _dbContextFactory.GetContext())
            {
                var dbPossibleToRentDays = await db.GetCarProfilePossibleToRentDayTask(carProfile.RegNr);
                var possibleToRentDay = _dbToAppModelConverter.Convert(dbPossibleToRentDays);
                return possibleToRentDay;
            }
        }

        public async Task<CarProfileModel> GetCarProfileTask(string regnr)
        {
            using (var db = _dbContextFactory.GetContext())
            {

                var carProfile = await db.GetCarProfile(regnr);
                var carProfileModel = _dbToAppModelConverter.Convert(carProfile);
                carProfileModel.DayThatIsRented = await GetDaysThatIsRentedTask(carProfileModel);
                carProfileModel.PossibleToRentDays = await GetPossibleToRentDayTask(carProfileModel);
                return carProfileModel;
            }
        }

        public async Task DeleteCarProfileTask(CarProfileModel CarProfile)
        {
            using (var db = _dbContextFactory.GetContext())
            {
                await db.RemoveCarProfile(CarProfile.RegNr);
            }
        }
    }
}