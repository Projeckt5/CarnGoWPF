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
        private bool _messageRead;
        private UserModel _sender;

        public int Id { get; set; }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public bool MessageRead
        {
            get => _messageRead;
            set
            {
                if(_messageRead == value)
                    return;
                _messageRead = value;
                OnPropertyChanged(nameof(MessageRead));
            }
        }

        public MessageType MsgType { get; set; }

        public UserModel Sender
        {
            get => _sender;
            set
            {
                if (_sender == value)
                    return;
                _sender = value;
                OnPropertyChanged(nameof(Sender));
            }
        }
        #endregion

    }
}
