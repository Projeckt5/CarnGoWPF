using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CarnGo.Database.Models;
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

        public static async Task  PullAllData()
        {
            using (var db = new AppDbContext())
            {
                var messages = await db.GetAllMessages();
                var carProfiles = await db.GetAllCarProfiles();
                var carEquipment = db.GetAllCarEquipment();
                var users = await db.GetAllUsers();
                var daysRented = await db.GetAllDayThatIsRented();
                var possibleToRent = await db.GetAllPossibleToRentDay();


                if (messages != null)
                {
                    foreach (var message in messages)
                    {
                        Console.WriteLine("        Lessor: " + message.LessorEmail);
                        Console.WriteLine("        Renter: " + message.RenterEmail);
                        Console.WriteLine("  ConfirmationStatus: " + message.ConfirmationStatus);
                        Console.WriteLine("  Message type: " + message.MsgType);
                        Console.WriteLine("   The message: " + message.TheMessage);
                        Console.WriteLine("      Has seen: " + message.HaveBeenSeen);
                        Console.WriteLine("    Message ID: " + message.MessageID);
                    }
                }


                if (users != null)
                {
                    foreach (var user in users)
                    {
                        Console.WriteLine("          Name: " + user.FirstName + " " + user.LastName);
                        Console.WriteLine("       Address: " + user.Address);
                        Console.WriteLine("         Email: " + user.Email);
                        Console.WriteLine("      Password: " + user.Password);
                        Console.WriteLine("     Renter type: " + user.UserType);
                        Console.WriteLine("Number of cars: " + user.Cars?.Count);
                    }
                }


                if (carProfiles != null)
                {
                    foreach (var car in carProfiles)
                    {
                        Console.WriteLine("         Brand: " + car.Brand);
                        Console.WriteLine("         Model: " + car.Model);
                        Console.WriteLine("           Age: " + car.Age);
                        Console.WriteLine("   Description: " + car.CarDescription);
                        Console.WriteLine("         Owner: " + car.Owner.FirstName + " " + car.Owner.LastName);
                    }
                }


                if (possibleToRent != null)
                {
                    foreach (var possible in possibleToRent)
                    {
                        Console.WriteLine("           Car: " + possible.CarProfile.Brand);
                        Console.WriteLine("          Date: " + possible.Date);
                    }
                }


                if (daysRented != null)
                {
                    foreach (var rented in daysRented)
                    {
                        Console.WriteLine("           Car: " + rented.CarProfile.Model);
                        Console.WriteLine("        Renter: " + rented.Renter.FirstName + " " + rented.Renter.LastName);
                        Console.WriteLine("          Date: " + rented.Date);
                    }
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

        public static async Task SeedDatabase()
        {
            User lessor = new User()
            {
                Email = "car@owner",
                FirstName = "Owner",
                LastName = "Owner",
                Password = "123asd",
                Address = "Here",
                UserType = 2,
                AuthenticationString = Guid.Empty,
            }; 
            
            User renter = new User()
            {
                Email = "car@renter",
                FirstName = "Renter",
                LastName = "Renter",
                Password = "123asd",
                Address = "There",
                UserType = 1,
                AuthenticationString = Guid.Empty,
            }; 

            List<CarProfile> ourCars = new List<CarProfile>();
            List<CarEquipment> ourEquip = new List<CarEquipment>();
            
            for (int i = 1; i < 12; i++)
            {
                CarEquipment tempCarEquipment = new CarEquipment();
                CarProfile tempCarProfile = new CarProfile();
                bool iseven;
                if (i/2 == 0)
                {
                    iseven = true;
                }
                else
                {
                    iseven = false;
                }
                 
                tempCarProfile.RegNr = "8FJSM2" + Convert.ToString(i);
                tempCarProfile.Model = "7";
                tempCarProfile.Brand = $"BMW{i}";
                tempCarProfile.Age = 5 + i;
                tempCarProfile.Location = "Jylland";
                tempCarProfile.Seats = i;
                tempCarProfile.Price = (i * 1000) + 50000;
                tempCarProfile.RentalPrice = (i * 100) + 500;
                tempCarProfile.FuelType = "92";
                tempCarProfile.CarDescription = $"#{i}: The very best, like no one ever was";
                tempCarProfile.Owner = lessor;
                tempCarProfile.User = renter;
                tempCarProfile.OwnerEmail = lessor.Email;
                tempCarProfile.UserEmail = renter.Email;
                tempCarProfile.PossibleToRentDays = new List<PossibleToRentDay>();
                tempCarProfile.MessagesCarOccursIn = new List<Message>();

                tempCarEquipment.Smoking = iseven;
                tempCarEquipment.Audioplayer = iseven;
                tempCarEquipment.GPS = iseven;
                tempCarEquipment.Childseat = iseven;
                tempCarEquipment.CarProfile = tempCarProfile;
                tempCarProfile.CarEquipment = tempCarEquipment;

                ourCars.Add(tempCarProfile);
                ourEquip.Add(tempCarEquipment);
            }

            
            ourCars.ForEach(c =>
            {
                List<PossibleToRentDay> possibleToRentDay = new List<PossibleToRentDay>();
                for (int i = 0; i < ourCars.Count; i++)
                {
                    DateTime tempDateTime = DateTime.Now + TimeSpan.FromDays(i);
                    PossibleToRentDay tempday = new PossibleToRentDay()
                    {
                        Date = tempDateTime,
                        CarProfile = c
                    };
                    possibleToRentDay.Add(tempday);
                }

                List<DayThatIsRented> daysThatIs = new List<DayThatIsRented>();
                for (int i = 5; i < 10; i++)
                {
                    DateTime tempDateTime = DateTime.Now + TimeSpan.FromDays(i + 4);
                    DayThatIsRented tempday = new DayThatIsRented
                    {
                        Date = tempDateTime,
                        CarProfile = c,
                        Renter = lessor
                    };
                    daysThatIs.Add(tempday);
                }
                c.PossibleToRentDays = possibleToRentDay;
                c.DaysThatIsRented = daysThatIs;
                c.StartLeaseTime = daysThatIs[0].Date;
                c.EndLeaseTime = daysThatIs[4].Date;
            });



            Message ourMessage1 = new Message()
            {
                HaveBeenSeen = true,
                ConfirmationStatus = 0,
                TheMessage = "Can I rent car fuckface?",
                LessorEmail = lessor.Email,
                RenterEmail = renter.Email,
                SenderEmail = renter.Email,
                ReceiverEmail = lessor.Email,
                CreatedDate = new DateTime(2019, 05, 2),
                MsgType = 1,
                CarProfile = ourCars[0],
                CarProfileRegNr = ourCars[0].RegNr,
                MessagesWithUsers = new List<MessagesWithUsers>()
            };
             
            Message ourMessage2 = new Message()
            {
                HaveBeenSeen = true,
                ConfirmationStatus = 2,
                TheMessage = "Yes you can motherfucker!",
                LessorEmail = lessor.Email,
                RenterEmail = renter.Email,
                SenderEmail = lessor.Email,
                ReceiverEmail = renter.Email,
                CreatedDate = new DateTime(2019, 05, 3),
                MsgType = 0,
                CarProfile = ourCars[1],
                CarProfileRegNr = ourCars[1].RegNr,
                MessagesWithUsers = new List<MessagesWithUsers>()
            };

            Message ourMessage3 = new Message()
            {
                HaveBeenSeen = false,
                ConfirmationStatus = 1,
                TheMessage = "Can I rent, yet another car? Cunt?",
                LessorEmail = lessor.Email,
                RenterEmail = renter.Email,
                SenderEmail = renter.Email,
                ReceiverEmail = lessor.Email,
                CreatedDate = new DateTime(2019, 05, 4),
                MsgType = 1,
                CarProfile = ourCars[2],
                CarProfileRegNr = ourCars[2].RegNr,
                MessagesWithUsers = new List<MessagesWithUsers>()
            };


            using (var db = new AppDbContext())
            {
                await db.AddUser(renter);
                await db.AddUser(lessor);
                foreach (var car in ourCars)
                {
                    await db.AddCarProfile(car);
                }

                await db.AddMessage(ourMessage1);
                await db.AddMessage(ourMessage2);
                await db.AddMessage(ourMessage3);
            }
        }
    }
}
