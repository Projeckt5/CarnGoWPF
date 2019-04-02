using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Prism.Commands;

namespace CarnGo
{
    public class SearchResultViewModel : CarProfileModel
    {
        #region Fields

        private ObservableCollection<SearchResultItemViewModel> _searchResultItems;
        private ObservableCollection<SearchResultItemViewModel> _filteredSearchResultItems;
        private string _lastSearchText;
        private string _searchText;

        #endregion
        // Do not add to this list as it will make filtered searchresults out of sync
        public ObservableCollection<SearchResultItemViewModel> SearchResultItems
        {
            get { return _searchResultItems; }
            set
            {
                if (_searchResultItems == value)
                    return;

                _searchResultItems = value;
                OnPropertyChanged(nameof(SearchResultItems));

                FilteredSearchResultItems = new ObservableCollection<SearchResultItemViewModel>(_searchResultItems);
                OnPropertyChanged(nameof(FilteredSearchResultItems));
            }
        }

        public ObservableCollection<SearchResultItemViewModel> FilteredSearchResultItems
        {
            get { return _filteredSearchResultItems; }
            set
            {
                if (_filteredSearchResultItems == value)
                    return;

                _filteredSearchResultItems = value;

                OnPropertyChanged(nameof(_filteredSearchResultItems));
            }

        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText == value)
                    return;

                _searchText = value;

                OnPropertyChanged(nameof(SearchText));
                //if (!string.IsNullOrEmpty(SearchText))
                //    Search();
            }
        }

        // remember this
        public string LastSearchText
        {
            get => _lastSearchText;
            set
            {
                if (_lastSearchText == value)
                    return;
                _lastSearchText = value;
                OnPropertyChanged(nameof(LastSearchText));
            }
        }

        #region Command Methods

        private void Search()
        {
            // Avoid re-searching the same text
            if ((string.IsNullOrEmpty(LastSearchText) && string.IsNullOrEmpty(SearchText)) ||
                string.Equals(LastSearchText, SearchText))
                return;

            // No search text or no SearchResultItems
            if (string.IsNullOrEmpty(SearchText) || SearchResultItems == null || SearchResultItems.Count <= 0)
            {
                FilteredSearchResultItems = new ObservableCollection<SearchResultItemViewModel>(SearchResultItems);

                LastSearchText = SearchText;

                return;
            }

            // Find all SearchResultItems that contain the given text
            FilteredSearchResultItems = new ObservableCollection<SearchResultItemViewModel>(
                SearchResultItems.Where(SearchResultItems => SearchResultItems.Location.Contains(SearchText)));

            LastSearchText = SearchText;
        }

        public void ClearSearch()
        {
            if (!string.IsNullOrEmpty(SearchText))
                SearchText = string.Empty;
        }


        #endregion



        #region Commands

        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get { return _searchCommand ?? (_searchCommand = new DelegateCommand(Search)); }
        }

        public ICommand ClearSearchCommand { get; set; }

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