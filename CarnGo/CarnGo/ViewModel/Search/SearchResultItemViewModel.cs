using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Prism.Commands;
using Prism.Events;

namespace CarnGo
{

    public class CarProfileDataEvent : PubSubEvent<CarProfileModel> { }

    public class SearchResultItemViewModel : BaseViewModel
    {
        private readonly IApplication _application;

        #region Constructor

        public SearchResultItemViewModel(IApplication application)
        {
            _application = application;
        }

        #endregion

        #region Fields

        protected string _model;
        protected string _brand;
        protected string _location;
        protected int _seats;
        protected int _price;
        protected DateTime _startLeaseTime;
        protected DateTime _endLeaseTime;
        protected UserModel _owner;

        #endregion

        #region Properties

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
            CarProfileModel carProfileModel = new CarProfileModel(Owner, Model, Brand, 2000, "A570403", Location, Seats, StartLeaseTime,
                EndLeaseTime, Price);

            _application.GoToPage(ApplicationPage.SendRequestPage);

            IoCContainer.Resolve<IEventAggregator>().GetEvent<CarProfileDataEvent>().Publish(carProfileModel);
        }

        #endregion
    }
}