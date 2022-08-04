using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;

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
                throw new ArgumentException($"Invalid number of arguments. Expected: {ExpectedParamsCount}, Received: {this.CommandParameters.Count}");
            }

            var stories = new List<IStory>();

            switch (this.CommandParameters[0].ToLower())
            {
                case "size":
                    stories = this.Repository.Stories.OrderBy(story => story.Size).ToList();
                    break;
                case "priority":
                    stories = this.Repository.Stories.OrderBy(story => story.Priority).ToList();
                    break;
                case "title":
                    stories = this.Repository.Stories.OrderBy(story => story.Title).ToList();
                    break;
                case "status":
                    stories = this.Repository.Stories.OrderBy(story => story.Status).ToList();
                    break;
                default:
                    throw new ArgumentException("You can sort list of stories only by title, size, priority or status!");
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
