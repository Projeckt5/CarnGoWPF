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
        #region fields

        private CarProfileModel _carProfileModel;
#endregion
        public SendRequestViewModel(IEventAggregator ea)
        {
            //ea.GetEvent<CarProfileDataEvent>().Subscribe(SearchCarProfileEvent);
        }

        #region constructor
        public void SearchCarProfileEvent(CarProfileModel car)
        {
            _carProfileModel = car;
        }
        #endregion

        #region Properties
        public string Message { get; set; }
        public DateTime To { get; set; }
        public DateTime From { get; set; }
        #endregion

        #region Commands
        private ICommand _rentCarCommand;

        public ICommand RentCarCommand
        {
            get { return _rentCarCommand??( _rentCarCommand=new DelegateCommand(RentCarFunction)); }
            
        }

        private void RentCarFunction()
        {
            
            var sendingMessage=new MessageFromRenterModel(ViewModelLocator.ApplicationViewModel.CurrentUser,_carProfileModel.Owner);
            //sendingMessage lægges ind i database MANGLER

            ViewModelLocator.ApplicationViewModel.GoToPage(ApplicationPage.SearchPage);//Der gås tilbage til SearchPage
        }
        #endregion
    }
}
