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
        public async Task AddDayThatIsRentedList(List<DayThatIsRented> list)
        {
            await context.AddDayThatIsRentedList(list);
            await context.SaveChangesAsync();
        }
        public async Task<CarProfile> GetCarProfileForSendRequestView(string regnr)
        {
            var carprofile = await context.GetCarProfileForSendRequestView(regnr);
            return carprofile;
        }

        public void UpdateUser(User user)
        {
            context.UpdateUser(user);
        }

        public async Task<User> GetUser(string email)
        {
            return await context.GetUser(email);
        }

        public async Task AddMessageToLessor(string mes, CarProfile carProfile, User renter)
        {
            var message = new Message();
            var messageBetweenLessor = new MessagesWithUsers();
            var messageBetweenRenter = new MessagesWithUsers();
            message.TheMessage = mes;
            message.HaveBeenSeen = false;
            //adding lessor and renter strings to database missing. Why?
            message.ConfirmationStatus = (int)MsgStatus.Unhandled;
            messageBetweenLessor.Message = message;
            messageBetweenRenter.Message = message;
            //messageBetweenLessor.User = carProfile.User;

            //messageBetweenRenter.User = renter;

            
            message.MessagesWithUsers = new List<MessagesWithUsers> { messageBetweenRenter, messageBetweenLessor };
            await context.AddMessageToLessor(message);
            context.Update(renter);
            context.Update(carProfile.User);
            renter.MessagesWithUsers.Add(messageBetweenRenter);
            carProfile.User.MessagesWithUsers.Add(messageBetweenLessor);
            await context.SaveChangesAsync();
        }

        public void UpdateUser(User user,MessagesWithUsers message)
        {
            context.UpdateUser(user);
            user.MessagesWithUsers.Add(message);
            context.SaveChanges();
        }
    }
}

