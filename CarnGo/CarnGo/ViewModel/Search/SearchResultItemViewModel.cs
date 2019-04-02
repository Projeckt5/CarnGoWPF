using System.Windows.Input;
using Prism.Commands;
using Prism.Events;

namespace CarnGo
{

    public class CarProfileDataEvent : PubSubEvent<SearchResultItemViewModel> { }

    public class SearchResultItemViewModel : CarProfileModel
    {
        #region Constructor

        public SearchResultItemViewModel()
        {
        }

        #endregion

        #region Commands

        private ICommand _sendRequestCommand;
        public ICommand SendRequestCommand
        {
            get
            {
                return _sendRequestCommand ?? (_sendRequestCommand = new DelegateCommand(SendRequest));
            }
        }

        #endregion

        #region Methods

        private void SendRequest()
        {
            ViewModelLocator.ApplicationViewModel
                .GoToPage(ApplicationPage.SendRequestPage);
            
            EventAggregatorSingleton.EventAggregatorObj.GetEvent<CarProfileDataEvent>().Publish(this);
        }

        #endregion
    }
}