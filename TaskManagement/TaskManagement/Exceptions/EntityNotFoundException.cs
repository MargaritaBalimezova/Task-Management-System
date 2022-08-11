using System;

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
