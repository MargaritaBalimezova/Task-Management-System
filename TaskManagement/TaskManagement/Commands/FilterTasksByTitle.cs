using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;

namespace TaskManagement.Commands
{
    public class FilterTasksByTitle : BaseCommand
    {
        public FilterTasksByTitle(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < this.CommandParameters.Count; i++)
            {
                sb.Append(CommandParameters[i]);
            }

            return this.Repository.FilterTaskBy(sb.ToString().Trim());
        }
    }
}
