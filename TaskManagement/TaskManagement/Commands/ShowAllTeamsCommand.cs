using System.Text;
using TaskManagement.Core.Contracts;

namespace TaskManagement.Commands
{
    public class ShowAllTeamsCommand : BaseCommand
    {
        public ShowAllTeamsCommand(IRepository repository)
        : base(repository)
        {

        }

        public override string Execute()
        {
            if(this.Repository.Teams.Count == 0)
            {
                return "There are no teams.";
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Number of all teams: {this.Repository.Teams.Count}");

            foreach (var team in this.Repository.Teams)
            {
                sb.AppendLine(team.ToString());
                sb.AppendLine("####################");
            }

            return sb.ToString().Trim();
        }
    }
}
