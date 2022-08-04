using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;

namespace TaskManagement.Commands
{
    public class ShowMemberActivityCommand : BaseCommand
    {
        public ShowMemberActivityCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count != 1)
            {
                throw new ArgumentException($"Invalid number of arguments. Expected: 1, Received: {this.CommandParameters.Count}");
            }

            // Parameters:
            //  [0] - memberName
            string memberName = base.CommandParameters[0];

            var member = this.Repository.FindMemberByName(memberName);

            return member.ShowActivity();
        }
    }
}