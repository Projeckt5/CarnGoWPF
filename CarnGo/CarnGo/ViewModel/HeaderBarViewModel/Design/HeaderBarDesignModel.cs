namespace CarnGo
{
    public class HeaderBarDesignModel : HeaderBarViewModel
    {
        public static HeaderBarDesignModel Instance => new HeaderBarDesignModel();

        public HeaderBarDesignModel()
        {
            NumUnreadNotifications = 10;
        }
    }
}