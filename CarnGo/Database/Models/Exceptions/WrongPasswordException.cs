using System;

namespace CarnGo.Database
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException()
        {

        }

        public WrongPasswordException(string msg) :
            base(msg)
        {

        }
    }
}