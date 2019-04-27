using System;
using System.ComponentModel.DataAnnotations;

namespace CarnGo.Database.Models
{
    public class DayThatIsRented 
    {
        [Key]
        public DateTime Date { get; set; }
        [Required]
        public CarRenter CarRenter { get; set; }
        [Required]
        public Car Car { get; set; }
    }
}
