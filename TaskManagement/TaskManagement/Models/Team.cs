using System;
using System.Collections.Generic;
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

        public Team(string name)
        {
            this.Name = name;
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
        #endregion

        #region Methods
        public void AddBoard(IBoard board)
        {
            Validator.ValidateArgumentIsNotNull(board, "Team's board");
            this.boards.Add(board);
        }

        public void RemoveBoard(IBoard board)
        {
            Validator.ValidateArgumentIsNotNull(board, "Team's board");
            this.boards.Remove(board);
        }

        public void AddMember(IMember memmber)
        {
            Validator.ValidateArgumentIsNotNull(members, "Team's member");
            this.members.Add(memmber);
        }
        public void RemoveMember(IMember member)
        {
            this.members.Remove(member);
        }

        public void AddTaskToBoard(ITask task, IBoard board)
        {
            Validator.ValidateArgumentIsNotNull(board, "Team's board");
            board.AddTaskToBoard(task);
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
