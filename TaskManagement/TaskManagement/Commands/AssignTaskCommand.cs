using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
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
                throw new InvalidUserInputException(string.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount, this.CommandParameters.Count));
            }

            // Parameters:
            //  [0] - taskId
            //  [1] - memberName
            //  [2] - teamName
            int taskId = int.Parse(base.CommandParameters[0]);
            string memberName = base.CommandParameters[1];
            string teamName = base.CommandParameters[2];

            var task = this.Repository.FindTaskById(taskId);
            var member = this.Repository.FindMemberByName(memberName);
            var team = this.Repository.FindTeamByName(teamName);

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
                throw new EntityNotFoundException($"Member with id {member.Name} was not found in the member list of team {team.Name}");
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
    }
}