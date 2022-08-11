using System;

namespace TaskManagement.Exceptions
{
    public class NameExistsException : ApplicationException
    {
        public NameExistsException(string message)
            : base(message)
        {

        }
    }
}
