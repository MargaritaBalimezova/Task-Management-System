using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Tasks;
using TaskManagement.Validations;

namespace TaskManagement.Commands
{
    public class AssignTaskCommand : BaseCommand
    {
        private const int ExpectedParamsCount = 3;

        public AssignTaskCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < ExpectedParamsCount)
            {
                throw new InvalidUserInputException(String.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount, this.CommandParameters.Count));
            }

            // Parameters:
            //  [0] - taskId
            //  [1] - memberName
            //  [2] - teamName
            int taskId = int.Parse(base.CommandParameters[0]);
            string memberName = base.CommandParameters[1];
            string teamName = base.CommandParameters[2];
            CheckIfTaskIsAssigned(taskId);
            
            var task = this.Repository.FindTaskById(taskId);
            var member = this.Repository.FindMemberByName(memberName);
            var team = this.Repository.FindTeamByName(teamName);
            CheckIfTaskIsOnTeamBoards(task, team);

            if (task.GetType().Name == "FeedBack")
            {
                throw new InvalidUserInputException($"Task of type Feedback can not have assignee.");
            }
            else if (Repository.IsMemberInTeam(team, member))
            {
                member.AddTask(task);
            }
            else
            {
                throw new EntityNotFoundException(String.Format(Constants.MEMBER_NOT_FOUND_ERR_MSG, memberName, teamName));
            }

            switch (task.GetType().Name)
            {
                case "Bug":
                    var bug = (Bug)task;
                    bug.AddAssignee(member);
                    break;

                case "Story":
                    var story = (Story)task;
                    story.AddAssignee(member);
                    break;
            }

            return $"Task with id {taskId} was assigned to {member.Name}";
        }

        public void CheckIfTaskIsAssigned(int taskId)
        {
            var task = this.Repository.FindTaskById(taskId);

            if (task.GetType().Name=="Bug")
            {
                var bug = (IBug)task;
                if (bug.Assignee != null)
                {
                    throw new InvalidOperationException($"Task with ID: {taskId} is already assigned to {bug.Assignee.Name}!");
                }
            }
            if (task.GetType().Name == "Story")
            {
                var story = (IStory)task;
                if (story.Assignee != null)
                {
                    throw new InvalidOperationException($"Task with ID: {taskId} is already assigned to {story.Assignee.Name}!");
                }
            }
        }

        public void CheckIfTaskIsOnTeamBoards(ITask task, ITeam team)
        {
            if (team.Boards.Count == 0)
            {
                throw new InvalidOperationException($"Team {team.Name} does not have board with tasks yet!");
            }
            foreach (var item in team.Boards)
            {                
                if (!item.BoardTasks.Contains(task))
                {
                    throw new InvalidOperationException($"Task with Id: {task.Id} is not attached to any of team {team.Name} boards!");
                }
            }
        }
    }
}