using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Models.Contracts
{
    public interface ICommentable
    {
        public IList<IComment> Comments{ get; }

        public void AddComment(IComment comment);

        public void RemoveComment(IComment comment);
    }
}
