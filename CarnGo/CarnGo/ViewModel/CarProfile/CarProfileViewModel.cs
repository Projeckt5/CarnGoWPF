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

 
        private CarProfileModel _originalCarProfileModel;
        private CarProfileModel _editedCarProfileModel;
        private bool _editing;
        public bool _isReadOnly;

        public bool IsOwner { get; set; }
        
        
        //TODO: Not sure if i should validate who the owner is og get it passed inn
        public CarProfileViewModel()
        {
            _editedCarProfileModel = new CarProfileModel(new UserModel("Edward", "Brunton", "edward.brunton@me.com", "Bernhard Jensens Boulevard 95, 10.3", UserType.OrdinaryUser),"R8", "Audi", 2017, "1337133" );
            Editing = false;
            IsReadOnly = true;
        }

        


        #region Public Properties

        public string CarMake
        {
            get => _editedCarProfileModel.Brand;
            set => _editedCarProfileModel.Brand = value;
        }
        public string CarModel
        {
            get => _editedCarProfileModel.Model;
            set => _editedCarProfileModel.Model = value;
        }

        public int CarSeats
        {
            get => _editedCarProfileModel.Seats;
            set => _editedCarProfileModel.Seats = value;
        }
        public string CarFuelType
        {
            get => _editedCarProfileModel.FuelType;
            set => _editedCarProfileModel.FuelType = value;
        }

        public int CarRentalPrice
        {
            get => _editedCarProfileModel.RentalPrice;
            set => _editedCarProfileModel.RentalPrice = value;
        }

        public int CarAge
        {
            get => _editedCarProfileModel.Age;
            set => _editedCarProfileModel.Age = value;
        }

        public string CarRegNr
        {
            get => _editedCarProfileModel.Regnr;
            set => _editedCarProfileModel.Regnr = value;
        }
        public DateTime CarStartLeaseDate
        {
            get => _editedCarProfileModel.StartLeaseTime;
            set => _editedCarProfileModel.StartLeaseTime = value;
        }
        public DateTime CarEndLeaseDate
        {
            get => _editedCarProfileModel.EndLeaseTime;
            set => _editedCarProfileModel.EndLeaseTime = value;
        }
        public BitmapImage CarPicture
        {
            get => _editedCarProfileModel.CarPicture;
            set => _editedCarProfileModel.CarPicture = value;
        }
        public UserModel CarOwner
        {
            get => _editedCarProfileModel.Owner;
            set => _editedCarProfileModel.Owner = value;
        }
        public string CarDescription
        {
            get => _editedCarProfileModel.CarDescription;
            set => _editedCarProfileModel.CarDescription = value;
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



        private void SaveFunction()
        {
            _originalCarProfileModel = _editedCarProfileModel;
            Editing = false;
            IsReadOnly = true;
        }

        private void EditCarProfileFunction()
        {
            Editing = true;
            IsReadOnly = false;
        }

        private void UploadPhotoFunction()
        {

        }
    }
}
