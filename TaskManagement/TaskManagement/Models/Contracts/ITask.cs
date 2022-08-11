namespace TaskManagement.Models.Contracts
{
    public interface ITask : IHasID, ICommentable, IActivityLog
    {
        public string Title { get; }

        public string Description { get; }

    }
}