using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Prism.Commands;
using Prism.Events;

namespace CarnGo
{
    public class SendRequestViewModel:BaseViewModel,IDataErrorInfo
    {
        #region fields

        private string _errorText = "";
        private bool _dirty;
        private CarProfileModel _carProfileModel;
        private DateTime _to = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        private DateTime _from= new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        private string _message = "Message to leaser";
        
        #endregion


        #region constructor
        public SendRequestViewModel()
        {
            EventAggregatorSingleton.EventAggregatorObj.GetEvent<CarProfileDataEvent>().Subscribe(SearchCarProfileEvent);

        }

        private void SearchCarProfileEvent(CarProfileModel obj)
        {
            CarModelName = obj.Brand;
            CarImage = obj.CarPicture;
            

            //Bilinformation skal trækkes ud af databasen 
        }

        #endregion


        #region Properties

        public string CarModelName { get; set; } = "Mercedes Benz CLA 250";
        public BitmapImage CarImage { get; set; }=new BitmapImage(new Uri("../../Images/Bilfoto.jpg",UriKind.Relative));
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

        
        public CarDetailsViewModel Car { get; private set; }= new CarDetailsViewModel { AudioPlayer = true, ChildSeats = false, Gps = true, Model = "Ford Mustang", Smoking = false, Year = 2010 };
        

        #endregion

        #region Commands
        private ICommand _rentCarCommand;

        public ICommand RentCarCommand => _rentCarCommand??( _rentCarCommand=new DelegateCommand(RentCarFunction));

        private void RentCarFunction()
        {
            if (Message == "Message to leaser" || To < DateTime.Now || From < DateTime.Now || To < From)
            {
                ErrorText = "*Informaton was not entered correctly";
                return;
            }


            //var sendingMessage=new MessageFromRenterModel(ViewModelLocator.ApplicationViewModel.CurrentUser,_carProfileModel.Owner);            
            //sendingMessage.From = From;
            //sendingMessage.To = To;
            //sendingMessage.Message = Message;

            //sendingMessage lægges ind i database MANGLER

            ViewModelLocator.ApplicationViewModel.GoToPage(ApplicationPage.SearchPage);//Der gås tilbage til SearchPage
        }

        private ICommand _emptyTextBoxCommand;

        public ICommand EmptyTextBoxCommand => _emptyTextBoxCommand ?? (_emptyTextBoxCommand = new DelegateCommand(EmptyTextBoxFunction));

        public void EmptyTextBoxFunction()
        {
            Message = "";
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
