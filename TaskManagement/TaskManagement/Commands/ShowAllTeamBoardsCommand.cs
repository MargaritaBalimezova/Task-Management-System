using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;

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
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: {ExpectedParamsCount}, received: {this.CommandParameters.Count}");
            }

            ITeam team = this.Repository.FindTeamByName(base.CommandParameters[0]);

            StringBuilder sb = new StringBuilder();

            foreach (var item in team.Boards)
            {
                sb.AppendLine($"==Board name: {item.Name}==");
                sb.AppendLine(item.ToString());
                sb.AppendLine("======");
            }

            return sb.ToString();
        }
    }
}
