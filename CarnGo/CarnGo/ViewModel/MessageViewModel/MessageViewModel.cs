using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;

namespace CarnGo
{
    public class MessageViewModel : BaseViewModel
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MessageViewModel(IApplication application, IEventAggregator eventAggregator)
        {
            _application = application;
            _eventAggregator = eventAggregator;
            eventAggregator.GetEvent<ClickOnNotificationEvent>().Subscribe(ClickOnNotificationEventHandler);
            eventAggregator.GetEvent<NotificationMessageUpdateEvent>().Subscribe(MessageUpdateEventHandler);

            var messages = application.CurrentUser.MessageModels;
            var notificationItemViewModel = new List<NotificationItemViewModel>(); 
            
            foreach (var message in messages)
            {
                notificationItemViewModel.Add(new NotificationItemViewModel(_application, _eventAggregator, message));
            }

            notificationItemViewModel.Reverse();
            Messages = new ObservableCollection<NotificationItemViewModel>(notificationItemViewModel);
        }

        private NotificationModel _currentMessage = null;
        private readonly IEventAggregator _eventAggregator;
        private readonly IApplication _application;

        #region Properties
        private ObservableCollection<NotificationItemViewModel> _messages;

        //Observable collection 
        public ObservableCollection<NotificationItemViewModel> Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }
        
        public NotificationModel CurrentMessage
        {
            get { return _currentMessage;  }
            set
            {
                _currentMessage = value; 
                OnPropertyChanged(nameof(CurrentMessage));
            }
        }
        #endregion

        private void MessageUpdateEventHandler(List<MessageModel> messages)
        {
            Messages.Clear();
            var notificationItemViewModel = new List<NotificationItemViewModel>();
            foreach (var message in messages)
            {
                notificationItemViewModel.Add(new NotificationItemViewModel(_application, _eventAggregator, message));
            }
            notificationItemViewModel.Reverse();
            Messages = new ObservableCollection<NotificationItemViewModel>(notificationItemViewModel);
        }

        private void ClickOnNotificationEventHandler(NotificationModel message)
        {
            CurrentMessage = message; 
        }
    }
}
