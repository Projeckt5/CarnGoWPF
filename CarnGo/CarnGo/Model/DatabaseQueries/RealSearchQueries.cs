using System.Collections.Generic;
using System.Threading.Tasks;
using CarnGo.Database;
using CarnGo.Database.Models;

namespace CarnGo
{
    public class RealSearchQueries : ISearchQueries
    {

        public RealSearchQueries()
        {
        }

        public async Task<List<CarProfile>> GetCarProfilesForSearchViewTask(string filterEmail, int pageIndex, int itemsPerPage)
        {
            using (var db = IoCContainer.Resolve<IAppDbContext>())
            {
                return await db.GetCarProfilesForSearchView(filterEmail,pageIndex, itemsPerPage);
            }
        }

        public async Task<int> GetCarProfilesCountTask()
        {
            using (var db = IoCContainer.Resolve<IAppDbContext>())
            {
                return await db.GetCarProfilesCount();
            }
        }
    }
}