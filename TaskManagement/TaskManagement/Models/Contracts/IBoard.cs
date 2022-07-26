using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Models.Contracts
{
    internal interface IBoard 
    {
        public string Name { get; }

        public List<ITask> BoardTasks { get; }

        public List<IEventLog> BoardHistory { get; }

        void AddTaskToBoard(ITask task);

        void RemoveTaskFromBoard(ITask task);

        string ViewBoardHistory();

    }
}
