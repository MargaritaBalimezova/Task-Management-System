using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Commands
{
    public class FilterStoryBy : BaseCommand
    {
        private const int ExpectedParamsCount = 3;

        public FilterStoryBy(IList<string> commandParameters, IRepository repository)
          : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
         /*   if(this.CommandParameters.Count < ExpectedParamsCount || 
                this.CommandParameters.Count != ExpectedParamsCount)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: {ExpectedParamsCount}, Received: {this.CommandParameters.Count}");
            }*/

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
                    throw new ArgumentException("You can only filter list of stories by assignee or status!");
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
