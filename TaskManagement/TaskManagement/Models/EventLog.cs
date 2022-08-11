using System;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Models
{
    public class EventLog : IEventLog
    {
        private string description;

        public EventLog(string description)
        {
            this.Description = description ?? throw new ArgumentNullException(nameof(description));
            this.Time = DateTime.Now;
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        public DateTime Time { get; }

        public string ViewInfo()
        {
            return $"[{this.Time.ToString("yyyy:MM:dd|HH:mm:ss")}]{this.Description}";
        }
    }
}