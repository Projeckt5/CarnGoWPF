using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class CarRenter
    {
        [Key]
        public string ContactInfo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string DrivingLicenceNumber { get; set; }
         
        public List<Car> Cars { get; set; }
        public List<CarRenterMessage> CarRenterMessages { get; set; }
    }  
}
