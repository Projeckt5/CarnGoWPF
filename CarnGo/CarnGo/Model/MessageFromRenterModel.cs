using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarnGo
{
    public class MessageFromRenterModel : MessageModel
    {
        #region Fields
        private UserModel _renter;
        private UserModel _lessor;
        private CarProfileModel _rentCar;
        private DateTime _from;
        private DateTime _to;
        #endregion

        #region Constructor

        public MessageFromRenterModel(UserModel renter, UserModel lessor, CarProfileModel rentCar, string message)
        {
            MsgType = MessageType.RenterMessage;
            Renter = renter;
            Lessor = lessor;
            RentCar = rentCar;
            Message = message;
        }
        

        #endregion

        #region Properties

        public UserModel Renter
        {
            get=> _renter;
            set
            {
                if (_renter == value)
                    return;
                _renter = value;
                OnPropertyChanged(nameof(Renter));
            }
        }

        public UserModel Lessor
        {
            get => _lessor;
            set
            {
                if(_lessor == value)
                    return;
                _lessor = value;
                OnPropertyChanged(nameof(Lessor));
            }
        }

        public CarProfileModel RentCar
        {
            get => _rentCar;
            set
            {
                if(_rentCar == value)
                    return;
                _rentCar = value;
                OnPropertyChanged(nameof(RentCar));
            }

        }
        #endregion

    }
}
