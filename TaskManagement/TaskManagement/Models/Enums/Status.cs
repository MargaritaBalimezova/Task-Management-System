using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Models.Enums.StoryStatus
{
    public enum Status
    {
        NotDone,
        InProgress,
        Done
    }
}

namespace TaskManagement.Models.Enums.FeedbackStatus
{
    public enum Status
    {
        New, 
        Unscheduled,
        Scheduled,
        Done
    }
}

namespace TaskManagement.Models.Enums.BugStatus
{
    public enum Status
    {
        Active,
        Fixed
    }
}