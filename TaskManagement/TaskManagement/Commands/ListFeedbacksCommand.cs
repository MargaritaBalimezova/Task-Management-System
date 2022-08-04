using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums.FeedbackStatus;

namespace TaskManagement.Commands
{
    public class ListFeedbacksCommand : BaseCommand
    {
        public ListFeedbacksCommand(IList<string> parameters, IRepository repository)
       : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            IList<IFeedback> feedbacks = new List<IFeedback>();

            //[0] - filter or sort

            string typeOfOperation = CommandParameters[0];

            if (typeOfOperation == "filter")
            {
                if (this.CommandParameters.Count != 3)
                {
                    throw new ArgumentException($"Invalid number of arguments. Expected: 3, Received: {this.CommandParameters.Count}");
                }

                //[1] - filterBy
                string filterBy = CommandParameters[1];

                if (filterBy == "status")
                {
                    //[2] - typeOfStatus
                    Status valueToFilterBy = ParseFeedbackStatus(base.CommandParameters[2]);

                    feedbacks = this.Repository.Feedbacks.Where(x => x.Status == valueToFilterBy).ToList();
                }
                else
                {
                    throw new ArgumentException("You can only filter list of feedbacks by status!");
                }
            }
            else if (typeOfOperation == "sort")
            {
                if (this.CommandParameters.Count != 2)
                {
                    throw new ArgumentException($"Invalid number of arguments. Expected: 2, Received: {this.CommandParameters.Count}");
                }
                //[1] - sortBy
                string sortBy = CommandParameters[1];

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
                    throw new ArgumentException("You can only sort list of feedbacks by title or rating!");
                }
            }
            else
            {
                throw new ArgumentException("You can only filter or sort list of feedbacks!");
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