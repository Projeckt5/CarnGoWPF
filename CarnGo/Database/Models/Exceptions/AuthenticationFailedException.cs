﻿using System;

namespace CarnGo
{
    public class AuthenticationFailedException : Exception
    {
        public AuthenticationFailedException()
        {
            
        }
        public AuthenticationFailedException(string msg = "The Email or Password was wrong")
            :base(msg)
        {
            
        }
    }
}