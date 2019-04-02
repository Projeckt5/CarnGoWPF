using System.Windows.Input;
using Prism.Commands;

namespace CarnGo
{
    public class SearchResultItemViewModel : CarProfileModel
    {

        public SearchResultItemViewModel()
        {
            
        }

        private ICommand _changetosendrequestcommand;

        public ICommand CangetosendrequestCommand
        {
            get
            {
                return _changetosendrequestcommand ?? (_changetosendrequestcommand = new DelegateCommand(() =>
                           ViewModelLocator.ApplicationViewModel
                               .GoToPage(ApplicationPage.SendRequestPage)));
            }
        }

        }
    }