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
        public IQueryDatabase _dbQuerier;
        public ISendRequestViewModelHelperFunction _helper;
        public IEventAggregator _events;

        #region fields

        private string _errorText = "";
       
        private DateTime _to;
        private DateTime _from;
        private string _message = "Message to lessor";

        private CarProfileModel _car; 
        #endregion

        #region constructor

        public SendRequestViewModel(IEventAggregator events, IApplication application,IQueryDatabase dbQuerier,ISendRequestViewModelHelperFunction helper)
        {
            _events = events;
            _helper = helper;
            _dbQuerier = dbQuerier;
            _application = application;
            _events.GetEvent<CarProfileDataEvent>().Subscribe(async (reg) => await SearchCarProfileEvent(reg));
            To = DateTime.Now;
            From = DateTime.Now;
        }

        public async Task SearchCarProfileEvent(string regnr)
        {
            Car = await _dbQuerier.GetCarProfileTask(regnr);
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
            set
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
            if (Message == "Message to lessor" || To < DateTime.Now.Date || From < DateTime.Now.Date || To < From)
            {
                ErrorText = "*Information was not entered correctly";
                return;
            }

            string text="";
            bool confirm = _helper.ConfirmRentingDates(Car, To, From,ref text);
            if (!confirm)
            {
                ErrorText = text;
                return;
            }

            var list = _helper.CreateDayThatIsRentedList(From,To,Car, _application.CurrentUser);
            Car.DayThatIsRented.AddRange(list);
            await _dbQuerier.UpdateCarProfileTask(Car);
                                 
            await _dbQuerier.AddMessageToLessor(Message,Car,_application.CurrentUser);

            //TODO: ADD A PAGE OR POP UP TO SHOW USER THEY HAVE SEND A REQUEST FOR RENT
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
