using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnGo
{
    public class SendRequestViewModelDesign:SendRequestViewModel
    {
        public SendRequestViewModelDesign()
        {
            Car = new CarDetailsViewModel{AudioPlayer = true,ChildSeats = false,Gps = true,Model = "Ford Mustang",Smoking = false,Year = 2010};
            From=DateTime.Now;
            To=DateTime.Now;
            Message = "Nothing to add";
        }
    }
}
