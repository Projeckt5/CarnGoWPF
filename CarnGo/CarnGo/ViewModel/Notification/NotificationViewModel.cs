using System;
using Prism.Commands;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Prism.Events;

namespace CarnGo
{
    public class NotificationViewModel : BaseViewModel
    {
        private readonly IApplication _application;
        private readonly IEventAggregator _eventAggregator;
        private readonly IQueryDatabase _databaseQuery;

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public NotificationViewModel(IApplication application, IEventAggregator eventAggregator, IQueryDatabase databaseQuery)
        {
            _application = application;
            _eventAggregator = eventAggregator;
            _databaseQuery = databaseQuery;
            _eventAggregator.GetEvent<NotificationMessagesUpdateEvent>().Subscribe(UpdateNotifications);
            _eventAggregator.GetEvent<NotificationConfirmationEvent>().Subscribe(async messageFromRenter =>
            {
                    //if (messageFromRenter.ConfirmationStatus == MsgStatus.Declined)
                       // await _databaseQuery.EraseDaysThatIsRented(messageFromRenter);
                    await UpdateNotificationConfirm(messageFromRenter);
                    MessageFromLessorModel response = CreateResponseForRenter(messageFromRenter);
                    await SendResponseToLessor(response);
                });
        }

        private async Task SendResponseToLessor(MessageFromLessorModel response)
        {
            await _databaseQuery.AddUserMessage(response);
        }

        private MessageFromLessorModel CreateResponseForRenter(MessageFromRenterModel messageToRespondTo)
        {
            var contactInfo = messageToRespondTo.ConfirmationStatus == MsgStatus.Confirmed
                ? $"Please contact me on my email: {messageToRespondTo.Lessor.Email}" : "";
            var msg = $"I {messageToRespondTo.ConfirmationStatus.ToString()} your request." + contactInfo;
            return new MessageFromLessorModel(messageToRespondTo.Renter, messageToRespondTo.Lessor,
                messageToRespondTo.RentCar, msg , messageToRespondTo.ConfirmationStatus)
            {
                Sender = messageToRespondTo.Lessor,
                Receiver = messageToRespondTo.Renter,
                MsgType = MessageType.LessorMessage,
                TimeStamp = DateTime.Now,
            };
        }

        #endregion

        #region Properties

        private List<MessageModel> _messagesForNotifications;
        private List<NotificationItemViewModel> _notificationsItemVMs;
        
        public List<NotificationItemViewModel> NotificationItemVMs
        {
            get => _notificationsItemVMs;
            set
            {
                _notificationsItemVMs = value;
                OnPropertyChanged(nameof(NotificationItemVMs));
            }
        }
        #endregion


        private async Task UpdateNotificationConfirm(MessageFromRenterModel messageFromRenterModel)
        {
            int indexToReplace = _messagesForNotifications.FindIndex(ind => ind.Equals(messageFromRenterModel));
            _messagesForNotifications[indexToReplace] = messageFromRenterModel;
            await _databaseQuery.UpdateUserMessagesTask(_messagesForNotifications);
            _eventAggregator.GetEvent<NotificationMessagesUpdateEvent>().Publish(_messagesForNotifications);
        }

        private void UpdateNotifications(List<MessageModel> msgList)
        {
            var notificationItemViewModels = new List<NotificationItemViewModel>();
            _messagesForNotifications = msgList;

            foreach (var currentUserMessageModel in msgList)
            {
                notificationItemViewModels.Add(new NotificationItemViewModel(_application, _eventAggregator, currentUserMessageModel));
            }

            NotificationItemVMs = notificationItemViewModels;
        }
    }
}
