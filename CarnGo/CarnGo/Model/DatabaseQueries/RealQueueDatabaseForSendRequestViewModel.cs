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

        public async Task UpdateUser(User user)
        {
            await context.UpdateUser(user);
        }

        public async Task<User> GetUser(string email)
        {
            return await context.GetUser(email);
        }

        public async Task AddMessageToLessor(string mes, CarProfile carProfile, User renter)
        {
            var message = new Message();
            message.TheMessage = mes;
            message.HaveBeenSeen = false;
            message.ConfirmationStatus = (int)MsgStatus.Unhandled;
            message.CarProfile = carProfile;
            message.CarProfileRegNr = carProfile.RegNr;
            message.ReceiverEmail = carProfile.OwnerEmail;
            message.LessorEmail = carProfile.OwnerEmail;
            message.RenterEmail = renter.Email;
            message.SenderEmail = renter.Email;
            message.MsgType = (int) MessageType.LessorMessage;

            await context.AddMessage(message);
        }

        public async Task UpdateUser(User user,MessagesWithUsers message)
        {
            await context.UpdateUser(user);
            user.MessagesWithUsers.Add(message);
            await context.SaveChangesAsync();
        }
    }
}

