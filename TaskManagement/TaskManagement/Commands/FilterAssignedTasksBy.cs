using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Tasks;

namespace TaskManagement.Commands
{
    public class FilterAssignedTasksBy : BaseCommand
    {
        public FilterAssignedTasksBy (IList<string> parameters, IRepository repository)
           : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            CommandParametersValidation(base.CommandParameters);

            IList<ITask> result = new List<ITask>();

            if (CommandParameters[0].ToLower()=="status")
            {
                switch (CommandParameters[1].ToLower())
                {
                    case "active":
                        result.Add((IBug)this.Repository.Bugs.Where(x => x.Assignee != null && x.Status == Models.Enums.BugStatus.Status.Active));
                        break;
                    case "fixed":
                        result.Add((IBug)this.Repository.Bugs.Where(x => x.Assignee != null && x.Status == Models.Enums.BugStatus.Status.Fixed));
                        break;
                    case "notdone":
                        result.Add((IStory)this.Repository.Stories.Where(x => x.Assignee != null && x.Status == Models.Enums.StoryStatus.Status.NotDone));
                        break;
                    case "inprogress":
                        result.Add((IStory)this.Repository.Stories.Where(x => x.Assignee != null && x.Status == Models.Enums.StoryStatus.Status.InProgress));
                        break;
                    case "done":
                        result.Add((IStory)this.Repository.Stories.Where(x => x.Assignee != null && x.Status == Models.Enums.StoryStatus.Status.Done));
                        break;
                    default:
                        throw new ArgumentException($"There is no status with name {CommandParameters[1]}");

                }
            }
            else if (CommandParameters[0].ToLower() == "assignee")
            {
                result.Add((IBug)this.Repository.Bugs.Where(x => x.Assignee.Name == CommandParameters[1]));
                result.Add((IStory)this.Repository.Stories.Where(x => x.Assignee.Name == CommandParameters[1]));
            }
            else if (CommandParameters[0].ToLower() == "statusandassignee")
            {
                switch (CommandParameters[1].ToLower())
                {
                    case "active":
                        result.Add((IBug)this.Repository.Bugs.Where(x => x.Assignee.Name == CommandParameters[2]
                        && x.Status == Models.Enums.BugStatus.Status.Active));
                        break;
                    case "fixed":
                        result.Add((IBug)this.Repository.Bugs.Where(x => x.Assignee.Name == CommandParameters[2]
                        && x.Status == Models.Enums.BugStatus.Status.Fixed));
                        break;
                    case "notdone":
                        result.Add((IStory)this.Repository.Stories.Where(x => x.Assignee.Name == CommandParameters[2]
                        && x.Status == Models.Enums.StoryStatus.Status.NotDone));
                        break;
                    case "inprogress":
                        result.Add((IStory)this.Repository.Stories.Where(x => x.Assignee.Name == CommandParameters[2] 
                        && x.Status == Models.Enums.StoryStatus.Status.InProgress));
                        break;
                    case "done":
                        result.Add((IStory)this.Repository.Stories.Where(x => x.Assignee.Name == CommandParameters[2]
                        && x.Status == Models.Enums.StoryStatus.Status.Done));
                        break;
                    default:
                        throw new ArgumentException($"There is no status with name {CommandParameters[1]}");

                }
            }


            
            StringBuilder sb = new StringBuilder();
            foreach (var item in result)
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
