using System;
using System.ComponentModel.DataAnnotations;

namespace CarnGo.Database.Models
{
    public class DayThatIsRented 
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public CarProfile CarProfile { get; set; }
    }
} 
