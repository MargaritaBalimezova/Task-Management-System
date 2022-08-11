using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManagement.Exceptions;
using TaskManagement.Models;

namespace TaskManagement.Tests.Models.Tests
{
    [TestClass]
    public class CommentTests
    {
        [TestMethod]
        public void ConstructorShould_CreateNewComment_When_CorrectValuesArePassed()
        {
            string author = "Test Author";
            string content = "This is a test description";
            var comment = new Comment(content, author);

            Assert.AreEqual("Test Author", comment.Author, "Comment created successfully!");
            Assert.AreEqual("This is a test description", comment.Content, "Comment created successfully!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException), "Input value can not be null!")]
        public void ConstructorShould_Throw_When_NullValueIsPassedToTheAuthor()
        {
            string author = null;
            string content = "This is a test description";
            var comment = new Comment(content, author);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException), "Input value can not be null!")]
        public void ConstructorShould_Throw_When_NullValueIsPassedToTheContent()
        {
            string author = "Test Author";
            string content = null;
            var comment = new Comment(content, author);
        }
    }
}