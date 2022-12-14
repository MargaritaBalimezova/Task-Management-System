using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Models.Contracts;
using TaskManagement.Validations;

namespace TaskManagement.Models
{
    public class Member : IMember
    {
        private string name;

        private readonly IList<ITask> tasks;
        private readonly IList<IEventLog> activityLog;

        public Member(string name)
        {
            this.Name = name;

            this.tasks = new List<ITask>();
            this.activityLog = new List<IEventLog>();

            AddEventLog(string.Format(Constants.CREATED_MSG, this.GetType().Name, this.Name));
        }

        #region Properties

        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                Validator.ValidateArgumentIsNotNull(value, "Member name");
                Validator.ValidateStringLength(value, Constants.MEMBER_NAME_MIN_LENGTH, Constants.MEMBER_NAME_MAX_LENGTH, "Member name");

                this.name = value;
            }
        }

        public IList<ITask> Tasks
        {
            get { return new List<ITask>(this.tasks); }
        }

        public IList<IEventLog> ActivityLog
        {
            get { return new List<IEventLog>(this.activityLog); }
        }

        #endregion Properties

        #region Methods

        public void AddTask(ITask task)
        {
            Validator.ValidateArgumentIsNotNull(task, "Task");
            this.tasks.Add(task);
            AddEventLog(($"{task.GetType().Name} '{task.Title}' with ID: {task.Id} was added to member '{this.Name}'"));
        }

        public void RemoveTask(ITask task)
        {
            Validator.ValidateArgumentIsNotNull(task, "Task");
            this.tasks.Remove(task);
            AddEventLog(($"{task.GetType().Name} '{task.Title}' with ID: {task.Id} was removed from member '{this.Name}'"));
        }

        public string ShowActivity()
        {
            return string.Join(Environment.NewLine, this.activityLog.Select(e => e.ViewInfo()));
        }

        public void AddEventLog(string desc)
        {
            this.activityLog.Add(new EventLog(desc));
        }

        private string PrintTasks()
        {
            var sb = new StringBuilder();

            if (this.tasks.Count == 0)
            {
                sb.AppendLine($"{Constants.SPACES2}{Constants.NO_TASK_HEADER}");
            }
            else
            {
                sb.AppendLine($"{Constants.SPACES2}{Constants.TASK_HEADER}");
                sb.AppendLine();

                foreach (var task in this.tasks)
                {
                    sb.Append(task.ToString());
                }

                sb.AppendLine($"{Constants.SPACES2}{Constants.TASK_HEADER}");
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(Constants.MEMBER_HEADER);
            sb.AppendLine($"{Constants.SPACES2}Member Name: {this.Name}");
            sb.AppendLine($"{this.PrintTasks()}");
            sb.AppendLine(Constants.MEMBER_HEADER);

            return sb.ToString();
        }

        #endregion Methods
    }
}