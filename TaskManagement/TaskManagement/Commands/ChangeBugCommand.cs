using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Enums.BugStatus;

namespace TaskManagement.Commands
{
    public class ChangeBugCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 3;

        public ChangeBugCommand(IList<string> parameters, IRepository repository)
           : base(parameters, repository)
        {
        }
          
        public override string Execute()
        {
            if (this.CommandParameters.Count < 3)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: 3, Received: {this.CommandParameters.Count}");
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
                    throw new ArgumentException($"Parameter with name {paramToChange} does not exist!");
            }
        }
    }
}
