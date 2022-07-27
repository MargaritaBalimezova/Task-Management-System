using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models.Contracts
{
    public interface IStory
    {
        public PriorityType Priority { get; }

        public SizeType Size { get; }

        public Member Assignee { get; }

    }
}
