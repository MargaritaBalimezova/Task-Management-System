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
    public class SortStoryBy : BaseCommand
    {
        private const int ExpectedParamsCount = 1;

        public SortStoryBy(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if(this.CommandParameters.Count != ExpectedParamsCount)
            {
                throw new InvalidUserInputException(String.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount, this.CommandParameters.Count));
            }

            var stories = new List<IStory>();

            switch (this.CommandParameters[0].ToLower())
            {
                case "size":
                    stories = this.Repository.Stories.OrderBy(story => story.Size.ToString()).ToList();
                    break;
                case "priority":
                    stories = this.Repository.Stories.OrderBy(story => story.Priority.ToString()).ToList();
                    break;
                case "title":
                    stories = this.Repository.Stories.OrderBy(story => story.Title).ToList();
                    break;
                case "status":
                    stories = this.Repository.Stories.OrderBy(story => story.Status.ToString()).ToList();
                    break;
                default:
                    throw new InvalidUserInputException(String.Format(Constants.PARAMETER_DOESNOT_EXIST_ERR_MSG, CommandParameters[0]));
            }

            var sb = new StringBuilder();

            foreach (var story in stories)
            {
                sb.Append(story.ToString());
                sb.AppendLine("####################");
            }

            return sb.ToString();
        }
    }
}
