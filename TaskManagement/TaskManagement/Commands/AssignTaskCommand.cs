using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;

namespace TaskManagement.Commands
{
    public class AssignTaskCommand : BaseCommand
    {
        public AssignTaskCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < 3)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: 3, Received: {this.CommandParameters.Count}");
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

            if (Repository.IsMemberInTeam(team, member))
            {
                member.AddTask(task);
                return $"Task with id {taskId} was assigned to {member.Name}";
            }

            throw new ArgumentException($"Member with id {member.Name} was not found in the member list of team {team.Name}");
        }
    }
}