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
using Prism.Events;

namespace CarnGo
{
    public class ClickOnNotificationEvent : PubSubEvent<NotificationModel> { }
    public class NotificationItemViewModel : BaseViewModel
    {
        private readonly IApplication _application;
        private readonly IEventAggregator _eventAggregator; 
        
        #region Constructors
        public NotificationItemViewModel(IApplication application, IEventAggregator eventAggregator, MessageModel message)
        {
            _application = application;
            _eventAggregator = eventAggregator; 
            //TODO: Move to factory
            switch (message.MsgType)
            {
                case MessageType.RenterMessage:
                {
                    var renterMessage = (MessageFromRenterModel)message;
                    NotificationMessage.MsgType = renterMessage.MsgType;
                    NotificationMessage.Message = renterMessage.Message;
                    NotificationMessage.CarPicture = renterMessage.RentCar.CarPicture;
                    NotificationMessage.Renter = $"{renterMessage.Renter.Firstname} {renterMessage.Renter.Lastname}";
                }
                    break;
                case MessageType.LessorMessage:
                {
                    var lessorMessage = (MessageFromLessorModel)message;
                    NotificationMessage.MsgType = lessorMessage.MsgType;
                    NotificationMessage.Message = lessorMessage.Message;
                    NotificationMessage.CarPicture = lessorMessage.RentCar.CarPicture;
                    NotificationMessage.Lessor = $"{lessorMessage.Lessor.Firstname} {lessorMessage.Lessor.Lastname}";
                    NotificationMessage.Confirmation = lessorMessage.StatusConfirmation;

                }
                    break;
            }
        }
        #endregion


        //TODO Make this a model?
        #region Properties
        public NotificationModel NotificationMessage { get; set; } = new NotificationModel();
        #endregion

        #region Commands
        private ICommand _notificationPressedCommand;
        public ICommand NotificationPressedCommand => _notificationPressedCommand ?? (_notificationPressedCommand = new DelegateCommand(NotificationExecute));

        private ICommand _messagePressedCommand;

        public ICommand MesssagePressedCommand =>
            _messagePressedCommand ?? (_messagePressedCommand = new DelegateCommand(MessageExecute));
        #endregion

        #region Executes & CanExecutes
        private void NotificationExecute()
        {
            //Probably needs to send the specific message with it. 
            _eventAggregator.GetEvent<ClickOnNotificationEvent>().Publish(NotificationMessage);
            if(_application.CurrentPage != ApplicationPage.MessageView)
                _application.GoToPage(ApplicationPage.MessageView);

        }

        private void MessageExecute()
        {
            //Send event
        }
        #endregion
    }

   
}
