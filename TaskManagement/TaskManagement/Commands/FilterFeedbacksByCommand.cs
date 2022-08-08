using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums.FeedbackStatus;
using TaskManagement.Validations;

namespace TaskManagement.Commands
{
    public class FilterFeedbacksByCommand : BaseCommand
    {
        private const int ExpectedParamsCount = 2;

        public FilterFeedbacksByCommand(IList<string> parameters, IRepository repository)
      : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count != ExpectedParamsCount)
            {
                throw new InvalidUserInputException(String.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount, this.CommandParameters.Count));
            }

            IList<IFeedback> feedbacks = new List<IFeedback>();
            //[0] - filterBy
            string filterBy = CommandParameters[0];

            if (filterBy == "status")
            {
                //[1] - typeOfStatus
                Status valueToFilterBy = ParseFeedbackStatus(base.CommandParameters[1]);

                feedbacks = this.Repository.Feedbacks.Where(x => x.Status == valueToFilterBy).ToList();
            }
            else
            {
                throw new InvalidUserInputException("You can only filter list of feedbacks by status!");
            }

            var sb = new StringBuilder();

            foreach (var item in feedbacks)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }
    }
}