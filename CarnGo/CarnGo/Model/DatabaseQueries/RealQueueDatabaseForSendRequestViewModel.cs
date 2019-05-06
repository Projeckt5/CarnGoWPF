using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarnGo.Database;
using CarnGo.Database.Models;


namespace CarnGo
{
    public class RealQueueDatabaseForSendRequestViewModel : IQueueDatabaseForSendRequestViewModel
    {
        private AppDbContext context;
        public RealQueueDatabaseForSendRequestViewModel()
        {
            context = new AppDbContext();
        }
        public void AddDayThatIsRentedList(List<DayThatIsRented> list)
        {
            context.AddDayThatIsRentedList(list);
            context.SaveChanges();
        }
        public CarProfile GetCarProfileForSendRequestView(string regnr)
        {
            var carprofile = context.GetCarProfileForSendRequestView(regnr);
            return carprofile;
        }

        public User GetUser(string email)
        {
            return context.GetUser(email);
        }

        public void AddMessageToLessor(Message message)
        {
            context.AddMessageToLessor(message);
            context.SaveChanges();
        }
    }
}

