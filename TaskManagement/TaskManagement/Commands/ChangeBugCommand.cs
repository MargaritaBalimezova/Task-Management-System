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

            string paramToChange = CommandParameters[0].ToLower();
            int taskId = ParseIntParameter(CommandParameters[2],"TaskId");

            IBug bug = (IBug)this.Repository.FindTaskById(taskId);

            switch (paramToChange)
            {
                case "severity":
                    Severity newSeverity = ParseSeverity(CommandParameters[1]);
                    bug.ChangeSeverity(newSeverity);
                    return $"Severity of bug with Id: {bug.Id} was changed!";

                case "priority":
                    PriorityType newPriority = ParsePriorityType(CommandParameters[1]);
                    bug.ChangePriority(newPriority);
                    return $"Priority of bug with Id: {bug.Id} was changed!";

                case "status":
                    Status newStatus = ParseBugStatus(CommandParameters[1]);
                    bug.ChangeStatus(newStatus);
                    return $"Status of bug with Id: {bug.Id} was changed!";

                default:
                    throw new ArgumentException($"Parameter with name {paramToChange} does not exist!");
            }
        }
    }
}
