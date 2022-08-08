using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Validations;

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
                throw new InvalidUserInputException(String.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount, this.CommandParameters.Count));
            }

            string teamName = base.CommandParameters[0];

            var team = this.Repository.FindTeamByName(teamName);

            return team.ShowActivity();
        }
    }
}
