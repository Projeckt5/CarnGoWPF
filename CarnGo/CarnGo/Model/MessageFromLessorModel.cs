using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnGo
{
    public class MessageFromLessorModel : MessageModel
    {
        #region Constructor 
        public MessageFromLessorModel(UserModel renter, UserModel lessor, CarProfileModel rentCar, string message, MsgStatus confirmationStatus)
        {
            Renter = renter;
            Lessor = lessor;
            RentCar = rentCar;
            Message = message;
            ConfirmationStatus = confirmationStatus;
        }
        #endregion

        #region Fields
        private UserModel _renter;
        private UserModel _lessor;
        private CarProfileModel _rentCar; 
        #endregion

        #region Properties
        public UserModel Renter
        {
            get => _renter;
            set
            {
                _renter = value;
                OnPropertyChanged(nameof(Renter));
            }
        }

        public UserModel Lessor
        {
            get { return _lessor;}
            set
            {
                _lessor = value;
                OnPropertyChanged(nameof(Lessor));
            }
        }

        public CarProfileModel RentCar
        {
            get { return _rentCar; }
            set
            {
                _rentCar = value;
                OnPropertyChanged(nameof(RentCar));
            }
        }

        #endregion
    }
}
