using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models.Contracts
{
    internal interface IBug : ITask
    {
        public string Assignee { get; }

        public PriorityType Priority { get; }

        public Severity Severity { get; }
        
        public IList<string> StepsToReproduce { get; }
    }
}
