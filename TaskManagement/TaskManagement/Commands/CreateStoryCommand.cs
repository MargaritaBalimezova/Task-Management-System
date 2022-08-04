using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;

namespace TaskManagement.Commands
{
    public class CreateStoryCommand : BaseCommand
    {
        private const int ExpectedParamsCount = 5;

        public CreateStoryCommand(IList<string> commandParameters, IRepository repository)
     : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if(this.CommandParameters.Count != ExpectedParamsCount)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: {ExpectedParamsCount}, Received: {this.CommandParameters.Count}");
            }

            //title, description, priority, size, assignee

            string title = this.CommandParameters[0];
            string description = this.CommandParameters[1];
            var priority = ParsePriorityType(this.CommandParameters[2]);
            var size = ParseSize(this.CommandParameters[3]);
            var asssignee = this.Repository.FindMemberByName(this.CommandParameters[4]);

            var story = this.Repository.CreateStory
                (title, description, priority, size, asssignee);

            return $"Story with id {story.Id} was created!";
        }
    }
}
