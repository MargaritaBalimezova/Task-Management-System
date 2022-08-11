using System.Collections.Generic;
using System.Linq;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Tasks;
using TaskManagement.Validations;

namespace TaskManagement.Commands
{
    public class UnassignTaskCommand : BaseCommand
    {
        private const int ExpectedParamsCount = 2;

        public UnassignTaskCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedParamsCount)
            {
                throw new InvalidUserInputException(string.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount, this.CommandParameters.Count));
            }

            // Parameters:
            //  [0] - taskId
            //  [1] - memberName
            int taskId = int.Parse(base.CommandParameters[0]);
            string memberName = base.CommandParameters[1];

            var task = this.Repository.FindTaskById(taskId);
            var member = this.Repository.FindMemberByName(memberName);

            if (member.Tasks.Any(x => x.Id == taskId))
            {
                member.RemoveTask(task);
            }
            else
            {
                throw new EntityNotFoundException($"Task with id {taskId} was not found in the task list of member {member.Name}");
            }

            switch (task.GetType().Name)
            {
                case "Bug":
                    var bug = (Bug)task;
                    bug.RemoveAssignee();
                    break;

                case "Story":
                    var story = (Story)task;
                    story.RemoveAssignee();
                    break;
            }

            return $"Task with id {taskId} was unassigned from {member.Name}";
        }
    }
}