using System.Collections.Generic;

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