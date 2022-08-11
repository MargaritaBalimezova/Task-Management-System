using System.Collections.Generic;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Enums.BugStatus;

namespace TaskManagement.Models.Contracts
{
    public interface IBug : ITask
    {
        IMember Assignee { get; }

        PriorityType Priority { get; }

        Severity Severity { get; }
        
        IList<string> StepsToReproduce { get; }

        Status Status { get; }

        void ChangeSeverity(Severity newSeverity);

        void ChangePriority(PriorityType newPriority);

        void ChangeStatus(Status newStatus);

        void AddAssignee(IMember assignee);

    }
}
