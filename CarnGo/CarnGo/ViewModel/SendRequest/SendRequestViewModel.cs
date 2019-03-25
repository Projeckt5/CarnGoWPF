using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;

namespace CarnGo
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
        public DateTime To { get; set; }
        public DateTime From { get; set; }

        private ICommand _rentCarCommand;

        public ICommand RentCarCommand
        {
            get { return _rentCarCommand??( _rentCarCommand=new DelegateCommand(RentCarFunction)); }
            
        }

        private void RentCarFunction()
        {
            
        }
    }
}
