using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Models;
using TaskManagement.Models.Contracts;
using TaskManagement.Models.Enums;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class UnassignTaskTests
    {
        private IRepository repository;
        private ICommandFactory commandFactory;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);
        }

        [TestMethod]
        [DataRow(2)]
        public void Execute_Should_ThrowException_When_ArgumentsCountDifferentThanExpected(int testValue)
        {
            // Arrange
            var commandParameters = Helpers.GetDummyList(testValue - 1);
            var command = new UnassignTaskCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_AssignTask_When_TaskFound()
        {
            // Arrange
            var member = this.repository.CreateMember("testMember");
            ITask task = this.repository.CreateStory("StoryTitle", "StoryDescription", PriorityType.Medium, SizeType.Large);

            member.AddTask(task);

            var commandParameters = new string[] { "1", "testMember" }.ToList();

            var command = new UnassignTaskCommand(commandParameters, repository);

            // Act & Assert
            Assert.AreEqual(command.Execute(), "Task with id 1 was unassigned from testMember");
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_TaskNotFound()
        {
            // Arrange
            var member = this.repository.CreateMember("testMember");
            ITask task = this.repository.CreateStory("StoryTitle", "StoryDescription", PriorityType.Medium, SizeType.Large);

            var commandParameters = new string[] { "1", "testMember" }.ToList();

            var command = new UnassignTaskCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<ArgumentException>(() =>
                command.Execute());
        }
    }
}