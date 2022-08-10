using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Validations;

namespace TaskManagement.Commands
{
    public class RemoveMemberFromTeam : BaseCommand
    {
        private const int ExpectedParamsCount = 2;

        public RemoveMemberFromTeam(IList<string> parameters, IRepository repository)
            : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count != ExpectedParamsCount)
            {
                throw new InvalidUserInputException(String.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount, this.CommandParameters.Count));
            }

            var team = this.Repository.FindTeamByName(CommandParameters[0]);
            var member = this.Repository.FindMemberByName(CommandParameters[1]);

            team.RemoveMember(member);

            return $"Member with name : {member.Name} was successfully removed from team {team.Name}!";

        }
    }
}
