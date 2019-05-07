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
        User GetUser(string email);

        void AddMessageToLessor(Message message);

        void AddDayThatIsRentedList(List<DayThatIsRented> list);

        CarProfile GetCarProfileForSendRequestView(string regnr);

    }
}
