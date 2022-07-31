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

        public Repository()
        {
            this.id = 0;
        }

        public IList<string> Names => throw new NotImplementedException();

        public IList<ITeam> Teams => throw new NotImplementedException();

        public IList<IMember> Members => throw new NotImplementedException();

        public IList<IBoard> Boards => throw new NotImplementedException();

        public IList<IBug> Bugs => throw new NotImplementedException();

        public IList<IFeedback> Feedbacks => throw new NotImplementedException();

        public IList<IStory> Stories => throw new NotImplementedException();

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
}
