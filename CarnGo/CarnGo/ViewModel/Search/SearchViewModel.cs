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
    public class CarProfileDataEvent : PubSubEvent<CarProfileModel> { }

    public class SearchViewModel : CarProfileModel
    {
        #region Fields

        protected string _locationText;
        protected string _brandText;
        protected string _seatsText;
        private List<Predicate<SearchResultItemViewModel>> _criteria = new List<Predicate<SearchResultItemViewModel>>();
        private ObservableCollection<SearchResultItemViewModel> _searchResultItems;

        #endregion

        #region Properties

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

        #endregion

        #region Methods

        private void Search()
        {
            ICollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(SearchResultItems);

            _criteria.Clear();

            if (!string.IsNullOrEmpty(LocationText))
            {
                _criteria.Add(new Predicate<SearchResultItemViewModel>(
                    x => x.Location.ToLower() == LocationText.ToLower()));
            }

            if (!string.IsNullOrEmpty(BrandText))
            {
                _criteria.Add(new Predicate<SearchResultItemViewModel>(
                    x => x.Brand.ToLower() == BrandText.ToLower()));
            }

            if (!string.IsNullOrEmpty(SeatsText))
            {
                _criteria.Add(new Predicate<SearchResultItemViewModel>(
                    x => x.Seats == int.Parse(SeatsText)));
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

        private void ClearSearch()
        {
            ICollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(SearchResultItems);

            _criteria.Clear();

            LocationText = String.Empty;
            BrandText = string.Empty;
            SeatsText = string.Empty;

            cv.Filter = null;
            OnPropertyChanged(nameof(cv));
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

        #region EventAggregator

        //IEventAggregator _eventAggregator;

        //public SearchViewModel(IEventAggregator ea)
        //{
        //    _eventAggregator = ea;

        //    var hej = new CarProfileDataEvent();
        //    _eventAggregator.GetEvent<CarProfileDataEvent>().Publish(hej);
        //}

        #endregion
    }
}