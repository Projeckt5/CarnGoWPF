using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public CarProfileModel(UserModel owner,string name, string brand, int age, string regNr)
        {
            Name = name;
            Brand = brand;
            Age = age;
            Regnr = regNr;
            Owner = owner;
        }
        #endregion

        #region Fields
        private string _name;
        private string _brand;
        private int _age;
        private string _regNr;
        private string _location;
        private int _seats;
        private int _price;
        private DateTime _startLeaseTime;
        private DateTime _endLeaseTime;
        
        #endregion

        //TODO Try/Catch block for Database exceptions
        #region Properties
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Brand
        {
            get { return _brand; }
            set
            {
                _brand = value;
                OnPropertyChanged(nameof(Brand));
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                _age = value > 1900 && value <= DateTime.Now.Year ? value : throw new ArgumentException("Invalid Year");
                OnPropertyChanged(nameof(Age));
            }
        }

        public string Regnr
        {
            get { return _regNr; }
            set
            {
                _regNr = value.Length == 7 ? value : throw new ArgumentException("Registration number must contain 7 digits");
                OnPropertyChanged(nameof(_regNr));
            }
        }

        public string Location
        {
            get { return _location; }
            set
            {
                _location = value.Length > 1 ? value : throw new ArgumentException("Location must contain more than 1 characters");
                OnPropertyChanged(nameof(_location));
            }
        }

        public int Seats
        {
            get { return _seats; }
            set
            {
                _seats = (value > 0 && value < 20) ? value : throw new ArgumentException("Car must have between 0 and 20 seats");
            }
        }

        public int Price
        {
            get { return _price; }
            set
            {
                _price = value > 0 ? value : throw new ArgumentException("Price must be higher than 0");
            }
        }

        public DateTime StartLeaseTime
        {
            get { return _startLeaseTime; }
            set
            {
                //TODO Make try/catch block to check if Database sendes exception
                _startLeaseTime = value;
                OnPropertyChanged(nameof(StartLeaseTime));
            }

        }

        public DateTime EndLeaseTime
        {
            get { return _endLeaseTime;  }
            set
            {
                //TODO Make try/catch block to check if Database sendes exception
                _endLeaseTime = value;
                OnPropertyChanged(nameof(EndLeaseTime));
            }
        }

        public UserModel Owner { get; set; }
        #endregion





    }
}
