using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Prism.Commands;

namespace CarnGo
{
    public class NotificationViewModel : BaseViewModel
    {
        #region Constructors
        #endregion

        #region Properties
        


        /// <summary>
        /// Messages sent from Renter to Lessor 
        /// </summary>
        private List<MessageFromRenterModel> _messages; 
        public List<MessageFromRenterModel> Messages
        {
            get { return _messages; }
            set
            {
                _messages = Messages;
                OnPropertyChanged(nameof(Messages)); 
            }
        }

        /// <summary>
        /// Dummy Data made by Martin (TODO: Make obsolete) 
        /// </summary>
        private List<NotificationItemViewModel> _items;
        public List<NotificationItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        #endregion

        #region Commands
        private ICommand _notificationPressedCommand;
        public ICommand NotificationPressedCommand => _notificationPressedCommand ?? (_notificationPressedCommand = new DelegateCommand(NotificationExecute));
        #endregion

        #region Executes & CanExecutes
        private void NotificationExecute()
        {
            ViewModelLocator.ApplicationViewModel.GoToPage(ApplicationPage.MessageView);
        }
        #endregion

    }
}
