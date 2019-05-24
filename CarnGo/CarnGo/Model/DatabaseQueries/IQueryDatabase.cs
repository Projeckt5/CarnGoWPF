using System;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;
using CarnGo.Database.Models;

namespace CarnGo
{
    public interface IQueryDatabase
    {
        Task RegisterUserTask(string email, SecureString password);
        Task RegisterCarProfileTask(CarProfileModel CarProfile);
        Task<UserModel> GetUserTask(string email, SecureString password);
        Task<List<MessageModel>> GetUserMessagesTask(UserModel user, int amount);
        Task UpdateUserMessagesTask(List<MessageModel> messages);
        Task UpdateUser(UserModel user);
        Task<UserModel> GetUserTask(UserModel user);
        Task UpdateCarProfileTask(CarProfileModel carProfile);
        Task<List<CarProfileModel>> GetCarProfilesTask(UserModel user);
        Task<CarProfileModel> GetCarProfileTask(string regnr);
        Task AddUserMessage(MessageModel message);
        Task EraseDaysThatIsRented(MessageModel message);
        Task AddMessageToLessor(string mes, CarProfileModel carProfile, UserModel renter);
        Task<List<DayThatIsRentedModel>> GetDaysThatIsRentedTask(CarProfileModel carProfile);
        Task<List<PossibleToRentDayModel>> GetPossibleToRentDayTask(CarProfileModel carProfile);
        Task DeleteCarProfileTask(CarProfileModel CarProfile);
    }
}