using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Validations;

namespace TaskManagement.Commands
{
    public class CreateBoardInTeamCommand : BaseCommand
    {
        private const int ExpectedParamsCount = 2;

        public CreateBoardInTeamCommand(IList<string> parameters, IRepository repository)
          : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count != ExpectedParamsCount)
            {
                throw new InvalidUserInputException(String.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount, this.CommandParameters.Count));
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
