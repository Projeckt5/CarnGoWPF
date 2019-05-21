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
            return new MessageFromLessorModel(messageToRespondTo.Renter, messageToRespondTo.Lessor,
                messageToRespondTo.RentCar, $"I {messageToRespondTo.ConfirmationStatus.ToString()} your request", messageToRespondTo.ConfirmationStatus)
            {
                Sender = messageToRespondTo.Lessor,
                Receiver = messageToRespondTo.Renter,
                MsgType = MessageType.LessorMessage
            };
        }

        #endregion

        #region Properties

        private List<MessageModel> _notificationModels;
        private List<NotificationItemViewModel> _messages;
        
        public List<NotificationItemViewModel> Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }
        #endregion


        private async Task UpdateNotificationConfirm(MessageFromRenterModel messageFromRenterModel)
        {
            int indexToReplace = _notificationModels.FindIndex(ind => ind.Equals(messageFromRenterModel));
            _notificationModels[indexToReplace] = messageFromRenterModel;
            await _databaseQuery.UpdateUserMessagesTask(_notificationModels);
        }

        private void UpdateNotifications(List<MessageModel> msgList)
        {
            var notificationItemViewModels = new List<NotificationItemViewModel>();
            _notificationModels = msgList;

            foreach (var currentUserMessageModel in msgList)
            {
                notificationItemViewModels.Add(new NotificationItemViewModel(_application, _eventAggregator, currentUserMessageModel));
            }

            Messages = notificationItemViewModels;
        }
    }
}
