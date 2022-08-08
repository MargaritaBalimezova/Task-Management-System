using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Enums.BugStatus;
using TaskManagement.Validations;

namespace TaskManagement.Commands
{
    public class ChangeBugCommand : BaseCommand
    {
        public const int  ExpectedParamsCount = 3;

        public ChangeBugCommand(IList<string> parameters, IRepository repository)
           : base(parameters, repository)
        {
        }
          
        public override string Execute()
        {
            if (this.CommandParameters.Count != ExpectedParamsCount)
            {
                throw new InvalidUserInputException(String.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount, this.CommandParameters.Count));
            }

            int taskId = ParseIntParameter(base.CommandParameters[0],"TaskId");
            string paramToChange = base.CommandParameters[1].ToLower();

            IBug bug = (IBug)this.Repository.FindTaskById(taskId);

            switch (paramToChange)
            {
                case "severity":
                    Severity newSeverity = ParseSeverity(base.CommandParameters[2]);
                    bug.ChangeSeverity(newSeverity);
                    return $"Severity of bug with Id: {bug.Id} was changed!";

                case "priority":
                    PriorityType newPriority = ParsePriorityType(base.CommandParameters[2]);
                    bug.ChangePriority(newPriority);
                    return $"Priority of bug with Id: {bug.Id} was changed!";

                case "status":
                    Status newStatus = ParseBugStatus(base.CommandParameters[2]);
                    bug.ChangeStatus(newStatus);
                    return $"Status of bug with Id: {bug.Id} was changed!";

                default:
                    throw new InvalidUserInputException(String.Format(Constants.PARAMETER_DOESNOT_EXIST_ERR_MSG, paramToChange));
            }
        }
    }
}
