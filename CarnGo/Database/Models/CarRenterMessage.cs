using System.ComponentModel.DataAnnotations;

namespace CarnGo.Database.Models
{
    public class CarRenterMessage 
    {
        [Key]
        public int CarRenterMessageid { get; set; }
        public string ContactInfo { get; set; }
        [Required]
        public bool HaveBeenSeen { get; set; }
        public string Commentary { get; set; }
        [Required]
        public Car Car { get; set; }
        public string RentedFromTo { get; set; }
        [Required]
        public CarRenter CarRenter { get; set; }
        [Required]
        public int Recipientid { get; set; }
    }
}
