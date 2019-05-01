using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnGo.Database.Models
{
    public class MessagesWithUsers
    {
        public User User { get; set; }
        public Message Message { get; set; }

    }
}
