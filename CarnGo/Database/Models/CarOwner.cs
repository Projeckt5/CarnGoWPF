using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class CarOwner
    {
        [Key]
        public string ContactInfo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string DrivingLicenceNumber { get; set; }
        public string CarRegistrationNumber { get; set; }
        
        public List<Car> Cars { get; set; }
        public List<CarOwnerMessage> CarOwnerMessages { get; set; }
    }
}
