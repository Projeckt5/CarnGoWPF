using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnGo.Database.Models
{
    public class MessagesWithUsers
    {
        public int MessageId { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public Message Message { get; set; }
        public string UserEmail { get; set; }
    }
}
