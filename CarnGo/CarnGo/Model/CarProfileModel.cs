using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using CarnGo.Database.Models;


namespace CarnGo
{
    public class CarProfileModel : BaseViewModel
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CarProfileModel()
        {}

        /// <summary>
        /// Explicit Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="brand"></param>
        /// <param name="age"></param>
        /// <param name="regNr"></param>

       

        public CarProfileModel(UserModel owner, string model, string brand, int age, string regNr, 
            string location, int seats, DateTime startlease, DateTime endlease, int price)
        {
            Model = model;
            Brand = brand;
            Age = age;
            RegNr = regNr;
            Owner = owner;
            Location = location;
            Seats = seats;
            Price = price;
            StartLeaseTime = startlease;
            EndLeaseTime = endlease;
            DayThatIsRented = new List<DayThatIsRentedModel>();
            PossibleToRentDays = new List<PossibleToRentDayModel>();
        }

        #endregion

        #region Fields

        
        private string _model;
        private string _brand;
        private int _age;
        private string _regNr;
        private string _location;
        private int _seats;
        private int _price;
        private DateTime _startLeaseTime;
        private DateTime _endLeaseTime;

        private CarEquipment _equipment;

        private BitmapImage _carPicture;
        private UserModel _owner;
        private int _rentalPrice;
        private string _fuelType;
        private string _carDescription;
        private List<DayThatIsRentedModel> _dayThatIsRented;
        private List<PossibleToRentDayModel> _possibleToRentDays;

        #endregion

        //TODO Try/Catch block for Database exceptions
        #region Properties

        public List<DayThatIsRentedModel> DayThatIsRented
        {
            get => _dayThatIsRented;
            set
            {
                if (_dayThatIsRented == value)
                    return;
                _dayThatIsRented = value;
                OnPropertyChanged(nameof(DayThatIsRented));
            }
        }
        public List<PossibleToRentDayModel> PossibleToRentDays
        {
            get => _possibleToRentDays;
            set
            {
                if (_possibleToRentDays == value)
                    return;
                _possibleToRentDays = value;
                OnPropertyChanged(nameof(PossibleToRentDays));
            }
        }
        

        public CarEquipment CarEquipment
        {
            get => _equipment;
            set
            {
                if (_equipment == value)
                    return;
                _equipment = value;
                OnPropertyChanged(nameof(CarEquipment));
            } }
        public BitmapImage CarPicture
        {
            get => _carPicture;
            set
            {
                _carPicture = value;
                OnPropertyChanged(nameof(CarPicture));
            }
        }


        public string Model
        {
            get => _model;
            set
            {
                if (_model == value)
                    return;
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        public string Brand
        {
            get => _brand;
            set
            {
                if (_brand == value)
                    return;
                _brand = value;
                OnPropertyChanged(nameof(Brand));
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                if (_age == value)
                    return;
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public string RegNr
        {
            get => _regNr;
            set
            {
                if (_regNr == value)
                    return;
                _regNr = value;
                OnPropertyChanged(nameof(RegNr));
            }
        }

        public string Location
        {
            get => _location;
            set
            {
                if (_location == value)
                    return;
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        public int Seats
        {
            get => _seats;
            set
            {
                if (_seats == value)
                    return;
                _seats = value;
                OnPropertyChanged(nameof(Seats));
            }
        }

        public int Price
        {
            get => _price;
            set
            {
                if (_price == value)
                    return;
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public DateTime StartLeaseTime
        {
            get => _startLeaseTime;
            set
            {
                if (_startLeaseTime == value)
                    return;
                //TODO Make try/catch block to check if Database sendes exception
                _startLeaseTime = value;
                OnPropertyChanged(nameof(StartLeaseTime));
            }

        }

        public DateTime EndLeaseTime
        {
            get => _endLeaseTime;
            set
            {
                if (_endLeaseTime == value)
                    return;
                //TODO Make try/catch block to check if Database sendes exception
                _endLeaseTime = value;
                OnPropertyChanged(nameof(EndLeaseTime));
            }
        }

        public UserModel Owner
        {
            get => _owner;
            set
            {
                if (_owner == value)
                    return;
                _owner = value;
                OnPropertyChanged(nameof(Owner));
            }
        }

        public int RentalPrice
        {
            get { return _rentalPrice; }
            set
            {
                if (_rentalPrice == value)
                    return;
                _rentalPrice = value;
                OnPropertyChanged(nameof(RentalPrice));
            }
        }

        public string FuelType
        {
            get { return _fuelType; }
            set
            {
                if (_fuelType == value)
                    return;
                _fuelType = value;
                OnPropertyChanged(nameof(FuelType));
            }
        }

        public string CarDescription
        {
            get { return _carDescription; }
            set
            {
                _carDescription = value;
                OnPropertyChanged(nameof(CarDescription));
            }
        }

        

        #endregion
    }
}
