using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Enums;

namespace TaskManagement.Tests.Commands.Tests
{

    [TestClass]
    public class CreateStoryTests
    {
        private const int ExpectedParamsCount = 4;

        private IRepository repository;
        private ICommandFactory commandFactory;
        private string title;
        private string description;
        private PriorityType priority;
        private SizeType size;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);
            this.title = "StoryDummy";
            this.description = "DescriptionDummy";
            this.priority = PriorityType.High;
            this.size = SizeType.Medium;
        }

        [TestMethod]
        public void Execute_Should_CreateStory_When_CommandParamsValid()
        {
            //Arrange
            var commandParameters = new string[] { title, description, priority.ToString(), size.ToString() };
            var command = new CreateStoryCommand(commandParameters,repository);
            
            //Act
            command.Execute();

            //Assert
            Assert.IsTrue(this.repository.Stories.Count > 0,
                "CreateStory command failed to create a command when valid params passed!");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "CreateStory parameters count validation failed!")]
        public void Execute_Should_ThrowException_When_ArgumentCountDiffThanExpected()
        {
            //Arrange
            var commandParameters = Helpers.GetDummyList(ExpectedParamsCount - 1);
            var command = new CreateStoryCommand(commandParameters, repository);

            //Act & Asssert
            command.Execute();
        }
    }
}
