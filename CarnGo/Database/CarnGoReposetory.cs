using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;

namespace Database
{
    class CarnGoReposetory
    {
        private AppDbContext context;
        public CarnGoReposetory()
        {
            context = new AppDbContext();;
        }


        //Adding Data
        public void AddCarRenter(CarRenter carRenter)
        {
            context.AddCarRenter(carRenter);
        }

        public void AddCarRenterMessage(CarRenterMessage carRenterMessage, CarRenter carRenter)
        {
            context.AddCarRenterMessage(carRenterMessage, carRenter);
        }


        public void AddCarOwner(CarOwner carOwner)
        {
            context.AddCarOwner(carOwner);
        }

        public void AddCarOwnerMessage(CarOwnerMessage carOwnerMessage, CarOwner carOwner)
        {
            context.AddCarOwnerMessage(carOwnerMessage, carOwner);
        }

        public void AddCar(Car car, CarOwner carOwner)
        {
            context.AddCar(car, carOwner);
        }

        public void AddDayThatIsRented(DayThatIsRented dayThatIsRented, Car car, CarOwner carOwner)
        {
            context.AddDayThatIsRented(dayThatIsRented, car, carOwner);
        }

        public void AddPossibleToRentDay(PossibleToRentDay possibleToRentDay, Car car, CarOwner carOwner)
        {
            context.AddPossibleToRentDay(possibleToRentDay, car, carOwner);
        }
        // Getting Data

        public List<Car> GetCars()
        {
            return context.GetCars();
        }

        public List<CarOwner> GetCarOwners()
        {
            return context.GetCarOwners();
        }

        public List<CarOwnerMessage> GetCarOwnerMessages()
        {
            return context.GetCarOwnerMessages();
        }

        public List<CarRenter> GetCarRenters()
        {
            return context.GetCarRenters();
        }

        public List<CarRenterMessage> GetCarRenterMessages()
        {
            return context.GetCarRenterMessages();
        }

        public List<DayThatIsRented> GetDaysThatIsRented()
        {
            return context.GetDaysThatIsRented();
        }

        public List<PossibleToRentDay> GetPossibleToRentDays()
        {
            return context.GetPossibleToRentDays();
        }



        //Removing data
        public void DeleteCar(string licenceplateNumber)
        {
            context.DeleteCar(licenceplateNumber);
        }

        public void DeleteCarOwner(string contactinfo)
        {
            context.DeleteCarOwner(contactinfo);
        }

        public void DeleteCarOwnerMessage(int carOwnerMessageid)
        {
            context.DeleteCarOwnerMessage(carOwnerMessageid);
        }

        public void DeleteCarRenter(string contactInfo)
        {
            context.DeleteCarRenter(contactInfo);
        }

        public void DeleteCarRenterMessage(int carRenterMessageid)
        {
            context.DeleteCarRenterMessage(carRenterMessageid);
        }

        public void DeleteDayThatIsRented(DateTime date)
        {
            context.DeleteDayThatIsRented(date);
        }

        public void DeletePossibleToRentDay(DateTime date)
        {
            context.DeletePossibleToRentDay(date);
        }

    }
}
