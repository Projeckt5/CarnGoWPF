using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FirstTryProject.Model
{
    public class PossibleToRentDay
    {
        [Key]
        public DateTime Date { get; set; }
        [Required]
        public Car Car { get; set; }
    }
}
