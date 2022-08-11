using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Models.Contracts;
using TaskManagement.Commands;
using TaskManagement.Exceptions;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class ShowAllTeamMembersTests
    {
        private const int ExpectedParamsCount = 1;

        private IRepository repository;
        private ICommandFactory commandFactory;
        private ITeam team;
        private IMember member;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);

            team = this.repository.CreateTeam(Constants.TeamName);
            member = this.repository.CreateMember(Constants.MemberName);
        }

        [TestMethod]
        public void Execute_Should_ReturnTheRightAmountOfMembersInTeam()
        {
            //Arrange
            var commandParameters = new string[] { Constants.TeamName, Constants.MemberName };
            var command = new ShowTeamMembersCommand(new string[] { Constants.TeamName }, repository);
            var addMemberCommand = new AddMemberToTeam(commandParameters, repository);

            //Act
            addMemberCommand.Execute();

            //Assert
            Assert.IsTrue(command.Execute().Contains($"Member Name: {this.member.Name}"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException),
            "ShowAllTeamMembers argument validation failed!")]
        public void Execute_Should_ThrowException_When_DiffParamsCountPassed()
        {
            //Arrange
            var commandParameters = Helpers.GetDummyList(ExpectedParamsCount + 1);
            var command = new ShowTeamMembersCommand(commandParameters, repository);

            //Act & Assert
            command.Execute();
        }
    }
}
