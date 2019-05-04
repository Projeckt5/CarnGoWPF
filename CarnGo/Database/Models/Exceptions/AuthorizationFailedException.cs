using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarnGo.Database
{
    public class AuthorizationFailedException : Exception
    {
        public AuthorizationFailedException()
        {
            
        }

        public AuthorizationFailedException(string msg):
            base(msg)
        {
            
        }
    }
}
