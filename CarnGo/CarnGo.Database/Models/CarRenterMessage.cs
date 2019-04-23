using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FirstTryProject.Model
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
    }
}
