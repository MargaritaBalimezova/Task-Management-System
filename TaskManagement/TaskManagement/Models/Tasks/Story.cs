using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models.Tasks
{
    public class Story : Task, IStory
    {
        private PriorityType priority;
        private SizeType size;
        private Member assignee;

        public Story(string title, string description, PriorityType priority, SizeType size, string assignee)
        :base(title, description)
        {
            this.assignee = new Member(assignee);
            this.size = size;
            this.priority = priority;
        }

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

        public Member Assignee
        {
            get
            {
                return this.assignee;
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
    }
}
