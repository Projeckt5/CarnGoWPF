using System;

namespace CarnGo.Security
{
    public class ValidationException : Exception
    {
        public ValidationException()
        { }

        public ValidationException(string msg)
            : base(msg)
        { }
    }
}