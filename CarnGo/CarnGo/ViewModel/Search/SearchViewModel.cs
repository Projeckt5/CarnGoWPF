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

    public class SearchViewModel : BaseViewModel
    {
        #region Constructor

        public SearchViewModel()
        {
            IoCContainer.Resolve<IEventAggregator>().GetEvent<SearchEvent>().Subscribe(SearchEventHandler);
            DateFrom = DateTime.Today;
            DateTo = DateTime.Today;
        }

        #endregion

        #region Fields

        protected string _locationText;
        protected string _brandText;
        protected string _seatsText;
        protected DateTime _dateFrom;
        protected DateTime _dateTo;
        private List<Predicate<SearchResultItemViewModel>> _criteria = new List<Predicate<SearchResultItemViewModel>>();
        private ObservableCollection<SearchResultItemViewModel> _searchResultItems;

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

        #endregion

        #region Methods

        private void Search()
        {
            ICollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(SearchResultItems);

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

            // Checks first if dates differ from default values
            if ((DateFrom > new DateTime(2019, 01, 01)) && (DateTo > new DateTime(2019, 01, 01)))
            {
                if (DateFrom < DateTo)
                {
                    _criteria.Add(new Predicate<SearchResultItemViewModel>(
                               x => x.StartLeaseTime <= DateFrom));

                    _criteria.Add(new Predicate<SearchResultItemViewModel>(
                        x => x.EndLeaseTime >= DateTo));
                }
                else
                {
                    // Warn that drop off date must be later than pickup date
                }
            }
            else if (DateFrom > new DateTime(2019, 01, 01))
            {
                _criteria.Add(new Predicate<SearchResultItemViewModel>(
                    x => x.StartLeaseTime <= DateFrom));
            }
            else if (DateTo > new DateTime(2019, 01, 01))
            {
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
            ICollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(SearchResultItems);

            _criteria.Clear();

            LocationText = String.Empty;
            BrandText = string.Empty;
            SeatsText = string.Empty;
            DateFrom = DateTime.Today;
                //new DateTime(2019, 01, 01);
            DateTo = DateTime.Today; 
                //new DateTime(2019, 01, 01);

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

        //public Dictionary<string, string> ErrorCollection { get; set; } = new Dictionary<string, string>();

        //public string this[string name]
        //{
        //    get
        //    {
        //        string result = null;
        //        switch (name)
        //        {
        //            case "DateTo":
        //                if (DateTo < DateTime.Now)
        //                    result = "The Date entered has to be after the current date or after";
        //                else if (DateTo <= DateFrom)
        //                    result = "Date has to be after the the day the car is rented";
        //                break;
        //            case "DateFrom":
        //                if (DateFrom < DateTime.Now)
        //                    result = "The Date entered has to be after the current date or after";
        //                break;
        //        }

        //        if (ErrorCollection.ContainsKey(name))
        //            ErrorCollection[name] = result;
        //        else if (result != null)
        //            ErrorCollection.Add(name, result);

        //        OnPropertyChanged("ErrorCollection");
        //        return result;
        //    }
        //}

        //public string Error
        //{
        //    get { return null; }
        //}

        #endregion
    }
}