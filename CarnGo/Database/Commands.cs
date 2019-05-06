using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();
            }
        }

        public static void PullAllData()
        {
            using (var db = new AppDbContext())
            {
                var messages = db.GetAllMessages().Result;
                var carProfiles = db.GetAllCarProfiles().Result;
                var carEquipment = db.GetAllCarEquipment().Result;
                var users = db.GetAllUsers().Result;
                var daysRented = db.GetAllDayThatIsRented().Result;
                var possibleToRent = db.GetAllPossibleToRentDay().Result;

                foreach (var message in messages)
                {
                    Console.WriteLine("Lessor: " + message.LessorEmail + " Renter: " + message.RenterEmail + " Confirmation: " + message.Confirmation + " Message type: " + message.MsgType + " The message: " + message.TheMessage + " Has seen: " + message.HaveBeenSeen + " Message ID: " + message.MessageID);
                }


                foreach (var user in users)
                {
                    Console.WriteLine("Name: " + user.FirstName + " " + user.LastName + " Address: " + user.Address + " Email: " + user.Email + " Password: " + user.Password + " User type: " + user.UserType + " Number of cars: " + user.Cars.Count);
                }

                
            }
        }

        public static void EmptyDatabase()
        {
            using (var db = new AppDbContext())
            {
                var listOfTables = new List<string> { "CarEquipment", "DaysThatIsRented", "MessagesWithUsersJunction", "Messages", "PossibleToRentDays", "CarProfiles", "Users" };

                foreach (var tableName in listOfTables)
                {
                    db.Database.ExecuteSqlCommand("DROP TABLE [" + tableName + "]");
                }
            }
        }
    }
}
