using System;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;
using CarnGo.Database.Models;

namespace CarnGo.Database
{
    public interface IAppDbContext
    {
        void AddCarEquipment(CarEquipment carEquipment);
        void AddCarProfile(CarProfile carProfile);
        void AddDaysThatIsRented(DayThatIsRented dayThatIsRented);
        void AddDayThatIsRentedList(List<DayThatIsRented> list);
        void AddMessage(Message message);
        void AddPossibleToRentDay(PossibleToRentDay possibleToRentDay);
        Task AddUser(User user);
        Task<List<Message>> GetMessages(User user);
        Task<User> GetUser(string email, Guid authorization);
        Task<User> GetUser(string email, string password);
        void RemoveCarEquipment(int ID);
        void RemoveCarProfile(string ID);
        void RemoveDayThatIsRented(DateTime ID);
        void RemoveMessage(int ID);
        void RemovePossibleToRentDay(DateTime ID);
        void RemoveUser(string ID);
        Task UpdateCarEquipment(CarEquipment carEquipment);
        Task UpdateCarProfile(CarProfile carProfile);
        Task UpdateDayThatIsRented(DayThatIsRented dayThatIsRented);
        Task UpdateMessage(Message message);
        Task UpdatePossibleToRentDay(PossibleToRentDay possibleToRentDay);
        Task UpdateUserInformation(User user);
    }
}