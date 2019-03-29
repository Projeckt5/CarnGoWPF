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

        private CarProfileModel _carProfileModel;

        #region Public Properties

        public string CarMake
        {
            get { return _carProfileModel.Brand;}
            set { _carProfileModel.Brand = value; }
        }
        public string CarModel
        {
            get { return _carProfileModel.Model;}
            set { _carProfileModel.Model = value; }
        }

        public int CarSeats
        {
            get { return _carProfileModel.Seats; }
            set { _carProfileModel.Seats = value; }
        }
        public string CarFuelType
        {
            get { return _carProfileModel.FuelType; }
            set { _carProfileModel.FuelType = value; }
        }

        public int CarRentalPrice
        {
            get { return _carProfileModel.RentalPrice; }
            set { _carProfileModel.RentalPrice = value; }
        }

        public int CarAge
        {
            get { return _carProfileModel.Age; }
            set { _carProfileModel.Age = value; }
        }

        public string CarRegNr
        {
            get { return _carProfileModel.Regnr; }
            set { _carProfileModel.Regnr = value; }
        }
        public DateTime CarStartLeaseDate
        {
            get { return _carProfileModel.StartLeaseTime; }
            set { _carProfileModel.StartLeaseTime = value; }
        }
        public DateTime CarEndLeaseTime
        {
            get { return _carProfileModel.EndLeaseTime; }
            set { _carProfileModel.EndLeaseTime = value; }
        }
        public Bitmap CarPicture
        {
            get { return _carProfileModel.CarPicture; }
            set { _carProfileModel.CarPicture = value; }
        }
        public UserModel CarOwner
        {
            get { return _carProfileModel.Owner; }
            set { _carProfileModel.Owner = value; }
        }

        #endregion

        #region Public Commands
        private ICommand _saveCommand;
        private ICommand _uploadPhotoCommand;


        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new DelegateCommand(SaveFunction)); }
        }

        public ICommand UpLoadPhotoCommand
        {
            get { return _uploadPhotoCommand ?? (_uploadPhotoCommand = new DelegateCommand(UploadPhotoFunction)); }
        }

        #endregion



        private void SaveFunction()
        {
            //_carProfileModel.
        }

        private void UploadPhotoFunction()
        {

        }
    }
}
