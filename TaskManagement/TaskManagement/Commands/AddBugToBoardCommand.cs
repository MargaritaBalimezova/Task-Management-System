using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;
using System.Linq;

namespace TaskManagement.Commands
{
    public class AddBugToBoardCommand : BaseCommand
    {

        public AddBugToBoardCommand(IList<string> parameters, IRepository repository)
           : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count<3)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: 3, Received: {this.CommandParameters.Count}");
            }

            int bugId = ParseIntParameter(CommandParameters[0], "BugId");
            string teamName = CommandParameters[1];
            string boardName = CommandParameters[2];

            ITask bug = this.Repository.FindTaskById(bugId);
            ITeam team = this.Repository.FindTeamByName(teamName);
            IBoard board = this.Repository.FindBoardByNameInTeam(team,boardName);

            CheckIfBugIsAlreadyOnTheBoard(bug, board);

            board.AddTaskToBoard(bug);
            
            return $"Bug with Id: {bug.Id} successfully added to board : {board.Name}!";
        }

        private static void CheckIfBugIsAlreadyOnTheBoard(ITask bug,IBoard board)
        {
            foreach  (ITask t in board.BoardTasks)
            {
                if (bug.Id == t.Id)
                {
                    throw new InvalidOperationException
                        ($"Bug with Id : {bug.Id} is already on board {board.Name}!");
                }
            }
        }
    }
}
