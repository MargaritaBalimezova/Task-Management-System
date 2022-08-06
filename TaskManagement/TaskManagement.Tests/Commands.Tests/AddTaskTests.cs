using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class AddTaskTests
    {
        private const int ExpectedParamsCount = 3;

        private IRepository repository;
        private ICommandFactory commandFactory;

        private ITeam team;
        private IBoard board;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);

            this.team = this.repository.CreateTeam(Constants.TeamName);
            this.board = this.repository.CreateBoard(Constants.BoardName);
            this.team.AddBoard(board);
        }

        [TestMethod]
        public void Execute_Should_AddTaskToTeamBoard()
        {
            //Arrange
            var story = this.repository.CreateStory(Constants.Title, Constants.Description,
                Constants.priority, Constants.size);
            var commandParams = new string[] { story.Id.ToString(), this.team.Name, this.board.Name };
            var command = new AddTaskCommand(commandParams, this.repository);
            //Act
            command.Execute();
            //Assert
            Assert.ReferenceEquals(story, this.repository.Teams[this.repository.Teams.Count - 1].Boards[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException),
            "AddTask commnad id validation failed!")]
        public void Execute_Should_ThrowException_When_TaskIdNotValid()
        {
            //Arrange
            var commandParams = new string[] { "fakeid", this.team.Name, this.board.Name };
            var command = new AddTaskCommand(commandParams, this.repository);
            //Act
            command.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException),
            "AddTask commnad argument validation failed!")]
        public void Execute_Should_ThrowException_When_DiffParamsCountPassed()
        {
            //Arrange
            var commandParams = Helpers.GetDummyList(ExpectedParamsCount + 1);
            var command = new AddTaskCommand(commandParams, this.repository);
            //Act
            command.Execute();
        }
    }
}
