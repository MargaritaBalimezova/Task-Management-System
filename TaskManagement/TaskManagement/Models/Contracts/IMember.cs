using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Models.Contracts
{
    public interface IMember
    {
        string Name { get; }
        IList<ITask> Tasks { get; }

        IList<IEventLog> ActivityLog { get; }

        void AddTask(ITask task);

        void RemoveTask(ITask task);

        string ShowActivity();
    }
}