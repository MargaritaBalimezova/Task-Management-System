using TaskManagement.Commands.Contracts;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Enums;
using System;
using System.Collections.Generic;

namespace TaskManagement.Commands
{
    public abstract class BaseCommand : ICommand
    {
        protected BaseCommand(IRepository repository)
            : this(new List<string>(), repository)
        {
        }

        protected BaseCommand(IList<string> parameters, IRepository repository)
        {
            this.CommandParameters = parameters;
            this.Repository = repository;
        }

        public abstract string Execute();

        protected IRepository Repository { get; }

        protected IList<string> CommandParameters { get; }

        protected int ParseIntParameter(string value, string parameterName)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            throw new ArgumentException($"Invalid value for {parameterName}. Should be an integer number.");
        }
                
        protected PriorityType ParsePriorityType(string value)
        {
            if (Enum.TryParse(value, true, out PriorityType result))
            {
                return result;
            }
            throw new ArgumentException($"None of the enums in PriorityTypes match the value {value}.");
        }

        protected Severity ParseSeverity(string value)
        {
            if (Enum.TryParse(value, true, out Severity result))
            {
                return result;
            }
            throw new ArgumentException($"None of the enums in Severity match the value {value}.");
        }

        protected SizeType ParseSize(string value)
        {
            if (Enum.TryParse(value, true, out SizeType result))
            {
                return result;
            }
            throw new ArgumentException($"None of the enums in SizeType match the value {value}.");
        }

        protected Models.Enums.BugStatus.Status ParseBugStatus(string value)
        {
            if (Enum.TryParse(value, true, out Models.Enums.BugStatus.Status result))
            {
                return result;
            }
            throw new ArgumentException($"None of the enums in BugStatus match the value {value}.");
        }

        protected Models.Enums.FeedbackStatus.Status ParseFeedbackStatus(string value)
        {
            if (Enum.TryParse(value, true, out Models.Enums.FeedbackStatus.Status result))
            {
                return result;
            }
            throw new ArgumentException($"None of the enums in FeedbackStatus match the value {value}.");
        }

        protected Models.Enums.StoryStatus.Status ParseStoryStatus(string value)
        {
            if (Enum.TryParse(value, true, out Models.Enums.StoryStatus.Status result))
            {
                return result;
            }
            throw new ArgumentException($"None of the enums in StoryStatus match the value {value}.");
        }
    }
}
