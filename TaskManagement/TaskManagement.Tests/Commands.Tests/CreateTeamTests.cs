using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class CreateTeamTests
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
        public void Execute_Should_CreateTeam_When_ParamsValid()
        {
            //Arrange
            var commandParameters = new string[] { Constants.TeamName };
            var command = new CreateTeamCommand(commandParameters, repository);

            //Act
            command.Execute();

            //Assert
            Assert.AreEqual(Constants.TeamName, this.repository.Teams[this.repository.Teams.Count - 1].Name,
                $"CreateCommand failed to create a team with name {Constants.TeamName}");
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
