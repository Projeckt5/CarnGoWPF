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
        [Key]
        public int MessageId { get; set; }
        public User User { get; set; }
        public Message Message { get; set; }
        public string UserEmail { get; set; }
    }
}
