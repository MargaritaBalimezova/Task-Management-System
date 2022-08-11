using System.Collections.Generic;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;

namespace TaskManagement.Core.Contracts
{
    public interface IRepository
    {
        public IList<string> Names { get; }

        public IList<ITeam> Teams { get; }

        public IList<IMember> Members { get; }

        public IList<IBug> Bugs { get; }

        public IList<IFeedback> Feedbacks { get; }

        public IList<IStory> Stories { get; }

        public ITeam CreateTeam(string name);

        public IMember CreateMember(string name);

        public IBoard CreateBoard(string board);

        public IBug CreateBug(string title, string description, PriorityType priority,
            Severity severity, IList<string> steps);

        public IFeedback CreateFeedBack(string title, string description, int rating);

        public IStory CreateStory(string title, string description, PriorityType priority,
            SizeType size);

        public ITask FindTaskById(int id);

        public ITeam FindTeamByName(string name);

        public IMember FindMemberByName(string name);

        public IBoard FindBoardByNameInTeam(ITeam team, string name);

        public bool IsMemberInTeam(ITeam team, IMember member);

        public bool IsBoardInTeam(ITeam team, IBoard board);

        public string FilterTaskBy(string keyword);

        /*        public void SortTasksByTitle();

                public void FilterTasksByTitle(string title);

                public void FilterTasksByStatus(string Status);

                public void FilterTasksByAssignee(IMember assignee);*/
    }
}