using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Models.Contracts
{
    public interface IComment
    {
        public string Author { get; }

        public string Content { get; }
    }
}
