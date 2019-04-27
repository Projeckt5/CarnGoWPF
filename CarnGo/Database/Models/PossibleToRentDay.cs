﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CarnGo.Database.Models
{
    public class PossibleToRentDay
    {
        [Key]
        public DateTime Date { get; set; }
        [Required]
        public Car Car { get; set; }
    }
}
