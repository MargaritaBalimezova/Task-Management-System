using System;
using System.Text;
using TaskManagement.Core.Contracts;

namespace TaskManagement.Commands
{
    public class Help : BaseCommand
    {
        public Help(IRepository repository) : base(repository)
        {
        }

        public override string Execute()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            var sb = new StringBuilder();

            sb.AppendLine();

            sb.AppendLine("IMPORTANT: Please when you input 'title', 'description' and 'comment' place them in quotes!");
            sb.AppendLine();

            sb.AppendLine("CreateTeam.................... - [teamName] - Create team");
            sb.AppendLine("CreateMember.................. - [memberName] - Create member");
            sb.AppendLine("CreateBoardInTeam............. - [boardName] [teamName] - Create board in a team");
            sb.AppendLine("CreateFeedback................ - [title] [description] [rating]  - Create feedback");
            sb.AppendLine("CreateBug..................... - [title] [description] [priority] [severity] - Create bug");
            sb.AppendLine("CreateStory................... - [title] [description] [priority] [size] - Create story");

            sb.AppendLine();

            sb.AppendLine("AddMemberToTeam............... - [teamName] [memberName] - Adds member to a team");
            sb.AppendLine("RemoveMemberFromTeam.......... - [teamName] [memberName] - Removes member from team");
            sb.AppendLine("AddComment.................... - [taskId] [memberName] [comment]  - Adds comment to a task");
            sb.AppendLine("AddTask....................... - [taskId] [teamName] [boardName]  - Adds task to a board");
            sb.AppendLine("AssignTask.................... - [taskId] [memberName] [teamName] - Assign task to a member");
            sb.AppendLine("UnassignTask ................. - [taskId] [memberName] - Unssign task from a member");

            sb.AppendLine();

            sb.AppendLine("ChangeBug..................... - [taskId] [paramToChange] [newValue] - Change bug severity/status/priority");
            sb.AppendLine("ChangeFeedback................ - [taskId] [paramToChange] [newValue] - Change feedback status/rating");
            sb.AppendLine("ChangeStory................... - [taskId] [paramToChange] [newValue] - Change story status/size/rating");

            sb.AppendLine();

            sb.AppendLine("FilterTasksByTitle............ - [titleToSearchFor] - Filter tasks by given title");
            sb.AppendLine("FilterAssignedTasksBy......... - [paramToFilterBy] [valueToSearchFor] / [secondValueToSearchFor] - Filter assigned tasks by given paramater and value");
            sb.AppendLine("FilterBugBy................... - [paramToFilterBy] [valueToSearchFor] / [secondValueToSearchFor] - Filter bugs by given paramater/s and value/s");
            sb.AppendLine("FilterStoryBy................. - [paramToFilterBy] [valueToSearchFor] / [secondValueToSearchFor] - Filter stories by given paramater/s and value/s");
            sb.AppendLine("FilterFeedbacksBy............. - [paramToFilterBy] [valueToSearchFor] - Filter feedbacks by given paramater and value");

            sb.AppendLine();

            sb.AppendLine("SortAssignedTasksByTitle...... - Showing sorted by title assgined tasks");
            sb.AppendLine("SortTasksByTitle.............. - Showing sorted by title tasks");
            sb.AppendLine("SortBugBy..................... - [paramToSortBy] - Showing sorted by given param bugs");
            sb.AppendLine("SortFeedbacksBy............... - [paramToSortBy] - Showing sorted by given param feedbacks");
            sb.AppendLine("SortStoryBy................... - [paramToSortBy] - Showing sorted by given param stories");

            sb.AppendLine();

            sb.AppendLine("ShowAllMembers................ - Showing all members that were created");
            sb.AppendLine("ShowAllTeams.................. - Showing all teams that were created");
            sb.AppendLine("ShowAllTeamBoards............. - [teamName] - Showing all board in a given team");
            sb.AppendLine("ShowMemberActivity............ - [memberName] - Showing member activity");
            sb.AppendLine("ShowTeamsActivity............. - [teamName] - Showing team activity");
            sb.AppendLine("ShowTeamMembers............... - [teamName] - Showing all members in a given team");
            sb.AppendLine("ShowBoardActivity............. - [boardName] [teamName] - Showing board activity in a given team");
            sb.AppendLine("ShowTaskActivity.............. - [taskID] - Showing a task activity of a specific task");

            sb.AppendLine();

            return sb.ToString();
        }
    }
}