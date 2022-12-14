using TaskManagement.Models.Enums;
using TaskManagement.Models.Enums.StoryStatus;

namespace TaskManagement.Models.Contracts
{
    public interface IStory : ITask
    {
        public Status Status { get; set; }

        public PriorityType Priority { get; }

        public SizeType Size { get; }

        public IMember Assignee { get; }

        void AddAssignee(IMember assignee);
    }
}