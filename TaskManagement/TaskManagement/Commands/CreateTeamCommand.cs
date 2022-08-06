using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;

namespace TaskManagement.Commands
{
    public class CreateTeamCommand : BaseCommand
    {
        private const int ExpectedParamsCount = 1;

        public CreateTeamCommand(IList<string> parameters, IRepository repository)
        :base(parameters, repository)
        {

        }
        public override string Execute()
        {
            if(this.CommandParameters.Count != ExpectedParamsCount)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: {ExpectedParamsCount}, Received: {this.CommandParameters.Count}");
            }

            var name = CommandParameters[0];

            var team = this.Repository.CreateTeam(name);

            return $"Team with name {team.Name} was created.";
        }
    }
}
