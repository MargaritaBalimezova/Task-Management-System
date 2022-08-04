using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;

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
                throw new ArgumentException($"Invalid number of arguments. Expected: {ExpectedParamsCount}, Received: {this.CommandParameters.Count}");
            }

            int taskId;
            try
            {
                taskId = int.Parse(this.CommandParameters[0]);
            }
            catch
            {
                throw new ArgumentException($"Invalid first parameter, id of task is expected, received: {this.CommandParameters[0]}");
            }

            var task = this.Repository.FindTaskById(taskId);
            var team = this.Repository.FindTeamByName(this.CommandParameters[1]);
            var board = this.Repository.FindBoardByNameInTeam(team, this.CommandParameters[2]);

            board.AddTaskToBoard(task);

            return $"Task with id {task.Id} added to board {board.Name} in team {team.Name}";
        }
    }
}
