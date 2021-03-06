﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarnGo.Database.Models
{
    public class Message 
    {
        [Key]
        public int MessageID { get; set; }
        [Required]
        public bool HaveBeenSeen { get; set; }
        public int ConfirmationStatus { get; set; }
        public string TheMessage { get; set; }
        public string LessorEmail { get; set; }
        public string RenterEmail { get; set; }
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public DateTime CreatedDate { get; set; }
        public int MsgType { get; set; }
        public CarProfile CarProfile { get; set; }
        public string CarProfileRegNr { get; set; }
        [Required]
        public List<MessagesWithUsers> MessagesWithUsers { get; set; }

    }
}
