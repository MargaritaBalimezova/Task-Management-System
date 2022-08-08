using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Validations;

namespace TaskManagement.Commands
{
    public class CreateStoryCommand : BaseCommand
    {
        private const int ExpectedParamsCount = 4;

        public CreateStoryCommand(IList<string> commandParameters, IRepository repository)
     : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if(this.CommandParameters.Count != ExpectedParamsCount)
            {
                throw new InvalidUserInputException(String.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount, this.CommandParameters.Count));
            }

            //title, description, priority, size, assignee

            string title = this.CommandParameters[0];
            string description = this.CommandParameters[1];
            var priority = ParsePriorityType(this.CommandParameters[2]);
            var size = ParseSize(this.CommandParameters[3]);
           
            var story = this.Repository.CreateStory
                (title, description, priority, size);

            return $"Story with id {story.Id} was created!";
        }
    }
}
