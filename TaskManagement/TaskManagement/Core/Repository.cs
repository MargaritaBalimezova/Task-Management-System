using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;

namespace TaskManagement.Core
{
    public class Repository : IRepository
    {
        private int id;

        private readonly IList<string> names;
        private readonly IList<ITeam> teams;
        private readonly IList<IMember> members;
        private readonly IList<IBoard> boards;
        private readonly IList<IBug> bugs;
        private readonly IList<IFeedback> feedbacks;
        private readonly IList<IStory> stories;

        public Repository()
        {
            this.names = new List<string>();
            this.teams = new List<ITeam>();
            this.members = new List<IMember>();
            this.boards = new List<IBoard>();
            this.bugs = new List<IBug>();
            this.feedbacks = new List<IFeedback>();
            this.stories = new List<IStory>();

            this.id = 0;
        }

        #region Properties

        public IList<string> Names
        {
            get
            {
                var copy = new List<string>(this.names);
                return copy;
            }
        }

        public IList<ITeam> Teams
        {
            get
            {
                var copy = new List<ITeam>(this.teams);
                return copy;
            }
        }

        public IList<IMember> Members
        {
            get
            {
                var copy = new List<IMember>(this.members);
                return copy;
            }
        }

        public IList<IBoard> Boards
        {
            get
            {
                var copy = new List<IBoard>(this.boards);
                return copy;
            }
        }

        public IList<IBug> Bugs
        {
            get
            {
                var copy = new List<IBug>(this.bugs);
                return copy;
            }
        }

        public IList<IFeedback> Feedbacks
        {
            get
            {
                var copy = new List<IFeedback>(this.feedbacks);
                return copy;
            }
        }

        public IList<IStory> Stories
        {
            get
            {
                var copy = new List<IStory>(this.stories);
                return copy;
            }
        }

        #endregion

        #region Methods

        public void CreateBoard(string board)
        {
            throw new NotImplementedException();
        }

        public void CreateBug(string title, string description, PriorityType priority, Severity severity, IMember assignee, IList<string> steps)
        {
            throw new NotImplementedException();
        }

        public void CreateFeedBack(string title, string description, int rating)
        {
            throw new NotImplementedException();
        }

        public void CreateMember(string name)
        {
            throw new NotImplementedException();
        }

        public void CreateStory(string title, string description, int id, PriorityType priority, SizeType size, IMember assignee)
        {
            throw new NotImplementedException();
        }

        public void CreateTeam(string name)
        {
            throw new NotImplementedException();
        }

        public IBoard FindBoardByName(string name)
        {
            throw new NotImplementedException();
        }

        public IMember FindMemberByName(string name)
        {
            throw new NotImplementedException();
        }

        public ITask FindTaskById(int id)
        {
            throw new NotImplementedException();
        }

        public ITeam FindTeamByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool IsBoardInTeam(ITeam team, IBoard board)
        {
            throw new NotImplementedException();
        }

        public bool IsMemberInTeam(ITeam team, IMember member)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
