using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnGo
{

    class NotificationItemDesignViewModel:NotificationItemViewModel
    {
        #region singleton
        public static NotificationItemDesignViewModel Instance=>new NotificationItemDesignViewModel();
#endregion

         public NotificationItemDesignViewModel()
         {
             Name = "Jens Jensen";
             Message = "Du kan hente bilen kl. 13";
             ImgUrl = "../../Images.Bilfoto.jpg";
             Confirmed = "Request Accepted";
         }
    }
}
