using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Models.Contracts
{
    public interface ITask : IHasID, ICommentable, IActivityLog
    {
        //TODO
        //status???

        public string Title { get; }

        public string Description { get; }
    }
}