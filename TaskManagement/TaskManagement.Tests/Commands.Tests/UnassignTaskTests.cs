using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Enums;
using TaskManagement.Tests.Commands.Tests.Common;

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
            var command = new UnassignTaskCommand(commandParameters, this.repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_UnassignTask_When_TaskFound_Ver1()
        {
            // Arrange
            var member = this.repository.CreateMember(Constants.MemberName);
            var task = this.repository.CreateStory(Constants.Title, Constants.Description, PriorityType.Medium, SizeType.Large);

            member.AddTask(task);
            task.AddAssignee(member);

            var commandParameters = new string[] { "1", Constants.MemberName }.ToList();

            var command = new UnassignTaskCommand(commandParameters, this.repository);

            // Act & Assert
            Assert.AreEqual(command.Execute(), $"Task with id 1 was unassigned from {Constants.MemberName}");
        }

        [TestMethod]
        public void Execute_Should_UnassignTask_When_TaskFound_Ver2()
        {
            // Arrange
            var member = this.repository.CreateMember(Constants.MemberName);
            var stepsToReproduce = new List<string>();
            var task = this.repository.CreateBug(Constants.Title, Constants.Description, PriorityType.Medium, Severity.Minor, stepsToReproduce);

            member.AddTask(task);
            task.AddAssignee(member);

            var commandParameters = new string[] { "1", Constants.MemberName }.ToList();

            var command = new UnassignTaskCommand(commandParameters, this.repository);

            // Act & Assert
            Assert.AreEqual(command.Execute(), $"Task with id 1 was unassigned from {Constants.MemberName}");
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_TaskNotFound()
        {
            // Arrange
            var member = this.repository.CreateMember(Constants.MemberName);
            var task = this.repository.CreateStory(Constants.Title, Constants.Description, PriorityType.Medium, SizeType.Large);

            var commandParameters = new string[] { "1", Constants.MemberName }.ToList();

            var command = new UnassignTaskCommand(commandParameters, this.repository);

            // Act, Assert
            Assert.ThrowsException<EntityNotFoundException>(() =>
                command.Execute());
        }
    }
}