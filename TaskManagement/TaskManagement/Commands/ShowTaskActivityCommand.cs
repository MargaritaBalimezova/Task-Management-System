using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Commands
{
    public class ShowTaskActivityCommand : BaseCommand
    {
        private const int ExpectedParamsCount = 1;

        public ShowTaskActivityCommand(IList<string> parameters, IRepository repository)
        : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count != ExpectedParamsCount)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: {ExpectedParamsCount}, received: {this.CommandParameters.Count}");
            }

            int taskId = ParseIntParameter(CommandParameters[0], "TaskId");
            ITask task = this.Repository.FindTaskById(taskId);

            StringBuilder sb = new StringBuilder();

            foreach (var item in task.ActivityLog)
            {
                sb.AppendLine(item.ViewInfo());
            }

            return sb.ToString();
        }
    }
}
