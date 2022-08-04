using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Enums.FeedbackStatus;
using TaskManagement.Models.Tasks;

namespace TaskManagement.Commands
{
    public class ChangeFeedbackCommand : BaseCommand
    {
        public ChangeFeedbackCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count != 3)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: 3, Received: {this.CommandParameters.Count}");
            }

            // Parameters:
            //  [0] - taskId
            //  [1] - paramToChange
            //  [2] - newValue

            int taskId = int.Parse(base.CommandParameters[0]);
            string paramToChange = base.CommandParameters[1];

            FeedBack task = (FeedBack)Repository.FindTaskById(taskId);

            if (paramToChange == "status")
            {
                Status status = ParseFeedbackStatus(base.CommandParameters[2]);
                task.ChangeStatus(status);
                return $"Status of task with {task.Id} was changed.";
            }
            else if (paramToChange == "rating")
            {
                int rating = int.Parse(base.CommandParameters[2]);
                task.ChangeRating(rating);
                return $"Rating of task with {task.Id} was changed.";
            }

            throw new ArgumentException("You can change only Feedback Status or Rating!");
        }
    }
}