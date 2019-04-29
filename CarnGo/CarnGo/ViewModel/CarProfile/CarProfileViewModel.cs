using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Prism.Commands;

namespace CarnGo
{
    public class CarProfileViewModel : BaseViewModel
    {

 
        public CarProfileModel _carProfileModel;

        private bool _editing;
        public bool _isReadOnly;

        public bool IsOwner { get; set; }
        
        
        //TODO: Not sure if i should validate who the owner is og get it passed inn
        public CarProfileViewModel(IApplication application, CarProfileModel carProfile)
        {
            _carProfileModel = carProfile;
            Editing = false;
            IsReadOnly = true;
        }

        


        #region Public Properties

        public string CarMake
        {
            get => _carProfileModel.Brand;
            set => _carProfileModel.Brand = value;
        }
        public string CarModel
        {
            get => _carProfileModel.Model;
            set => _carProfileModel.Model = value;
        }

        public int CarSeats
        {
            get => _carProfileModel.Seats;
            set => _carProfileModel.Seats = value;
        }
        public string CarFuelType
        {
            get => _carProfileModel.FuelType;
            set => _carProfileModel.FuelType = value;
        }

        public int CarRentalPrice
        {
            get => _carProfileModel.RentalPrice;
            set => _carProfileModel.RentalPrice = value;
        }

        public int CarAge
        {
            get => _carProfileModel.Age;
            set => _carProfileModel.Age = value;
        }

        public string CarRegNr
        {
            get => _carProfileModel.RegNr;
            set => _carProfileModel.RegNr = value;
        }
        public DateTime CarStartLeaseDate
        {
            get => _carProfileModel.StartLeaseTime;
            set => _carProfileModel.StartLeaseTime = value;
        }
        public DateTime CarEndLeaseDate
        {
            get => _carProfileModel.EndLeaseTime;
            set => _carProfileModel.EndLeaseTime = value;
        }
        public BitmapImage CarPicture
        {
            get => _carProfileModel.CarPicture;
            set => _carProfileModel.CarPicture = value;
        }
        public UserModel CarOwner
        {
            get => _carProfileModel.Owner;
            set => _carProfileModel.Owner = value;
        }
        public string CarDescription
        {
            get => _carProfileModel.CarDescription;
            set => _carProfileModel.CarDescription = value;
        }

        public bool Editing
        {
            get => _editing;
            set
            {
                _editing = value;
                OnPropertyChanged(nameof(Editing));
            }
        }

        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                _isReadOnly = value;
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        #endregion

        #region Public Commands
        private ICommand _saveCommand;
        private ICommand _uploadPhotoCommand;
        private ICommand _editCarProfile;


        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveFunction));

        public ICommand UpLoadPhotoCommand => _uploadPhotoCommand ?? (_uploadPhotoCommand = new DelegateCommand(UploadPhotoFunction));

        public ICommand EditCarProfileCommand => _editCarProfile ?? (_editCarProfile = new DelegateCommand(EditCarProfileFunction));

        #endregion



        public void SaveFunction()
        {

            Editing = false;
            IsReadOnly = true;
        }

        public void EditCarProfileFunction()
        {
            Editing = true;
            IsReadOnly = false;
        }

        private void UploadPhotoFunction()
        {
            
        }
    }
}
