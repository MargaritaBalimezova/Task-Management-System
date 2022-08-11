using System.Collections.Generic;

namespace TaskManagement.Models.Contracts
{
    public interface ICommentable
    {
        public IList<IComment> Comments{ get; }

        public void AddComment(IComment comment);

        public void RemoveComment(IComment comment);
    }
}
