using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums.FeedbackStatus;

namespace TaskManagement.Commands
{
    public class FilterFeedbacksByCommand : BaseCommand
    {
        public FilterFeedbacksByCommand(IList<string> parameters, IRepository repository)
      : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count != 2)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 2, Received: {this.CommandParameters.Count}");
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