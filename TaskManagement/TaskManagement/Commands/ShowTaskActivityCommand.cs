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
                throw new InvalidUserInputException(String.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount, this.CommandParameters.Count));
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
