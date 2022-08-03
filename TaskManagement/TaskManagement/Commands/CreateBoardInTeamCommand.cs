using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Commands
{
    public class CreateBoardInTeamCommand : BaseCommand
    {

        public CreateBoardInTeamCommand(IList<string> parameters, IRepository repository)
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
            string teamName = base.CommandParameters[1];
           
            ITeam team = this.Repository.FindTeamByName(teamName);   
            IBoard board = this.Repository.CreateBoard(boardName);

            if (this.Repository.IsBoardInTeam(team, board))
            {
                throw new InvalidOperationException
                    ($"Board: {board.Name} is already added in team {team.Name}!");
            }

            team.AddBoard(board);

            return $"Board with name {board.Name} created in team {team.Name}!";
        }
    }
}
