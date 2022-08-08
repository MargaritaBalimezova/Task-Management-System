using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Validations;

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
                throw new InvalidUserInputException(String.Format(Constants.ARGUMENTS_ERROR_MSG, ExpectedParamsCount, this.CommandParameters.Count));
            }

            int taskId = ParseIntParameter(this.CommandParameters[0], "task id");

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
