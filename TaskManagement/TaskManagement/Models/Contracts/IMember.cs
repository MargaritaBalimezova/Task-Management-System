using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Models.Contracts
{
    public interface IMember : IActivityLog
    {
        string Name { get; }
        IList<ITask> Tasks { get; }

        void AddTask(ITask task);

        void RemoveTask(ITask task);

        string ShowActivity();
    }
}