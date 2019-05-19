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
        private UserModel _receiver;
        private MsgStatus _confirmationStatus;

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
        public UserModel Receiver
        {
            get => _receiver;
            set
            {
                if (_receiver == value)
                    return;
                _receiver = value;
                OnPropertyChanged(nameof(Receiver));
            }
        }

        private DateTime _timeStamp;

        public DateTime TimeStamp
        {
            get { return _timeStamp; }
            set
            {
                if(_timeStamp == value)
                    return;
                _timeStamp = value;
                OnPropertyChanged(nameof(TimeStamp));
            }
        }

        public MsgStatus ConfirmationStatus
        {
            get { return _confirmationStatus; }
            set
            {
                _confirmationStatus = value;
                OnPropertyChanged(nameof(ConfirmationStatus));
            }
        }
        #endregion

    }
}
