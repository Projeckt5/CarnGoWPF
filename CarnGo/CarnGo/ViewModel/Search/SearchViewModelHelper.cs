using CarnGo.Database.Models;
using Microsoft.EntityFrameworkCore;
using Prism.Events;

namespace CarnGo
{
    public class SearchViewModelHelper : ISearchViewModelHelper
    {
        private IDbToAppModelConverter _converter;
        public SearchResultItemViewModel ConvertCarProfileToSearchResultItem(CarProfile carProfile)
        {
            SearchResultItemViewModel searchResultItem = new SearchResultItemViewModel(IoCContainer.Resolve<IEventAggregator>(), IoCContainer.Resolve<IApplication>());
            _converter = IoCContainer.Resolve<IDbToAppModelConverter>();
            searchResultItem.RegNr = carProfile.RegNr ?? "";
            searchResultItem.Brand = carProfile.Brand ?? "";
            searchResultItem.Model = carProfile.Model ?? "";
            searchResultItem.Location = carProfile.Location ?? "";
            searchResultItem.Price = carProfile.RentalPrice;
            searchResultItem.Seats = carProfile.Seats;
            searchResultItem.StartLeaseTime = carProfile.StartLeaseTime;
            searchResultItem.EndLeaseTime = carProfile.EndLeaseTime;
            searchResultItem.CarImage = carProfile.CarPicture;
            searchResultItem.Owner = new UserModel()
            {
                FirstName = carProfile.Owner.FirstName ?? "",
                LastName = carProfile.Owner.LastName ?? ""
            };
           
            return searchResultItem;
        }
    }
}
