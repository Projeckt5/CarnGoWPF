using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace CarnGo
{
    class CarProfileViewModel : BaseViewModel
    {
        private CarProfileModel _carProfileModel;

        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string CarSeats { get; set; }
        public string CarFuelType { get; set; }
        public string CarRentalPrice { get; set; }





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

        private void SaveFunction()
        {
            //_carProfileModel.
        }

        private void UploadPhotoFunction()
        {

        }





    }
}
