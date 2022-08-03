using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Commands
{
    public class ShowAllTeamBoardsCommand : BaseCommand
    {
        public ShowAllTeamBoardsCommand(IList<string> parameters, IRepository repository)
           : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < 1)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: 1, Received: {this.CommandParameters.Count}");
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
