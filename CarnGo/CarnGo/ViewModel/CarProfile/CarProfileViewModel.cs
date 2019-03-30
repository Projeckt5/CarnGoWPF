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

        private bool _editing;
        private bool _isOwner;
        
        //Not sure if i should validate who the owner is og get it passed inn
        public CarProfileViewModel(CarProfileModel _originalCarProfileModel, bool isOwner)
        {
            
        }

        


        #region Public Properties

        public string CarMake
        {
            get { return _editedCarProfileModel.Brand;}
            set { _editedCarProfileModel.Brand = value; }
        }
        public string CarModel
        {
            get { return _editedCarProfileModel.Model;}
            set { _editedCarProfileModel.Model = value; }
        }

        public int CarSeats
        {
            get { return _editedCarProfileModel.Seats; }
            set { _editedCarProfileModel.Seats = value; }
        }
        public string CarFuelType
        {
            get { return _editedCarProfileModel.FuelType; }
            set { _editedCarProfileModel.FuelType = value; }
        }

        public int CarRentalPrice
        {
            get { return _editedCarProfileModel.RentalPrice; }
            set { _editedCarProfileModel.RentalPrice = value; }
        }

        public int CarAge
        {
            get { return _editedCarProfileModel.Age; }
            set { _editedCarProfileModel.Age = value; }
        }

        public string CarRegNr
        {
            get { return _editedCarProfileModel.Regnr; }
            set { _editedCarProfileModel.Regnr = value; }
        }
        public DateTime CarStartLeaseDate
        {
            get { return _editedCarProfileModel.StartLeaseTime; }
            set { _editedCarProfileModel.StartLeaseTime = value; }
        }
        public DateTime CarEndLeaseTime
        {
            get { return _editedCarProfileModel.EndLeaseTime; }
            set { _editedCarProfileModel.EndLeaseTime = value; }
        }
        public Bitmap CarPicture
        {
            get { return _editedCarProfileModel.CarPicture; }
            set { _editedCarProfileModel.CarPicture = value; }
        }
        public UserModel CarOwner
        {
            get { return _editedCarProfileModel.Owner; }
            set { _editedCarProfileModel.Owner = value; }
        }
        public string CarDescription
        {
            get { return _editedCarProfileModel.CarDescription; }
            set { _editedCarProfileModel.CarDescription = value; }
        }

        #endregion

        #region Public Commands
        private ICommand _saveCommand;
        private ICommand _uploadPhotoCommand;
        private ICommand _editCarProfile;


        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new DelegateCommand(SaveFunction)); }
        }

        public ICommand UpLoadPhotoCommand
        {
            get { return _uploadPhotoCommand ?? (_uploadPhotoCommand = new DelegateCommand(UploadPhotoFunction)); }
        }

        public ICommand EditCarProfileCommand
        {
            get { return _editCarProfile ?? (_editCarProfile = new DelegateCommand(UploadPhotoFunction)); }
        }

        #endregion



        private void SaveFunction()
        {
            _originalCarProfileModel = _editedCarProfileModel;
            _editing = false;
        }

        private void EditCarProfileFunction()
        {
            _editing = true;
        }

        private void UploadPhotoFunction()
        {

        }
    }
}
