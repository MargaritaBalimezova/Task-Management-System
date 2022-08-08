using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Tasks;
using TaskManagement.Validations;

namespace TaskManagement.Commands
{
    public class SortBugBy : BaseCommand
    {
        private const int ExpectedParamsCount = 1;

        public SortBugBy(IList<string> parameters, IRepository repository)
           : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count != ExpectedParamsCount)
            {
                throw new InvalidUserInputException(String.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount, this.CommandParameters.Count));
            }

            IEnumerable<IBug> bugs = new List<IBug>();
            switch (CommandParameters[0].ToLower())
            {
                case "title":
                    bugs = this.Repository.Bugs.OrderBy(x => x.Title);
                    break;
                case "priority":
                    bugs = this.Repository.Bugs.OrderBy(x => x.Priority);
                    break;
                case "severity":
                    bugs = this.Repository.Bugs.OrderBy(x => x.Severity);
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
    }
}
