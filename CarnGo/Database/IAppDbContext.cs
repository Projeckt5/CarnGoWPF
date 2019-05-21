using System;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;
using CarnGo.Database.Models;

namespace CarnGo.Database
{
    public interface IAppDbContext
    {
        Task UpdateUser(User user);
        Task AddCarEquipment(CarEquipment carEquipment);
        Task AddCarProfile(CarProfile carProfile);
        Task AddDaysThatIsRented(DayThatIsRented dayThatIsRented);
        Task AddDayThatIsRentedList(List<DayThatIsRented> list);
        Task AddMessage(Message message);
        Task AddPossibleToRentDay(PossibleToRentDay possibleToRentDay);
        Task AddUser(User user);
        Task<List<Message>> GetMessages(User user, List<Message> messageAlreadyRead, int amount);
        Task<User> GetUser(string email, Guid authorization);
        Task<User> GetUser(string email, string password);
        Task<CarProfile> GetCarProfile(string regNr);
        Task<List<CarProfile>> GetAllCars(User user);
        Task<List<CarProfile>> GetAllCarProfiles();
        Task<List<CarProfile>> GetCarProfilesForSearchView(int pageIndex, int itemsPerPage);
        Task<int> GetCarProfilesCount();
        Task RemoveCarEquipment(int ID);
        Task RemoveCarProfile(string ID);
        Task RemoveDayThatIsRented(DateTime ID);
        Task RemoveMessage(int ID);
        Task RemovePossibleToRentDay(DateTime ID);
        Task RemoveUser(string ID);
        Task UpdateCarEquipment(CarEquipment carEquipment);
        Task UpdateCarProfile(CarProfile carProfile);
        Task UpdateDayThatIsRented(DayThatIsRented dayThatIsRented);
        Task UpdateMessage(Message message);
        Task UpdatePossibleToRentDay(PossibleToRentDay possibleToRentDay);
        Task UpdateUserInformation(User user);
        Task<List<DayThatIsRented>> GetDaysThatIsRentedTask(string user, CarProfile carProfile);
        Task DeleteDaysThatIsRentedTask(List<DayThatIsRented> list)
    }
}