using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CarnGo.Annotations;
using Prism.Commands;

namespace CarnGo
{
    public class LessorItemViewModel : BaseViewModel
    {
        #region Constructors
        public LessorItemViewModel(MessageFromLessorModel lessorNotification)
        {
            Message = lessorNotification.Message;
            CarPicture = lessorNotification.RentCar.CarPicture; 
            Renter = $"{lessorNotification.Renter.Firstname} {lessorNotification.Renter.Lastname}";
            Lessor = $"{lessorNotification.Lessor.Firstname} {lessorNotification.Lessor.Lastname}";
            Confirmation = lessorNotification.StatusConfirmation; 
        }
        #endregion

        #region Properties
        public string Message { get; set; }
        public BitmapImage CarPicture { get; set; }
        public string Renter { get; set; }
        public string Lessor { get; set; }
        public bool Confirmation { get; set; }
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
