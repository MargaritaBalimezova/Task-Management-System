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
            if (this.CommandParameters.Count < 5)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: 5, Received: {this.CommandParameters.Count}");
            }

            string bugTitle = base.CommandParameters[0];
            string bugDescription = base.CommandParameters[1];
            PriorityType bugPriority = ParsePriorityType(base.CommandParameters[2]);
            Severity bugSeverity = ParseSeverity(base.CommandParameters[3]);
            IMember bugAssignee = this.Repository.FindMemberByName(base.CommandParameters[4]);
            IList<string> stepsToReproduce = StepsToReproduce();

            IBug bug = this.Repository.CreateBug
                (bugTitle, bugDescription, bugPriority, bugSeverity, bugAssignee, stepsToReproduce);

            return $"Bug with Id: {bug.Id} was created!";
        }       

        private IList<string> StepsToReproduce()
        {
            IList<string> result = new List<string>();
            Console.WriteLine("Please enter step to reproduce the bug without enumerating it and press <Enter> to add new step.");
            Console.WriteLine("When you are ready just type <end>");
            string input = Console.ReadLine().ToLower();

            while (input != "end")
            {
                if (String.IsNullOrEmpty(input)|| String.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("This input can not be empty!Please enter your step again!");
                    input = Console.ReadLine();
                    continue;
                }
                result.Add(input);
                input = Console.ReadLine().ToLower();
            }
            return result;
        }
    }
}
