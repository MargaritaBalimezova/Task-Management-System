using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Commands
{
    public class SortAssignedTasksByTitle : BaseCommand
    {
        public SortAssignedTasksByTitle(IRepository repository) : base(repository)
        {
        }

        public override string Execute()
        {
            var bugs = this.Repository.Bugs.Where(b => b.Assignee!= null);
            var stories = this.Repository.Stories.Where(s => s.Assignee!= null);
            List<ITask> tasks = bugs.Cast<ITask>().Concat(stories.Cast<ITask>()).OrderBy(t => t.Title).ToList();

            StringBuilder sb = new StringBuilder();
            
            if (tasks.Count == 0)
            {
                return "There is no assigned tasks for the moment!";
            }
            else
            {
                foreach (var item in tasks)
                {
                    sb.AppendLine(item.ToString());
                }
            }

            return sb.ToString();
        }

      
    }
}
