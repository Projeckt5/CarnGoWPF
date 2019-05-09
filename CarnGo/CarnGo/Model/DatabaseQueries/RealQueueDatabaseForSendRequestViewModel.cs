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
        public async Task<CarProfile> GetCarProfileForSendRequestView(string regnr)
        {
            var carprofile = await context.GetCarProfileForSendRequestView(regnr);
            return carprofile;
        }

        public async Task<User> GetUser(string email)
        {
            return await context.GetUser(email);
        }

        public async Task AddMessageToLessor(Message message)
        {
            await context.AddMessageToLessor(message);
            await context.SaveChangesAsync();
        }
    }
}

