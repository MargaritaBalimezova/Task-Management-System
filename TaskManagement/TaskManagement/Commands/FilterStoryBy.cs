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
    public class FilterStoryBy : BaseCommand
    {
        private const int ExpectedParamsCount1 = 2;
        private const int ExpectedParamsCount2 = 3;

        public FilterStoryBy(IList<string> commandParameters, IRepository repository)
          : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            CommandParametersValidation(base.CommandParameters);

            var stories = new List<IStory>();

            switch (CommandParameters[0].ToLower())
            {
                case "status":
                    var status = ParseStoryStatus(CommandParameters[1]);
                    stories = this.Repository.Stories.Where(story => story.Status == status).ToList();
                    break;
                case "assignee":
                    var assigneeName = CommandParameters[1];
                    stories = this.Repository.Stories.Where(story => story.Assignee.Name == assigneeName).ToList();
                    break;
                case "statusandassignee":
                    var stat = ParseStoryStatus(CommandParameters[1]);
                    var assignee = CommandParameters[2];
                    stories = this.Repository.Stories.Where(story => story.Status == stat)
                        .Where(story => story.Assignee.Name == assignee).ToList();
                    break;
                default:
                    throw new InvalidUserInputException("You can only filter list of stories by assignee or status!");
            }

            var sb = new StringBuilder();

            foreach (var story in stories)
            {
                sb.Append(story.ToString());
                sb.AppendLine("####################");
            }

            return sb.ToString();
        }

        public void CommandParametersValidation(IList<string> commands)
        {
            switch (commands[0].ToLower())
            {
                case "status":
                case "assignee":
                    if (commands.Count != ExpectedParamsCount1)
                    {
                        throw new InvalidUserInputException(String.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount1, this.CommandParameters.Count));
                    }
                    break;
                case "statusandassignee":
                    if (commands.Count != ExpectedParamsCount2)
                    {
                        throw new InvalidUserInputException(String.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount2, this.CommandParameters.Count));
                    }
                    break;

            }
        }
    }
}
