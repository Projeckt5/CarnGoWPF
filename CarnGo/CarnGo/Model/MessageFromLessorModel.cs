using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnGo.Model
{
    public class MessageFromLessorModel : BaseViewModel
    {
        #region Constructor 
        public MessageFromLessorModel(UserModel renter, UserModel lessor, CarProfileModel rentCar)
        {
            Renter = renter;
            Lessor = lessor;
            RentCar = rentCar; 
        }
        #endregion

        #region Fields
        private UserModel _renter;
        private UserModel _lessor;
        private CarProfileModel _rentCar; 
        private string _message;
        private bool _statusConfirmation;
        #endregion

        #region Properties
        public UserModel Renter
        {
            get { return _renter; }
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

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public bool StatusConfirmation
        {
            get { return _statusConfirmation; }
            set
            {
                _statusConfirmation = value;
                OnPropertyChanged(nameof(StatusConfirmation));
            }
        }
        #endregion
    }
}
