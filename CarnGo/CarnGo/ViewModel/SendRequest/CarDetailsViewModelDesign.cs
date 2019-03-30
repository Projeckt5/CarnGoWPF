using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarnGo
{
    public static class CarDetailsViewModelDesign
    {
        
        public static CarDetailsViewModel Cardetail { get; }=new CarDetailsViewModel{AudioPlayer = true,ChildSeats = true,Model = "Ford Mustang 2000",Smoking =false,Gps = false,Year = 2000};
    }
}
