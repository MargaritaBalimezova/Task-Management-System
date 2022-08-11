using System;

namespace TaskManagement.Models.Contracts
{
    public interface IEventLog
    {
        public string Description { get; }

        public DateTime Time { get; }

        public string ViewInfo();
    }
}
