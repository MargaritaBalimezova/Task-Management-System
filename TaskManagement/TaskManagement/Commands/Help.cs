using System;
using System.Collections.Generic;
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
            var sb = new StringBuilder();

            sb.AppendLine();

            sb.AppendLine("createteam - [teamName] - Create team");
            sb.AppendLine("createboardinteam - [boardName] [teamName] - Create board in a team");
            sb.AppendLine("createmember - [memberName] - Create member");
            sb.AppendLine("createbug - [title] [description] [priority] [severity] - Create bug");
            sb.AppendLine("createfeedback - [title] [description] [rating] - Create feedback");
            sb.AppendLine("createstory - [title] [description] [priority] [size]  - Create story");

            sb.AppendLine();

            sb.AppendLine("addcomment - [taskId] [memberName] [comment] - Adds comment to a task");
            sb.AppendLine("addmembertoteam - [teamName] [memberName] - Adds memebr to a team");
            sb.AppendLine("addtask - [taskId] [teamName] [boardName] - Adds task to a board");
            sb.AppendLine("assigntask - [taskId] [memberName] [teamName] - Assign task to a member");
            sb.AppendLine("unassigntask - [taskId] [memberName] - Unssign task from a member");

            sb.AppendLine();

            sb.AppendLine("changebug - [taskId] [paramToChange] [newValue] - Change bug severity/status/priority");
            sb.AppendLine("changefeedback - [taskId] [paramToChange] [newValue] - Change feedback status/rating");
            sb.AppendLine("changestory - [taskId] [paramToChange] [newValue] - Change story status/size/rating");

            sb.AppendLine();

            sb.AppendLine("filterassignedtasksby - [paramToFilterBy] [valueToSearchFor] / [secondValueToSearchFor] - Filter assigned tasks by given paramater and value");
            sb.AppendLine("filterbugby - [paramToFilterBy] [valueToSearchFor] / [secondValueToSearchFor] - Filter bugs by given paramater/s and value/s");
            sb.AppendLine("filterfeedbacksby - [paramToFilterBy] [valueToSearchFor] - Filter feedbacks by given paramater and value");
            sb.AppendLine("filterstoryby - [paramToFilterBy] [valueToSearchFor] / [secondValueToSearchFor]- Filter stories by given paramater/s and value/s");
            sb.AppendLine("filtertaskbytitle - [titleToSearchFor] - Filter tasks by given title");

            sb.AppendLine();

            sb.AppendLine("sortassignedtasksbytitle - Showing sorted by title assgined tasks");
            sb.AppendLine("sorttaskbytitle - Showing sorted by title tasks");
            sb.AppendLine("sortbugby - [paramToSortBy] - Showing sorted by given param bugs");
            sb.AppendLine("sortfeedbacksby - [paramToSortBy] - Showing sorted by given param feedbacks");
            sb.AppendLine("sortstoryby - [paramToSortBy] - Showing sorted by given param stories");

            sb.AppendLine();

            sb.AppendLine("showallmembers  - Showing all members that were created");
            sb.AppendLine("showallteams - Showing all teams that were created");
            sb.AppendLine("showallteamboard - [teamName] - Showing all board in a given team");
            sb.AppendLine("showboardactivity - [boardName] [teamName] - Showing board activity in a given team");
            sb.AppendLine("showmemberactivity - [memberName] - Showing member activity");
            sb.AppendLine("showteamsactivity - [teamName] - Showing team activity");
            sb.AppendLine("showteammembers - [teamName] - Showing all members in a given team");

            sb.AppendLine();

            return sb.ToString().Trim();
        }
    }
}