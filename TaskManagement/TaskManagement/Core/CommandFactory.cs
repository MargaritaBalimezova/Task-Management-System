using TaskManagement.Commands;
using TaskManagement.Commands.Contracts;
using TaskManagement.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TaskManagement.Core
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IRepository repository;

        public CommandFactory(IRepository repository)
        {
            this.repository = repository;
        }

        public ICommand Create(string commandLine)
        {
            string commandName = ExtractCommandName(commandLine);
            List<string> commandParameters = this.ExtractCommandParameters(commandLine);

            switch (commandName.ToLower())
            {
                case "createteam":
                    return new CreateTeamCommand(commandParameters, repository);

                case "createmember":
                    return new CreateMemberCommand(commandParameters, repository);

                case "createboardinteam":
                    return new CreateBoardInTeamCommand(commandParameters, repository);

                case "createbug":
                    return new CreateBugCommand(commandParameters, repository);

                case "createfeedback":
                    return new CreateFeedbackCommand(commandParameters, repository);

                case "createstory":
                    return new CreateStoryCommand(commandParameters, repository);

                case "changebug":
                    return new ChangeBugCommand(commandParameters, repository);

                case "changefeedback":
                    return new ChangeFeedbackCommand(commandParameters, repository);

                case "changestory":
                    return new ChangeStoryCommand(commandParameters, repository);

                case "showallmembers":
                    return new ShowAllMembersCommand(repository);

                case "showallteams":
                    return new ShowAllTeamsCommand(repository);

                case "showallteamboards":
                    return new ShowAllTeamBoardsCommand(commandParameters, repository);

                case "showmemberactivity":
                    return new ShowMemberActivityCommand(commandParameters, repository);

                case "showboardactivity":
                    return new ShowBoardActivityCommand(commandParameters, repository);

                case "showteammembers":
                    return new ShowTeamMembersCommand(commandParameters, repository);

                case "showteamsactivity":
                    return new ShowTeamsActivity(commandParameters, repository);

                case "showtaskactivity":
                    return new ShowTaskActivityCommand(commandParameters, repository);

                case "addtask":
                    return new AddTaskCommand(commandParameters, repository);

                case "addcomment":
                    return new AddCommentCommand(commandParameters, repository);

                case "addmembertoteam":
                    return new AddMemberToTeam(commandParameters, repository);

                case "removememberfromteam":
                    return new RemoveMemberFromTeam(commandParameters, repository);

                case "unassigntask":
                    return new UnassignTaskCommand(commandParameters, repository);

                case "assigntask":
                    return new AssignTaskCommand(commandParameters, repository);

                case "filtertasksbytitle":
                    return new FilterTasksByTitle(commandParameters, repository);

                case "filterassignedtasksby":
                    return new FilterAssignedTasksBy(commandParameters, repository);

                case "filterbugby":
                    return new FilterBugBy(commandParameters, repository);

                case "filterstoryby":
                    return new FilterStoryBy(commandParameters, repository);

                case "filterfeedbacksby":
                    return new FilterFeedbacksByCommand(commandParameters, repository);

                case "sorttasksbytitle":
                    return new SortTasksByTitle(repository);

                case "sortassignedtasksbytitle":
                    return new SortAssignedTasksByTitle(repository);

                case "sortstoryby":
                    return new SortStoryBy(commandParameters, repository);

                case "sortbugby":
                    return new SortBugBy(commandParameters, repository);

                case "sortfeedbacksby":
                    return new SortFeedbacksByCommand(commandParameters, repository);

                case "help":
                    return new Help(repository);

                default:
                    throw new ArgumentException($"Command with name: {commandName} doesn't exist!");
            }
        }

        private string ExtractCommandName(string commandLine)
        {
            return commandLine.Split(" ")[0];
        }

        private List<string> ExtractCommandParameters(string commandLine)
        {
            string[] commandTokens = commandLine.Split(" ");

            var list = commandTokens.Skip(1).ToList();

            if (string.Join(" ", list).Contains("\""))
            {
                var arg = string.Join(" ", list);
                var parameters = new List<string>();
                var word = new StringBuilder();
                bool isQuoteOpen = false;
                for (int i = 0; i < arg.Length; i++)
                {
                    var currSymbol = arg[i].ToString();
                    if (currSymbol == "\"")
                    {
                        isQuoteOpen = isQuoteOpen == true ? false : true;
                    }
                    else if (isQuoteOpen)
                    {
                        word.Append(currSymbol);
                    }
                    else
                    {
                        word.Append(arg[i]);
                    }
                    if (i == arg.Length - 1 || !isQuoteOpen && currSymbol == " ")
                    {
                        parameters.Add(word.ToString());
                        word.Clear();
                    }
                }
                return parameters.Select(x => x.Trim()).ToList();
            }
            return list;
        }
    }
}