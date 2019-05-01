using System;

namespace CarnGo
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string msg)
            :base(msg)
        {
            
        }
    }
}