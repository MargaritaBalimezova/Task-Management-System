using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Models.Contracts
{
    public interface IEventLog
    {
        public string Description { get; }

        public DateTime Time { get; }

        public string ViewInfo();
    }
}
