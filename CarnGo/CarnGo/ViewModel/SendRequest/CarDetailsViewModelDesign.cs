using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarnGo
{
    public class CarDetailsViewModelDesign:CarDetailsViewModel
    {
        
        public static CarDetailsViewModelDesign Cardetail =>new CarDetailsViewModelDesign();

        public CarDetailsViewModelDesign()
        {
            AudioPlayer = true;
            ChildSeats = true;
            Model = "Ford Mustang 2000";
            Smoking = false;
            Gps = false;
            Year = 2000;
        }
    }
}
