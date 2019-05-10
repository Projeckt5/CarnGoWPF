using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarnGo.Database.Models;

namespace CarnGo
{
    public interface IQueueDatabaseForSendRequestViewModel
    {
        Task<User> GetUser(string email);

        Task AddMessageToLessor(Message message);

        void AddDayThatIsRentedList(List<DayThatIsRented> list);

        Task<CarProfile> GetCarProfileForSendRequestView(string regnr);

    }
}
