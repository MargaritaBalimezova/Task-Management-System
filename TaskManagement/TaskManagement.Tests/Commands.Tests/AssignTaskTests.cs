using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
    public class AssignTaskTests
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
        [DataRow(3)]
        public void Execute_Should_ThrowException_When_ArgumentsCountDifferentThanExpected(int testValue)
        {
            // Arrange
            var commandParameters = Helpers.GetDummyList(testValue - 1);
            var command = new AssignTaskCommand(commandParameters, this.repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_AssignTask_When_ValidParameters_Ver1()
        {
            // Arrange
            var member = this.repository.CreateMember(Constants.MemberName);
            var stepsToReproduce = new List<string>();
            var task = this.repository.CreateBug(Constants.Title, Constants.Description, PriorityType.Medium, Severity.Minor, stepsToReproduce);
            var team = this.repository.CreateTeam(Constants.TeamName);
            var board = this.repository.CreateBoard(Constants.BoardName);
            team.AddBoard(board);
            team.AddMember(member);
            board.AddTaskToBoard(task);

            var commandParameters = new string[] { "1", Constants.MemberName, Constants.TeamName }.ToList();

            var command = new AssignTaskCommand(commandParameters, this.repository);

            // Act & Assert
            Assert.AreEqual(command.Execute(), $"Task with id 1 was assigned to {Constants.MemberName}");
        }

        [TestMethod]
        public void Execute_Should_AssignTask_When_ValidParameters_Ver2()
        {
            // Arrange
            var member = this.repository.CreateMember(Constants.MemberName);
            var task = this.repository.CreateStory(Constants.Title, Constants.Description, PriorityType.Medium, SizeType.Large);
            var team = this.repository.CreateTeam(Constants.TeamName);
            var board = this.repository.CreateBoard(Constants.BoardName);
            team.AddBoard(board);
            team.AddMember(member);
            board.AddTaskToBoard(task);

            var commandParameters = new string[] { "1", Constants.MemberName, Constants.TeamName }.ToList();

            var command = new AssignTaskCommand(commandParameters, this.repository);

            // Act & Assert
            Assert.AreEqual(command.Execute(), $"Task with id 1 was assigned to {Constants.MemberName}");
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_TaskIsFeedback()
        {
            // Arrange
            var member = this.repository.CreateMember(Constants.MemberName);
            var task = this.repository.CreateFeedBack(Constants.Title, Constants.Description, 59);
            var team = this.repository.CreateTeam(Constants.TeamName);
            var board = this.repository.CreateBoard(Constants.BoardName);
            team.AddBoard(board);
            team.AddMember(member);
            board.AddTaskToBoard(task);

            var commandParameters = new string[] { "1", Constants.MemberName, Constants.TeamName }.ToList();

            var command = new AssignTaskCommand(commandParameters, this.repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_MemberNotInTeam()
        {
            // Arrange
            var member = this.repository.CreateMember(Constants.MemberName);
            var task = this.repository.CreateStory(Constants.Title, Constants.Description, PriorityType.Medium, SizeType.Large);
            var team = this.repository.CreateTeam(Constants.TeamName);
            var board = this.repository.CreateBoard(Constants.BoardName);
            team.AddBoard(board);
            board.AddTaskToBoard(task);

            var commandParameters = new string[] { "1", Constants.MemberName, Constants.TeamName }.ToList();

            var command = new AssignTaskCommand(commandParameters, this.repository);

            // Act, Assert
            Assert.ThrowsException<EntityNotFoundException>(() =>
                command.Execute());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Execute_Should_Throw_When_SameStoryIsAssignedTwice()
        {
            var member = this.repository.CreateMember(Constants.MemberName);
            var member2 = this.repository.CreateMember(Constants.MemberName2);
            var task = this.repository.CreateStory(Constants.Title, Constants.Description, PriorityType.Medium, SizeType.Large);
            var team = this.repository.CreateTeam(Constants.TeamName);
            team.AddMember(member);
            team.AddMember(member2);

            var commandParameters = new string[] { "1", Constants.MemberName, Constants.TeamName }.ToList();
            var commandParameters2 = new string[] { "1", Constants.MemberName2, Constants.TeamName }.ToList();

            var command = new AssignTaskCommand(commandParameters, this.repository);
            var command2 = new AssignTaskCommand(commandParameters2, this.repository);

            command.Execute();
            command2.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Execute_Should_Throw_When_SameBugIsAssignedTwice()
        {
            var member = this.repository.CreateMember(Constants.MemberName);
            var member2 = this.repository.CreateMember(Constants.MemberName2);
            var task = this.repository.CreateBug(Constants.BugTitle, Constants.Description, PriorityType.Medium, Severity.Critical, Constants.steps);
            var team = this.repository.CreateTeam(Constants.TeamName);
            team.AddMember(member);
            team.AddMember(member2);

            var commandParameters = new string[] { "1", Constants.MemberName, Constants.TeamName }.ToList();
            var commandParameters2 = new string[] { "1", Constants.MemberName2, Constants.TeamName }.ToList();

            var command = new AssignTaskCommand(commandParameters, this.repository);
            var command2 = new AssignTaskCommand(commandParameters2, this.repository);

            command.Execute();
            command2.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Execute_Should_Throw_When_TaskIsAssignedWithoutHavingTeamBoard()
        {
            var member = this.repository.CreateMember(Constants.MemberName);
            var task = this.repository.CreateBug(Constants.BugTitle, Constants.Description, PriorityType.Medium, Severity.Critical, Constants.steps);
            var team = this.repository.CreateTeam(Constants.TeamName);
            team.AddMember(member);

            var commandParameters = new string[] { "1", Constants.MemberName, Constants.TeamName }.ToList();

            var command = new AssignTaskCommand(commandParameters, this.repository);

            command.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Execute_Should_Throw_When_TaskWhichIsNotOnAnyOfTeamBoardsIsAssignedToTeamMember()
        {
            var member = this.repository.CreateMember(Constants.MemberName);
            var task = this.repository.CreateBug(Constants.BugTitle, Constants.Description, PriorityType.Medium, Severity.Critical, Constants.steps);
            var board = this.repository.CreateBoard(Constants.BoardName);
            var team = this.repository.CreateTeam(Constants.TeamName);
            team.AddMember(member);
            team.AddBoard(board);

            var commandParameters = new string[] { "1", Constants.MemberName, Constants.TeamName }.ToList();
            var command = new AssignTaskCommand(commandParameters, this.repository);

            command.Execute();
        }
    }
}