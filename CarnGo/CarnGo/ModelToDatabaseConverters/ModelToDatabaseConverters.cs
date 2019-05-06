using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarnGo.Database.Models;

namespace CarnGo
{
    public static class ModelToDatabaseConverters
    {
        public static CarProfile CarProfileModelToCarProfil(CarProfileModel model)
        {
            var newModel = new CarProfile();
            var carEquip=new CarEquipment();
            newModel.CarEquipment = new Database.Models.CarEquipment();
            newModel.Age = model.Age;
            newModel.Brand = model.Brand;
            newModel.CarDescription = model.CarDescription;
            newModel.FuelType = model.FuelType;
            newModel.Location = model.Location;
            newModel.Model = model.Model;
            newModel.Price = model.Price;
            
            return newModel;
        }

        public static CarProfileModel CarProfileToCarProfileModelForSendRequestViewModel(CarProfile model)
        {
            var newModel = new CarProfileModel();
            var newOwner=new UserModel(){Address = model.User.Adress,
                Email =model.User.Email,
                Firstname = model.User.FirstName,
                Lastname = model.User.LastName,

            };
            newModel.DayThatIsRented = model.DaysThatIsRented;
            newModel.PossibleToRentDays = model.PossibleToRentDays;
            newModel.Brand = model.Brand;
            newModel.CarDescription = model.CarDescription;
            newModel.FuelType = model.FuelType;
            newModel.Location = model.Location;
            newModel.Model = model.Model;
            newModel.Price = model.Price;
            newModel.Age = model.Age;
            newModel.RentalPrice = model.RentalPrice;
            newModel.Seats = model.Seats;
            
            newModel.Owner = newOwner;


            return newModel;
        }

    }
}
