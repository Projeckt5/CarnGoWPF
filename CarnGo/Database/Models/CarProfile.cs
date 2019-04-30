using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CarnGo.Database.Models
{
    
    public class CarProfile
    {
        [Key]
        public string RegNr { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public int Seats { get; set; }
        public int Price { get; set; }
        //public BitmapImage CarPicture { get; set; }
        public int RentalPrice { get; set; }
        public string FuelType { get; set; }
        public string CarDescription { get; set; }
        //private UserModel _owner;

        public CarEquipment Equipment { get; set; }
        public List<PossibleToRentDay> PossibleToRentDays { get; set; }
        public List<DayThatIsRented> DaysThatIsRented { get; set; }
        

    }
    
}