using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Models;
using TaskManagement.Models.Contracts;
using TaskManagement.Validations;

namespace TaskManagement.Models
{
    public class Board : IBoard
    {
        private string name;
        private readonly List<ITask> boardTasks;
        private readonly IList<IEventLog> activytLog;

        public Board(string name)
        {
            this.Name = name;

            boardTasks = new List<ITask>();
            activytLog = new List<IEventLog>();

            AddEventLog(string.Format(Constants.CREATED_MSG, GetType().Name, this.Name));
        }

        #region Properties

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                Validator.ValidateArgumentIsNotNull(value, "Board name");
                Validator.ValidateStringLength(value, Constants.BOARD_NAME_MIN_LEN, Constants.BOARD_NAME_MAX_LEN, "Board name");

                this.name = value;
            }
        }

        public List<ITask> BoardTasks
        {
            get
            {
                var copy = new List<ITask>(this.boardTasks);
                return copy;
            }
        }

        public IList<IEventLog> ActivityLog
        {
            get
            {
                var copy = new List<IEventLog>(this.activytLog);
                return copy;
            }
        }

        #endregion Properties

        #region Methods

        public void AddEventLog(string desc)
        {
            this.activytLog.Add(new EventLog(desc));
        }

        public void AddTaskToBoard(ITask task)
        {
            Validator.ValidateArgumentIsNotNull(task, "Task");

            if (boardTasks.Contains(task))
            {
                throw new InvalidOperationException($"Task: {task.Title} with ID: {task.Id}  is already on board {this.Name}.");
            }

            this.boardTasks.Add(task);
            AddEventLog(string.Format(Constants.TASK_ADDED_MSG, task.GetType().Name, task.Id, this.Name));
        }

        public void RemoveTaskFromBoard(ITask task)
        {
            if (!boardTasks.Contains(task))
            {
                throw new InvalidOperationException($"Task: {task.Title} with ID: {task.Id} does not exist on board {this.Name}.");
            }

            this.boardTasks.Remove(task);

            AddEventLog(string.Format(Constants.TASK_REMOVE_MSG, task.GetType().Name, task.Id, this.Name));
        }

        public string ViewBoardHistory()
        {
            StringBuilder sb = new StringBuilder();

            foreach (IEventLog item in activytLog)
            {
                sb.AppendLine(item.ViewInfo());
            }

            return sb.ToString();
        }

        public string PrintBoardTasks()
        {
            StringBuilder sb = new StringBuilder();

            if (activytLog.Count == 0)
            {
                sb.AppendLine($"{Constants.SPACES2}{Constants.NO_TASKS_TO_SHOW_HEADER}");
            }
            else
            {
                sb.AppendLine($"{Constants.SPACES2}{Constants.TASK_HEADER}");
                sb.AppendLine();
                foreach (ITask item in boardTasks)
                {
                    sb.AppendLine(item.ToString());
                }
                sb.AppendLine($"{Constants.SPACES2}{Constants.TASK_HEADER}");
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Constants.BOARD_HEADER);
            sb.AppendLine(PrintBoardTasks());
            sb.AppendLine(Constants.BOARD_HEADER);

            return sb.ToString();
        }

        #endregion Methods
    }
}