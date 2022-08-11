using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManagement.Commands;
using TaskManagement.Core;
using TaskManagement.Core.Contracts;
using TaskManagement.Exceptions;
using TaskManagement.Models.Contracts;
using TaskManagement.Tests.Commands.Tests.Common;

namespace TaskManagement.Tests.Commands.Tests
{
    [TestClass]
    public class RemoveMemberFromTeamTests
    {
        private const int ExpectedParamsCount = 2;

        private IMember member;
        private ITeam team;
        private IRepository repository;
        private ICommandFactory commandFactory;

        [TestInitialize]
        public void InitTest()
        {
            this.repository = new Repository();
            this.commandFactory = new CommandFactory(this.repository);

            this.team = this.repository.CreateTeam(Constants.TeamName);
            this.member = this.repository.CreateMember(Constants.MemberName);
            this.team.AddMember(member);
        }

        [TestMethod]
        public void Execute_Should_RemoveMemberFromTeam_When_ParamsValid()
        {
            //Arrange
            var commandParameters = new string[] { Constants.TeamName, Constants.MemberName };
            var command = new RemoveMemberFromTeam(commandParameters, this.repository);
            //Act
            command.Execute();
            //Assert
            Assert.IsFalse(this.repository.IsMemberInTeam(this.team, this.member),
                "RemoveMemberFromTeam command failed to remove the the member!");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserInputException),
            "AddMemberToTeam parameters count validation failed!")]
        public void Execute_Should_ThrowException_When_ArgumentCountDiffThanExpected()
        {
            //Arrange
            var commandParameters = Helpers.GetDummyList(ExpectedParamsCount - 1);
            var command = new RemoveMemberFromTeam(commandParameters, this.repository);

            //Act & Asssert
            command.Execute();
        }
    }
}