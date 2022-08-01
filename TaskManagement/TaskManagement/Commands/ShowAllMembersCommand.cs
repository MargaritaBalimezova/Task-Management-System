using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;

namespace TaskManagement.Commands
{
    public class ShowAllMembersCommand : BaseCommand
    {
        public ShowAllMembersCommand(IRepository repository)
            : base(repository)
        {
        }

        public override string Execute()
        {
            if (this.Repository.Members.Count > 0)
            {
                var sb = new StringBuilder();

                sb.AppendLine($"Number of all members: {this.Repository.Members.Count}");

                foreach (var member in this.Repository.Members)
                {
                    sb.AppendLine(member.ToString());
                    sb.AppendLine("####################");
                }

                return sb.ToString().Trim();
            }
            else
            {
                return "There are no members.";
            }
        }
    }
}