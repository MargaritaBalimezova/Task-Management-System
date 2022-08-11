using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Enums.StoryStatus;
using TaskManagement.Models.Tasks;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class SortStoryByTests
    {
        private const int ExpectedParamsCount = 1;

        private IRepository repository;
        private ICommandFactory commandFactory;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);
        }

        [TestMethod]
        public void Execute_Should_ReturnSortedStoriesByTitle()
        {
            //Arrange
            var story1 = this.repository.CreateStory(Constants.Title, Constants.Description, Constants.priorityHigh, Constants.sizeLarge);
            var story2 = this.repository.CreateStory(Constants.Title2, Constants.Description, Constants.priorityMedium, Constants.sizeMedium);

            var commandParameters = new string[] { "title" };
            var command = new SortStoryBy(commandParameters, this.repository);

            var sb = new StringBuilder();
            sb.Append(story1.ToString());
            sb.AppendLine("####################");
            sb.Append(story2.ToString());
            sb.AppendLine("####################");
            //Act && Assert
            Assert.IsTrue(command.Execute().Contains(sb.ToString()));
        }

        [TestMethod]
        public void Execute_Should_ReturnSortedStoriesBySize()
        {
            //Arrange
            var story1 = this.repository.CreateStory(Constants.Title, Constants.Description, Constants.priorityHigh, Constants.sizeLarge);
            var story2 = this.repository.CreateStory(Constants.Title2, Constants.Description, Constants.priorityMedium, Constants.sizeMedium);

            var commandParameters = new string[] { "size" };
            var command = new SortStoryBy(commandParameters, this.repository);

            var sb = new StringBuilder();
            sb.Append(story2.ToString());
            sb.AppendLine("####################");
            sb.Append(story1.ToString());
            sb.AppendLine("####################");
            
            //Act && Assert
            Assert.IsTrue(command.Execute().Contains(sb.ToString()));
        }

        [TestMethod]
        public void Execute_Should_ReturnSortedStoriesByPriority()
        {
            //Arrange
            var story1 = this.repository.CreateStory(Constants.Title, Constants.Description, Constants.priorityHigh, Constants.sizeLarge);
            var story2 = this.repository.CreateStory(Constants.Title2, Constants.Description, Constants.priorityMedium, Constants.sizeMedium);

            var commandParameters = new string[] { "priority" };
            var command = new SortStoryBy(commandParameters, this.repository);

            var sb = new StringBuilder();
            sb.Append(story1.ToString());
            sb.AppendLine("####################");
            sb.Append(story2.ToString());
            sb.AppendLine("####################");
            //Act && Assert
            Assert.IsTrue(command.Execute().Contains(sb.ToString()));
        }

        [TestMethod]
        public void Execute_Should_ReturnSortedStoriesByStutus()
        {
            //Arrange
            var story1 = (Story)this.repository.CreateStory(Constants.Title, Constants.Description, Constants.priorityHigh, Constants.sizeLarge);
            var story2 = this.repository.CreateStory(Constants.Title2, Constants.Description, Constants.priorityMedium, Constants.sizeMedium);
            story1.Status = Status.Done;

            var commandParameters = new string[] { "status" };
            var command = new SortStoryBy(commandParameters, this.repository);

            var sb = new StringBuilder();
            sb.Append(story1.ToString());
            sb.AppendLine("####################");
            sb.Append(story2.ToString());
            sb.AppendLine("####################");
            //Act && Assert
            Assert.IsTrue(command.Execute().Contains(sb.ToString()));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException),
            "SortStoryBy argument validation failed!")]
        public void Execute_Should_ThrowException_When_ParamsInvalid()
        {
            //Arrange
            var commandParameters = new string[] { "fakeParam" };
            var command = new SortStoryBy(commandParameters, this.repository);

            //Act && Assert
            command.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException),
            "SortStoryBy argument validation failed!")]
        public void Execute_Should_ThrowException_When_DiffArgumentCountPassed()
        {
            //Arrange
            var commandParameters = Helpers.GetDummyList(ExpectedParamsCount + 1);
            var command = new SortStoryBy(commandParameters, this.repository);

            //Act && Assert
            command.Execute();
        }
    }
}
