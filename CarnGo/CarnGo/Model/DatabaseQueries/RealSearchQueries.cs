using System.Collections.Generic;
using System.Threading.Tasks;
using CarnGo.Database;
using CarnGo.Database.Models;

namespace CarnGo
{
    public class RealSearchQueries : ISearchQueries
    {
        private readonly IAppDbContext _dbContext;

        public RealSearchQueries(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CarProfile>> GetCarProfilesForSearchViewTask(int pageIndex, int itemsPerPage)
        {
            return await _dbContext.GetCarProfilesForSearchView(pageIndex, itemsPerPage);
        }

        public async Task<int> GetCarProfilesCountTask()
        {
            return await _dbContext.GetCarProfilesCount();
        }
    }
}