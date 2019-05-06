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
                    Console.WriteLine("        Lessor: " + message.LessorEmail);
                    Console.WriteLine("        Renter: " + message.RenterEmail);
                    Console.WriteLine("  Confirmation: " + message.Confirmation);
                    Console.WriteLine("  Message type: " + message.MsgType);
                    Console.WriteLine("   The message: " + message.TheMessage);
                    Console.WriteLine("      Has seen: " + message.HaveBeenSeen);
                    Console.WriteLine("    Message ID: " + message.MessageID);
                }


                foreach (var user in users)
                {
                    Console.WriteLine("          Name: " + user.FirstName + " " + user.LastName);
                    Console.WriteLine("       Address: " + user.Address);
                    Console.WriteLine("         Email: " + user.Email);
                    Console.WriteLine("      Password: " + user.Password);
                    Console.WriteLine("     User type: " + user.UserType);
                    Console.WriteLine("Number of cars: " + user.Cars.Count);
                }

                foreach (var car in carProfiles)
                {
                    Console.WriteLine("         Brand: " + car.Brand);
                    Console.WriteLine("         Model: " + car.Model);
                    Console.WriteLine("           Age: " + car.Age);
                    Console.WriteLine("   Description: " + car.CarDescription);
                    Console.WriteLine("         Owner: " + car.Owner.FirstName + " " + car.Owner.LastName);
                }

                foreach (var possible in possibleToRent)
                {
                    Console.WriteLine("           Car: " + possible.CarProfile.Brand);
                    Console.WriteLine("          Date: " + possible.Date);
                }

                foreach (var rented in daysRented)
                {
                    Console.WriteLine("           Car: " + rented.CarProfile.Model);
                    Console.WriteLine("        Renter: " + rented.User.FirstName + " " + rented.User.LastName);
                    Console.WriteLine("          Date: " + rented.Date);
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
