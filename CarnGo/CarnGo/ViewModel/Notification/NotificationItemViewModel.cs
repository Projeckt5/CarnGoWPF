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
using Microsoft.Expression.Interactivity.Core;
using Prism.Commands;
using Prism.Events;

namespace CarnGo
{
    public class ClickOnNotificationEvent : PubSubEvent<NotificationModel> { }
    public class NotificationConfirmationEvent : PubSubEvent<MessageFromRenterModel> { }
    public class NotificationItemViewModel : BaseViewModel
    {
        private readonly IApplication _application;
        private readonly IEventAggregator _eventAggregator;
        private readonly MessageModel _message;

        #region Constructors
        public NotificationItemViewModel(IApplication application, 
            IEventAggregator eventAggregator,
            MessageModel message)
        {
            _application = application;
            _eventAggregator = eventAggregator;
            _message = message;
            ConfirmButton = message.ConfirmationStatus == MsgStatus.Confirmed || message.ConfirmationStatus == MsgStatus.Unhandled;
            DeclineButton = message.ConfirmationStatus == MsgStatus.Declined || message.ConfirmationStatus == MsgStatus.Unhandled;
            //TODO: Move to factory
            switch (message.MsgType)
            {
                case MessageType.RenterMessage:
                {
                    var renterMessage = (MessageFromRenterModel)message;
                    NotificationMessage.MsgType = renterMessage.MsgType;
                    NotificationMessage.Message = renterMessage.Message;
                    NotificationMessage.CarPicture = renterMessage.RentCar.CarPicture;
                    NotificationMessage.Renter = $"{renterMessage.Renter.FirstName} {renterMessage.Renter.LastName}";
                    NotificationMessage.Confirmation = renterMessage.ConfirmationStatus;
                    NotificationMessage.TimeStamp = renterMessage.TimeStamp;
                    NotificationMessage.IsRead = renterMessage.MessageRead;
                }
                    break;
                case MessageType.LessorMessage:
                {
                    var lessorMessage = (MessageFromLessorModel)message;
                    NotificationMessage.MsgType = lessorMessage.MsgType;
                    NotificationMessage.Message = lessorMessage.Message;
                    NotificationMessage.CarPicture = lessorMessage.RentCar.CarPicture;
                    NotificationMessage.Lessor = $"{lessorMessage.Lessor.FirstName} {lessorMessage.Lessor.LastName}";
                    NotificationMessage.Confirmation = lessorMessage.ConfirmationStatus;
                    NotificationMessage.TimeStamp = lessorMessage.TimeStamp;
                    NotificationMessage.IsRead = lessorMessage.MessageRead;
                }
                    break;
            }
        }
        #endregion


        #region Properties
        public NotificationModel NotificationMessage { get; set; } = new NotificationModel();

        private bool _confirmButton; 
        public bool ConfirmButton
        {
            get { return _confirmButton; }
            set
            {
                _confirmButton = value;
                OnPropertyChanged(nameof(ConfirmButton));
            }
        }

        private bool _declineButton; 
        public bool DeclineButton
        {
            get { return _declineButton; }
            set
            {
                _declineButton = value;
                OnPropertyChanged(nameof(DeclineButton));
            }
        }

        #endregion

        #region Commands
        private ICommand _notificationPressedCommand;
        public ICommand NotificationPressedCommand => _notificationPressedCommand ?? (_notificationPressedCommand = new DelegateCommand(NotificationExecute));

        private ICommand _rentalButtonPressedCommand;

        public ICommand RentalButtonPressedCommand => _rentalButtonPressedCommand ??
                                                      (_rentalButtonPressedCommand =
                                                          new DelegateCommand<string>(RentalButtonExecute));
        #endregion

        #region Executes & CanExecutes
        private void NotificationExecute()
        {
            //Probably needs to send the specific message with it. 
            if(_application.CurrentPage != ApplicationPage.MessageView)
                _application.GoToPage(ApplicationPage.MessageView);
            _eventAggregator.GetEvent<ClickOnNotificationEvent>().Publish(NotificationMessage);
        }

        private void RentalButtonExecute(string arg)
        {
            //Since we are here the message must be a renter message
            var renterMessage = _message as MessageFromRenterModel;
            bool isConfirmed = arg.ToLower() == "confirm";
            ConfirmRental(renterMessage, isConfirmed);
        }

        #endregion

        private void ConfirmRental(MessageFromRenterModel renterMessage, bool rentalConfirmed)
        {
            if(renterMessage.ConfirmationStatus != MsgStatus.Unhandled)
                return;

            DeclineButton ^= rentalConfirmed;
            ConfirmButton = rentalConfirmed;
            NotificationMessage.Confirmation = rentalConfirmed ? MsgStatus.Confirmed : MsgStatus.Declined;
            renterMessage.ConfirmationStatus = rentalConfirmed ? MsgStatus.Confirmed : MsgStatus.Declined;
            _eventAggregator.GetEvent<NotificationConfirmationEvent>().Publish(renterMessage);
        }
    }
}
