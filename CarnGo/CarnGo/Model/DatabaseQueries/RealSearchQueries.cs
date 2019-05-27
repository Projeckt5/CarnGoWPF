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

        public async Task<List<CarProfile>> GetCarProfilesForSearchViewTask(int pageIndex, int itemsPerPage)
        {
            using (var db = IoCContainer.Resolve<AppDbContext>())
            {
                return await db.GetCarProfilesForSearchView(pageIndex, itemsPerPage);
            }
        }

        public async Task<int> GetCarProfilesCountTask()
        {
            using (var db = IoCContainer.Resolve<AppDbContext>())
            {
                return await db.GetCarProfilesCount();
            }
        }
    }
}