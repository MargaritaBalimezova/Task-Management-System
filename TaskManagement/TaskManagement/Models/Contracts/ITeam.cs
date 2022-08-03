using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Models.Contracts
{
    public interface ITeam : IActivityLog
    {
        public string Name { get; }
        public List<IMember> Members { get; }

        public List<IBoard> Boards { get; }

        public void AddMember(IMember memmber);

        public void RemoveMember(IMember member);

        public void AddBoard(IBoard board);

        public void AddTaskToBoard(ITask task, IBoard board);

        public string ShowActivity();

        public string ShowMembers();
    }
}
