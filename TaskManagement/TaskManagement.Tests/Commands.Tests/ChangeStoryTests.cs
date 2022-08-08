using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;
using TaskManagement.Models.Enums.StoryStatus;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class ChangeStoryTests
    {
        private const int ExpectedParamsCount = 3;

        private IRepository repository;
        private ICommandFactory commandFactory;
        private IStory story;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);

            this.story = this.repository.CreateStory(Constants.Title, Constants.Description, Constants.priority, Constants.size);
        }

        [TestMethod]
        public void Execute_Should_ChangePriority_When_DiffPriorityIsPassed()
        {
            //Arrange
            var expectedPriority = PriorityType.Low;
            var commandParameters = new string[] { this.story.Id.ToString()
                                                  ,"Priority"
                                                  , expectedPriority.ToString()};
            var command = new ChangeStoryCommand(commandParameters, repository);
            //Act
            command.Execute();
            //Assert
            Assert.AreEqual(expectedPriority, this.story.Priority,
               "ChangeStory command failed to change Priority!");
        }

        [TestMethod]
        public void Execute_Should_ChangeSize_When_DiffSizeIsPassed()
        {
            //Arrange
            var expectedSize = SizeType.Small;
            var commandParameters = new string[] { this.story.Id.ToString()
                                                  ,"Size"
                                                  , expectedSize.ToString()};
            var command = new ChangeStoryCommand(commandParameters, repository);
            //Act
            command.Execute();
            //Assert
            Assert.AreEqual(expectedSize, this.story.Size,
                "ChangeStory command failed to change Size!");
        }

        [TestMethod]
        public void Execute_Should_ChangeStatus_When_DiffStatusIsPassed()
        {
            //Arrange
            var expectedStatus = Status.Done;
            var commandParameters = new string[] { this.story.Id.ToString()
                                                  ,"Status"
                                                  , expectedStatus.ToString()};
            var command = new ChangeStoryCommand(commandParameters, repository);
            //Act
            command.Execute();
            //Assert
            Assert.AreEqual(expectedStatus, this.story.Status,
                "ChangeStory command failed to change Status!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException),
            "Validation for field to change failed!")]
        public void Execute_Should_ThrowException_When_ChangeParamIsNotListed()
        {
            //Arrange
            var expectedStatus = Status.Done;
            var commandParameters = new string[] { this.story.Id.ToString()
                                                  ,"FakeField"
                                                  , expectedStatus.ToString()};
            var command = new ChangeStoryCommand(commandParameters, repository);
            //Act & Assert
            command.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException),
       "Validation for field to change failed!")]
        public void Execute_Should_ThrowException_When_ArgumentCountDiffThanExpected()
        {
            //Arrange
            var commandParameters = Helpers.GetDummyList(ExpectedParamsCount - 1);
            var command = new ChangeStoryCommand(commandParameters, repository);
            //Act & Assert
            command.Execute();
        }
    }
}
