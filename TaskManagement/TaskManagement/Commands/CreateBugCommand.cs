using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Tasks;
using TaskManagement.Validations;

namespace TaskManagement.Commands
{
    public class CreateBugCommand : BaseCommand
    {
        private const int ExpectedParamsCount = 4;

        public CreateBugCommand(IList<string> parameters, IRepository repository)
           : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count != ExpectedParamsCount)
            {
                throw new InvalidUserInputException(String.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount, this.CommandParameters.Count));
            }

            string bugTitle = base.CommandParameters[0];
            string bugDescription = base.CommandParameters[1];
            PriorityType bugPriority = ParsePriorityType(base.CommandParameters[2]);
            Severity bugSeverity = ParseSeverity(base.CommandParameters[3]);            
            IList<string> stepsToReproduce = StepsToReproduce();

            IBug bug = this.Repository.CreateBug
                (bugTitle, bugDescription, bugPriority, bugSeverity, stepsToReproduce);

            return $"Bug with Id: {bug.Id} was created!";
        }       

        private IList<string> StepsToReproduce()
        {
            IList<string> result = new List<string>();

           
            Console.WriteLine("Please enter step to reproduce the bug without enumerating it and press <Enter> to add new step.");
            Console.WriteLine("When you are ready just type <end>");
            

            while (true)
            {
                string input = Console.ReadLine().ToLower();

                if (String.IsNullOrEmpty(input)|| String.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("This input can not be empty!Please enter your step again!");
                    continue;
                }
                if (result.Count == 0 && input == "end")
                {
                    Console.WriteLine("You must provide steps to reproduce before ending the program!");
                    continue;
                }
               
                    result.Add(char.ToUpper(input[0]) + input.Substring(1));

                if (input == "end")
                {
                    break;
                }
               
            }
            return result;
        }
    }
}
