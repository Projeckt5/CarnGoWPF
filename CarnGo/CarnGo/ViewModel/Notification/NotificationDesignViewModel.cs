using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnGo
{

    class NotificationDesignViewModel:NotificationViewModel
    {
        #region singleton
        public static NotificationDesignViewModel Instance=>new NotificationDesignViewModel();
#endregion

         public NotificationDesignViewModel()
         {
             Items = new List<NotificationItemViewModel>
             {
                 new NotificationItemViewModel
                 {
                     Name = "Jens Jensen",
                     Message = "Du kan hente bilen kl. 13",
                     ImgUrl = "../../Images.Bilfoto.jpg",
                     Confirmed = "Request Accepted",
                 },
                 new NotificationItemViewModel{
                     Name = "Jens Jensen",
                     Message = "Du kan hente bilen kl. 13",
                     ImgUrl = "../../Images.Bilfoto.jpg",
                     Confirmed = "Request Accepted",
                 },
                 new NotificationItemViewModel{
                     Name = "Jens Jensen",
                     Message = "Du kan hente bilen kl. 13",
                     ImgUrl = "../../Images.Bilfoto.jpg",
                     Confirmed = "Request Accepted",
                 },
                 new NotificationItemViewModel{
                     Name = "Jens Jensen",
                     Message = "Du kan hente bilen kl. 13",
                     ImgUrl = "../../Images.Bilfoto.jpg",
                     Confirmed = "Request Accepted",
                 },
                 new NotificationItemViewModel{
                     Name = "Jens Jensen",
                     Message = "Du kan hente bilen kl. 13",
                     ImgUrl = "../../Images.Bilfoto.jpg",
                     Confirmed = "Request Accepted",
                 },
             };
         }
    }
}
