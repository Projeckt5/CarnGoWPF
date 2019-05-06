using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace CarnGo
{
    public class SendRequestViewModelDesign:SendRequestViewModel
    {
        public SendRequestViewModelDesign(IEventAggregator events, IApplication application,IQueueDatabaseForSendRequestViewModel queue,ISendRequestViewModelHelperFunction helper):base(events, application,queue,helper)
        {
            //Car = new CarDetailsViewModel{AudioPlayer = true,ChildSeats = false,Gps = true,Model = "Ford Mustang",Smoking = false,Year = 2010};
            From=DateTime.Now;
            To=DateTime.Now;
            Message = "Nothing to add";
        }
    }
}
