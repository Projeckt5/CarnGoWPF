using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CarnGo.Database;
using Prism.Commands;

namespace CarnGo
{
    public class ShowCarsListItemViewModel : BaseViewModel
    {


        private CarProfileModel _carProfile;
        private bool _editing;
        private IApplication _application;
        public bool _isReadOnly;


        public ShowCarsListItemViewModel(IApplication application, CarProfileModel carProfile)
        {
            _application = application;
            _carProfile = carProfile;
            _carProfile.Model = "Test";
            _carProfile.Brand = "Tester";
            _carProfile.StartLeaseTime = DateTime.Today;
            _carProfile.EndLeaseTime = DateTime.Today;
        }


        #region Public Properties

        public string CarMake
        {
            get => _carProfile.Brand;
            set => _carProfile.Brand = value;
        }
        public string CarModel
        {
            get => _carProfile.Model;
            set => _carProfile.Model = value;
        }

        public int CarSeats
        {
            get => _carProfile.Seats;
            set => _carProfile.Seats = value;
        }
        public string CarFuelType
        {
            get => _carProfile.FuelType;
            set => _carProfile.FuelType = value;
        }

        public int CarRentalPrice
        {
            get => _carProfile.RentalPrice;
            set => _carProfile.RentalPrice = value;
        }

        public int CarAge
        {
            get => _carProfile.Age;
            set => _carProfile.Age = value;
        }

        public string CarRegNr
        {
            get => _carProfile.RegNr;
            set => _carProfile.RegNr = value;
        }
        public DateTime CarStartLeaseDate
        {
            get => _carProfile.StartLeaseTime;
            set => _carProfile.StartLeaseTime = value;
        }
        public DateTime CarEndLeaseDate
        {
            get => _carProfile.EndLeaseTime;
            set => _carProfile.EndLeaseTime = value;
        }
        public BitmapImage CarPicture
        {
            get => _carProfile.CarPicture;
            set
            {
                _carProfile.CarPicture = value;
                OnPropertyChanged(nameof(CarPicture));
            }
        }
        public UserModel CarOwner
        {
            get => _carProfile.Owner;
            set => _carProfile.Owner = value;
        }
        public string CarDescription
        {
            get => _carProfile.CarDescription;
            set => _carProfile.CarDescription = value;
        }


        #endregion

        #region Public Commands



        #endregion



       

    }
}
