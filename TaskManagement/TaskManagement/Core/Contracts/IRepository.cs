using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;

namespace TaskManagement.Core.Contracts
{
    public interface IRepository
    {
        public IList<string> Names { get; }

        public IList<ITeam> Teams { get; }

        public IList<IMember> Members { get; }

        public IList<IBoard> Boards { get; }

        public IList<IBug> Bugs { get; }

        public IList<IFeedback> Feedbacks { get; }

        public IList<IStory> Stories { get; }

        public void CreateTeam(string name);

        public void CreateMember(string name);

        public void CreateBoard(string board);

        public void CreateBug(string title, string description, PriorityType priority,
            Severity severity, IMember assignee, IList<string> steps);

        public void CreateFeedBack(string title, string description, int rating);

        public void CreateStory(string title, string description, int id, PriorityType priority,
            SizeType size, IMember assignee);

        public ITask FindTaskById(int id);

        public ITeam FindTeamByName(string name);

        public IMember FindMemberByName(string name);

        public IBoard FindBoardByName(string name);

        public bool IsMemberInTeam(ITeam team, IMember member);

        public bool IsBoardInTeam(ITeam team, IBoard board);

        /*        public void SortTasksByTitle();

                public void FilterTasksByTitle(string title);

                public void FilterTasksByStatus(string Status);

                public void FilterTasksByAssignee(IMember assignee);*/
    }
}
