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
        public UserModel Renter { get; set; }
        public UserModel Lessor { get; set; }
        public CarProfileModel RentCar { get; set; }


        public DateTime From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged(nameof(From));
            }
        }

        public DateTime To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged(nameof(To));
            }
        }
        #endregion

    }
}
