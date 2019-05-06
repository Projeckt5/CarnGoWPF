using System;
using Prism.Commands;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Prism.Events;

namespace CarnGo
{
    public class NotificationViewModel : BaseViewModel
    {
        private readonly IApplication _application;
        private readonly IEventAggregator _eventAggregator;

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public NotificationViewModel(IApplication application, IEventAggregator eventAggregator)
        {
            _application = application;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<NotificationMessageUpdateEvent>().Subscribe(msgList =>
            {

                var notificationItemViewModels = new List<NotificationItemViewModel>();

                foreach (var currentUserMessageModel in msgList)
                {
                    notificationItemViewModels.Add(new NotificationItemViewModel(_application, _eventAggregator, currentUserMessageModel));
                }

                notificationItemViewModels.Reverse();
                Messages = notificationItemViewModels;
            });
        }
        #endregion

        #region Properties
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
    }
}
