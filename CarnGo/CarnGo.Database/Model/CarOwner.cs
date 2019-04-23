using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FirstTryProject.Model
{
    public class CarOwner
    {
        [Key]
        public string KontactInfo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string DrivingLicenceNumber { get; set; }
        public string CarRegistrationNumber { get; set; }
        
        public List<Car> Cars { get; set; }
        public List<CarOwnerMessage> CarOwnerMessages { get; set; }
    }
}
