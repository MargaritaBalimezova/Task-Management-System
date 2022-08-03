using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Models;

namespace TaskManagement.Commands
{
    public class AddCommentCommand : BaseCommand
    {
        public const int ExpectedParamsCount = 3;

        public AddCommentCommand(IList<string> parameters, IRepository repository)
        : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            if(this.CommandParameters.Count < ExpectedParamsCount)
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
            var member = this.Repository.FindMemberByName(this.CommandParameters[1]);
           
            var sb = new StringBuilder();

            for(int i = 2; i < this.CommandParameters.Count; i++)
            {
                sb.Append($"{CommandParameters[i]} ");
            }

            task.AddComment(new Comment(sb.ToString(), member.Name));

            return $"Comment added to task with id {task.Id} by {member.Name}";
        }
    }
}
