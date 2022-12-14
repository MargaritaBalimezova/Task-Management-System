using System.Collections.Generic;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Validations;

namespace TaskManagement.Commands
{
    public class CreateMemberCommand : BaseCommand
    {
        private const int ExpectedParamsCount = 1;

        public CreateMemberCommand(IList<string> commandParameters, IRepository repository)
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
            //  [0] - memberName
            string memberName = base.CommandParameters[0];

            var member = this.Repository.CreateMember(memberName);
            return $"Member with name {member.Name} was created.";
        }
    }
}