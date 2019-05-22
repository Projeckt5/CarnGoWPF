using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Prism.Events;
using Timer = System.Timers.Timer;

namespace CarnGo.Model.ThreadTimer
{
    
    public class DatabasePollingLoop : PubSubEvent { }
    public class ThreadTimer
    {
        private IEventAggregator _eventAggregator;
        private Timer _timer;
        private BackgroundWorker _worker;
        
        public ThreadTimer(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _worker=new BackgroundWorker();
            _worker.DoWork += NotificationQueryThread;
            _eventAggregator.GetEvent<StartThreadTimer>().Subscribe(StartTimer);
        }

        public void StartTimer(bool start)
        {
            if (start)
            {
                // _timer = new Timer(new TimerCallback(NotificationQueryThread), "Notification Query Thread", 2000, 2000);
                _timer = new Timer(2000);
                _timer.Elapsed += timer_Elapsed;
                _timer.Start();
            }
            else
                _timer.Dispose();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_worker.IsBusy)
                _worker.RunWorkerAsync();
        }
        public void NotificationQueryThread(object sender, DoWorkEventArgs e)
        {
            _eventAggregator.GetEvent<DatabasePollingLoop>().Publish();
        }

        ~ThreadTimer()
        {
            _timer?.Dispose();
        }
    }
}
