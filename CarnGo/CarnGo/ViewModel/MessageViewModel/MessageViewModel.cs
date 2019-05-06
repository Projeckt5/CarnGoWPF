using System;
using System.Collections.Generic;
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
        }

        private NotificationModel _currentMessage = null;
        private readonly IEventAggregator _eventAggregator;
        private readonly IApplication _application;


        public NotificationModel CurrentMessage
        {
            get { return _currentMessage;  }
            set
            {
                _currentMessage = value; 
                OnPropertyChanged(nameof(CurrentMessage));
            }
        }

        private void ClickOnNotificationEventHandler(NotificationModel message)
        {
            CurrentMessage = message; 
        }
    }
}
