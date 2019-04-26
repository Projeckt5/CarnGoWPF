using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security;

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
        [Required]
        public SecureString EmailPassword { get; set; }

        public List<Car> Cars { get; set; }
        public List<CarRenterMessage> CarRenterMessages { get; set; }
    }  
}
