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
        private readonly IApplication _application;

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

        public SendRequestViewModel(IEventAggregator events, IApplication application)
        {
            _application = application;
           events.GetEvent<CarProfileDataEvent>().Subscribe(SearchCarProfileEvent);
        }

        private void SearchCarProfileEvent(CarProfileModel obj)
        {
            Car = obj;


            
        }

        #endregion

        #region Properties


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

        public ICommand RentCarCommand => _rentCarCommand??( _rentCarCommand=new DelegateCommand(RentCarFunction));

        public void RentCarFunction()
        {
            if (Message == "Message to lessor" || To < DateTime.Now || From < DateTime.Now || To < From)
            {
                ErrorText = "*Informaton was not entered correctly";
                return;
            }

            /*if (!ConfirmRentingDates(Car car))
            {
                return;
            }*/

            /*var list = CreateDayThatIsRentedList();

            var repo = new CarnGoReposetory();
            repo.AddDayThatIsRentedList(list);
            */
            /*var message=new CarRenterMessage();
            message.Commentary = Message;
            message.Car
            var repo = new CarnGoReposetory();
            repo.AddCarRenterMessage(message);*/


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

        public bool ConfirmRentingDates(CarProfile car)
        {
            try
            {
                for (var rentingDate = From; rentingDate <= To; rentingDate = rentingDate.AddDays(1))
                {
                    foreach (var date in car.DaysThatIsRented)
                    {
                       
                        if (date.Date == rentingDate)
                        {
                            ErrorText = "*Another lessor has rented this car in the specified period";
                            return false;
                        }

                    }
                }



                for (var rentingDate = From; rentingDate <= To; rentingDate = rentingDate.AddDays(1))
                {

                    bool rent = false;
                    foreach (var date in car.PossibleToRentDays)
                    {
                        if (date.Date == rentingDate)
                        {
                            rent = true;
                        }
                    }

                    if (!rent)
                    {
                        ErrorText = "*It is not possible to rent the car in the specified period";
                        return false;
                    }
                }
            }
            catch (NullReferenceException e)
            {
                
            }

            return true;
        }


        public List<DayThatIsRented> CreateDayThatIsRentedList()
        {
            var list = new List<DayThatIsRented>();
            for (var rentingDate = From; rentingDate.Date <= To.Date; rentingDate = rentingDate.AddDays(1))
            {
                list.Add(new DayThatIsRented(){Car=new Car(),CarRenter = new CarRenter(),Date = rentingDate});//ændre denne linie til database er færdig
            }

            return list;
        }



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
