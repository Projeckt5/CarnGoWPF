using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FirstTryProject.Model
{
    
    public class Car
    {
        [Key]
        public string LicenceplateNumber { get; set; }
        public string Picture { get; set; }
        public bool HaveTowbar { get; set; }
        [Required]
        public string Condition { get; set; }
        [Required]
        public bool IsReserved { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public int Hight { get; set; }
        [Required]
        public int Width { get; set; }
        [Required]
        public string Type { get; set; }
        public string Area { get; set; }
        
        public List<PossibleToRentDay> PossibleToRentDays { get; set; }
        public List<DayThatIsRented> DaysThatIsRented { get; set; }
        
        public CarRenter CarRenter { get; set; }
        [Required]
        public CarOwner CarOwner { get; set; }
    }
    
} 