using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Enums.StoryStatus;

namespace TaskManagement.Models.Tasks
{
    public class Story : Task, IStory
    {
        private PriorityType priority;
        private SizeType size;
        private IMember assignee = null;
        private Status status= Status.NotDone;

        public Story(string title, string description, int id, PriorityType priority, SizeType size)
        : base(title, description, id)
        {
            this.size = size;
            this.priority = priority;
        }

        #region Properties

        public PriorityType Priority
        {
            get
            {
                return this.priority;
            }
            set
            {
                if (this.Priority != value)
                {
                    AddEventLog($"Status of Story with ID {this.Id} {this.Title} was changed from {this.Priority} to {priority}.");
                    this.priority = value;
                }
                else
                {
                    AddEventLog($"Status of Story with ID {this.Id} {this.Title} is already at {this.Priority}.");
                }
            }
        }

        public SizeType Size
        {
            get
            {
                return this.size;
            }
            set
            {
                if (this.Size != value)
                {
                    AddEventLog($"Status of Story with ID {this.Id} {this.Title} was changed from {this.Size} to {value}.");
                    this.size = value;
                }
                else
                {
                    AddEventLog($"Status of Story with ID {this.Id} {this.Title} is already at {this.Size}.");
                }
            }
        }

        public IMember Assignee
        {
            get
            {
                return this.assignee;
            }
            private set
            {
                Validator.ValidateArgumentIsNotNull(value, "Assignee");
                this.assignee = value;
            }
        }

        public Status Status
        {
            get
            {
                return this.status;
            }
            set
            {
                if (this.Status != value)
                {
                    AddEventLog($"Status of Story with ID {this.Id} {this.Title} was changed from {this.Status} to {value}.");
                    this.status = value;
                }
                else
                {
                    AddEventLog($"Status of Story with ID {this.Id} {this.Title} is already at {this.Status}.");
                }
            }
        }

        #endregion Properties

        #region Methods

        public void AddAssignee(IMember assignee)
        {
            this.Assignee = assignee;
        }

        public void RemoveAssignee()
        {
            this.assignee = null;
        }

        public override string AdditionalInfo()
        {
            StringBuilder sb = new StringBuilder();
            if (assignee == null)
            {
                sb.AppendLine($"Assignee: No assignee");
            }
            else
            {
                sb.AppendLine($"Assignee: {this.Assignee.Name}");
            }
            sb.AppendLine($"Priority: {this.Priority}");
            sb.AppendLine($"Size: {this.Size}");
            sb.AppendLine($"Status: {this.Status}");

            return sb.ToString();
        }

        #endregion Methods
    }
}