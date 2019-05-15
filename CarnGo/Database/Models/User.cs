using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security;

namespace CarnGo.Database.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int UserType { get; set; }
        public Guid AuthorizationString { get; set; }
        public string UserPhoto { get; set; }
        public List<CarProfile> Cars { get; set; }
        public List<MessagesWithUsers> MessagesWithUsers { get; set; }
    }
}
