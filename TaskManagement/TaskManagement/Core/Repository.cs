using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Tasks;

namespace TaskManagement.Core
{
    public class Repository : IRepository
    {
        private int id;

        private readonly IList<string> names;
        private readonly IList<ITeam> teams;
        private readonly IList<IMember> members;
        private readonly IList<IBug> bugs;
        private readonly IList<IFeedback> feedbacks;
        private readonly IList<IStory> stories;

        public Repository()
        {
            this.names = new List<string>();
            this.teams = new List<ITeam>();
            this.members = new List<IMember>();
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

        #endregion Properties

        #region Methods

        public IBoard CreateBoard(string board)
        {
            IBoard newBoard = new Board(board);
            return newBoard;
        }

        public IBug CreateBug(string title, string description, PriorityType priority, Severity severity, IList<string> steps)
        {
            IBug bug = new Bug(title, description, id + 1, priority, severity, steps);
            this.id++;
            this.bugs.Add(bug);
            return bug;
        }

        public IFeedback CreateFeedBack(string title, string description, int rating)
        {
            var feedback = new FeedBack(title, description, id + 1, rating);
            id++;
            this.feedbacks.Add(feedback);
            return feedback;
        }

        public IMember CreateMember(string name)
        {
            if (this.names.Contains(name))
            {
                throw new NameExistsException("Members's name should be unique in the aplication!");
            }

            var member = new Member(name);

            this.members.Add(member);
            this.names.Add(name);

            return member;
        }

        public IStory CreateStory(string title, string description, PriorityType priority, SizeType size)
        {
            var story = new Story(title, description, this.id + 1, priority, size);
            this.id++;
            this.stories.Add(story);

            return story;
        }

        public ITeam CreateTeam(string name)
        {
            if (this.Names.Contains(name))
            {
                throw new NameExistsException("Team's name should be unique in the aplication!");
            }

            var team = new Team(name);
            this.names.Add(name);
            this.teams.Add(team);

            return team;
        }

        public IBoard FindBoardByNameInTeam(ITeam team, string name)
        {
            return team.Boards.FirstOrDefault(x => x.Name == name) ?? throw new EntityNotFoundException($"There is no board with name {name} in team {team.Name}!");
        }

        public IMember FindMemberByName(string name)
        {
            return this.Members.FirstOrDefault(x => x.Name == name) ?? throw new EntityNotFoundException($"There is no member with name {name}!");
        }

        public ITask FindTaskById(int id)
        {
            List<ITask> tasks = bugs.Cast<ITask>().Concat(feedbacks.Cast<ITask>()).Concat(stories.Cast<ITask>()).ToList();
            return tasks.FirstOrDefault(x => x.Id == id) ?? throw new EntityNotFoundException($"There is no task with id {id}!");
        }

        public ITeam FindTeamByName(string name)
        {
            return this.teams.FirstOrDefault(team => team.Name == name) ?? throw new EntityNotFoundException($"There is no team with name {name}!");
        }

        public bool IsBoardInTeam(ITeam team, IBoard board)
        {
            if (team.Boards.Any(item => item.Name == board.Name))
            {
                return true;
            }

            return false;
        }

        public bool IsMemberInTeam(ITeam team, IMember member)
        {
            if (team.Members.Any(mem => mem.Name == member.Name))
            {
                return true;
            }

            return false;
        }

        public void AddBoardToTeam(ITeam team, IBoard board)
        {
            if (IsBoardInTeam(team, board))
            {
                throw new NameExistsException($"Board with name {board.Name} already exists");
            }

            team.AddBoard(board);
        }

        public string FilterTaskBy(string keyword)
        {
            var regex = new Regex(@"^.*" + keyword + ".*$");
            var tasks = bugs.Cast<ITask>().Concat(feedbacks.Cast<ITask>())
                .Concat(stories.Cast<ITask>()).ToList();

            var filtered = tasks.Where(task => regex.IsMatch(task.Title)).ToList();

            var sb = new StringBuilder();

            foreach (var task in filtered)
            {
                sb.AppendLine(task.ToString());
                sb.AppendLine("####################");
            }

            return sb.ToString();
        }
    }

    #endregion Methods
}