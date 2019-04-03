using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CarnGo
{
    public class MessageModel : BaseViewModel
    {
        #region Properties
        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public MessageType MsgType { get; set; }
        #endregion

    }
}
