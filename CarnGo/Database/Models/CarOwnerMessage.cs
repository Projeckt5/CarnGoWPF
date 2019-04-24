using System.ComponentModel.DataAnnotations;

namespace Database.Models
{
    public class CarOwnerMessage
    {
        [Key]
        public int CarOwnerMessageid { get; set; }
        public bool HaveBeenRejected { get; set; }
        [Required]
        public bool HaveBeenSeen { get; set; }
        public string Commentary { get; set; }
        public Car Car { get; set; }
        [Required]
        public string RentedFromTo { get; set; }
        [Required]
        public CarOwner CarOwner { get; set; }
    }
}
