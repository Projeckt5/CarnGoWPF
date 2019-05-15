using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using CarnGo.Database;
using CarnGo.Database.Models;

using Prism.Commands;
using Prism.Events;
using Unity;



namespace CarnGo
{
    public class SendRequestViewModel:BaseViewModel,IDataErrorInfo
    {
        public IApplication _application;
        public IQueueDatabaseForSendRequestViewModel _dbContext;
        public ISendRequestViewModelHelperFunction _helper;
        public IEventAggregator _events;
        

        #region fields

        private string _errorText = "";
       
        private CarProfileModel _carProfileModel;
        private DateTime _to = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        private DateTime _from= new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        private string _message = "Message to lessor";

        private CarProfileModel _car =
            new CarProfileModel
            {
                CarEquipment = new CarEquipment
                {
                    AudioPlayer = true,
                    ChildSeat = false,
                    Gps = true,
                    Smoking = false
                },
                Model = "Mustang",
                Brand = "Ford",
                Age = 2010,
                CarDescription = "Bilen har kun været brugt 5 gange i løbet af de 10 år jeg har eget den, så den er så god som ny.",
                CarPicture = new BitmapImage(new Uri("../../Images/Bilfoto.jpg", UriKind.Relative)),
                RegNr = "1107959",
                RentalPrice = 5000,
                FuelType = "Premium91",
                Seats = 5,
                Price = 200000,
                Location = "Århus",
                StartLeaseTime = new DateTime(2019, 4, 25),
                EndLeaseTime = new DateTime(2019, 5, 25)
            };
        #endregion

        #region constructor

        public SendRequestViewModel(IEventAggregator events, IApplication application,IQueueDatabaseForSendRequestViewModel dbcontext,ISendRequestViewModelHelperFunction helper)
        {
            _events = events;
            _helper = helper;
            _dbContext = dbcontext;
            _application = application;
            _events.GetEvent<CarProfileDataEvent>().Subscribe(async (reg) => await SearchCarProfileEvent(reg));
        }

        public async Task SearchCarProfileEvent(string regnr)
        {
            CarProfile = await _dbContext.GetCarProfileForSendRequestView(regnr);
            var converter = new ApptoDbModelConverter();
            var currentUserAsDbUser = converter.Convert(_application.CurrentUser);
            CarProfile.User = currentUserAsDbUser;
            CarProfile.UserEmail = currentUserAsDbUser.Email;
            Car = ModelToDatabaseConverters.CarProfileToCarProfileModelForSendRequestViewModel(CarProfile);
        }

        #endregion

        #region Properties

        public CarProfile CarProfile { get; set; }
        public string ErrorText
        {
            get => _errorText;
            set
            {
                _errorText = value;
                OnPropertyChanged(nameof(ErrorText));
            }
        }
     
       
        public string Message
        {
            get => _message;
            set
            {          
                    _message = value;
                    OnPropertyChanged(nameof(Message));

            }
        } 

        public DateTime To
        {
            get { return _to; }
            set
            {
                
                _to = value;
                OnPropertyChanged(nameof(To));
                
            }
        }

        public DateTime From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged(nameof(From));
            }
        }

        
        public CarProfileModel Car
        {
            get => _car;
            private set
            {
                _car = value;
                OnPropertyChanged(nameof(Car));
            }
        }

        
        #endregion

        #region Commands
        private ICommand _rentCarCommand;

        public ICommand RentCarCommand => _rentCarCommand??( _rentCarCommand=new DelegateCommand(async ()=> await RentCarFunction()));

        public async Task RentCarFunction()
        {
            if (Message == "Message to lessor" || To < DateTime.Now || From < DateTime.Now || To < From)
            {
                ErrorText = "*Information was not entered correctly";
                return;
            }

            string text="";
            bool confirm = _helper.ConfirmRentingDates(CarProfile, To, From,ref text);
            if (!confirm)
            {
                ErrorText = text;
                return;
            }

            var list = _helper.CreateDayThatIsRentedList(From,To,CarProfile);
            var renterUser = await _dbContext.GetUser(_application.CurrentUser.Email);
            _dbContext.UpdateUser(CarProfile.User);
            _dbContext.UpdateUser(renterUser);

            await _dbContext.AddDayThatIsRentedList(list);
                                 
            await _dbContext.AddMessageToLessor(Message,CarProfile,renterUser);

            
            _application.GoToPage(ApplicationPage.SearchPage);//Der gås tilbage til SearchPage
        }

        private ICommand _emptyTextBoxCommand;

        public ICommand EmptyTextBoxCommand => _emptyTextBoxCommand ?? (_emptyTextBoxCommand = new DelegateCommand(EmptyTextBoxFunction));

        public void EmptyTextBoxFunction()
        {
            Message = "";
        }

        private ICommand _textBoxLostFocusCommand;
        public ICommand TextBoxLostFocusCommand => _textBoxLostFocusCommand ?? (_textBoxLostFocusCommand = new DelegateCommand(TextBoxLostFocusFunction));

        public void TextBoxLostFocusFunction()
        {
            if (Message == "")
                Message = "Message to lessor";

        }

        #endregion

        #region Functions



        #endregion

        #region ErrorHandling

        public Dictionary<string,string> ErrorCollection { get; set; }=new Dictionary<string, string>();

        public string this[string name]
        {
            get
            {
                string result = null;
                switch (name)
                {
                    case "To":                        
                        if (To < DateTime.Now)
                            result = "The Date entered has to be after the current date or after";
                        else if (To <= From)
                            result = "Date has to be after the the day the car is rented";
                        break;
                    case "From":
                        if (From < DateTime.Now)
                            result = "The Date entered has to be after the current date or after";
                        break;
                }

                if (ErrorCollection.ContainsKey(name))
                    ErrorCollection[name] = result;
                else if(result!=null)
                    ErrorCollection.Add(name,result);

                OnPropertyChanged("ErrorCollection");
                return result;
            }
        }

        public string Error
        {
            get { return null; }
        }

        #endregion
    }
}
