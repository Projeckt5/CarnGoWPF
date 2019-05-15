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
        bool ConfirmRentingDates(CarProfile car, DateTime to, DateTime from, ref string errorMessage);



        List<DayThatIsRented> CreateDayThatIsRentedList(DateTime from, DateTime to, CarProfile carProfile);


       

    }
}
