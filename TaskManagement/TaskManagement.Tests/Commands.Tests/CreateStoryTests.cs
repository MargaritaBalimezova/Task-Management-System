using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class CreateStoryTests
    {
        private const int ExpectedParamsCount = 4;

        private IRepository repository;
        private ICommandFactory commandFactory;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);
        }

        [TestMethod]
        public void Execute_Should_CreateStory_When_CommandParamsValid()
        {
            //Arrange
            var commandParameters = new string[] { Constants.Title, Constants.Description, Constants.priority.ToString(), Constants.size.ToString() };
            var command = new CreateStoryCommand(commandParameters, this.repository);

            //Act
            command.Execute();

            //Assert
            Assert.IsTrue(this.repository.Stories.Count > 0,
                "CreateStory command failed to create a command when valid params passed!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException),
            "CreateStory parameters count validation failed!")]
        public void Execute_Should_ThrowException_When_ArgumentCountDiffThanExpected()
        {
            //Arrange
            var commandParameters = Helpers.GetDummyList(ExpectedParamsCount - 1);
            var command = new CreateStoryCommand(commandParameters, this.repository);

            //Act & Asssert
            command.Execute();
        }
    }
}