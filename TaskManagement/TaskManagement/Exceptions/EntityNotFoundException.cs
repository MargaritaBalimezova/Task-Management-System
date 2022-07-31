using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Exceptions
{
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException(string msg)
        :base(msg)
        {

        }
    }
}
