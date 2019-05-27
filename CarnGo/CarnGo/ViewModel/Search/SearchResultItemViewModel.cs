using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Events;

namespace CarnGo
{

    public class CarProfileDataEvent : PubSubEvent<string> { }

    public class SearchResultItemViewModel : BaseViewModel
    {
        #region Constructor

        public SearchResultItemViewModel(IEventAggregator eventAggregator, IApplication application)
        {
            _eventAggregator = eventAggregator;
            _application = application;
        }

        #endregion

        #region Fields

        protected string _model;
        protected string _brand;
        protected string _location;
        protected string _regNr;
        protected int _seats;
        protected int _price;
        protected DateTime _startLeaseTime;
        protected DateTime _endLeaseTime;
        protected UserModel _owner;
        protected byte[] _image;
        private readonly IEventAggregator _eventAggregator;
        private readonly IApplication _application;

        #endregion

        #region Properties

        public byte[] CarImage
        {
            get => _image;
            set
            {
                if (_image == value)
                    return;
                _image = value;
                OnPropertyChanged(nameof(CarImage));
            }
        }

        public string Model
        {
            get => _model;
            set
            {
                if (_model == value)
                    return;
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        public string RegNr
        {
            get => _regNr;
            set
            {
                if (_regNr == value)
                    return;
                _regNr = value;
                OnPropertyChanged(nameof(RegNr));
            }
        }

        public string Brand
        {
            get => _brand;
            set
            {
                if (_brand == value)
                    return;
                _brand = value;
                OnPropertyChanged(nameof(Brand));
            }
        }

        public string Location
        {
            get =>_location;
            set
            {
                if (_location == value)
                    return;
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        public int Seats
        {
            get => _seats;
            set
            {
                if (_seats == value)
                    return;
                _seats = value;
                OnPropertyChanged(nameof(Seats));
            }
        }

        public int Price
        {
            get => _price;
            set
            {
                if (_price == value)
                    return;
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public DateTime StartLeaseTime
        {
            get => _startLeaseTime;
            set
            {
                if (_startLeaseTime == value)
                    return;
                _startLeaseTime = value;
                OnPropertyChanged(nameof(StartLeaseTime));
            }

        }

        public DateTime EndLeaseTime
        {
            get => _endLeaseTime;
            set
            {
                if (_endLeaseTime == value)
                    return;
                _endLeaseTime = value;
                OnPropertyChanged(nameof(EndLeaseTime));
            }
        }

        public UserModel Owner
        {
            get => _owner;
            set
            {
                if (_owner == value)
                    return;
                _owner = value;
                OnPropertyChanged(nameof(Owner));
            }
        }

        #endregion

        #region Commands

        private ICommand _sendRequestCommand;
        public ICommand SendRequestCommand
        {
            get
            {
                return _sendRequestCommand ?? (_sendRequestCommand = new DelegateCommand(SendRequest));
            }
        }

        #endregion

        #region Methods

        private void SendRequest()
        {

            _application.GoToPage(ApplicationPage.SendRequestPage);
            
            _eventAggregator.GetEvent<CarProfileDataEvent>().Publish(RegNr);
        }

        #endregion
    }
}