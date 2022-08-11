using System.Collections.Generic;

namespace TaskManagement.Models.Contracts
{
    public interface IBoard : IActivityLog
    {
        public string Name { get; }

        public List<ITask> BoardTasks { get; }

        void AddTaskToBoard(ITask task);

        void RemoveTaskFromBoard(ITask task);

        string ViewBoardHistory();
    }
}