using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;

namespace CarnGo
{

    public class SearchViewModel : BaseViewModel, IDataErrorInfo
    {
        #region Constructor

        public SearchViewModel(IEventAggregator eventAggregator)
        {
            //IoCContainer.Resolve<IEventAggregator>().GetEvent<SearchEvent>().Subscribe(SearchEventHandler);
            eventAggregator.GetEvent<SearchEvent>().Subscribe(SearchEventHandler);
            DateFrom = DateTime.Today;
            DateTo = DateTime.Today;
            _criteria = new List<Predicate<SearchResultItemViewModel>>();
        }

        #endregion

        #region Fields

        protected string _locationText;
        protected string _brandText;
        protected string _seatsText;
        protected DateTime _dateFrom;
        protected DateTime _dateTo;
        private List<Predicate<SearchResultItemViewModel>> _criteria;
        private ObservableCollection<SearchResultItemViewModel> _searchResultItems;
        private CollectionView cv;

        #endregion

        #region Properties

        public CarProfileModel CarProfileModel { get; set; }

        public ObservableCollection<SearchResultItemViewModel> SearchResultItems
        {
            get { return _searchResultItems; }
            set
            {
                if (_searchResultItems == value)
                    return;

                _searchResultItems = value;
                OnPropertyChanged(nameof(SearchResultItems));
            }
        }

        public string LocationText
        {
            get => _locationText;
            set
            {
                if (_locationText == value)
                    return;
                _locationText = value;
                OnPropertyChanged(nameof(LocationText));
            }
        }

        public string BrandText
        {
            get => _brandText;
            set
            {
                if (_brandText == value)
                    return;
                _brandText = value;
                OnPropertyChanged(nameof(BrandText));
            }
        }

        public string SeatsText
        {
            get => _seatsText;
            set
            {
                if (_seatsText == value)
                    return;
                _seatsText = value;
                OnPropertyChanged(nameof(SeatsText));
            }
        }

        public DateTime DateFrom
        {
            get => _dateFrom;
            set
            {
                if (_dateFrom == value)
                    return;
                _dateFrom = value;
                OnPropertyChanged(nameof(DateFrom));
            }
        }

        public DateTime DateTo
        {
            get => _dateTo;
            set
            {
                if (_dateTo == value)
                    return;
                _dateTo = value;
                OnPropertyChanged(nameof(DateTo));
            }
        }

        public List<Predicate<SearchResultItemViewModel>> Criteria
        {
            get => _criteria;
            set { _criteria = value; }
        }

        public CollectionView Cv
        {
            get => cv;
            set { cv = value; }
        }

        #endregion

        #region Methods

        private void Search()
        {
            if (!IsValid)
                return;

            cv = (CollectionView)CollectionViewSource.GetDefaultView(SearchResultItems);

            _criteria.Clear();

            if (!string.IsNullOrEmpty(LocationText))
            {
                _criteria.Add(new Predicate<SearchResultItemViewModel>(
                    x => x.Location.ToLower().Contains(LocationText.ToLower())));
            }

            if (!string.IsNullOrEmpty(BrandText))
            {
                _criteria.Add(new Predicate<SearchResultItemViewModel>(
                    x => x.Brand.ToLower().Contains(BrandText.ToLower())));
            }

            if (!string.IsNullOrEmpty(SeatsText))
            {
                _criteria.Add(new Predicate<SearchResultItemViewModel>(
                    x => x.Seats == int.Parse(SeatsText)));
            }

            if ((DateFrom.Date != DateTime.Today.Date && DateTo.Date != DateTime.Today.Date))
            {
                    _criteria.Add(new Predicate<SearchResultItemViewModel>(
                        x => x.StartLeaseTime <= DateFrom));

                    _criteria.Add(new Predicate<SearchResultItemViewModel>(
                        x => x.EndLeaseTime >= DateTo));
            }
            cv.Filter = Filtering;
            OnPropertyChanged(nameof(cv));
        }

        protected bool Filtering(object item)
        {
            SearchResultItemViewModel s = item as SearchResultItemViewModel;
            bool isIn = true;
            if (_criteria.Count == 0)
                return isIn;
            isIn = _criteria.TrueForAll(x => x(s));
            return isIn;
        }

        public void ClearSearch()
        {
            cv = (CollectionView)CollectionViewSource.GetDefaultView(SearchResultItems);

            _criteria.Clear();

            LocationText = String.Empty;
            BrandText = string.Empty;
            SeatsText = string.Empty;
            DateFrom = DateTime.Today;
            DateTo = DateTime.Today; 

            cv.Filter = null;
            OnPropertyChanged(nameof(cv));
        }

        private void SearchEventHandler(string location)
        {
            if (!string.IsNullOrEmpty(location))
            {
                ClearSearch();
                LocationText = location;
                Search();
            }
        }

        #endregion

        #region Commands

        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get { return _searchCommand ?? (_searchCommand = new DelegateCommand(Search)); }
        }

        private ICommand _clearSearchCommand;
        public ICommand ClearSearchCommand
        {
            get { return _clearSearchCommand ?? (_clearSearchCommand = new DelegateCommand(ClearSearch)); }
        }

        #endregion

        #region ErrorHandling

        string IDataErrorInfo.Error
        {
            get { return null; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return GetValidationError(propertyName); }
        }

        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();

        public string GetValidationError(string propertyName)
        {
            string error = null;

            switch (propertyName)
            {
                case "SeatsText":
                    error = ValidateSeatsText();
                    break;

                case "DateFrom":
                    error = ValidateDateFrom();
                    break;

                case "DateTo":
                    error = ValidateDateTo();
                    break;
            }

            if (Errors.ContainsKey(propertyName))
                Errors[propertyName] = error;
            else if (!string.IsNullOrEmpty(error))
                Errors.Add(propertyName, error);

            OnPropertyChanged(nameof(Errors));
            return error;
        }

        // Checks that number of seats qualifies as a number and exceeds 0
        private string ValidateSeatsText()
        {
            if (!string.IsNullOrEmpty(SeatsText))
            {
                bool isNumeric = int.TryParse(SeatsText, out int number);

                if (!isNumeric)
                    return "Number of seats must be an integer";

                if (number <= 0)
                    return "Number of seats must be larger than 0";
            }
            return null;
        }

        private string ValidateDateTo()
        {
            if (DateTo.Date < DateTime.Today.Date)
                return "Drop off date must be after the current date";
            else if (DateTo.Date < DateFrom.Date)
                return "Drop off date must be after the pick-up date";

            return null;
        }

        private string ValidateDateFrom()
        {
            if (DateFrom.Date < DateTime.Today.Date)
                return "Pick-up and drop off dates must be after the current date";

            return null;
        }

        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                    if (GetValidationError(property) != null)
                        return false;

                return true;
            }
        }

        private static readonly string[] ValidatedProperties =
        {
            "SeatsText",
            "DateFrom",
            "DateTo"
        };

        #endregion
    }
}