using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Commands
{
    public class CreateFeedbackCommand : BaseCommand
    {
        public CreateFeedbackCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < 5)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: 5, Received: {this.CommandParameters.Count}");
            }

            // Parameters:
            //  [0] - title
            //  [1] - description
            //  [2] - rating
            //  [3] - boardName
            //  [4] - teamName
            string title = base.CommandParameters[0];
            string description = base.CommandParameters[1];
            int rating = int.Parse(base.CommandParameters[2]);
            string boardName = base.CommandParameters[3];
            string teamName = base.CommandParameters[4];

            var feedback = this.Repository.CreateFeedBack(title, description, rating);

            var team = Repository.FindTeamByName(teamName);

            var board = Repository.FindBoardByNameInTeam(team, boardName);

            board.AddTaskToBoard((ITask)feedback);

            return $"Feedback with title {title} and id {feedback.Id} was created in board {boardName}.";
        }
    }
}