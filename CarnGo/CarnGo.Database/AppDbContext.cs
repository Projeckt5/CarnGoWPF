using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FirstTryProject.Model;
using Microsoft.EntityFrameworkCore;

namespace FirstTryProject.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:mowinckel.database.windows.net,1433;Initial Catalog = CarnGo; Persist Security Info=False;User ID = ProjectDB@mowinckel;Password=Vores1.sødedatabase;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30");

        }
        private DbSet<CarRenter> CarRenters { get; set; }
        private DbSet<CarRenterMessage> CarRenterMessages { get; set; }
        private DbSet<CarOwner> CarOwners { get; set; }
        private DbSet<CarOwnerMessage> CarOwnerMessages { get; set; }
        private DbSet<Car> Cars { get; set; }
        private DbSet<DayThatIsRented> DaysThatIsRented { get; set; }
        private DbSet<PossibleToRentDay> PossibleToRentDays { get; set; }


        //Reposetory pattern
            // Setting Data
        public void AddCarRenter(CarRenter carRenter)
        {
            using (var db = new AppDbContext())
            {
                bool isAlreadyInThere = false;
                foreach (var lokalecarRenter in db.CarRenters)
                {
                    if(lokalecarRenter.ContactInfo == carRenter.ContactInfo)
                    {
                        isAlreadyInThere = true;
                        lokalecarRenter.CarRenterMessages = carRenter.CarRenterMessages;
                        lokalecarRenter.Cars = carRenter.Cars;
                        lokalecarRenter.DrivingLicenceNumber = carRenter.DrivingLicenceNumber;
                        lokalecarRenter.Name = carRenter.Name;
                        break;
                    }
                }

                if (!isAlreadyInThere)
                {
                    db.CarRenters.AddAsync(carRenter);
                }

                db.SaveChangesAsync();
            }


        }

        public void AddCarRenterMessage(CarRenterMessage carRenterMessage, CarRenter carRenter)
        {
            // check if carRenter is linked up with carRenterMessage
            bool isAlreadyInThere = false;
            foreach (var carRenterMessages in carRenter.CarRenterMessages)
            {
                if (carRenterMessages.CarRenterMessageid == carRenterMessage.CarRenterMessageid)
                {
                    isAlreadyInThere = true;
                    carRenterMessages.ContactInfo = carRenterMessage.ContactInfo;
                    carRenterMessages.HaveBeenSeen = carRenterMessage.HaveBeenSeen;
                    carRenterMessages.Commentary = carRenterMessage.Commentary;
                    carRenterMessages.Car = carRenterMessage.Car;
                    carRenterMessages.RentedFromTo = carRenterMessage.RentedFromTo;
                    carRenterMessages.CarRenter = carRenterMessage.CarRenter;

                    break;
                }
            }

            if (!isAlreadyInThere)
            {
                carRenter.CarRenterMessages.Add(carRenterMessage);
            }

            // make sure carRenterMessage is linked up with carRenter
            carRenterMessage.CarRenter = carRenter;

            // make sure CarRenter is already in DB and update it, if not, add it.
            isAlreadyInThere = false;
            using (var db = new AppDbContext())
            {
                foreach (var localCarRenter in db.CarRenters)
                {
                    if (localCarRenter.ContactInfo == carRenter.ContactInfo)
                    {
                        isAlreadyInThere = true;
                        localCarRenter.Name = carRenter.Name;
                        localCarRenter.DrivingLicenceNumber = carRenter.DrivingLicenceNumber;
                        localCarRenter.Cars = carRenter.Cars;
                        localCarRenter.CarRenterMessages = carRenter.CarRenterMessages;
                        break;
                    }
                }
                if (isAlreadyInThere)
                {
                    db.CarRenters.AddAsync(carRenter);
                }

                // make sure CarRenterMessage is already in DB and update it, if not, add it.
                isAlreadyInThere = false;
                foreach (var localCarRenterMessages in db.CarRenterMessages)
                {
                    if (localCarRenterMessages.CarRenterMessageid == carRenterMessage.CarRenterMessageid)
                    {
                        isAlreadyInThere = true;
                        localCarRenterMessages.ContactInfo = carRenterMessage.ContactInfo;
                        localCarRenterMessages.HaveBeenSeen = carRenterMessage.HaveBeenSeen;
                        localCarRenterMessages.Commentary = carRenterMessage.Commentary;
                        localCarRenterMessages.Car = carRenterMessage.Car;
                        localCarRenterMessages.RentedFromTo = carRenterMessage.RentedFromTo;
                        localCarRenterMessages.CarRenter = carRenterMessage.CarRenter;
                        break;
                    }
                }
                if (isAlreadyInThere)
                {
                    db.CarRenters.AddAsync(carRenter);
                }

                // make sure CarRenter is already in DB and update it, if not, add it.
                isAlreadyInThere = false;
                foreach (var carRenterMessages in carRenter.CarRenterMessages)
                {
                    if (carRenterMessages.CarRenterMessageid == carRenterMessage.CarRenterMessageid)
                    {
                        isAlreadyInThere = true;
                        carRenterMessages.ContactInfo = carRenterMessage.ContactInfo;
                        carRenterMessages.HaveBeenSeen = carRenterMessage.HaveBeenSeen;
                        carRenterMessages.Commentary = carRenterMessage.Commentary;
                        carRenterMessages.Car = carRenterMessage.Car;
                        carRenterMessages.RentedFromTo = carRenterMessage.RentedFromTo;
                        carRenterMessages.CarRenter = carRenterMessage.CarRenter;
                        carRenterMessages.Recipientid = carRenterMessages.Recipientid;

                        break;
                    }
                }

                if (!isAlreadyInThere)
                {
                    carRenter.CarRenterMessages.Add(carRenterMessage);
                }
                db.CarRenterMessages.AddAsync(carRenterMessage);

                db.SaveChangesAsync();

            }
        }


        public void AddCarOwner(CarOwner carOwner)
        {
            bool isAlreadyInThere = false;
            using (var db = new AppDbContext())
            { 
                foreach (var lokaleCarOwner in db.CarOwners)
                {
                    if (lokaleCarOwner.ContactInfo == carOwner.ContactInfo)
                    { 
                        isAlreadyInThere = true;
                        lokaleCarOwner.Name = carOwner.Name;
                        lokaleCarOwner.DrivingLicenceNumber = carOwner.DrivingLicenceNumber;
                        lokaleCarOwner.CarRegistrationNumber = carOwner.CarRegistrationNumber;
                        lokaleCarOwner.Cars = carOwner.Cars;
                        lokaleCarOwner.CarOwnerMessages = carOwner.CarOwnerMessages;
                        break;
                    }
                }

                if (!isAlreadyInThere)
                {
                    db.CarOwners.AddAsync(carOwner);
                }

                db.SaveChangesAsync();
            }
        }

        public void AddCarOwnerMessage(CarOwnerMessage carOwnerMessage, CarOwner carOwner)
        {
            bool isAlreadyInThere = false;
            using (var db = new AppDbContext())
            {
                foreach (var lokaleCarOwner in db.CarOwners)
                {
                    if (lokaleCarOwner.ContactInfo == carOwner.ContactInfo)
                    {
                        isAlreadyInThere = true;
                        lokaleCarOwner.Name = carOwner.Name;
                        lokaleCarOwner.DrivingLicenceNumber = carOwner.DrivingLicenceNumber;
                        lokaleCarOwner.CarRegistrationNumber = carOwner.CarRegistrationNumber;
                        lokaleCarOwner.Cars = carOwner.Cars;
                        lokaleCarOwner.CarOwnerMessages = carOwner.CarOwnerMessages;
                        break;
                    }
                }

                if (!isAlreadyInThere)
                {
                    db.CarOwners.AddAsync(carOwner);
                }

                isAlreadyInThere = false;
                foreach (var localCarOwnerMessage in db.CarOwnerMessages)
                {
                    if (localCarOwnerMessage.CarOwnerMessageid == carOwnerMessage.CarOwnerMessageid)
                    {
                        isAlreadyInThere = true;
                        localCarOwnerMessage.HaveBeenRejected = carOwnerMessage.HaveBeenRejected;
                        localCarOwnerMessage.HaveBeenSeen = carOwnerMessage.HaveBeenSeen;
                        localCarOwnerMessage.Commentary = carOwnerMessage.Commentary;
                        localCarOwnerMessage.Car = carOwnerMessage.Car;
                        localCarOwnerMessage.RentedFromTo = carOwnerMessage.RentedFromTo;
                        localCarOwnerMessage.CarOwner = carOwnerMessage.CarOwner;
                        break;
                    }
                }

                if (!isAlreadyInThere)
                {
                    db.CarOwnerMessages.AddAsync(carOwnerMessage);
                }
            }
        }


        
        public void AddCar(Car car, CarOwner carOwner)
        {
            bool isAlreadyInThere = false;
            using (var db = new AppDbContext())
            {
                foreach (var lokaleCarOwner in db.CarOwners)
                {
                    if (lokaleCarOwner.ContactInfo == carOwner.ContactInfo)
                    {
                        isAlreadyInThere = true;
                        lokaleCarOwner.Name = carOwner.Name;
                        lokaleCarOwner.DrivingLicenceNumber = carOwner.DrivingLicenceNumber;
                        lokaleCarOwner.CarRegistrationNumber = carOwner.CarRegistrationNumber;
                        lokaleCarOwner.Cars = carOwner.Cars;
                        lokaleCarOwner.CarOwnerMessages = carOwner.CarOwnerMessages;
                        break;
                    }
                }

                if (!isAlreadyInThere)
                {
                    db.CarOwners.AddAsync(carOwner);
                }

                isAlreadyInThere = false;
                foreach (var lokaleCar in db.Cars)
                {
                    if (lokaleCar.LicenceplateNumber == car.LicenceplateNumber)
                    {
                        isAlreadyInThere = true;
                        lokaleCar.Picture = car.Picture;
                        lokaleCar.HaveTowbar = car.HaveTowbar;
                        lokaleCar.Condition = car.Condition;
                        lokaleCar.IsReserved = car.IsReserved;
                        lokaleCar.Weight = car.Weight;
                        lokaleCar.Hight = car.Hight;
                        lokaleCar.Width = car.Width;
                        lokaleCar.Type = car.Type;
                        lokaleCar.Area = car.Area;
                        lokaleCar.PossibleToRentDays = car.PossibleToRentDays;
                        lokaleCar.DaysThatIsRented = car.DaysThatIsRented;
                        lokaleCar.CarRenter = car.CarRenter;
                        lokaleCar.CarOwner = car.CarOwner;
                        break;
                    }
                }

                if (!isAlreadyInThere)
                {
                    db.CarOwners.AddAsync(carOwner);
                }
            }
        }
        
        public void AddDayThatIsRented(DayThatIsRented dayThatIsRented, Car car, CarOwner carOwner)
        {
            bool isAlreadyInThere = false;
            using (var db = new AppDbContext())
            {
                foreach (var lokaleCarOwner in db.CarOwners)
                {
                    if (lokaleCarOwner.ContactInfo == carOwner.ContactInfo)
                    {
                        isAlreadyInThere = true;
                        lokaleCarOwner.Name = carOwner.Name;
                        lokaleCarOwner.DrivingLicenceNumber = carOwner.DrivingLicenceNumber;
                        lokaleCarOwner.CarRegistrationNumber = carOwner.CarRegistrationNumber;
                        lokaleCarOwner.Cars = carOwner.Cars;
                        lokaleCarOwner.CarOwnerMessages = carOwner.CarOwnerMessages;
                        break;
                    }
                }

                if (!isAlreadyInThere)
                {
                    db.CarOwners.AddAsync(carOwner);
                }

                isAlreadyInThere = false;
                foreach (var lokaleCar in db.Cars)
                {
                    if (lokaleCar.LicenceplateNumber == car.LicenceplateNumber)
                    {
                        isAlreadyInThere = true;
                        lokaleCar.Picture = car.Picture;
                        lokaleCar.HaveTowbar = car.HaveTowbar;
                        lokaleCar.Condition = car.Condition;
                        lokaleCar.IsReserved = car.IsReserved;
                        lokaleCar.Weight = car.Weight;
                        lokaleCar.Hight = car.Hight;
                        lokaleCar.Width = car.Width;
                        lokaleCar.Type = car.Type;
                        lokaleCar.Area = car.Area;
                        lokaleCar.PossibleToRentDays = car.PossibleToRentDays;
                        lokaleCar.DaysThatIsRented = car.DaysThatIsRented;
                        lokaleCar.CarRenter = car.CarRenter;
                        lokaleCar.CarOwner = car.CarOwner;
                        break;
                    }
                }

                if (!isAlreadyInThere)
                {
                    db.Cars.AddAsync(car);
                }

                isAlreadyInThere = false;
                foreach (var lokaleDayThatIsRented in db.DaysThatIsRented)
                { 
                    if (lokaleDayThatIsRented.Date == dayThatIsRented.Date)
                    {
                        isAlreadyInThere = true;
                        lokaleDayThatIsRented.CarRenter = dayThatIsRented.CarRenter;
                        lokaleDayThatIsRented.Car = dayThatIsRented.Car;
                        break;
                    }
                }

                if (!isAlreadyInThere)
                {
                    db.DaysThatIsRented.AddAsync(dayThatIsRented);
                }
            }
        }
         
        public void AddPossibleToRentDay(PossibleToRentDay possibleToRentDay, Car car, CarOwner carOwner)
        {
            bool isAlreadyInThere = false;
            using (var db = new AppDbContext())
            {
                foreach (var lokaleCarOwner in db.CarOwners)
                {
                    if (lokaleCarOwner.ContactInfo == carOwner.ContactInfo)
                    {
                        isAlreadyInThere = true;
                        lokaleCarOwner.Name = carOwner.Name;
                        lokaleCarOwner.DrivingLicenceNumber = carOwner.DrivingLicenceNumber;
                        lokaleCarOwner.CarRegistrationNumber = carOwner.CarRegistrationNumber;
                        lokaleCarOwner.Cars = carOwner.Cars;
                        lokaleCarOwner.CarOwnerMessages = carOwner.CarOwnerMessages;
                        break;
                    }
                }

                if (!isAlreadyInThere)
                {
                    db.CarOwners.AddAsync(carOwner);
                }

                isAlreadyInThere = false;
                foreach (var lokaleCar in db.Cars)
                {
                    if (lokaleCar.LicenceplateNumber == car.LicenceplateNumber)
                    {
                        isAlreadyInThere = true;
                        lokaleCar.Picture = car.Picture;
                        lokaleCar.HaveTowbar = car.HaveTowbar;
                        lokaleCar.Condition = car.Condition;
                        lokaleCar.IsReserved = car.IsReserved;
                        lokaleCar.Weight = car.Weight;
                        lokaleCar.Hight = car.Hight;
                        lokaleCar.Width = car.Width;
                        lokaleCar.Type = car.Type;
                        lokaleCar.Area = car.Area;
                        lokaleCar.PossibleToRentDays = car.PossibleToRentDays;
                        lokaleCar.DaysThatIsRented = car.DaysThatIsRented;
                        lokaleCar.CarRenter = car.CarRenter;
                        lokaleCar.CarOwner = car.CarOwner;
                        break;
                    }
                }

                if (!isAlreadyInThere)
                {
                    db.Cars.AddAsync(car);
                }

                isAlreadyInThere = false;
                foreach (var lokalePossibleToRentDay in db.PossibleToRentDays)
                {
                    if (lokalePossibleToRentDay.Date == possibleToRentDay.Date)
                    {
                        isAlreadyInThere = true;
                        lokalePossibleToRentDay.Car = possibleToRentDay.Car;
                        break;
                    }
                }

                if (!isAlreadyInThere)
                {
                    db.PossibleToRentDays.AddAsync(possibleToRentDay);
                }
            }
        }
            // Getting Data

        public List<Car> GetCars()
        {
            List<Car> tempCar = new List<Car>();
            using (var db = new AppDbContext())
            {
                foreach (var car in db.Cars)
                {
                    tempCar.Add(car);
                }
                return tempCar;
            }
        }
        
        public List<CarOwner> GetCarOwners()
        {
            List<CarOwner> tempCarOwner = new List<CarOwner>();
            using (var db = new AppDbContext())
            {
                foreach (var carOwner in db.CarOwners)
                {
                    tempCarOwner.Add(carOwner);
                }
                return tempCarOwner;
            }
        }

        public List<CarOwnerMessage> GetCarOwnerMessages()
        {
            List<CarOwnerMessage> tempCarOwnerMessage = new List<CarOwnerMessage>();
            using (var db = new AppDbContext()) 
            {
                foreach (var carOwnerMessage in db.CarOwnerMessages)
                {
                    tempCarOwnerMessage.Add(carOwnerMessage);
                }
                return tempCarOwnerMessage;
            }
        }
        
        public List<CarRenter> GetCarRenters()
        {
            List<CarRenter> tempCarRenter = new List<CarRenter>();
            using (var db = new AppDbContext())
            {
                foreach (var carRenter in db.CarRenters)
                {
                    tempCarRenter.Add(carRenter);
                }
                return tempCarRenter;
            }
        }
        
        public List<CarRenterMessage> GetCarRenterMessages()
        { 
            List<CarRenterMessage> tempCarRenterMessages = new List<CarRenterMessage>();
            using (var db = new AppDbContext())
            {
                foreach (var renterMessages in db.CarRenterMessages)
                {
                    tempCarRenterMessages.Add(renterMessages);
                }
                return tempCarRenterMessages;
            }
        }
        
        public List<DayThatIsRented> GetDaysThatIsRented()
        {
            List<DayThatIsRented> tempDaysThatIsRented = new List<DayThatIsRented>();
            using (var db = new AppDbContext())
            {
                foreach (var dayThatIsRented in db.DaysThatIsRented)
                {
                    tempDaysThatIsRented.Add(dayThatIsRented);
                }
                return tempDaysThatIsRented;
            }
        }
        
        public List<PossibleToRentDay> GetPossibleToRentDays()
        {
            List<PossibleToRentDay> tempPossibleToRentDays = new List<PossibleToRentDay>();
            using (var db = new AppDbContext())
            { 
                foreach (var possibleToRentDay in db.PossibleToRentDays)
                {
                    tempPossibleToRentDays.Add(possibleToRentDay);
                }
                return tempPossibleToRentDays;
            }
        }
            //Removing data
        public void DeleteCar(string licenceplateNumber)
        {
            using (var db = new AppDbContext())
            {
                foreach (var car in db.Cars)
                {
                    if (car.LicenceplateNumber == licenceplateNumber)
                    {
                        db.Cars.Remove(car);
                        break;
                    }
                }

                db.SaveChanges();
            }
        }

        public void DeleteCarOwner(string contactinfo)
        {
            using (var db = new AppDbContext())
            {
                foreach (var carOwner in db.CarOwners)
                {
                    if (carOwner.ContactInfo == contactinfo)
                    {
                        db.CarOwners.Remove(carOwner);
                        break;
                    }
                }
                db.SaveChanges();
            }
        }
        
        public void DeleteCarOwnerMessage(int carOwnerMessageid) 
        {
            using (var db = new AppDbContext())
            { 
                foreach (var carOwnerMessage in db.CarOwnerMessages)
                {
                    if (carOwnerMessage.CarOwnerMessageid == carOwnerMessageid)
                    {
                        db.CarOwnerMessages.Remove(carOwnerMessage);
                        break;
                    }
                }
                db.SaveChanges();
            }
        }

        public void DeleteCarRenter(string contactInfo)
        {
            using (var db = new AppDbContext())
            {
                foreach (var renter in db.CarRenters)
                {
                    if (renter.ContactInfo == contactInfo)
                    {
                        db.CarRenters.Remove(renter);
                        break;
                    }
                }
                db.SaveChanges();
            }
        }

        public void DeleteCarRenterMessage(int carRenterMessageid)
        {
            using (var db = new AppDbContext())
            {
                foreach (var carRenterMessage in db.CarRenterMessages)
                {
                    if (carRenterMessage.CarRenterMessageid == carRenterMessageid)
                    {
                        db.CarRenterMessages.Remove(carRenterMessage);
                        break;
                    }
                }
                db.SaveChanges();
            }
        }

        public void DeleteDayThatIsRented(DateTime date)
        {
            using (var db = new AppDbContext())
            {
                foreach (var dayThatIsRented in db.DaysThatIsRented)
                {
                    if (dayThatIsRented.Date == date)
                    {
                        db.DaysThatIsRented.Remove(dayThatIsRented);
                        break;
                    }
                }
                db.SaveChanges();
            }
        }

        public void DeletePossibleToRentDay(DateTime date)
        {
            using (var db = new AppDbContext())
            {
                foreach (var possibleToRentDay in db.PossibleToRentDays)
                {
                    if (possibleToRentDay.Date == date)
                    {
                        db.PossibleToRentDays.Remove(possibleToRentDay);
                        break;
                    }
                }
                db.SaveChanges();
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PossibleToRentDay>()
                .HasOne(p => p.Car)
                .WithMany(b => b.PossibleToRentDays);

            modelBuilder.Entity<DayThatIsRented>()
                .HasOne(p => p.Car)
                .WithMany(b => b.DaysThatIsRented);

            modelBuilder.Entity<CarOwnerMessage>()
                .HasOne(p => p.CarOwner)
                .WithMany(b => b.CarOwnerMessages);

            modelBuilder.Entity<CarRenterMessage>()
                .HasOne(p => p.CarRenter)
                .WithMany(b => b.CarRenterMessages);

            modelBuilder.Entity<Car>()
                .HasOne(p => p.CarOwner)
                .WithMany(b => b.Cars);

            modelBuilder.Entity<CarRenterMessage>()
                .HasOne(p => p.CarRenter)
                .WithMany(b => b.CarRenterMessages);

            modelBuilder.Entity<CarOwnerMessage>()
                .HasOne(p => p.CarOwner)
                .WithMany(b => b.CarOwnerMessages);



        }
    }
}
    