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
            using (var db = new AppDbContext())
            {
                //foreach (var VARIABLE in db.Cars)
                {
                    //db.Cars.Remove(VARIABLE);
                }

                //foreach (var VARIABLE in db.PossibleToRentDays)
                {
                    //db.PossibleToRentDays.Remove(VARIABLE);
                }

                //foreach (var VARIABLE in db.CarRenters)
                {
                    //db.CarRenters.Remove(VARIABLE);
                }

                //foreach (var VARIABLE in db.CarRenterMessages)
                {
                    //db.CarRenterMessages.Remove(VARIABLE);
                }

                //foreach (var VARIABLE in db.CarOwners)
                {
                    //db.CarOwners.Remove(VARIABLE);
                }

                //foreach (var VARIABLE in db.CarOwnerMessages)
                {
                    //db.CarOwnerMessages.Remove(VARIABLE);
                }

                //foreach (var VARIABLE in db.DaysThatIsRented)
                {
                    //db.DaysThatIsRented.Remove(VARIABLE);
                }


                db.SaveChanges();

            }
        }
    }
}
