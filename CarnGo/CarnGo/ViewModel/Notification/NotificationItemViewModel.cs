using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using CarnGo.Annotations;
using Prism.Commands;

namespace CarnGo
{
    public class NotificationItemViewModel : BaseViewModel
    {
        #region Constructors
        public NotificationItemViewModel(MessageFromLessorModel lessorNotification)
        {
            LessorNotification = lessorNotification; 
        }
        #endregion

        #region Properties
        private MessageFromLessorModel _notification;

        public MessageFromLessorModel LessorNotification
        {
            get { return _notification; }
            set
            {
                _notification = value;
                OnPropertyChanged(nameof(Message));
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
            //Probably needs to send the specific message with it. 
            ViewModelLocator.ApplicationViewModel.GoToPage(ApplicationPage.LoginPage);
        }
        #endregion
    }
}
