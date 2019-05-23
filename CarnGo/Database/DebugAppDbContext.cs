using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using CarnGo.Database.Models;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace CarnGo.Database
{
    public class DebugAppDbContext : AppDbContext
    {
        public DebugAppDbContext(DbContextOptions<AppDbContext> options):
            base(options)
        {
            if (!Users.Any())
            {
                SeedDatabase().Wait();
            }
        }

        private async Task SeedDatabase()
        {
            User lessor = new User()
            {
                Email = "car@owner",
                FirstName = "Owner",
                LastName = "Owner",
                Password = "123asd",
                Address = "Here",
                UserType = 2,
                AuthorizationString = Guid.Empty,
            };

            User renter = new User()
            {
                Email = "car@renter",
                FirstName = "Renter",
                LastName = "Renter",
                Password = "123asd",
                Address = "There",
                UserType = 1,
                AuthorizationString = Guid.Empty,
            };

            List<CarProfile> ourCars = new List<CarProfile>();
            List<CarEquipment> ourEquip = new List<CarEquipment>();

            for (int i = 1; i < 12; i++)
            {
                CarEquipment tempCarEquipment = new CarEquipment();
                CarProfile tempCarProfile = new CarProfile();
                bool iseven;
                if (i / 2 == 0)
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

                await AddUser(renter);
                await AddUser(lessor);
                foreach (var car in ourCars)
                {
                    await AddCarProfile(car);
                }

                await AddMessage(ourMessage1);
                await AddMessage(ourMessage2);
                await AddMessage(ourMessage3);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:carngo.database.windows.net,1433;Initial Catalog=CarnGo;Persist Security Info=False;Renter ID=carngo;Password=Aarhus123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }
    }

}
    