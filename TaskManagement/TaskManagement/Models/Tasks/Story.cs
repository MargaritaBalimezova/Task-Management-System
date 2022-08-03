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
        private IMember assignee;
        private Status status;

        public Story(string title, string description, int id, PriorityType priority, SizeType size, IMember assignee)
        :base(title, description, id)
        {
            this.assignee = assignee;
            this.size = size;
            this.priority = priority;

            this.Status = Status.NotDone;
        }

        #region Properties
        public PriorityType Priority
        {
            get
            {
                return this.priority;
            }
        }

        public SizeType Size
        {
            get
            {
                return this.size;
            }
        }

        public IMember Assignee
        {
            get
            {
                return this.assignee;
            }
        }

        public Status Status
        {
            get
            {
                return this.status;
            }
            protected set
            {
                this.status = value;
            }
        }
        #endregion

        #region Methods
        public void ChangeStatus(Status status)
        {
            if(this.Status != status)
            {
                AddEventLog($"Status of Story with ID {this.Id} {this.Title} was changed from {this.Status} to {status}.");
                this.Status = status;
            }
            else
            {
                AddEventLog($"Status of Story with ID {this.Id} {this.Title} is already at {this.Status}.");
            }
        }

        public void ChangePriority(PriorityType priority)
        {
            if (this.Priority != priority)
            {
                AddEventLog($"Status of Story with ID {this.Id} {this.Title} was changed from {this.Priority} to {priority}.");
                this.Status = status;
            }
            else
            {
                AddEventLog($"Status of Story with ID {this.Id} {this.Title} is already at {this.Priority}.");
            }
        }

        public void ChangeSize(SizeType size)
        {
            if (this.Size != size)
            {
                AddEventLog($"Status of Story with ID {this.Id} {this.Title} was changed from {this.Size} to {size}.");
                this.Status = status;
            }
            else
            {
                AddEventLog($"Status of Story with ID {this.Id} {this.Title} is already at {this.Size}.");
            }
        }

        public override string AdditionalInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Assignee: {this.Assignee}");
            sb.AppendLine($"Priority: {this.Priority}");
            sb.AppendLine($"Size: {this.Size}");

            return sb.ToString();
        }
        #endregion
    }
}
