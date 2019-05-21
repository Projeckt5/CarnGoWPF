using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Prism.Events;

namespace CarnGo.Model.ThreadTimer
{
    
    public class DatabasePollingLoop : PubSubEvent { }
    public class ThreadTimer
    {
        private IEventAggregator _eventAggregator;
        private Timer _timer;
        
        public ThreadTimer(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            
            _eventAggregator.GetEvent<StartThreadTimer>().Subscribe(StartTimer);
        }

        public void StartTimer(bool start)
        {
            if (start)
                _timer = new Timer(new TimerCallback(NotificationQueryThread), "Notification Query Thread", 2000, 2000);
            else
                _timer.Dispose();
        }

        public void NotificationQueryThread(Object o)
        {
            _eventAggregator.GetEvent<DatabasePollingLoop>().Publish();
        }

        ~ThreadTimer()
        {
            _timer.Dispose();
        }
    }
}
