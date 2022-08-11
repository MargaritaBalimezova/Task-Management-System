using System.Collections.Generic;

namespace TaskManagement.Models.Contracts
{
    public interface IActivityLog
    {
        public IList<IEventLog> ActivityLog { get; }

        public void AddEventLog(string desc);
    }
}