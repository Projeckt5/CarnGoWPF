using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Internal;

namespace CarnGo.Database
{
    class Commands
    {
        public static void CreateDatabase()
        {
            AppDbContext db = new AppDbContext();
            db.Database.EnsureCreated();
        }

        public static void PullAllData()
        {

        }

        public static void EmptyDatabase()
        {
        }
    }
}
