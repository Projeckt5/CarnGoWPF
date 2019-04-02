using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace CarnGo
{
    public class CarProfileViewModel : BaseViewModel
    {
 
        private CarProfileModel _originalCarProfileModel;
        private CarProfileModel _editedCarProfileModel;

        public bool Editing = false;
        public bool _isOwner = true;
        
        //TODO: Not sure if i should validate who the owner is og get it passed inn
        public CarProfileViewModel()
        {
            _editedCarProfileModel = new CarProfileModel(new UserModel("Edward", "Brunton", "edward.brunton@me.com", "Bernhard Jensens Boulevard 95, 10.3", UserType.OrdinaryUser),"R8", "Audi", 2, "1337" );
        }

        


        #region Public Properties

        public string CarMake
        {
            get => _editedCarProfileModel.Brand;
            set
            {
                _editedCarProfileModel.Brand = value;
                OnPropertyChanged(nameof(CarMake));
            }
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
        public Bitmap CarPicture
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

        #endregion

        #region Public Commands
        private ICommand _saveCommand;
        private ICommand _uploadPhotoCommand;
        private ICommand _editCarProfile;


        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveFunction));

        public ICommand UpLoadPhotoCommand => _uploadPhotoCommand ?? (_uploadPhotoCommand = new DelegateCommand(UploadPhotoFunction));

        public ICommand EditCarProfileCommand => _editCarProfile ?? (_editCarProfile = new DelegateCommand(UploadPhotoFunction));

        #endregion



        private void SaveFunction()
        {
            _originalCarProfileModel = _editedCarProfileModel;
            Editing = false;
        }

        private void EditCarProfileFunction()
        {
            Editing = true;
        }

        private void UploadPhotoFunction()
        {

        }
    }
}
