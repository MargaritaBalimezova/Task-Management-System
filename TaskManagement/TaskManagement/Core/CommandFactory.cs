using TaskManagement.Commands;
using TaskManagement.Commands.Contracts;
using TaskManagement.Core.Contracts;
using System;
using System.Collections.Generic;

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
                case "createbug":
                    return new CreateBugCommand(commandParameters, repository);

                case "createfeedback":
                    return new CreateFeedbackCommand(commandParameters, repository);

                case "createstory":
                    return new CreateStoryCommand(commandParameters, repository);

                case "createboardinteam":
                    return new CreateBoardInTeamCommand(commandParameters, repository);

                case "createmember":
                    return new CreateMemberCommand(commandParameters, repository);

                case "createteam":
                    return new CreateTeamCommand(commandParameters, repository);

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

                case "unassigntask":
                    return new UnassignTaskCommand(commandParameters, repository);

                case "assigntask":
                    return new AssignTaskCommand(commandParameters, repository);

                case "addtask":
                    return new AddTaskCommand(commandParameters, repository);

                case "addcomment":
                    return new AddCommentCommand(commandParameters, repository);

                case "filterbugby":
                    return new FilterBugBy(commandParameters, repository);

                case "addmembertoteam":
                    return new AddMemberToTeam(commandParameters, repository);

                case "sortfeedbacksby":
                    return new SortFeedbacksByCommand(commandParameters, repository);

                case "filterstoryby":
                    return new FilterStoryBy(commandParameters, repository);

                case "filtertasksbytitle":
                    return new FilterTasksByTitle(commandParameters, repository);

                case "filterfeedbacksby":
                    return new FilterFeedbacksByCommand(commandParameters, repository);

                case "sortstoryby":
                    return new SortStoryBy(commandParameters, repository);

                case "sorttasksbytitle":
                    return new SortTasksByTitle(repository);

                case "sortassignedtasksbytitle":
                    return new SortAssignedTasksByTitle(repository);

                case "filterassignedtasksby":
                    return new FilterAssignedTasksBy(commandParameters, repository);

                case "help":
                    return new Help(repository);

                default:
                    throw new ArgumentException($"Command with name: {commandName} doesn't exist!");
            }
        }

        // Receives a full line and extracts the command to be executed from it.
        // For example, if the input line is "FilterBy Assignee John", the method will return "FilterBy".
        private string ExtractCommandName(string commandLine)
        {
            return commandLine.Split(" ")[0];
        }

        // Receives a full line and extracts the parameters that are needed for the command to execute.
        // For example, if the input line is "FilterBy Assignee John",
        // the method will return a list of ["Assignee", "John"].
        private List<string> ExtractCommandParameters(string commandLine)
        {
            string[] commandTokens = commandLine.Split(" ");
            List<string> parameters = new List<string>();
            for (int i = 1; i < commandTokens.Length; i++)
            {
                parameters.Add(commandTokens[i]);
            }
            return parameters;
        }
    }
}