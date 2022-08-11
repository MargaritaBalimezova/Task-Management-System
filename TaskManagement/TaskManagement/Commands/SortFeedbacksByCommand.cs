using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Validations;

namespace TaskManagement.Commands
{
    public class SortFeedbacksByCommand : BaseCommand
    {
        private const int ExpectedParamsCount = 1;

        public SortFeedbacksByCommand(IList<string> parameters, IRepository repository)
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

            //[0] - sortBy
            string sortBy = CommandParameters[0];

            if (sortBy == "title")
            {
                feedbacks = this.Repository.Feedbacks.OrderBy(x => x.Title).ToList();
            }
            else if (sortBy == "rating")
            {
                feedbacks = this.Repository.Feedbacks.OrderBy(x => x.Rating).ToList();
            }
            else
            {
                throw new InvalidUserInputException("You can only sort list of feedbacks by title or rating!");
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