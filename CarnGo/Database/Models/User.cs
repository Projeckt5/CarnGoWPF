using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security;

namespace CarnGo.Database.Models
{
    public class User
    {
        [Key] public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SecureString Password { get; set; }
        public string Adress { get; set; }
        public int UserType { get; set; }
        public Guid AuthorizationString { get; set; }

        public List<CarProfile> Cars { get; set; }
        public List<MessagesWithUsers> MessagesWithUsers { get; set; }
    }
}
