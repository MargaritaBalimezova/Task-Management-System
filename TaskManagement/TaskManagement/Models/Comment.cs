using System.Text;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Models
{

    public class Comment : IComment
    {
        private string content;
        private string author;

        public Comment(string content, string author)
        {
            this.Content = content;
            this.Author = author;
        }

        #region Properties
        public string Content
        {
            get { return this.content; }
            set
            {
                Validator.ValidateArgumentIsNotNull(value, "Content must not be NULL!");
                this.content = value;
            }
        }

        public string Author
        {
            get { return this.author; }
            set
            {
                Validator.ValidateArgumentIsNotNull(value, "Author must not be NULL!");
                this.author = value;
            }
        }
        #endregion

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("    ----------");
            sb.AppendLine($"    {this.content}");
            sb.AppendLine($"      User: {this.author}");
            sb.AppendLine("    ----------");

            return sb.ToString();
        }
    }
}
