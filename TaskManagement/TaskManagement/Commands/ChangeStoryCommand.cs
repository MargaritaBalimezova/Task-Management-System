using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Enums.StoryStatus;
using TaskManagement.Models.Tasks;

namespace TaskManagement.Commands
{
    public class ChangeStoryCommand : BaseCommand
    {
        public const int ExpectedParamsCount = 3;

        public ChangeStoryCommand(IList<string> parameters, IRepository repository)
           : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            if(this.CommandParameters.Count != ExpectedParamsCount)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: {ExpectedParamsCount}, Received: {this.CommandParameters.Count}");
            }

            var taskId = ParseIntParameter(this.CommandParameters[0], "Task Id");
            var paramToChange = this.CommandParameters[1].ToLower();

            var story = (Story)this.Repository.FindTaskById(taskId);

            switch (paramToChange)
            {
                case "priority":
                    var priority = ParsePriorityType(this.CommandParameters[2]);
                    story.Priority = priority;
                    return $"Priority of story with id {story.Id} was changed!";
                case "size":
                    var size = ParseSize(CommandParameters[2]);
                    story.Size = size;
                    return $"Size of story with id {story.Id} was changed!";
                case "status":
                    Status newStatus = ParseStoryStatus(CommandParameters[2]);
                    story.ChangeStatus(newStatus);
                    return $"Priority of story with id {story.Id} was changed!";
                default:
                    throw new ArgumentException($"Parameter with name {paramToChange} does not exist!");
            }
        }
    }
}
