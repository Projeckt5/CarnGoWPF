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
    public class NotificationItemViewModel : BaseViewModel
    {
        private readonly IApplication _application;
        private readonly IEventAggregator _eventAggregator; 
        
        #region Constructors
        public NotificationItemViewModel(IApplication application, IEventAggregator eventAggregator, MessageModel message)
        {
            _application = application;
            _eventAggregator = eventAggregator;
            //Activate both buttons
            ConfirmButton = true;
            DeclineButton = true;
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
            if (arg.ToLower() == "confirm")
            {
                DeclineButton = false;
                NotificationMessage.Confirmation = true;
            }
            else
            {
                ConfirmButton = false;
                NotificationMessage.Confirmation = false;
            }
        }
        #endregion
    }

   
}
