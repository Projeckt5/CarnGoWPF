using System;
using System.Collections.Generic;
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

        public async static Task  PullAllData()
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
                        Console.WriteLine("  Confirmation: " + message.Confirmation);
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
                        Console.WriteLine("     User type: " + user.UserType);
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
                        Console.WriteLine("        Renter: " + rented.User.FirstName + " " + rented.User.LastName);
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
            User renter = new User()
            {
                Email = "At@at.at",
                FirstName = "Car ",
                LastName = "Owner",
                Password = "1234",
                Address = "Here",
                UserType = 1,
                AuthorizationString = Guid.Empty,
            }; 
            
            User lessor = new User()
            {
                Email = "Dot@Dot.dot",
                FirstName = "Car ",
                LastName = "Renter",
                Password = "4321",
                Address = "There",
                UserType = 2,
                AuthorizationString = Guid.Empty,
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
                tempCarProfile.Brand = "BMW";
                tempCarProfile.Age = 5 + i;
                tempCarProfile.Location = "Jylland";
                tempCarProfile.Seats = i;
                tempCarProfile.Price = (i * 1000) + 50000;
                tempCarProfile.CarPicture = "ASDJIasfjn37687yh97jtg864h78jt/TG&/DG#&B/CASV(XASDM57f576879m8aysb87tdv7asrd6ANSYDMATSNDR543afstd78as79d8ynvbfas675FASYGDMA687";
                tempCarProfile.RentalPrice = (i * 100) + 500;
                tempCarProfile.FuelType = "92";
                tempCarProfile.CarDescription = "The very best, like no one ever was";
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

            List<PossibleToRentDay> possibleToRentDay = new List<PossibleToRentDay>();
            for (int i = 0; i < 15; i++)
            {
                DateTime tempDateTime = new DateTime(2019, 05, i + 4);
                PossibleToRentDay tempday = new PossibleToRentDay()
                {
                    Date = tempDateTime,
                };
                possibleToRentDay.Add(tempday);
            }
            ourCars.ForEach(c => c.PossibleToRentDays = possibleToRentDay);



            List<DayThatIsRented> daysThatIs = new List<DayThatIsRented>();
            for (int i = 5; i < 10; i++)
            {
                DateTime tempDateTime = new DateTime(2019, 05, i + 4);
                DayThatIsRented tempday = new DayThatIsRented
                {
                    Date = tempDateTime,
                    CarProfile = ourCars[0],
                    User = lessor
                };
                daysThatIs.Add(tempday);
            }
            ourCars.ForEach(c => c.DaysThatIsRented = daysThatIs);
            ourCars.ForEach(c => c.StartLeaseTime = daysThatIs[0].Date);
            ourCars.ForEach(c => c.EndLeaseTime = daysThatIs[4].Date);



            Message ourMessage1 = new Message()
            {
                HaveBeenSeen = true,
                Confirmation = false,
                TheMessage = "Can I rent car fuckface?",
                LessorEmail = lessor.Email,
                RenterEmail = renter.Email,
                CreatedDate = new DateTime(2019, 05, 2),
                MsgType = 1,
                CarProfile = ourCars[0],
                CarProfileRegNr = ourCars[0].RegNr,
                MessagesWithUsers = new List<MessagesWithUsers>()
            };
             
            Message ourMessage2 = new Message()
            {
                HaveBeenSeen = true,
                Confirmation = true,
                TheMessage = "Yes you can motherfucker!",
                LessorEmail = lessor.Email,
                RenterEmail = renter.Email,
                CreatedDate = new DateTime(2019, 05, 3),
                MsgType = 0,
                CarProfile = ourCars[1],
                CarProfileRegNr = ourCars[1].RegNr,
                MessagesWithUsers = new List<MessagesWithUsers>()
            };

            Message ourMessage3 = new Message()
            {
                HaveBeenSeen = false,
                Confirmation = false,
                TheMessage = "Can I rent, yet another car? Cunt?",
                LessorEmail = lessor.Email,
                RenterEmail = renter.Email,
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
