﻿using System.Collections.Generic;
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

        public Task<List<CarProfile>> GetAllCarProfilesTask()
        {
            return _dbContext.GetAllCarProfiles();
        }
    }
}