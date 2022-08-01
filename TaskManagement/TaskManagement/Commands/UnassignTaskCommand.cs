using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;

namespace TaskManagement.Commands
{
    public class UnassignTaskCommand : BaseCommand
    {
        public UnassignTaskCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < 2)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: 2, Received: {this.CommandParameters.Count}");
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
                return $"Task with id {taskId} was unassigned from {member.Name}";
            }

            throw new ArgumentException($"Task with id {taskId} was not found in the task list of member {member.Name}");
        }
    }
}