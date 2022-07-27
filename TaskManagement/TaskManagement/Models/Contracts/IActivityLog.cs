using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Models.Contracts
{
    public interface IActivityLog
    {
        public IList<IEventLog> ActivityLog { get; }

        public void AddEventLog(string desc);
    }
}