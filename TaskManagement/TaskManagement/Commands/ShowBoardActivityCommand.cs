using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Commands
{
    public class ShowBoardActivityCommand : BaseCommand
    {
        public ShowBoardActivityCommand(IList<string> parameters, IRepository repository)
           : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < 2)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: 2, Received: {this.CommandParameters.Count}");
            }

            string boardName = base.CommandParameters[0];
            ITeam team = this.Repository.FindTeamByName(base.CommandParameters[1]);
            IBoard board = this.Repository.FindBoardByNameInTeam(team, boardName);
                        
            return board.ViewBoardHistory();
        }
    }
}
