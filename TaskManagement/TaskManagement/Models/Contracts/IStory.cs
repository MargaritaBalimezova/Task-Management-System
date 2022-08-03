using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Enums.StoryStatus;

namespace TaskManagement.Models.Contracts
{
    public interface IStory : ITask
    {
        public Status Status { get; }

        public PriorityType Priority { get; }

        public SizeType Size { get; }

        public IMember Assignee { get; }
    }
}