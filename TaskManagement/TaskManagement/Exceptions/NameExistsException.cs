using System;
using System.Collections.Generic;
using System.Text;

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
