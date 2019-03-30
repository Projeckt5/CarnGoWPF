using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace CarnGo
{
    /// <summary>
    /// Event aggregator singleton using lazy loading for thread safety
    /// TODO: Move this into an IoC-container when created
    /// </summary>
    public class EventAggregatorSingleton
    {
        private static readonly Lazy<EventAggregatorSingleton> _lazyInstance =
            new Lazy<EventAggregatorSingleton>(() => new EventAggregatorSingleton());

        private EventAggregatorSingleton() { }


        public static EventAggregatorSingleton Instance => _lazyInstance.Value;


        public static IEventAggregator EventAggregatorObj { get; set; }=new EventAggregator();

    }
}
