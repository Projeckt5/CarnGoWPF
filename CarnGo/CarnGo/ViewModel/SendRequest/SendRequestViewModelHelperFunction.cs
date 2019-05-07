using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarnGo.Database.Models;


namespace CarnGo
{
    public class SendRequestViewModelHelperFunction : ISendRequestViewModelHelperFunction
    {
        public bool ConfirmRentingDates(CarProfile car, DateTime to, DateTime from, ref string errorMessage)
        {
            try
            {
                for (var rentingDate = from; rentingDate <= to; rentingDate = rentingDate.AddDays(1))
                {
                    foreach (var date in car.DaysThatIsRented)
                    {

                        if (date.Date == rentingDate)
                        {
                            errorMessage = "*Another lessor has rented this car in the specified period";
                            return false;
                        }

                    }
                }



                for (var rentingDate = from; rentingDate <= to; rentingDate = rentingDate.AddDays(1))
                {

                    bool rent = false;
                    foreach (var date in car.PossibleToRentDays)
                    {
                        if (date.Date == rentingDate)
                        {
                            rent = true;
                        }
                    }

                    if (!rent)
                    {
                        errorMessage = "*It is not possible to rent the car in the specified period";
                        return false;
                    }
                }
            }
            catch (NullReferenceException e)
            {

            }

            return true;
        }


        public List<DayThatIsRented> CreateDayThatIsRentedList(DateTime from, DateTime to, CarProfile carProfile)
        {
            var list = new List<DayThatIsRented>();
            for (var rentingDate = from; rentingDate.Date <= to.Date; rentingDate = rentingDate.AddDays(1))
            {
                list.Add(new DayThatIsRented() { CarProfile = carProfile, Date = rentingDate, User = carProfile.User });
            }

            return list;
        }

        public Message CreateMessageToLessor(string mes, CarProfile carProfile, User renter)
        {
            var message = new Message();
            var messageBetweenLessor = new MessagesWithUsers();
            var messageBetweenRenter = new MessagesWithUsers();
            message.TheMessage = mes;
            message.HaveBeenSeen = false;
            //adding lessor and renter strings to database missing. Why?
            message.Confirmation = false;
            messageBetweenLessor.Message = message;
            messageBetweenRenter.Message = message;
            messageBetweenLessor.User = carProfile.User;

            messageBetweenRenter.User = renter;

            message.MessagesWithUsers = new List<MessagesWithUsers> { messageBetweenRenter, messageBetweenLessor };
            return message;

        }
    }
}

