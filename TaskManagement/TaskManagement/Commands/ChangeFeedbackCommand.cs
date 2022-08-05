using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Enums.FeedbackStatus;
using TaskManagement.Models.Tasks;
using TaskManagement.Validations;

namespace TaskManagement.Commands
{
    public class ChangeFeedbackCommand : BaseCommand
    {
        private const int ExpectedParamsCount = 3;

        public ChangeFeedbackCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedParamsCount)
            {
                throw new InvalidUserInputException(string.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount, this.CommandParameters.Count));
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

            throw new InvalidUserInputException("You can change only Feedback Status or Rating!");
        }
    }
}