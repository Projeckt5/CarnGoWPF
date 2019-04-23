using Prism.Events;

namespace CarnGo
{
    public class HeaderBarDesignModel : HeaderBarViewModel
    {
        public static HeaderBarDesignModel Instance => new HeaderBarDesignModel(IoCContainer.Resolve<IEventAggregator>());

        public HeaderBarDesignModel(IEventAggregator eventAggregator)
            :base(eventAggregator)
        {
            NumUnreadNotifications = 10;
        }
    }
}