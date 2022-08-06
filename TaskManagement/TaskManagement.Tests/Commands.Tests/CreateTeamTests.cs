using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class CreateTeamTests
    {
        private const string teamName = "DummyTeam";
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
        public void Execute_Should_CreateTeam_When_ParamsValid()
        {
            //Arrange
            var commandParameters = new string[] { teamName };
            var command = new CreateTeamCommand(commandParameters, repository);

            //Act
            command.Execute();

            //Assert
            Assert.AreEqual(teamName, this.repository.Teams[this.repository.Teams.Count - 1].Name,
                $"CreateCommand failed to create a team with name {teamName}");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException),
            "CreateTeam parameters count validation failed!")]
        public void Execute_Should_ThrowException_When_ArgumentCountDiffThanExpected()
        {
            //Arrange
            var commandParameters = Helpers.GetDummyList(ExpectedParamsCount + 1);
            var command = new CreateTeamCommand(commandParameters, repository);

            //Act & Asssert
            command.Execute();
        }
    }
}
