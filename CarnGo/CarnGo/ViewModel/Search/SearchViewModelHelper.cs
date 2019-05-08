using CarnGo.Database.Models;
using Prism.Events;

namespace CarnGo
{
    public class SearchViewModelHelper : ISearchViewModelHelper
    {
        public SearchResultItemViewModel ConvertCarProfileToSearchResultItem(CarProfile carProfile)
        {
            SearchResultItemViewModel searchResultItem = new SearchResultItemViewModel(IoCContainer.Resolve<IEventAggregator>(), IoCContainer.Resolve<IApplication>());
            searchResultItem.RegNr = carProfile.RegNr;
            searchResultItem.Brand = carProfile.Brand;
            searchResultItem.Model = carProfile.Model;
            searchResultItem.Location = carProfile.Location;
            searchResultItem.Price = carProfile.Price;
            searchResultItem.Seats = carProfile.Seats;
            searchResultItem.StartLeaseTime = carProfile.StartLeaseTime;
            searchResultItem.EndLeaseTime = carProfile.EndLeaseTime;
            searchResultItem.Owner = new UserModel()
            {
                Firstname = carProfile.Owner.FirstName,
                Lastname = carProfile.Owner.LastName
            };

            return searchResultItem;
        }
    }
}
