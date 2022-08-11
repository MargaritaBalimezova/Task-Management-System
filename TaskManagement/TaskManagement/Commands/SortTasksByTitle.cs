using System.Linq;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Commands
{
    public class SortTasksByTitle : BaseCommand
    {
        public SortTasksByTitle(IRepository repository)
        : base(repository)
        {

        }
        public override string Execute()
        {
            var sorted = this.Repository.Bugs.Cast<ITask>()
                .Concat(this.Repository.Feedbacks.Cast<ITask>())
                .Concat(this.Repository.Stories.Cast<ITask>())
                .OrderBy(task => task.Title).ToList();

            var sb = new StringBuilder();

            foreach (var task in sorted)
            {
                sb.AppendLine(task.ToString());
                sb.AppendLine("####################");
            }

            return sb.ToString();
        }
    }
}
