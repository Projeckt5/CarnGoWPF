using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using CarnGo.Model;
using Prism.Commands;

namespace CarnGo
{
    public class NotificationViewModel : BaseViewModel
    {
        #region Constructors
        #endregion

        #region Properties
        private List<MessageFromLessorModel> _lessorMessages;
        public List<MessageFromLessorModel> LessorMessages
        {
            get { return _lessorMessages; }
            set
            {
                _lessorMessages = value;
                OnPropertyChanged(nameof(LessorMessages));
            }
        }

        /// <summary>
        /// Messages sent from Renter to Lessor 
        /// </summary>
        private List<MessageFromRenterModel> _renterMessages; 
        public List<MessageFromRenterModel> RenterMessages
        {
            get { return _renterMessages; }
            set
            {
                _renterMessages = value;
                OnPropertyChanged(nameof(RenterMessages)); 
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
