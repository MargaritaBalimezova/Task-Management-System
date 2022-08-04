using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using System.Linq;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Commands
{
    public class FilterBugBy : BaseCommand
    {
        public FilterBugBy(IList<string> commandParameters, IRepository repository)
           : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            CommandParametersValidation(base.CommandParameters);

            IEnumerable<IBug> bugs = new List<IBug>();
            switch (CommandParameters[0])
            {
                case "status":
                    bugs  = this.Repository.Bugs.Where(b => b.Status == ParseBugStatus(CommandParameters[1]));
                    break;
                case "assignee":
                    bugs = this.Repository.Bugs.Where(b=>b.Assignee.Name == CommandParameters[1]);
                    break;
                case "statusandassignee":
                    bugs = this.Repository.Bugs.Where(b => b.Status == ParseBugStatus(CommandParameters[1])).
                        Where(b => b.Assignee.Name == CommandParameters[2]);
                   
                    break;
            }

            StringBuilder sb = new StringBuilder();
            foreach (var item in bugs)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }

        public void CommandParametersValidation(IList<string> commands)
        {
            switch (commands[0].ToLower())
            {
                case "status":
                case "assignee":
                    if (commands.Count < 2)
                    {
                        throw new ArgumentException($"Invalid number of arguments. Expected: 2, Received: {this.CommandParameters.Count}");
                    }
                    break;
                case "statusandassignee":
                    if (commands.Count < 3)
                    {
                        throw new ArgumentException($"Invalid number of arguments. Expected: 3, Received: {this.CommandParameters.Count}");
                    }
                    break;

            }
        }
    }
}
