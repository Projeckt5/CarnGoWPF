using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CarnGo.Database;
using Prism.Commands;
using Prism.Events;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace CarnGo
{
    public class CarLeaseViewModel : BaseViewModel
    {
        public class GetCarEvent : PubSubEvent {}
 
        private CarProfileModel _carProfile = new CarProfileModel();
        private ObservableCollection<CarProfileModel> _carProfileList = new ObservableCollection<CarProfileModel>();
        
        private IApplication _application;
        private readonly IQueryDatabase _queryDatabase;
        private readonly IEventAggregator _eventAggregator;
        private bool _editing = false;
        public bool isReadOnly = true;
        private bool _isSaving = false;
        private bool _isNew = false;
        
        
        public CarLeaseViewModel(IApplication application, IQueryDatabase queryDatabase, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _queryDatabase = queryDatabase;
            _application = application;
            _eventAggregator.GetEvent<GetCarEvent>().Subscribe(GetCarModel);
            randomList = new ObservableCollection<string>();
        }





        #region Public Properties
        public ObservableCollection<string> randomList { get; set; }

        public ObservableCollection<CarProfileModel> CarProfileList
        {
            get => _carProfileList;
            set => _carProfileList = value;
        }

        public CarProfileModel SelectedCarProfile
        {
            get => _carProfile;
            set
            {
                _carProfile = value;
                UpdateUi();
            }
        }

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

        public string CarSeats
        {
            get => _carProfile.Seats.ToString();
            set => _carProfile.Seats = int.Parse(value);
        }
        public string CarFuelType
        {
            get => _carProfile.FuelType;
            set => _carProfile.FuelType = value;
        }

        public string CarRentalPrice
        {
            get => _carProfile.RentalPrice.ToString();
            set => _carProfile.RentalPrice = int.Parse(value);
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
            get => isReadOnly;
            set
            {
                isReadOnly = value;
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        #endregion

        #region Public Commands

        private ICommand _uploadPhotoCommand;
        private ICommand _editCarProfile;


        public ICommand SaveCommand => new DelegateCommand(async () => await SaveFunction());

        public ICommand DeleteCommand => new DelegateCommand(async () => await DeleteFunction());

        public ICommand UploadPhotoCommand => _uploadPhotoCommand ?? (_uploadPhotoCommand = new DelegateCommand(UploadPhotoFunction));

        public ICommand EditCarProfileCommand => _editCarProfile ?? (_editCarProfile = new DelegateCommand(EditCarProfileFunction));

        public ICommand NewCarCommand => new DelegateCommand(()=> NewCarFunction());

        #endregion



        public async Task SaveFunction()
        {
            Editing = false;
            IsReadOnly = true;

            if (_isSaving)
            {
                return;
            }
                
            _isSaving = true;
            try
            {
                if (_isNew)
                {
                    _carProfile.Owner = _application.CurrentUser;
                    await _queryDatabase.RegisterCarProfileTask(_carProfile);
                    _carProfileList.Add(_carProfile);
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
                _isSaving = false;
                _isNew = false;
                UpdateUi();
            }
        }

        public async Task DeleteFunction()
        {
            Editing = false;
            IsReadOnly = true;

            try
            {
                await _queryDatabase.DeleteCarProfileTask(_carProfile);

                CarProfileList.Remove(_carProfile);

               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                
                if (CarProfileList.Count != 0)
                {
                    _carProfile = CarProfileList.First();
                    UpdateUi();
                }
                else
                {
                    _application.GoToPage(ApplicationPage.StartPage);
                }
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
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Filter = "jpg images | *.jpg"
            };

            if (fileDialog.ShowDialog() == true)
            {
                CarPicture = new BitmapImage(new Uri(fileDialog.FileName));
            }
        }

        private void NewCarFunction()
        {
            _carProfile = new CarProfileModel();
            EditCarProfileFunction();
            _isNew = true;
            UpdateUi();
        }

        private async void GetCarModel()
        {
            try
            {
                CarProfileList = new ObservableCollection<CarProfileModel>(await _queryDatabase.GetCarProfilesTask(_application.CurrentUser));
                if (CarProfileList.Count == 0)
                {
                    _carProfile = new CarProfileModel();
                }
                else
                {
                    _carProfile = CarProfileList.First();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (_carProfile.RegNr == null)
            {
                _isNew = true;
                Editing = true;
                IsReadOnly = false;
            }

            UpdateUi();
        }


        private void UpdateUi()
        {
            OnPropertyChanged(nameof(CarRegNr));
            OnPropertyChanged(nameof(CarAge));
            OnPropertyChanged(nameof(CarDescription));
            OnPropertyChanged(nameof(CarEndLeaseDate));
            OnPropertyChanged(nameof(CarRegNr));
            OnPropertyChanged(nameof(CarFuelType));
            OnPropertyChanged(nameof(CarMake));
            OnPropertyChanged(nameof(CarModel));
            OnPropertyChanged(nameof(CarSeats));
            OnPropertyChanged(nameof(CarStartLeaseDate));
            OnPropertyChanged(nameof(CarRentalPrice));
            OnPropertyChanged(nameof(CarPicture));
            OnPropertyChanged(nameof(CarProfileList));
        }
    }

    
}
