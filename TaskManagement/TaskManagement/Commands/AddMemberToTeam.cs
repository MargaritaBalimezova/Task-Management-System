using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;

namespace TaskManagement.Commands
{
    public class AddMemberToTeam : BaseCommand
    {
        private const int ExpectedParamsCount = 2;

        public AddMemberToTeam(IList<string> parameters, IRepository repository)
            : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count != ExpectedParamsCount)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: {ExpectedParamsCount}, Received: {this.CommandParameters.Count}");
            }
            var team = this.Repository.FindTeamByName(CommandParameters[0]);
            var member = this.Repository.FindMemberByName(CommandParameters[1]);

            team.AddMember(member);

            return $"Member with name : {member.Name} was successfully added in team {team.Name}!";
            
        }
    }
}
