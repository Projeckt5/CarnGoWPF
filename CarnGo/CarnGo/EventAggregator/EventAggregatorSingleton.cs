using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace CarnGo
{
    public class EventAggregatorSingleton
    {
        private EventAggregatorSingleton() { }
        public static EventAggregatorSingleton GetEventAggregator { get; }=new EventAggregatorSingleton();
       
        public static IEventAggregator EventAggregatorObj { get; set; }=new EventAggregator();

    }
}
