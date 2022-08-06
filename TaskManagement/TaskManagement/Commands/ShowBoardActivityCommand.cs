using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Commands
{
    public class ShowBoardActivityCommand : BaseCommand
    {
        private const int ExpectedParamsCount = 2;

        public ShowBoardActivityCommand(IList<string> parameters, IRepository repository)
           : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count != ExpectedParamsCount)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: {ExpectedParamsCount}, Received: {this.CommandParameters.Count}");
            }

            string boardName = base.CommandParameters[0];
            ITeam team = this.Repository.FindTeamByName(base.CommandParameters[1]);
            IBoard board = this.Repository.FindBoardByNameInTeam(team, boardName);
                        
            return board.ViewBoardHistory();
        }
    }
}
