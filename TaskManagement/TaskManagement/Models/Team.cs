using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Models
{
    public class Team : ITeam
    {
        private const string TEAM_HEADER = "--TEAM--";
        private const int NameMinLen = 5;
        private const int NameMaxLen = 15;

        private string name;
        private List<IMember> members = new List<IMember>();
        private List<IBoard> boards = new List<IBoard>();
        private List<IEventLog> activityLog = new List<IEventLog>();

        public Team(string name)
        {
            this.Name = name;

            AddEventLog($"Team with {this.Name} was created!");
        }


        #region Properties
        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                Validator.ValidateArgumentIsNotNull(value, "Team's name");
                Validator.ValidateStringLength(value, NameMinLen, NameMaxLen, "Team's name");
                this.name = value;
            }
        }


        public List<IMember> Members
        {
            get
            {
                return new List<IMember>(this.members);
            }
        }

        public List<IBoard> Boards
        {
            get
            {
                return new List<IBoard>(this.boards);
            }
        }

        public IList<IEventLog> ActivityLog
        {
            get
            {
                return new List<IEventLog>(this.activityLog);
            }
        }
        #endregion

        #region Methods
        public void AddBoard(IBoard board)
        {
            Validator.ValidateArgumentIsNotNull(board, "Team's board");
            this.boards.Add(board);

            AddEventLog($"Board with name {board.Name} added to team {this.Name}.");
        }

        public void RemoveBoard(IBoard board)
        {
            Validator.ValidateArgumentIsNotNull(board, "Team's board");
            this.boards.Remove(board);

            AddEventLog($"Board with name {board.Name} removed from team {this.Name}.");
        }

        public void AddMember(IMember member)
        {
            Validator.ValidateArgumentIsNotNull(members, "Team's member");
            this.members.Add(member);

            AddEventLog($"Member {member.Name} joined {this.Name} team!");
        }
        public void RemoveMember(IMember member)
        {
            this.members.Remove(member);

            AddEventLog($"Member {member.Name} removed from {this.Name} team!");
        }

        public void AddTaskToBoard(ITask task, IBoard board)
        {
            Validator.ValidateArgumentIsNotNull(board, "Team's board");
            board.AddTaskToBoard(task);

            AddEventLog($"Task with {task.Id} added to {board.Name} in {this.Name} team.");
        }
        public void AddEventLog(string desc)
        {
            this.activityLog.Add(new EventLog(desc));
        }

        public string ShowActivity()
        {
            return string.Join(Environment.NewLine, this.activityLog.Select(e => e.ViewInfo()));
        }
        public string ShowMembers()
        {
            return string.Join(Environment.NewLine, this.Members.Select(e => e.ToString()));
        }

        #endregion

        //TODO check the printing format
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(TEAM_HEADER);
            sb.AppendLine($"Team name: {this.name}");
            sb.AppendLine($"Members of the team: ");
            sb.AppendLine(string.Join('\n', this.members));
            sb.AppendLine(string.Join('\n', this.boards));
            sb.AppendLine(TEAM_HEADER);

            return sb.ToString();
        }

    }
}
