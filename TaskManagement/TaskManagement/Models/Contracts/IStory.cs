using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Enums.FeedbackStatus;

namespace TaskManagement.Models.Contracts
{
    public interface IStory
    {
        public Status Status { get; }

        public PriorityType Priority { get; }

        public SizeType Size { get; }

        public IMember Assignee { get; }

    }
}
