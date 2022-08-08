using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using System.Linq;
using TaskManagement.Models.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Validations;

namespace TaskManagement.Commands
{
    public class FilterBugBy : BaseCommand
    {
        private const int ExpectedParamsCount1 = 2;
        private const int ExpectedParamsCount2 = 3;

        public FilterBugBy(IList<string> commandParameters, IRepository repository)
           : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            CommandParametersValidation(base.CommandParameters);

            IEnumerable<IBug> bugs = new List<IBug>();
            switch (CommandParameters[0])
            {
                case "status":
                    bugs  = this.Repository.Bugs.Where(b => b.Status == ParseBugStatus(CommandParameters[1]));
                    break;
                case "assignee":
                    bugs = this.Repository.Bugs.Where(b=>b.Assignee.Name == CommandParameters[1]);
                    break;
                case "statusandassignee":
                    bugs = this.Repository.Bugs.Where(b => b.Status == ParseBugStatus(CommandParameters[1])).
                        Where(b => b.Assignee.Name == CommandParameters[2]);
                    break;
                default:
                    throw new InvalidUserInputException(String.Format(Constants.PARAMETER_DOESNOT_EXIST_ERR_MSG, CommandParameters[0]));

            }

            StringBuilder sb = new StringBuilder();
            foreach (var item in bugs)
            {
                sb.AppendLine(item.ToString());
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
