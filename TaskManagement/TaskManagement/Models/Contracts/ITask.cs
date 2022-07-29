using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Models.Enums.BugStatus;
using TaskManagement.Models.Enums.FeedbackStatus;
using TaskManagement.Models.Enums.StoryStatus;

namespace TaskManagement.Models.Contracts
{
    public interface ITask : IHasID, ICommentable, IActivityLog
    {
        public string Title { get; }

        public string Description { get; }

    }
}