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
            for (var rentingDate = from; rentingDate <= to; rentingDate = rentingDate.AddDays(1))
            {
                bool rent = false;
                foreach (var date in car.DaysThatIsRented)
                {

                    if (date.Date.Date == rentingDate.Date)
                    {
                        errorMessage = "*Another lessor has rented this car in the specified period";
                        return false;
                    }

                }

                foreach (var date in car.PossibleToRentDays)
                {
                    if (date.Date.Date == rentingDate.Date)
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

        
    }
}

