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
    public class NotificationItemViewModel : BaseViewModel
    {
        #region Constructors
        public NotificationItemViewModel(MessageModel message)
        {
            //TODO: Move to factory
            switch (message.MsgType)
            {
                case MessageType.RenterMessage:
                {
                    var renterMessage = (MessageFromRenterModel) message;
                    MsgType = renterMessage.MsgType;  
                    Message = renterMessage.Message;
                    CarPicture = renterMessage.RentCar.CarPicture;
                    Renter = $"{renterMessage.Renter.Firstname} {renterMessage.Renter.Lastname}";
                }
                    break;
                case MessageType.LessorMessage:
                {
                    var lessorMessage = (MessageFromLessorModel) message;
                    MsgType = lessorMessage.MsgType;
                    Message = lessorMessage.Message;
                    CarPicture = lessorMessage.RentCar.CarPicture;
                    Lessor = $"{lessorMessage.Lessor.Firstname} {lessorMessage.Lessor.Lastname}";
                    Confirmation = lessorMessage.StatusConfirmation; 
                        
                }
                    break;
            }
        }
        #endregion


        //TODO Make this a model?
        #region Properties
        public MessageType MsgType { get; set; }
        public string Message { get; set; }
        public BitmapImage CarPicture { get; set; }
        public string Lessor { get; set; }
        public string Renter { get; set; }
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
            ViewModelLocator.ApplicationViewModel.GoToPage(ApplicationPage.MessageView);
        }
        #endregion
    }
}
