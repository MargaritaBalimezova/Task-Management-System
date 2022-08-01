using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Tasks;

namespace TaskManagement.Commands
{
    public class CreateBugCommand : BaseCommand
    {
        public CreateBugCommand(IList<string> parameters, IRepository repository)
           : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < 6)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: 6, Received: {this.CommandParameters.Count}");
            }

            string bugTitle = CommandParameters[0];
            string bugDescription = CommandParameters[1];
            PriorityType bugPriority = ParsePriorityType(CommandParameters[2]);
            Severity bugSeverity = ParseSeverity(CommandParameters[3]);
            IMember bugAssignee = this.Repository.FindMemberByName(CommandParameters[4]);
            IList<string> stepsToReproduce = StepsToReproduce(CommandParameters[5]);

            IBug bug = this.Repository.CreateBug
                (bugTitle, bugDescription, bugPriority, bugSeverity, bugAssignee, stepsToReproduce);

            return $"Bug with Id: {bug.Id} was created!";
        }       

        private IList<string> StepsToReproduce(string steps)
        {
            IList<string> result = steps.Split(';').ToList();
            return result;
        }
    }
}
