using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;

namespace TaskManagement.Commands
{
    public class ShowTeamsActivity : BaseCommand
    {
        private const int ExpectedParamsCount = 1;

        public ShowTeamsActivity(IList<string> commandParameters, IRepository repository)
            :base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            if(this.CommandParameters.Count != ExpectedParamsCount)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: {ExpectedParamsCount}, Received: {this.CommandParameters.Count}");
            }

            string teamName = base.CommandParameters[0];

            var team = this.Repository.FindTeamByName(teamName);

            return team.ShowActivity();
        }
    }
}
