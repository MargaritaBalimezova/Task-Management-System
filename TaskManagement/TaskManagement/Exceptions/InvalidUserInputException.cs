using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Exceptions
{
    public class InvalidUserInputException : ApplicationException
    {
        public InvalidUserInputException(string message)
            : base(message)
        {
        }
    }
}