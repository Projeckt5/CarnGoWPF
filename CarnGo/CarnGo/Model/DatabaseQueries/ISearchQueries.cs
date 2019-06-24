using System.Collections.Generic;
using System.Threading.Tasks;
using CarnGo.Database.Models;

namespace CarnGo
{
    public interface ISearchQueries
    {
        Task<List<CarProfile>> GetCarProfilesForSearchViewTask(string filterEmail ,int pageIndex, int itemsPerPage);
        Task<int> GetCarProfilesCountTask();
    }
}