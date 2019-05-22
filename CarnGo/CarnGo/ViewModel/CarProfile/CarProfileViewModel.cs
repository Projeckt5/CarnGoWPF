using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CarnGo.Database;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;

namespace CarnGo
{
    public class CarProfileViewModel : BaseViewModel
    {
        public class GetCarEvent : PubSubEvent {}
 
        private CarProfileModel _carProfile;
        private IApplication _application;
        private readonly IQueryDatabase _queryDatabase;
        private readonly IEventAggregator _eventAggregator;
        private bool _editing = false;
        public bool _isReadOnly = true;
        private bool isSaving = false;
        private bool isNew = false;
        
        
        public CarProfileViewModel(IApplication application, IQueryDatabase queryDatabase)
        {
            _queryDatabase = queryDatabase;
            _application = application;
            _eventAggregator.GetEvent<GetCarEvent>().Subscribe(GetCarModel);
        }

        public CarProfileViewModel(IApplication application)
        {
            _application = application;
            _carProfile = new CarProfileModel();
            Editing = true;
            IsReadOnly = false;
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

        private ICommand _uploadPhotoCommand;
        private ICommand _editCarProfile;


        public ICommand SaveCommand => new DelegateCommand(async () => await SaveFunction());

        public ICommand UploadPhotoCommand => _uploadPhotoCommand ?? (_uploadPhotoCommand = new DelegateCommand(UploadPhotoFunction));

        public ICommand EditCarProfileCommand => _editCarProfile ?? (_editCarProfile = new DelegateCommand(EditCarProfileFunction));

        #endregion



        public async Task SaveFunction()
        {
            Editing = false;
            IsReadOnly = true;

            if (isSaving)
            {
                return;
            }
                
            isSaving = true;
            try
            {
                if (isNew)
                {
                    _carProfile.Owner = _application.CurrentUser;
                    _application.CurrentUser.UserType = UserType.Lessor;

                    await _queryDatabase.UpdateUser(_application.CurrentUser);
                    await _queryDatabase.RegisterCarProfileTask(_carProfile);
                   
                }
                else
                {
                    await _queryDatabase.UpdateCarProfileTask(_carProfile);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                isSaving = false;
                isNew = false;
            }
        }

        public void EditCarProfileFunction()
        {
            Editing = true;
            IsReadOnly = false;
        }

        private void UploadPhotoFunction()
        {
            var fileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (fileDialog.ShowDialog() == true)
            {
                CarPicture = new BitmapImage(new Uri(fileDialog.FileName));
            }
        }

        private async void GetCarModel()
        {
            var profiles = await _queryDatabase.GetCarProfilesTask(_application.CurrentUser);
            _carProfile = profiles == null ? new CarProfileModel() : profiles.First();
            if (_carProfile.RegNr == null)
            {
                isNew = true;
                Editing = true;
                IsReadOnly = false;
            }
        }
    }
}
