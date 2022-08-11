using System;
using System.Collections.Generic;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Validations;

namespace TaskManagement.Commands
{
    public class AddTaskCommand : BaseCommand
    {
        private const int ExpectedParamsCount = 3;

        public AddTaskCommand(IList<string> parameters, IRepository repository)
         : base(parameters, repository)
        {
        }
        public override string Execute()
        {
            if(this.CommandParameters.Count != ExpectedParamsCount)
            {
                throw new InvalidUserInputException(String.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount, this.CommandParameters.Count));
            }

            int taskId = ParseIntParameter(this.CommandParameters[0], "task id");

            var task = this.Repository.FindTaskById(taskId);
            var team = this.Repository.FindTeamByName(this.CommandParameters[1]);
            var board = this.Repository.FindBoardByNameInTeam(team, this.CommandParameters[2]);

            board.AddTaskToBoard(task);

            return $"Task with id {task.Id} added to board {board.Name} in team {team.Name}";
        }
    }
}
