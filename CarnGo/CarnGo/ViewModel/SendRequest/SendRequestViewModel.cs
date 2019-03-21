using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace CarnGo.ViewModel.SendRequest
{
    class SendRequestViewModel:BaseViewModel
    {
        private CarProfileModel carProfileModel;
        public SendRequestViewModel(IEventAggregator ea)
        {
            //ea.GetEvent<CarProfileDataEvent>().Subscribe(SearchCarProfileEvent);
        }

        public void SearchCarProfileEvent(CarProfileModel car)
        {
            carProfileModel = car;
        }

        public string Message { get; set; }
    }
}
