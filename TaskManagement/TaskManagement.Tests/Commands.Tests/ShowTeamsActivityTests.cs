using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class ShowTeamsActivityTests
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
        public void Execute_Should_ReturnAllTeamsActivityLog()
        {
            //Arrange
            var team = this.repository.CreateTeam(Constants.TeamName);
            team.AddMember(new Member(Constants.MemberName));

            var command = new ShowTeamsActivity(new string[] { Constants.TeamName }, this.repository);
            //Act & Assert
            Assert.IsTrue(command.Execute().Contains(team.ShowActivity()));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException),
            "ShowTeamsActivity argument validation failed!")]
        public void Execute_Should_ThrowException_When_DiffArgumentCountPassed()
        {
            var command = new ShowTeamsActivity(Helpers.GetDummyList(ExpectedParamsCount - 1), this.repository);
            //Act & Assert
            command.Execute();
        }
    }
}
