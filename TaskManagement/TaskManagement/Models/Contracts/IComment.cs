namespace TaskManagement.Models.Contracts
{
    public interface IComment
    {
        public string Author { get; }

        public string Content { get; }
    }
}
