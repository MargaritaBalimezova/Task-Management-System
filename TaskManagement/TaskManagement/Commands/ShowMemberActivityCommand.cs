using System;
using System.Collections.Generic;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Validations;

namespace TaskManagement.Commands
{
    public class ShowMemberActivityCommand : BaseCommand
    {
        private const int ExpectedParamsCount = 1;

        public ShowMemberActivityCommand(IList<string> commandParameters, IRepository repository)
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
            //  [0] - memberName
            string memberName = base.CommandParameters[0];

            var member = this.Repository.FindMemberByName(memberName);

            return member.ShowActivity();
        }
    }
}