using System;
using System.Collections.Generic;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Validations;

namespace TaskManagement.Commands
{
    public class CreateFeedbackCommand : BaseCommand
    {
        private const int ExpectedParamsCount = 3;

        public CreateFeedbackCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedParamsCount)
            {
                throw new InvalidUserInputException(String.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount, this.CommandParameters.Count));
            }

            // Parameters:
            //  [0] - title
            //  [1] - description
            //  [2] - rating
            string title = base.CommandParameters[0];
            string description = base.CommandParameters[1];
            int rating = int.Parse(base.CommandParameters[2]);

            var feedback = this.Repository.CreateFeedBack(title, description, rating);

            return $"Feedback with title {title} and id {feedback.Id} was created.";
        }
    }
}