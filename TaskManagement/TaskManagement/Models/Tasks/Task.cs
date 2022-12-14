using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Models.Contracts;
using TaskManagement.Validations;

namespace TaskManagement.Models
{
    public abstract class Task : ITask
    {
        private string title;
        private string description;
        private readonly List<IComment> comments;
        private readonly IList<IEventLog> activityLog;

        public Task(string title, string description, int id)
        {
            this.Title = title;
            this.Description = description;
            this.Id = id;

            comments = new List<IComment>();
            activityLog = new List<IEventLog>();

            AddEventLog(string.Format(Constants.CREATE_TASK_MSG, this.GetType().Name, this.Id));
        }

        #region Properties

        public string Title
        {
            get
            {
                return this.title;
            }
            private set
            {
                Validator.ValidateArgumentIsNotNull(value, "Title");
                Validator.ValidateStringLength(value, Constants.TITLE_MIN_LEN, Constants.TITLE_MAX_LEN, "Title");

                this.title = value;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            private set
            {
                Validator.ValidateArgumentIsNotNull(value, "Description");
                Validator.ValidateStringLength(value, Constants.DESCRIPTION_MIN_LEN, Constants.DESCRIPTION_MAX_LEN, "Description");

                this.description = value;
            }
        }

        public IList<IComment> Comments
        {
            get
            {
                return new List<IComment>(this.comments);
            }
        }

        public IList<IEventLog> ActivityLog
        {
            get
            {
                return new List<IEventLog>(this.activityLog);
            }
        }

        public int Id { get; }

        #endregion Properties

        #region Methods

        public void AddComment(IComment comment)
        {
            comments.Add(comment);
            this.AddEventLog($"Added comment to  task '{this.title}'");
        }

        public void RemoveComment(IComment comment)
        {
            comments.Remove(comment);
            this.AddEventLog($"Removed comment from  task '{this.title}'");
        }

        public string ViewHistory()
        {
            return string.Join(Environment.NewLine, this.activityLog.Select(e => e.ViewInfo()));
        }

        public void AddEventLog(string desc)
        {
            this.activityLog.Add(new EventLog(desc));
        }

        //TODO ChangeStatus

        public abstract string AdditionalInfo();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Constants.SPACES4}--{GetType().Name.ToUpper()}--");
            sb.AppendLine($"{Constants.SPACES4}Id: {this.Id}");
            sb.AppendLine($"{Constants.SPACES4}Title: {this.Title}");
            sb.AppendLine($"{Constants.SPACES4}Description: {this.Description}");
            sb.Append($"{this.AdditionalInfo()}");
            sb.AppendLine($"{this.PrintComments()}");

            return sb.ToString();
        }

        private string PrintComments()
        {
            var sb = new StringBuilder();

            if (this.comments.Count == 0)
            {
                sb.AppendLine($"{Constants.SPACES4}{Constants.NO_COMMENT_HEADER}");
            }
            else
            {
                sb.AppendLine($"{Constants.SPACES4}{Constants.COMMENT_HEADER}");

                foreach (var comment in this.comments)
                {
                    sb.Append(comment.ToString());
                }

                sb.AppendLine($"{Constants.SPACES4}{Constants.COMMENT_HEADER}");

            }

            return sb.ToString();
        }

        #endregion Methods
    }
}