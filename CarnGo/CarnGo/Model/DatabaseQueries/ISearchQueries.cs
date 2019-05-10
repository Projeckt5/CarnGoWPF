using System.Collections.Generic;
using System.Threading.Tasks;
using CarnGo.Database.Models;

namespace CarnGo
{
    public interface ISearchQueries
    {
        Task<List<CarProfile>> GetAllCarProfilesTask();
    }
}