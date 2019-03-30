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
    public class SendRequestViewModel:BaseViewModel
    {
        #region fields

        private CarProfileModel _carProfileModel;
#endregion
        public SendRequestViewModel()
        {
            EventAggregatorSingleton.EventAggregatorObj.GetEvent<CarProfileDataEvent>().Subscribe(SearchCarProfileEvent);
        }

        #region constructor
        private void SearchCarProfileEvent(CarProfileModel car)
        {
            _carProfileModel = car;
        }
        #endregion

        #region Properties

        public string Message { get; set; } = "Message to leaser";

        public DateTime To { get; set; } = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        public DateTime From { get; set; } =new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

        public CarDetailsViewModel Car { get; set; }= new CarDetailsViewModel { AudioPlayer = true, ChildSeats = false, Gps = true, Model = "Ford Mustang", Smoking = false, Year = 2010 };
        public Object CarDetail => Car;

        #endregion

        #region Commands
        private ICommand _rentCarCommand;

        public ICommand RentCarCommand => _rentCarCommand??( _rentCarCommand=new DelegateCommand(RentCarFunction));

        private void RentCarFunction()
        {
            if (Message == null || From < DateTime.Now || To < DateTime.Now)
                return;
  
            //var sendingMessage=new MessageFromRenterModel(ViewModelLocator.ApplicationViewModel.CurrentUser,_carProfileModel.Owner);            
            //sendingMessage.From = From;
            //sendingMessage.To = To;
            //sendingMessage.Message = Message;

            //sendingMessage lægges ind i database MANGLER

            ViewModelLocator.ApplicationViewModel.GoToPage(ApplicationPage.SearchPage);//Der gås tilbage til SearchPage
        }
        #endregion
    }
}
