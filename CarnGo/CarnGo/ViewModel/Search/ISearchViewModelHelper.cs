using CarnGo.Database.Models;

namespace CarnGo
{
    public interface ISearchViewModelHelper
    {
        SearchResultItemViewModel ConvertCarProfileToSearchResultItem(CarProfile carProfile);
    }
}