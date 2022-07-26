using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Models
{
    public class Member : IMember
    {
        private const int MEMBER_NAME_MIN_LENGTH = 5;
        private const int MEMBER_NAME_MAX_LENGTH = 15;

        private const string MEMBER_CREATED_MSG = "Successfuly created {0} with name {1}!";
        private const string NO_TASK_HEADER = "--NO TASKS--";
        private const string TASK_HEADER = "--TASKS--";
        private const string MEMBER_HEADER = "--MEMBER--";

        private string name;
        private readonly IList<ITask> tasks;

        private readonly IList<IEventLog> activityLog;

        public Member(string name)
        {
            this.Name = name;

            this.tasks = new List<ITask>();
            this.activityLog = new List<IEventLog>();

            AddEventLog(string.Format(MEMBER_CREATED_MSG, this.GetType().Name, this.Name));
        }

        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                Validator.ValidateArgumentIsNotNull(value, "Title");
                Validator.ValidateStringLength(value, MEMBER_NAME_MIN_LENGTH, MEMBER_NAME_MAX_LENGTH, "Title");

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

        protected void AddEventLog(string desc)
        {
            this.activityLog.Add(new EventLog(desc));
        }

        private string PrintTasks()
        {
            var sb = new StringBuilder();

            if (this.tasks.Count == 0)
            {
                sb.AppendLine(NO_TASK_HEADER);
            }
            else
            {
                sb.AppendLine(TASK_HEADER);

                foreach (var task in this.tasks)
                {
                    sb.Append(task.ToString());
                }

                sb.AppendLine(TASK_HEADER);
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(MEMBER_HEADER);
            sb.AppendLine($"Member Name: {this.Name}");
            sb.AppendLine($"  {this.PrintTasks()}");
            sb.AppendLine(MEMBER_HEADER);

            return sb.ToString();
        }
    }
}