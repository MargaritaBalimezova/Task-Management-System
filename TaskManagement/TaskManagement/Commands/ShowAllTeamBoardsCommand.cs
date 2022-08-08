using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Validations;

namespace TaskManagement.Commands
{
    public class ShowAllTeamBoardsCommand : BaseCommand
    {
        private const int ExpectedParamsCount = 1;

        public ShowAllTeamBoardsCommand(IList<string> parameters, IRepository repository)
           : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count != ExpectedParamsCount)
            {
                throw new InvalidUserInputException(String.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount, this.CommandParameters.Count));
            }

            ITeam team = this.Repository.FindTeamByName(base.CommandParameters[0]);

            StringBuilder sb = new StringBuilder();

            foreach (var item in team.Boards)
            {
                sb.AppendLine($"-BOARD NAME: {item.Name}-");
                sb.AppendLine(item.ToString());
                sb.AppendLine("--------");
            }

            return sb.ToString();
        }
    }
}
