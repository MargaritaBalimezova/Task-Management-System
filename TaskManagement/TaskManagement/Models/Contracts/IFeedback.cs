using TaskManagement.Models.Enums.FeedbackStatus;

namespace TaskManagement.Models.Contracts
{
    public interface IFeedback : ITask
    {
        int Rating { get; }
        Status Status { get; }

        void ChangeRating(int number);
    }
}