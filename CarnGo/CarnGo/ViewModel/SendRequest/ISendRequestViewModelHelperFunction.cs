using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarnGo.Database.Models;

namespace CarnGo
{
    public interface ISendRequestViewModelHelperFunction
    {
        bool ConfirmRentingDates(CarProfileModel car, DateTime to, DateTime from, ref string errorMessage);
        List<DayThatIsRentedModel> CreateDayThatIsRentedList(DateTime from, DateTime to, CarProfileModel carProfile, UserModel renter);
    }
}
