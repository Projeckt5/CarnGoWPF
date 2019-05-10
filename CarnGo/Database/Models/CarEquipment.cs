using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security;


namespace CarnGo.Database.Models
{
    public class CarEquipment
    {
        [Key]
        public int CarEquipmentID { get; set; }
        public bool Smoking { get; set; }
        public bool Audioplayer { get; set; }
        public bool GPS { get; set; }
        public bool Childseat { get; set; }
        public string CarProfileId { get; set; }
        [Required]
        public CarProfile CarProfile { get; set; }
    }
    
}
