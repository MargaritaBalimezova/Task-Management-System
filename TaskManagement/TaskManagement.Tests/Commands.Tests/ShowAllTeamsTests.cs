using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class ShowAllTeamsTests
    {
        private const string teamName = "DummyTeam";

        private IRepository repository;
        private ICommandFactory commandFactory;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);
        }

        [TestMethod]
        public void Execute_Should_ShowAllTeams()
        {
            //Arrange
            var command = new ShowAllTeamsCommand(this.repository);
            this.repository.CreateTeam("teamName");
            //Act & Assert
            Assert.IsTrue(command.Execute().Contains($"Number of all teams: {this.repository.Teams.Count}")
                ,"Show all teams failed to return the right amount of teams!");
        }

        [TestMethod]
        public void Execute_Should_ReturnNoTeamsMessage_When_TeamsListEmpty()
        {
            //Arrange
            var command = new ShowAllTeamsCommand(this.repository);

            //Act & Assert
            Assert.IsTrue(command.Execute().Contains("There are no teams."));
        }
    }
}
