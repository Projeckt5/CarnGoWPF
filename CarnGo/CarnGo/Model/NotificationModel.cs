using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CarnGo
{
    public class NotificationModel
    {
        #region Properties
        public MessageType MsgType { get; set; }
        public string Message { get; set; }
        public BitmapImage CarPicture { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsRead { get; set; }
        public string Lessor { get; set; }
        public string Renter { get; set; }
        public MsgStatus Confirmation { get; set; }
        #endregion
    }
}
